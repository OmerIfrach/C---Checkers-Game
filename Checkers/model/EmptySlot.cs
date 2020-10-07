using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Enums;

namespace Checkers.model
{
    internal class EmptySlot : CheckersGameObject
    {
        public EmptySlot(eTeam i_Team)
            : base(i_Team)
        {
        }

        public override bool HasAvailableSkip(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard)
        {
            return false;
        }

        public override List<Position> GetAllSkipMoves(Position i_NextPosition, CheckersGameObject[,] i_GameBoard)
        {
            return new List<Position>();
        }

        public override bool IsMoveLegal(Position i_CurrentPosition, Position i_NextPosition, CheckersGameObject[,] i_GameBoard)
        {
            return false;
        }

        public override Position SuggestComputerPosition(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard)
        {
            return null;
        }

        public override List<Position> GetAvailableMoves(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard)
        {
            return new List<Position>();
        }
    }
}
