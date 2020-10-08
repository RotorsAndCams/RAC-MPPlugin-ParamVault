namespace MissionPlanner.RACParamVault
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
            this.myButton1 = new MissionPlanner.Controls.MyButton();
            this.myButton2 = new MissionPlanner.Controls.MyButton();
            this.myButton3 = new MissionPlanner.Controls.MyButton();
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
            this.dgwParams.Location = new System.Drawing.Point(12, 128);
            this.dgwParams.Name = "dgwParams";
            this.dgwParams.ReadOnly = true;
            this.dgwParams.Size = new System.Drawing.Size(547, 284);
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
            // myButton1
            // 
            this.myButton1.Location = new System.Drawing.Point(13, 93);
            this.myButton1.Name = "myButton1";
            this.myButton1.Size = new System.Drawing.Size(111, 23);
            this.myButton1.TabIndex = 2;
            this.myButton1.Text = "UPDATE Vehicle";
            this.myButton1.UseVisualStyleBackColor = true;
            // 
            // myButton2
            // 
            this.myButton2.Location = new System.Drawing.Point(230, 92);
            this.myButton2.Name = "myButton2";
            this.myButton2.Size = new System.Drawing.Size(111, 23);
            this.myButton2.TabIndex = 3;
            this.myButton2.Text = "Update VAULT";
            this.myButton2.UseVisualStyleBackColor = true;
            // 
            // myButton3
            // 
            this.myButton3.Location = new System.Drawing.Point(447, 93);
            this.myButton3.Name = "myButton3";
            this.myButton3.Size = new System.Drawing.Size(111, 23);
            this.myButton3.TabIndex = 4;
            this.myButton3.Text = "IGNORE";
            this.myButton3.UseVisualStyleBackColor = true;
            this.myButton3.Click += new System.EventHandler(this.myButton3_Click_1);
            // 
            // ParamDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 420);
            this.Controls.Add(this.myButton3);
            this.Controls.Add(this.myButton2);
            this.Controls.Add(this.myButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgwParams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ParamDiff";
            this.Text = "Parameter missmatch ";
            ((System.ComponentModel.ISupportInitialize)(this.dgwParams)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn value_on_vehicle;
        private System.Windows.Forms.DataGridViewTextBoxColumn value_in_vault;
        private System.Windows.Forms.Label label1;
        private Controls.MyButton myButton1;
        private Controls.MyButton myButton2;
        private Controls.MyButton myButton3;
    }
}