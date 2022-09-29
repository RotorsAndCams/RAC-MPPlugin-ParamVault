using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MissionPlanner.Controls;
using MissionPlanner;

namespace RACParamVault
{
    public partial class NewVaultFile : Form
    {

        private RACParamVaultPlugin plugin;

        public NewVaultFile(RACParamVaultPlugin _plugin)
        {
            plugin = _plugin;
            InitializeComponent();
            label1.Text = "There is no param file in the Vault for the current MP config/Vehicle BRD_ID\r\n" +
                           "( " + plugin.vehicle_configuration + " / " + MainV2.comPort.MAV.param["BRD_SERIAL_NUM"] + " )\r\n\r\n" +
                           "To save current parameters into the Vault,\r\nenter data and press Save.\r\n" +
                           "To ignore Vault check until next Connect, press Cancel.";
            tbName.Text = plugin.vehicle_name;
            tbConfig.Text = plugin.vehicle_configuration;
            tbOperator.Text = plugin.operator_name;


        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = (DialogResult)CustomMessageBox.Show("Are you sure you want to ignore Vault till next connect?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if ((result == DialogResult.No) || (result == DialogResult.Cancel)) return;
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

        }

        private void bSave_Click(object sender, EventArgs e)
        {
            //Check fields

            Color original = label1.ForeColor;

            bool bOK = true;
            if (tbName.Text.Length == 0) { lName.ForeColor = Color.Red; bOK = false; } else lName.ForeColor = original;
            if (tbConfig.Text.Length == 0) { lConfig.ForeColor = Color.Red; bOK = false; } else lConfig.ForeColor = original;
            if (tbOperator.Text.Length == 0) { lOperator.ForeColor = Color.Red; bOK = false; } else lOperator.ForeColor = original;
            if (cbSave.Checked == false) { cbSave.ForeColor = Color.Red; bOK = false; } else cbSave.ForeColor = original;

            if (bOK)
            {
                plugin.vehicle_name = tbName.Text;
                plugin.vehicle_configuration = tbConfig.Text;
                plugin.operator_name = tbOperator.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
