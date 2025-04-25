using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MissionPlanner.Controls;
using MissionPlanner.Utilities;
using System.Globalization;

namespace RACParamVault
{
    public class ParamPair
    {
        public double inVehicle { set; get; }
        public double inVault { set; get; }
    }

    public enum Changelog
    {
        NewVaultFile = 0,
        UpdateVehicle = 1,
        UpdateVault = 2,
        IgonoreChangesOnVehicle = 3
    }

    public class RACParamVaultPlugin : MissionPlanner.Plugin.Plugin
    {
        private string _vConfigKey = "MPConfigDesc"; // Key for the vehicle configuration in the settings file

        public string vehicle_name;                // Marked with ! in the vault file
        public string vehicle_configuration;       // Marked with @ in the vault file
        public string operator_name;               // Marked with ~ in the vault file
        public string desc_of_change;              // Description field in the changelog 

        // This will contains the actual parameter list from the vehicle
        public Dictionary<string, double> _actual_parameters = new Dictionary<string, double>();

        public Dictionary<string, double> _vault_parameters = new Dictionary<string, double>();
        public Dictionary<string, double> _params_to_update = new Dictionary<string, double>();

        //public IEnumerable<KeyValuePair<string, double>> _differences = new Dictionary<string, double>();

        public Dictionary<string, ParamPair> _diff = new Dictionary<string, ParamPair>();

        private ToolStripMenuItem but;

        /// <summary>
        /// The vault contect is loaded for the actual vehicle
        /// </summary>
        private bool _vault_loaded = false;

        /// <summary>
        /// Vault is ignored, do not askt till next connect
        /// </summary>
        public bool _vault_ignored = false;

        #region Plugin info

        public override string Name
        {
            get { return "RAC-ParamVault"; }
        }

        public override string Version
        {
            get { return "0.1"; }
        }

        public override string Author
        {
            get { return "Rotors and Cams"; }
        }

        #endregion

        //[DebuggerHidden]
        public override bool Init()
        {
            loopratehz = 0.2f;  // Every 5 seconds
            return true;
        }

        public override bool Loaded()
        {
            but = new ToolStripMenuItem("Vault Test");
            but.Click += but_Click;
            ToolStripItemCollection col = Host.FDMenuMap.Items;
            col.Add(but);

            vehicle_name = "GigaRotor 6 v1";
            vehicle_configuration = Host.config[_vConfigKey, "Default"];
            Host.config[_vConfigKey] = vehicle_configuration;

            return true;
        }

