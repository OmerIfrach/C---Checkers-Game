using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checkers.View
{
    public partial class CheckersGameOptionsD : Form
    {
        private string m_SavedPlayer2 = string.Empty;
        private int m_BoardDimensions = 6;

        public CheckersGameOptionsD()
        {
            InitializeComponent();
        }

        public int BoardDimensions
        {
            get
            {
                return this.m_BoardDimensions;
            }
        }

        public string Player1
        {
            get
            {
                return this.Player1Textbox.Text;
            }
        }

        public string Player2
        {
            get
            {
                return this.Player2TextBox.Text;
            }
        }

        public bool IsPlayer2Computer
        {
            get
            {
                return !this.Player2Checkbox.Checked;
            }
        }

        private void Player2Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = (sender as CheckBox).Checked;

            if(isChecked)
            {
                this.Player2TextBox.Enabled = true;
                this.Player2TextBox.Text = this.m_SavedPlayer2;
            }
            else
            {
                this.Player2TextBox.Enabled = false;
                this.m_SavedPlayer2 = this.Player2TextBox.Text;
                this.Player2TextBox.Text = "[Computer]";
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if(this.Player1 != string.Empty && Player2 != string.Empty)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Player 1 and Player 2 names must not be empty",
                    "Damka",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void radioDimensions6_CheckedChanged(object sender, EventArgs e)
        {
            this.m_BoardDimensions = 6;
        }

        private void radioDimensions8_CheckedChanged(object sender, EventArgs e)
        {
            this.m_BoardDimensions = 8;
        }

        private void radioDimensions10_CheckedChanged(object sender, EventArgs e)
        {
            this.m_BoardDimensions = 10;
        }
    }
}
