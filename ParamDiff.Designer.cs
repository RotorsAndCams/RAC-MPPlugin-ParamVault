using MissionPlanner.Controls;

namespace RACParamVault
{
    partial class ParamDiff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgwParams = new System.Windows.Forms.DataGridView();
            this.parameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value_on_vehicle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value_in_vault = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.bUpdateVehicle = new MissionPlanner.Controls.MyButton();
            this.bUpdate_Vault = new MissionPlanner.Controls.MyButton();
            this.bIgnore = new MissionPlanner.Controls.MyButton();
            this.tbOperator = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bIgnore5min = new MissionPlanner.Controls.MyButton();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgwParams)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwParams
            // 
            this.dgwParams.AllowUserToAddRows = false;
            this.dgwParams.AllowUserToDeleteRows = false;
            this.dgwParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.parameter,
            this.value_on_vehicle,
            this.value_in_vault});
            this.dgwParams.Location = new System.Drawing.Point(12, 243);
            this.dgwParams.Name = "dgwParams";
            this.dgwParams.ReadOnly = true;
            this.dgwParams.Size = new System.Drawing.Size(547, 409);
            this.dgwParams.TabIndex = 0;
            // 
            // parameter
            // 
            this.parameter.HeaderText = "Parameter Name";
            this.parameter.Name = "parameter";
            this.parameter.ReadOnly = true;
            this.parameter.Width = 280;
            // 
            // value_on_vehicle
            // 
            this.value_on_vehicle.HeaderText = "ON Vehicle";
            this.value_on_vehicle.Name = "value_on_vehicle";
            this.value_on_vehicle.ReadOnly = true;
            // 
            // value_in_vault
            // 
            this.value_in_vault.HeaderText = "IN Vault";
            this.value_in_vault.Name = "value_in_vault";
            this.value_in_vault.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(547, 68);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bUpdateVehicle
            // 
            this.bUpdateVehicle.Location = new System.Drawing.Point(13, 93);
            this.bUpdateVehicle.Name = "bUpdateVehicle";
            this.bUpdateVehicle.Size = new System.Drawing.Size(111, 23);
            this.bUpdateVehicle.TabIndex = 2;
            this.bUpdateVehicle.Text = "UPDATE Vehicle";
            this.bUpdateVehicle.UseVisualStyleBackColor = true;
            this.bUpdateVehicle.Click += new System.EventHandler(this.bUpdateVehicle_Click);
            // 
            // bUpdate_Vault
            // 
            this.bUpdate_Vault.Location = new System.Drawing.Point(143, 93);
            this.bUpdate_Vault.Name = "bUpdate_Vault";
            this.bUpdate_Vault.Size = new System.Drawing.Size(111, 23);
            this.bUpdate_Vault.TabIndex = 3;
            this.bUpdate_Vault.Text = "Update VAULT";
            this.bUpdate_Vault.UseVisualStyleBackColor = true;
            this.bUpdate_Vault.Click += new System.EventHandler(this.bUpdate_Vault_Click);
            // 
            // bIgnore
            // 
            this.bIgnore.Location = new System.Drawing.Point(330, 93);
            this.bIgnore.Name = "bIgnore";
            this.bIgnore.Size = new System.Drawing.Size(111, 35);
            this.bIgnore.TabIndex = 4;
            this.bIgnore.Text = "IGNORE till next Connect";
            this.bIgnore.UseVisualStyleBackColor = true;
            this.bIgnore.Click += new System.EventHandler(this.bIgnore_Click);
            // 
            // tbOperator
            // 
            this.tbOperator.Location = new System.Drawing.Point(69, 134);
            this.tbOperator.Name = "tbOperator";
            this.tbOperator.Size = new System.Drawing.Size(148, 20);
            this.tbOperator.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Operator:";
            // 
            // bIgnore5min
            // 
            this.bIgnore5min.Location = new System.Drawing.Point(447, 93);
            this.bIgnore5min.Name = "bIgnore5min";
            this.bIgnore5min.Size = new System.Drawing.Size(111, 35);
            this.bIgnore5min.TabIndex = 7;
            this.bIgnore5min.Text = "IGNORE for 3 min";
            this.bIgnore5min.UseVisualStyleBackColor = true;
            this.bIgnore5min.Click += new System.EventHandler(this.bIgnore5min_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Desc:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(69, 162);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(489, 75);
            this.textBoxDescription.TabIndex = 9;
            // 
            // ParamDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 660);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bIgnore5min);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbOperator);
            this.Controls.Add(this.bIgnore);
            this.Controls.Add(this.bUpdate_Vault);
            this.Controls.Add(this.bUpdateVehicle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgwParams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ParamDiff";
            this.Text = "Parameter missmatch ";
            ((System.ComponentModel.ISupportInitialize)(this.dgwParams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn value_on_vehicle;
        private System.Windows.Forms.DataGridViewTextBoxColumn value_in_vault;
        private System.Windows.Forms.Label label1;
        private MyButton bUpdateVehicle;
        private MyButton bUpdate_Vault;
        private MyButton bIgnore;
        private System.Windows.Forms.TextBox tbOperator;
        private System.Windows.Forms.Label label2;
        private MyButton bIgnore5min;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDescription;
    }
}