        public override bool Loop()
        {
            // If we are not connected then we skip
            if (!Host.comPort.MAV.cs.connected)
            {
                _vault_loaded = false;
                _vault_ignored = false;
                return true;
            }
            // If we connected and armed we also skip
            if (Host.comPort.MAV.cs.armed)
            {
                Console.WriteLine("MAV is armed skipped vault checking");
                return true;
            }
            else if ((Host.comPort.MAV.param.TotalReceived < Host.comPort.MAV.param.TotalReported) || Host.comPort.MAV.param.TotalReported == 0) // If Not all parameters are loaded then we skip as well
            {
                Console.WriteLine("Parameters are not fully loaded, skip vault checking");
                return true;
            }
            else if (Host.comPort.MAV.param["BRD_SERIAL_NUM"].Value == 0) // Ignore vehicles with BRD_SERIAL_NUM == 0
            {
                Console.WriteLine("BRD_SERIAL_NUM is zero, skip vault check");
                return true;
            }
            else if (_vault_ignored) // If _vault_ignored is set skip
            {
                Console.WriteLine("Vault ignored");
                return true;
            }

            // Check if vault is loaded, if not try to load, if vault is not find then ask operator to create a new vault data for this vehicle
            if (!_vault_loaded)
            {
                string filename = Settings.GetUserDataDirectory() + Path.GetFileNameWithoutExtension(Settings.FileName) + "_" + Host.comPort.MAV.param["BRD_SERIAL_NUM"].Value.ToString() + ".paramvault";
                if (!File.Exists(filename))
                {
                    using (Form NewVaultFile = new NewVaultFile(this))
                    {
                        MissionPlanner.Utilities.ThemeManager.ApplyThemeTo(NewVaultFile);
                        _vault_ignored = true; // Ignore further checks during the dialog box
                        NewVaultFile.ShowDialog();
                        _vault_ignored = false;
                        if ((NewVaultFile.DialogResult == DialogResult.No) || (NewVaultFile.DialogResult == DialogResult.Cancel))
                        {
                            _vault_ignored = true;
                            return true;
                        }
                        else
                        {
                            CreateVaultFile();
                            LoadVaultFile();
                            WriteChangeLog(Changelog.NewVaultFile); // Update Changelog with the new vaultfile (for reference all params are stored at the begining)
                            _vault_loaded = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Load vault file");
                    LoadVaultFile();
                    _vault_loaded = true;
                }
            }
            else
            {
                // We have a loaded vault file now.
                GetParamsFromVehicle(); // Update current parameters

                //var _differences = _actual_parameters.Except(_vault_parameters);

                if (CompareParamsWithVault()) // There are differences in the actual config and the Vault
                {
                    _vault_ignored = true;  // Do not reopen during dialog box
                    using (Form ParamDiff = new ParamDiff(this))
                    {
                        MissionPlanner.Utilities.ThemeManager.ApplyThemeTo(ParamDiff);
                        ParamDiff.ShowDialog();
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Compare parameters in _actual_parameters with _vault_parameters
        /// </summary>
        /// <returns> true if there are differences (put into : _diff : dictionary of paramname, ParamPair </returns>
        public bool CompareParamsWithVault()
        {
            IEnumerable<KeyValuePair<string, double>> _differences = _actual_parameters.Except(_vault_parameters);
            if (_differences.Count() == 0) return false; // No differences

            // OK we have differences, fill up the _diff
            _diff.Clear();
            
            foreach (KeyValuePair<string, double> entry in _differences)
            {
                if (_vault_parameters.TryGetValue(entry.Key, out double outValue))
                {
                    _diff.Add(entry.Key, new ParamPair { inVehicle = entry.Value, inVault = outValue });
                }
                else
                {
                    _diff.Add(entry.Key, new ParamPair { inVehicle = entry.Value, inVault = 0 });
                }

                //var inVaultValue = _vault_parameters[entry.Key];
                //_diff.Add(entry.Key, new ParamPair { inVehicle = entry.Value, inVault = inVaultValue });
            }

            return true;
        }

        public override bool Exit()
        {
            return true;
        }

        private bool IsParamReadOnly(string parameter_name)
        {
            string readonlyStr = ParameterMetaDataRepository.GetParameterMetaData(parameter_name, ParameterMetaDataConstants.ReadOnly, Host.comPort.MAV.cs.firmware.ToString());
            if (readonlyStr != String.Empty && bool.TryParse(readonlyStr, out bool result))
            {
                return result;
            }
            else
            {
                return false;
            }
        }

        private bool IsParamIgnored(string parameter_name)
        {
            return new List<string> {
                "SYSID_THISMAV",
                "SYSID_SW_TYPE",
                "SYSID_SW_MREV",
                "WP_TOTAL",
                "CMD_TOTAL",
                "FENCE_TOTAL",
                "SYS_NUM_RESETS",
                "ARSPD_OFFSET",
                "GND_ABS_PRESS",
                "GND_TEMP",
                "CMD_INDEX",
                "LOG_LASTFILE",
                "FORMAT_VERSION",
                "MOT_THST_HOVER",
                "COMPASS_DEC"
            }.Contains(parameter_name)
            || new List<string>{
                "SIM_",
                "SR0_",
                "INS_GYROFFS",
                "INS_GYR2OFF",
                "INS_GYR3OFF"
            }.Any(p => parameter_name.StartsWith(p));
        }

        public bool UpdateParamsOnVehicle()
        {
            // Get the values in _diff and write paremeters back to vehicle
            _params_to_update.Clear();
            foreach (KeyValuePair<string, ParamPair> entry in _diff)
            {
                _params_to_update.Add(entry.Key, entry.Value.inVault);
                Form paramCompareForm = new ParamCompare(null, Host.comPort.MAV.param, _params_to_update);
                paramCompareForm.Text = "Update parameters from VAULT";
                ThemeManager.ApplyThemeTo(paramCompareForm);
                var button = paramCompareForm.Controls.Find("BUT_save", true).FirstOrDefault() as MissionPlanner.Controls.MyButton;
                button.Text = "Write to FC";
                var dgv = paramCompareForm.Controls.Find("Params", true).FirstOrDefault() as System.Windows.Forms.DataGridView;
                dgv.Columns[1].HeaderText = "On Vehicle";
                dgv.Columns[2].HeaderText = "From Vault";
                paramCompareForm.ShowDialog();
            }

            return true;
        }

        private void GetParamsFromVehicle()
        {
            // Check if all params are loaded
            if (Host.comPort.MAV.param.TotalReceived < Host.comPort.MAV.param.TotalReported) return;

            // Clear _actual_params
            _actual_parameters.Clear();

            foreach (string item in Host.comPort.MAV.param.Keys)
            {
                if (IsParamIgnored(item))
                    continue;
                if (IsParamReadOnly(item))
                    continue;

                var value = Math.Round(Host.comPort.MAV.param[item].Value, 5); // Rounding for 6 decimal digits to overcome stupid double conversion errors.
                _actual_parameters.Add(item, value);
            }
        }

        public void WriteChangeLog(Changelog action)
        {
            string filename = Settings.GetUserDataDirectory() + Path.GetFileNameWithoutExtension(Settings.FileName) + "_" + Host.comPort.MAV.param["BRD_SERIAL_NUM"].Value.ToString() + ".changelog.xml";

            if (!File.Exists(filename))
            {
                // Add XML header
                using (StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Append)))
                {
                    sw.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
                    sw.WriteLine(@"<!-- There is no closing Changelog node due the nature of the log -->");
                    sw.WriteLine("<Changelog>");
                }
            }
            using (StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Append)))
            {
                sw.WriteLine(@"<Logentry>");
                sw.WriteLine("\t<Action>" + action.ToString() + "</Action>");
                sw.WriteLine("\t<Timestamp>" + System.DateTime.Now.ToString("yyyy-mm-dd-hh:mm") + "</Timestamp>");
                sw.WriteLine("\t<Operator>" + operator_name + "</Operator>");
                sw.WriteLine("\t<Config>" + vehicle_configuration + "</Config>");
                sw.WriteLine("\t<Vehicle_name>" + vehicle_name + "</Vehicle_name>");
                sw.WriteLine("\t<Description>" + desc_of_change + "</Description>");

                sw.WriteLine("\t<Changes>");

                if (action == Changelog.NewVaultFile)
                {
                    foreach (KeyValuePair<string, double> entry in _vault_parameters)
                        sw.WriteLine(entry.Key + "," + entry.Value.ToString("G",CultureInfo.InvariantCulture));
                }
                else if (action == Changelog.UpdateVehicle)
                {
                    foreach (KeyValuePair<string, ParamPair> entry in _diff)
                        sw.WriteLine(entry.Key + " value on Vehicle (" + entry.Value.inVehicle.ToString("G", CultureInfo.InvariantCulture) + ") <- updated to " + entry.Value.inVault.ToString("G", CultureInfo.InvariantCulture));
                }
                else if (action == Changelog.UpdateVault)
                {
                    foreach (KeyValuePair<string, ParamPair> entry in _diff)
                        sw.WriteLine(entry.Key + " value in Vault (" + entry.Value.inVault.ToString("G", CultureInfo.InvariantCulture) + ") <- changed to " + entry.Value.inVehicle.ToString("G", CultureInfo.InvariantCulture));
                }
                else if (action == Changelog.IgonoreChangesOnVehicle)
                {
                    foreach (KeyValuePair<string, ParamPair> entry in _diff)
                        sw.WriteLine(entry.Key + " value on Vehicle (" + entry.Value.inVehicle.ToString("G", CultureInfo.InvariantCulture) + ") <-> in Vault " + entry.Value.inVault.ToString("G", CultureInfo.InvariantCulture));
                }
                sw.WriteLine("</Changes>");
                sw.WriteLine(@"</Logentry>");
            }
        }

        public void CreateVaultFile()
        {
            // This will be the filename
            string filename = Settings.GetUserDataDirectory() + Path.GetFileNameWithoutExtension(Settings.FileName) + "_" + Host.comPort.MAV.param["BRD_SERIAL_NUM"].Value.ToString() + ".paramvault";

            using (StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create)))
            {
                var list = new List<string>();
                foreach (string item in Host.comPort.MAV.param.Keys)
                {
                    // Exclude these readonly fields
                    if (IsParamIgnored(item) || IsParamReadOnly(item))
                        continue;
                    list.Add(item);
                }

                sw.WriteLine("#" + " This is a configuration vault file, do not modify directly");
                sw.WriteLine("#" + " Created : " + System.DateTime.Now.ToString());

                sw.WriteLine("!" + vehicle_name);
                sw.WriteLine("@" + vehicle_configuration);
                sw.WriteLine("~" + operator_name);

                foreach (string value in list)
                {
                    if (value == null || value == "")
                        return;
                    double val = Host.comPort.MAV.param[value].GetValue();
                    
                    // Write out only if it is not readonly (defined in Parameter Metadata)
                    if (!IsParamReadOnly(value))
                    {
                        sw.WriteLine(value + "," + val.ToString("G",CultureInfo.InvariantCulture));
                    }
                }
            }
        }

