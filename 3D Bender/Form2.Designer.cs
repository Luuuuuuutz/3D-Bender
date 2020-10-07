namespace _3D_Bender
{
    partial class Form2
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
            this.insertBendButton = new System.Windows.Forms.Button();
            this.RadiusTxtBox = new System.Windows.Forms.TextBox();
            this.arcAngleTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.arcFeedTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.helixSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.feedTxtBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pitchTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pitchRotRadioBtn = new System.Windows.Forms.RadioButton();
            this.pitchFeedRadioBtn = new System.Windows.Forms.RadioButton();
            this.rotationsTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.helixCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.helixSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // insertBendButton
            // 
            this.insertBendButton.Location = new System.Drawing.Point(91, 219);
            this.insertBendButton.Name = "insertBendButton";
            this.insertBendButton.Size = new System.Drawing.Size(100, 27);
            this.insertBendButton.TabIndex = 0;
            this.insertBendButton.Text = "Insert Bend";
            this.insertBendButton.UseVisualStyleBackColor = true;
            this.insertBendButton.Click += new System.EventHandler(this.InsertBendButton_Click);
            // 
            // RadiusTxtBox
            // 
            this.RadiusTxtBox.Location = new System.Drawing.Point(6, 36);
            this.RadiusTxtBox.Name = "RadiusTxtBox";
            this.RadiusTxtBox.Size = new System.Drawing.Size(55, 20);
            this.RadiusTxtBox.TabIndex = 1;
            this.RadiusTxtBox.TextChanged += new System.EventHandler(this.RadiusTxtBox_TextChanged);
            // 
            // arcAngleTxtBox
            // 
            this.arcAngleTxtBox.Location = new System.Drawing.Point(89, 36);
            this.arcAngleTxtBox.Name = "arcAngleTxtBox";
            this.arcAngleTxtBox.Size = new System.Drawing.Size(67, 20);
            this.arcAngleTxtBox.TabIndex = 3;
            this.arcAngleTxtBox.TextChanged += new System.EventHandler(this.ArcAngleTxtBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Radius";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Arc Angle";
            // 
            // arcFeedTxtBox
            // 
            this.arcFeedTxtBox.Location = new System.Drawing.Point(183, 36);
            this.arcFeedTxtBox.Name = "arcFeedTxtBox";
            this.arcFeedTxtBox.ReadOnly = true;
            this.arcFeedTxtBox.Size = new System.Drawing.Size(60, 20);
            this.arcFeedTxtBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Feed";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.helixSettingsGroupBox);
            this.groupBox1.Controls.Add(this.helixCheckBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.arcFeedTxtBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.arcAngleTxtBox);
            this.groupBox1.Controls.Add(this.RadiusTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(2, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 201);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Circular Arc";
            // 
            // helixSettingsGroupBox
            // 
            this.helixSettingsGroupBox.Controls.Add(this.feedTxtBox);
            this.helixSettingsGroupBox.Controls.Add(this.label6);
            this.helixSettingsGroupBox.Controls.Add(this.pitchTxtBox);
            this.helixSettingsGroupBox.Controls.Add(this.label5);
            this.helixSettingsGroupBox.Controls.Add(this.pitchRotRadioBtn);
            this.helixSettingsGroupBox.Controls.Add(this.pitchFeedRadioBtn);
            this.helixSettingsGroupBox.Controls.Add(this.rotationsTxtBox);
            this.helixSettingsGroupBox.Controls.Add(this.label4);
            this.helixSettingsGroupBox.Location = new System.Drawing.Point(6, 92);
            this.helixSettingsGroupBox.Name = "helixSettingsGroupBox";
            this.helixSettingsGroupBox.Size = new System.Drawing.Size(252, 94);
            this.helixSettingsGroupBox.TabIndex = 10;
            this.helixSettingsGroupBox.TabStop = false;
            this.helixSettingsGroupBox.Text = "Helix Settings";
            // 
            // feedTxtBox
            // 
            this.feedTxtBox.Location = new System.Drawing.Point(189, 25);
            this.feedTxtBox.Name = "feedTxtBox";
            this.feedTxtBox.Size = new System.Drawing.Size(48, 20);
            this.feedTxtBox.TabIndex = 14;
            this.feedTxtBox.TextChanged += new System.EventHandler(this.FeedTxtBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(186, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Feed";
            // 
            // pitchTxtBox
            // 
            this.pitchTxtBox.Location = new System.Drawing.Point(122, 40);
            this.pitchTxtBox.Name = "pitchTxtBox";
            this.pitchTxtBox.Size = new System.Drawing.Size(48, 20);
            this.pitchTxtBox.TabIndex = 12;
            this.pitchTxtBox.TextChanged += new System.EventHandler(this.PitchTxtBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Pitch";
            // 
            // pitchRotRadioBtn
            // 
            this.pitchRotRadioBtn.AutoSize = true;
            this.pitchRotRadioBtn.Location = new System.Drawing.Point(6, 45);
            this.pitchRotRadioBtn.Name = "pitchRotRadioBtn";
            this.pitchRotRadioBtn.Size = new System.Drawing.Size(106, 17);
            this.pitchRotRadioBtn.TabIndex = 11;
            this.pitchRotRadioBtn.Text = "Pitch && Rotations";
            this.pitchRotRadioBtn.UseVisualStyleBackColor = true;
            // 
            // pitchFeedRadioBtn
            // 
            this.pitchFeedRadioBtn.AutoSize = true;
            this.pitchFeedRadioBtn.Checked = true;
            this.pitchFeedRadioBtn.Location = new System.Drawing.Point(6, 22);
            this.pitchFeedRadioBtn.Name = "pitchFeedRadioBtn";
            this.pitchFeedRadioBtn.Size = new System.Drawing.Size(85, 17);
            this.pitchFeedRadioBtn.TabIndex = 10;
            this.pitchFeedRadioBtn.TabStop = true;
            this.pitchFeedRadioBtn.Text = "Pitch && Feed";
            this.pitchFeedRadioBtn.UseVisualStyleBackColor = true;
            this.pitchFeedRadioBtn.CheckedChanged += new System.EventHandler(this.pitchFeedRadioBtn_CheckedChanged);
            // 
            // rotationsTxtBox
            // 
            this.rotationsTxtBox.Location = new System.Drawing.Point(189, 67);
            this.rotationsTxtBox.Name = "rotationsTxtBox";
            this.rotationsTxtBox.ReadOnly = true;
            this.rotationsTxtBox.Size = new System.Drawing.Size(48, 20);
            this.rotationsTxtBox.TabIndex = 8;
            this.rotationsTxtBox.TextChanged += new System.EventHandler(this.RotationsTxtBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Rotations";
            // 
            // helixCheckBox
            // 
            this.helixCheckBox.AutoSize = true;
            this.helixCheckBox.Location = new System.Drawing.Point(6, 67);
            this.helixCheckBox.Name = "helixCheckBox";
            this.helixCheckBox.Size = new System.Drawing.Size(55, 17);
            this.helixCheckBox.TabIndex = 8;
            this.helixCheckBox.Text = "Helix?";
            this.helixCheckBox.UseVisualStyleBackColor = true;
            this.helixCheckBox.CheckedChanged += new System.EventHandler(this.HelixCheckBox_CheckedChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 254);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.insertBendButton);
            this.Name = "Form2";
            this.Text = "Advanced Setup";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.helixSettingsGroupBox.ResumeLayout(false);
            this.helixSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button insertBendButton;
        private System.Windows.Forms.TextBox RadiusTxtBox;
        private System.Windows.Forms.TextBox arcAngleTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox arcFeedTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox helixCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox rotationsTxtBox;
        private System.Windows.Forms.GroupBox helixSettingsGroupBox;
        private System.Windows.Forms.RadioButton pitchRotRadioBtn;
        private System.Windows.Forms.RadioButton pitchFeedRadioBtn;
        private System.Windows.Forms.TextBox feedTxtBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox pitchTxtBox;
        private System.Windows.Forms.Label label5;
    }
}