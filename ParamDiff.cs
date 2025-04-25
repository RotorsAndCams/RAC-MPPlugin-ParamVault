using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MissionPlanner;

namespace RACParamVault
{
    public partial class ParamDiff : Form
    {
        private RACParamVaultPlugin _plugin;

        public ParamDiff(RACParamVaultPlugin plugin)
        {
            this._plugin = plugin;

            InitializeComponent();

            foreach (KeyValuePair<string, ParamPair> entry in plugin._diff)
            {
                dgwParams.Rows.Add(entry.Key, entry.Value.inVehicle.ToString(), entry.Value.inVault.ToString());
            }

            //this.Height = (dgwParams.PreferredSize.Height + 190) > 600 ? 600 : (dgwParams.PreferredSize.Height + 190);
            //Console.WriteLine(this.Height);

            label1.Text = "Parameters stored in the vault for this vehicle " +
                          "are different from current ones.\r\nPlease select how to proceed!\r\n" +
                          "BRD ID:" + MainV2.comPort.MAV.param["BRD_SERIAL_NUM"].Value.ToString() +
                          " Vehicle name:" + plugin.vehicle_name + " Config:" + plugin.vehicle_configuration;
        }

        private void bIgnore_Click(object sender, EventArgs e)
        {
            _plugin._vault_ignored = true;
            if (tbOperator.Text.Length == 0 || textBoxDescription.MaxLength == 0)
            {
                label2.ForeColor = Color.Red;
                label3.ForeColor = Color.Red;
                return;
            }
            _plugin.operator_name = tbOperator.Text;
            _plugin.desc_of_change = textBoxDescription.Text;
            _plugin.WriteChangeLog(Changelog.IgonoreChangesOnVehicle);
            this.Close();
        }

        private void bUpdateVehicle_Click(object sender, EventArgs e)
        {
            if (tbOperator.Text.Length == 0 || textBoxDescription.MaxLength == 0)
            {
                label2.ForeColor = Color.Red;
                label3.ForeColor = Color.Red;
                return;
            }
            _plugin.operator_name = tbOperator.Text;
            _plugin.desc_of_change = textBoxDescription.Text;
            _plugin.UpdateParamsOnVehicle();
            _plugin.WriteChangeLog(Changelog.UpdateVehicle);
            _plugin._vault_ignored = false;
            this.Close();
        }

        private void bUpdate_Vault_Click(object sender, EventArgs e)
        {
            if (tbOperator.Text.Length == 0 || textBoxDescription.MaxLength == 0)
            {
                label2.ForeColor = Color.Red;
                label3.ForeColor = Color.Red;
                return;
            }
            _plugin.operator_name = tbOperator.Text;
            _plugin.desc_of_change = textBoxDescription.Text;
            _plugin.CreateVaultFile(); // TODO: Add error handling
            _plugin.LoadVaultFile();
            _plugin.WriteChangeLog(Changelog.UpdateVault);
            _plugin._vault_ignored = false;
            this.Close();
        }

        private void bIgnore5min_Click(object sender, EventArgs e)
        {
            // Instead of ignoring for the rest of session. 
            // set the nextrun to 5 min from now.
            _plugin.NextRun = DateTime.Now.AddMinutes(3);
            _plugin._vault_ignored = false;
            this.Close();
        }
    }
}