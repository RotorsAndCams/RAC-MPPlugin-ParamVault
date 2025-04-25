using System;
using System.Drawing;
using System.Windows.Forms;

namespace RACParamVault
{
    public partial class NewVaultFile : Form
    {
        private RACParamVaultPlugin _plugin;

        public NewVaultFile(RACParamVaultPlugin plugin)
        {
            _plugin = plugin;

            InitializeComponent();

            label1.Text = "There is no param file in the Vault for the current MP config/Vehicle BRD_ID\r\n" +
                           "( " + this._plugin.vehicle_configuration + " / " + this._plugin.Host.comPort.MAV.param["BRD_SERIAL_NUM"] + " )\r\n\r\n" +
                           "To save current parameters into the Vault,\r\nenter data and press Save.\r\n" +
                           "To ignore Vault check until next Connect, press Cancel.";
            tbName.Text = this._plugin.vehicle_name;
            tbConfig.Text = this._plugin.vehicle_configuration;
            tbOperator.Text = this._plugin.operator_name;
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
            // Check fields

            Color original = label1.ForeColor;

            bool bOK = true;
            if (tbName.Text.Length == 0) { lName.ForeColor = Color.Red; bOK = false; } else lName.ForeColor = original;
            if (tbConfig.Text.Length == 0) { lConfig.ForeColor = Color.Red; bOK = false; } else lConfig.ForeColor = original;
            if (tbOperator.Text.Length == 0) { lOperator.ForeColor = Color.Red; bOK = false; } else lOperator.ForeColor = original;
            if (cbSave.Checked == false) { cbSave.ForeColor = Color.Red; bOK = false; } else cbSave.ForeColor = original;

            if (bOK)
            {
                _plugin.vehicle_name = tbName.Text;
                _plugin.vehicle_configuration = tbConfig.Text;
                _plugin.operator_name = tbOperator.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