        public bool LoadVaultFile() // TODO: add error handling
        {
            string filename = Settings.GetUserDataDirectory() + Path.GetFileNameWithoutExtension(Settings.FileName) + "_" + Host.comPort.MAV.param["BRD_SERIAL_NUM"].Value.ToString() + ".paramvault";
            _vault_parameters.Clear();
            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (line.StartsWith("#"))
                    {
                        continue;
                    }
                    if (line.StartsWith("!"))
                    {
                        vehicle_name = line.Substring(1);
                        continue;
                    }
                    if (line.StartsWith("@"))
                    {
                        vehicle_configuration = line.Substring(1);
                        continue;
                    }
                    if (line.StartsWith("~"))
                    {
                        operator_name = line.Substring(1);
                        continue;
                    }

                    string[] items = line.Split(new char[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (items.Length != 2)
                    {
                        continue;
                    }

                    string name = items[0];
                    double value = 0;
                    try
                    {
                        value = double.Parse(items[1], System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        //log.Error(ex);
                        throw new FormatException("Invalid number on param " + name + " : " + items[1].ToString());
                    }
                    _vault_parameters[name] = value;
                }
            }
            return true;
        }

        private void but_Click(object sender, EventArgs e)
        {
            WriteChangeLog(Changelog.NewVaultFile);
        }
    }
}
