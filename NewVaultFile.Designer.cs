namespace RACParamVault
{
    partial class NewVaultFile
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
            this.label1 = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.lConfig = new System.Windows.Forms.Label();
            this.lOperator = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbConfig = new System.Windows.Forms.TextBox();
            this.tbOperator = new System.Windows.Forms.TextBox();
            this.cbSave = new System.Windows.Forms.CheckBox();
            this.lineSeparator1 = new MissionPlanner.Controls.LineSeparator();
            this.bSave = new MissionPlanner.Controls.MyButton();
            this.bCancel = new MissionPlanner.Controls.MyButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(410, 156);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(53, 224);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(73, 13);
            this.lName.TabIndex = 1;
            this.lName.Text = "Vehicle Name";
            // 
            // lConfig
            // 
            this.lConfig.AutoSize = true;
            this.lConfig.Location = new System.Drawing.Point(53, 254);
            this.lConfig.Name = "lConfig";
            this.lConfig.Size = new System.Drawing.Size(125, 13);
            this.lConfig.TabIndex = 2;
            this.lConfig.Text = "Configuration Description";
            // 
            // lOperator
            // 
            this.lOperator.AutoSize = true;
            this.lOperator.Location = new System.Drawing.Point(53, 284);
            this.lOperator.Name = "lOperator";
            this.lOperator.Size = new System.Drawing.Size(79, 13);
            this.lOperator.TabIndex = 3;
            this.lOperator.Text = "Operator Name";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(190, 221);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(135, 20);
            this.tbName.TabIndex = 4;
            // 
            // tbConfig
            // 
            this.tbConfig.Location = new System.Drawing.Point(190, 251);
            this.tbConfig.Name = "tbConfig";
            this.tbConfig.Size = new System.Drawing.Size(135, 20);
            this.tbConfig.TabIndex = 5;
            // 
            // tbOperator
            // 
            this.tbOperator.Location = new System.Drawing.Point(190, 281);
            this.tbOperator.Name = "tbOperator";
            this.tbOperator.Size = new System.Drawing.Size(135, 20);
            this.tbOperator.TabIndex = 6;
            // 
            // cbSave
            // 
            this.cbSave.AutoSize = true;
            this.cbSave.Location = new System.Drawing.Point(56, 349);
            this.cbSave.Name = "cbSave";
            this.cbSave.Size = new System.Drawing.Size(207, 17);
            this.cbSave.TabIndex = 7;
            this.cbSave.Text = "Save current parameters into the Vault";
            this.cbSave.UseVisualStyleBackColor = true;
            // 
            // lineSeparator1
            // 
            this.lineSeparator1.Location = new System.Drawing.Point(12, 192);
            this.lineSeparator1.MaximumSize = new System.Drawing.Size(2000, 2);
            this.lineSeparator1.MinimumSize = new System.Drawing.Size(0, 2);
            this.lineSeparator1.Name = "lineSeparator1";
            this.lineSeparator1.Size = new System.Drawing.Size(415, 2);
            this.lineSeparator1.TabIndex = 8;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(56, 406);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(126, 23);
            this.bSave.TabIndex = 9;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(248, 406);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(126, 23);
            this.bCancel.TabIndex = 10;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // NewVaultFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 450);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.lineSeparator1);
            this.Controls.Add(this.cbSave);
            this.Controls.Add(this.tbOperator);
            this.Controls.Add(this.tbConfig);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lOperator);
            this.Controls.Add(this.lConfig);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.label1);
            this.Name = "NewVaultFile";
            this.Text = "NewVaultFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.Label lConfig;
        private System.Windows.Forms.Label lOperator;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbConfig;
        private System.Windows.Forms.TextBox tbOperator;
        private System.Windows.Forms.CheckBox cbSave;
        private MissionPlanner.Controls.LineSeparator lineSeparator1;
        private MissionPlanner.Controls.MyButton bSave;
        private MissionPlanner.Controls.MyButton bCancel;
    }
}