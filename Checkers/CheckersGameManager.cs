using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Checkers.model;
using Checkers.View;

namespace Checkers
{
    public class CheckersGameManager
    {
        private readonly CheckersGameOptionsD r_GameOptionsD = new CheckersGameOptionsD();
        private GameLogic m_CheckersGame;

        public void RunCheckersApp()
        {
            DialogResult dialogResult = r_GameOptionsD.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                m_CheckersGame = new GameLogic(r_GameOptionsD.BoardDimensions, r_GameOptionsD.Player1, r_GameOptionsD.Player2, r_GameOptionsD.IsPlayer2Computer);
                new GameForm(r_GameOptionsD.BoardDimensions, m_CheckersGame).ShowDialog();
            }
        }
    }
}
