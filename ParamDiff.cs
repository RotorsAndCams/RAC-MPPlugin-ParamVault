using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MissionPlanner.RACParamVault
{
    public partial class ParamDiff : Form
    {
        RACParamVaultPlugin plugin;

        public ParamDiff(RACParamVaultPlugin plugin)
        {
            InitializeComponent();
            this.plugin = plugin;

            foreach (KeyValuePair<string, double> entry in plugin._differences)
            {
                var inVault = plugin._vault_parameters[entry.Key].ToString();
                dgwParams.Rows.Add(entry.Key, entry.Value.ToString(), inVault);
            }

            //for (int i = 0; i < 35; i++)
            var i = 1;
            dgwParams.Rows.Add(i.ToString(), i.ToString(), i.ToString());

            this.Height = (dgwParams.PreferredSize.Height + 150) > 600 ? 600 : (dgwParams.PreferredSize.Height + 150);
            Console.WriteLine(this.Height);

            label1.Text = "Parameters stored in the vault for this vehicle " +
                          "are different from current ones.\r\nPlease select how to proceed!\r\n" +
                          "BRD ID:" + MainV2.comPort.MAV.param["BRD_SERIAL_NUM"].Value.ToString() +
                          " Vehicle name:" + plugin.vehicle_name + " Config:" + plugin.vehicle_configuration;
        }

    

        private void myButton3_Click_1(object sender, EventArgs e)
        {
            plugin._vault_ignored = true;
            this.Close();
        }
    }
}