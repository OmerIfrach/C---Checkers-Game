namespace Checkers.View
{
    public partial class CheckersGameOptionsD
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
            this.radioDimensions6 = new System.Windows.Forms.RadioButton();
            this.radioDimensions8 = new System.Windows.Forms.RadioButton();
            this.radioDimensions10 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Player1Textbox = new System.Windows.Forms.TextBox();
            this.Player2TextBox = new System.Windows.Forms.TextBox();
            this.Player2Checkbox = new System.Windows.Forms.CheckBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Board Size:";
            // 
            // radioDimensions6
            // 
            this.radioDimensions6.AutoSize = true;
            this.radioDimensions6.Location = new System.Drawing.Point(34, 29);
            this.radioDimensions6.Name = "radioDimensions6";
            this.radioDimensions6.Size = new System.Drawing.Size(59, 21);
            this.radioDimensions6.TabIndex = 1;
            this.radioDimensions6.TabStop = true;
            this.radioDimensions6.Text = "6 x 6";
            this.radioDimensions6.UseVisualStyleBackColor = true;
            this.radioDimensions6.CheckedChanged += new System.EventHandler(this.radioDimensions6_CheckedChanged);
            // 
            // radioDimensions8
            // 
            this.radioDimensions8.AutoSize = true;
            this.radioDimensions8.Location = new System.Drawing.Point(99, 29);
            this.radioDimensions8.Name = "radioDimensions8";
            this.radioDimensions8.Size = new System.Drawing.Size(59, 21);
            this.radioDimensions8.TabIndex = 2;
            this.radioDimensions8.TabStop = true;
            this.radioDimensions8.Text = "8 x 8";
            this.radioDimensions8.UseVisualStyleBackColor = true;
            this.radioDimensions8.CheckedChanged += new System.EventHandler(this.radioDimensions8_CheckedChanged);
            // 
            // radioDimensions10
            // 
            this.radioDimensions10.AutoSize = true;
            this.radioDimensions10.Location = new System.Drawing.Point(164, 29);
            this.radioDimensions10.Name = "radioDimensions10";
            this.radioDimensions10.Size = new System.Drawing.Size(75, 21);
            this.radioDimensions10.TabIndex = 3;
            this.radioDimensions10.TabStop = true;
            this.radioDimensions10.Text = "10 x 10";
            this.radioDimensions10.UseVisualStyleBackColor = true;
            this.radioDimensions10.CheckedChanged += new System.EventHandler(this.radioDimensions10_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Players:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Player 1:";
            // 
            // Player1Textbox
            // 
            this.Player1Textbox.Location = new System.Drawing.Point(139, 87);
            this.Player1Textbox.Name = "Player1Textbox";
            this.Player1Textbox.Size = new System.Drawing.Size(100, 22);
            this.Player1Textbox.TabIndex = 7;
            // 
            // Player2TextBox
            // 
            this.Player2TextBox.Enabled = false;
            this.Player2TextBox.Location = new System.Drawing.Point(139, 124);
            this.Player2TextBox.Name = "Player2TextBox";
            this.Player2TextBox.Size = new System.Drawing.Size(100, 22);
            this.Player2TextBox.TabIndex = 8;
            this.Player2TextBox.Text = "[Computer]";
            // 
            // Player2Checkbox
            // 
            this.Player2Checkbox.AutoSize = true;
            this.Player2Checkbox.Location = new System.Drawing.Point(34, 124);
            this.Player2Checkbox.Name = "Player2Checkbox";
            this.Player2Checkbox.Size = new System.Drawing.Size(82, 21);
            this.Player2Checkbox.TabIndex = 9;
            this.Player2Checkbox.Text = "Player2:";
            this.Player2Checkbox.UseVisualStyleBackColor = true;
            this.Player2Checkbox.CheckedChanged += new System.EventHandler(this.Player2Checkbox_CheckedChanged);
            // 
            // buttonDone
            // 
            this.buttonDone.AccessibleName = string.Empty;
            this.buttonDone.Location = new System.Drawing.Point(164, 161);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 10;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // CheckersGameOptionsD
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 197);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.Player2Checkbox);
            this.Controls.Add(this.Player2TextBox);
            this.Controls.Add(this.Player1Textbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioDimensions10);
            this.Controls.Add(this.radioDimensions8);
            this.Controls.Add(this.radioDimensions6);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckersGameOptionsD";
            this.ShowIcon = false;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioDimensions6;
        private System.Windows.Forms.RadioButton radioDimensions8;
        private System.Windows.Forms.RadioButton radioDimensions10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Player1Textbox;
        private System.Windows.Forms.TextBox Player2TextBox;
        private System.Windows.Forms.CheckBox Player2Checkbox;
        private System.Windows.Forms.Button buttonDone;
    }
}