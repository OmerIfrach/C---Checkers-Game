using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checkers.View
{
    public class PawnButton : Button
    {
        private readonly int r_Row;
        private readonly int r_Column;

        public PawnButton(int i_Row, int i_Column, string i_Text, bool i_Enabled)
        {
            this.r_Row = i_Row;
            this.r_Column = i_Column;
            this.Text = i_Text;
            this.Enabled = i_Enabled;
            if(!this.Enabled)
            {
                this.BackColor = Color.Gray;
            }

            this.Font = new Font(this.Font, this.Font.Style | FontStyle.Bold);
        }

        public int Row
        {
            get
            {
                return this.r_Row;
            }
        }

        public int Col
        {
            get
            {
                return this.r_Column;
            }
        }

        public void PawnSelectedToggle()
        {
            if(this.BackColor == Color.Aqua)
            {
                this.BackColor = Color.White;
            }
            else
            {
                this.BackColor = Color.Aqua;
            }
        }

        public void OnChange(string i_ChangeEvent)
        {
            this.Text = i_ChangeEvent;
        }
    }
}
