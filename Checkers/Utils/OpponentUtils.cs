using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Enums;

namespace Checkers.Utils
{
    public class OpponentUtils
    {
        public static eTeam GetOpponent(eTeam i_PlayerTeam)
        {
            if (i_PlayerTeam == eTeam.Player1)
            {
                return eTeam.Player2;
            }
            else
            {
                return eTeam.Player1;
            }
        }

        public static string GetSecondTeamSymbol(string i_BasicSymbol)
        {
            return i_BasicSymbol == ((char)eTeam.Player1).ToString() ? "K" : "U";
        }
    }
}
