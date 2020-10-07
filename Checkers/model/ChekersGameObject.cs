using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Enums;

namespace Checkers.model
{
    public abstract class CheckersGameObject
    {
        private eTeam m_Team;

        protected CheckersGameObject(eTeam i_Team)
        {
            this.m_Team = i_Team;
        }

        public eTeam Team
        {
            get
            {
                return this.m_Team;
            }
        }

        public abstract bool IsMoveLegal(
            Position i_CurrentPosition,
            Position i_NextPosition,
            CheckersGameObject[,] i_GameBoard);

        public abstract bool HasAvailableSkip(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard);

        public abstract List<Position> GetAllSkipMoves(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard);

        public abstract Position SuggestComputerPosition(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard);

        public abstract List<Position> GetAvailableMoves(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard);

        public override string ToString()
        {
            return ((char)this.m_Team).ToString();
        }

        protected bool isMoveInsideBoard(Position i_PositionToCheck, CheckersGameObject[,] i_GameBoard)
        {
            int rowColLength = i_GameBoard.GetLength(1);

            return rowColLength > i_PositionToCheck.Row && rowColLength > i_PositionToCheck.Col && i_PositionToCheck.Row >= 0 && i_PositionToCheck.Col >= 0;
        }
    }
}
