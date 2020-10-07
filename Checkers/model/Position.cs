using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.model
{
    public class Position
    {
        private readonly int r_Row;
        private readonly int r_Col;

        public Position(int i_Row, int i_Col)
        {
            this.r_Row = i_Row;
            this.r_Col = i_Col;
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
                return this.r_Col;
            }
        }
    }
}
