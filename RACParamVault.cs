using MissionPlanner.Controls;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MissionPlanner.RACParamVault
{
    public class RACParamVaultPlugin : MissionPlanner.Plugin.Plugin
    {
        public string vehicle_name;                //Marked with ! in the vault file
        public string vehicle_configuration;       //Marked with @ in the vault file
        public string operator_name;               //Marked with ~ in the vault file

        public Dictionary<string, double> _actual_parameters = new Dictionary<string, double>();       //This will contains the actual parameter list from the vehicle
        public Dictionary<string, double> _vault_parameters = new Dictionary<string, double>();       //This will contains the actual parameter list from the vehicle
       
        
        public IEnumerable<KeyValuePair<string, double>> _differences = new Dictionary<string, double>();
        
        
        private ToolStripMenuItem but;

        private Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// The vault contect is loaded for the actual vehicle
        /// </summary>
        private bool _vault_loaded = false;

        /// <summary>
        /// Vault is ignored, do not askt till next connect
        /// </summary>
        public bool _vault_ignored = false;

        public override string Name
        {
            get { return "RACParamVault"; }
        }

        public override string Version
        {
            get { return "0.1"; }
        }

        public override string Author
        {
            get { return "Andras Schaffer"; }
        }

        //[DebuggerHidden]
        public override bool Init()
        {
            loopratehz = 0.2f;  //Every 5 seconds

            return true;
        }

        public override bool Loaded()
        {
            but = new ToolStripMenuItem("Vault Test");
            but.Click += but_Click;
            ToolStripItemCollection col = Host.FDMenuMap.Items;
            col.Add(but);

            vehicle_name = "GigaRotor 6 v1";
            vehicle_configuration = Host.config["MPConfigDesc", "Default"];
            if (vehicle_configuration?.Length == 0) vehicle_configuration = "Default";

            return true;
        }

        public override bool Loop()
        {
            //if we are not connected then we skip
            if (!MainV2.comPort.MAV.cs.connected)
            {
                _vault_loaded = false; //
                _vault_ignored = false;
                return true;
            }
            //If we connected and armed we also skip
            if (MainV2.comPort.MAV.cs.armed) { Console.WriteLine("MAV is armed skipped vault checking"); return true; }
            //if Not all parameters are loaded then we skip as well
            else if ((MainV2.comPort.MAV.param.TotalReceived < MainV2.comPort.MAV.param.TotalReported) || MainV2.comPort.MAV.param.TotalReported == 0) { Console.WriteLine("Parameters are not fully loaded, skip vault checking"); return true; }
            //ignore vehicles with BRD_SERIAL_NUM == 0;
            else if (MainV2.comPort.MAV.param["BRD_SERIAL_NUM"].Value == 0) { Console.WriteLine("BRD_SERIAL_NUM is zero, skip vault check"); return true; }
            //If _vault ignored is set skip
            else if (_vault_ignored) { Console.WriteLine("Vault ignored"); return true; }

            //Ok Here we go.
            //Check if vault is loaded, if not try to load, if vault is not find then ask operator to create a new vault data for this vehicle
            if (!_vault_loaded)
            {
                string filename = Settings.GetUserDataDirectory() + Path.GetFileNameWithoutExtension(Settings.FileName) + "_" + MainV2.comPort.MAV.param["BRD_SERIAL_NUM"].Value.ToString() + ".paramvault";
                if (!File.Exists(filename))
                {
                    Console.WriteLine("No vault file exists");
                    if (CustomMessageBox.Show("No Vault file find for config:" + Settings.FileName + "/BRD_SERIAL:" + MainV2.comPort.MAV.param["BRD_SERIAL_NUM"].Value.ToString() + "\r\nDo you want to register current config in the vault?",
                        "No Vault file", CustomMessageBox.MessageBoxButtons.YesNo, CustomMessageBox.MessageBoxIcon.Question) == CustomMessageBox.DialogResult.No)
                    {
                        _vault_ignored = true;
                        return true;
                    }
                    InputBox.Show("Config Vault", "Please enter the name for vehicle", ref vehicle_name);
                    InputBox.Show("Config Vault", "Please enter a description for this configuration", ref vehicle_configuration);
                    InputBox.Show("Config Vault", "Please enter the name of the operator", ref operator_name);
                    create_vault_file();
                    loadParamFile();        //TODO: add error handling
                    _vault_loaded = true;
                }
                else
                {
                    Console.WriteLine("Load vault file");
                    loadParamFile();        //TODO: add error handling
                    _vault_loaded = true;
                }
            }
            else
            {
                //We have a loaded_vault file now.
                update_actual_parameter_list(); //Update current parameters

                _differences = _actual_parameters.Except(_vault_parameters);

                if (_differences.Count() != 0)              // There are differences in the actual config and the Vault
                {
                    using (Form ParamDiff = new ParamDiff(this))
                    {
                        MissionPlanner.Utilities.ThemeManager.ApplyThemeTo(ParamDiff);
                        
                        ParamDiff.ShowDialog();
                    }
                    //CustomMessageBox.Show("Config changed !");
                }
            }

            return true;
        }

        public override bool Exit()
        {
            return true;
        }

        private bool is_param_readonly(string parameter_name)
        {
            bool readonly2 = false;
            var readonly1 = ParameterMetaDataRepository.GetParameterMetaData(parameter_name, ParameterMetaDataConstants.ReadOnly, MainV2.comPort.MAV.cs.firmware.ToString());
            if (readonly1 == String.Empty) return false;
            var _ = bool.TryParse(readonly1, out readonly2);
            return readonly2;
        }

        private bool is_param_ignored(string parameter_name)
        {
            if (parameter_name == "SYSID_SW_MREV")
                return true;
            if (parameter_name == "WP_TOTAL")
                return true;
            if (parameter_name == "CMD_TOTAL")
                return true;
            if (parameter_name == "FENCE_TOTAL")
                return true;
            if (parameter_name == "SYS_NUM_RESETS")
                return true;
            if (parameter_name == "ARSPD_OFFSET")
                return true;
            if (parameter_name == "GND_ABS_PRESS")
                return true;
            if (parameter_name == "GND_TEMP")
                return true;
            if (parameter_name == "CMD_INDEX")
                return true;
            if (parameter_name == "LOG_LASTFILE")
                return true;
            if (parameter_name == "FORMAT_VERSION")
                return true;
            if (parameter_name == "MOT_THST_HOVER")
                return true;
            if (parameter_name == "COMPASS_DEC")
                return true;
            if (parameter_name.Length > 4)
            {
                if (parameter_name.Substring(0, 4) == "SIM_")
                    return true;
            }
            if (parameter_name.Length > 11)
            {
                if (parameter_name.Substring(0, 11) == "INS_GYROFFS")
                    return true;
                if (parameter_name.Substring(0, 11) == "INS_GYR2OFF")
                    return true;
                if (parameter_name.Substring(0, 11) == "INS_GYR3OFF")
                    return true;
            }
            return false;
        }

        private void update_actual_parameter_list()
        {
            //Check if all params are loaded
            if (MainV2.comPort.MAV.param.TotalReceived < MainV2.comPort.MAV.param.TotalReported) return;

            //Clear _actuam_params
            _actual_parameters.Clear();

            foreach (string item in MainV2.comPort.MAV.param.Keys)
            {
                //Exclude these readonly fields
                if (is_param_ignored(item))
                    continue;
                if (is_param_readonly(item))
                    continue;

                var value = Math.Round(MainV2.comPort.MAV.param[item].Value, 5); //Rounding for 6 decimal digits to overcome stupid double conversion errors.
                _actual_parameters.Add(item, value);
            }
        }

        private void but_Click(object sender, EventArgs e)
        {
            update_actual_parameter_list();
        }

        private void create_vault_file()
        {
            //This will be the filename
            string filename = Settings.GetUserDataDirectory() + Path.GetFileNameWithoutExtension(Settings.FileName) + "_" + MainV2.comPort.MAV.param["BRD_SERIAL_NUM"].Value.ToString() + ".paramvault";

            using (StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create)))
            {
                var list = new List<string>();
                foreach (string item in MainV2.comPort.MAV.param.Keys)
                {
                    //Exclude these readonly fields
                    if (is_param_ignored(item))
                        continue;
                    list.Add(item);
                }

                //Write out remark block
                //Write out !config-definition
                //Write out !remark

                sw.WriteLine("#" + " This is a configuration vault file, do not modify directly");
                sw.WriteLine("#" + " Created : " + System.DateTime.Now.ToString());

                sw.WriteLine("!" + vehicle_name);
                sw.WriteLine("@" + vehicle_configuration);
                sw.WriteLine("~" + operator_name);

                foreach (string value in list)
                {
                    if (value == null || value == "")
                        return;
                    var val = MainV2.comPort.MAV.param[value].ToString();
                    //Write out only if it is not readonly (defined in Parameter Metadata)
                    if (!is_param_readonly(value))
                    {
                        sw.WriteLine(value + "," + val);
                    }
                }
            }
        }

        public void loadParamFile()
        {
            string filename = Settings.GetUserDataDirectory() + Path.GetFileNameWithoutExtension(Settings.FileName) + "_" + MainV2.comPort.MAV.param["BRD_SERIAL_NUM"].Value.ToString() + ".paramvault";
            _vault_parameters.Clear();
            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (line.StartsWith("#"))
                        continue;
                    if (line.StartsWith("!"))
                        continue;
                    if (line.StartsWith("@"))
                        continue;
                    if (line.StartsWith("~"))
                        continue;

                    string[] items = line.Split(new char[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (items.Length != 2)
                        continue;

                    string name = items[0];
                    double value = 0;
                    try
                    {
                        value = double.Parse(items[1], System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch (Exception ex)
                    {
                        //log.Error(ex);
                        throw new FormatException("Invalid number on param " + name + " : " + items[1].ToString());
                    }
                    _vault_parameters[name] = value;
                }
            }
        }
    }
}