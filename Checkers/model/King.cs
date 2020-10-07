using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Enums;
using Checkers.Utils;

namespace Checkers.model
{
    public class King : CheckersGameObject
    {
        public King(eTeam i_Team)
            : base(i_Team)
        {
        }

        public override Position SuggestComputerPosition(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard)
        {
            List<Position> skipPositions = GetAllSkipMoves(i_CurrentPosition, i_GameBoard);
            Random rnd = new Random();
            if (skipPositions.Count > 0)
            {
                int randomIndex = rnd.Next(skipPositions.Count - 1);
                return skipPositions[randomIndex];
            }

            List<Position> availableMoves = GetAvailableMoves(i_CurrentPosition, i_GameBoard);
            if (availableMoves.Count > 0)
            {
                int randomIndex = rnd.Next(availableMoves.Count - 1);
                return availableMoves[randomIndex];
            }

            return null;
        }

        public override List<Position> GetAllSkipMoves(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard)
        {
            int[] toAddRow = { 1, -1 };
            int[] colValues = { 1, -1 };
            eTeam enemy = OpponentUtils.GetOpponent(this.Team);
            List<Position> skipPositions = new List<Position>();
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    int rowsToSearchEnemy = i_CurrentPosition.Row + toAddRow[j];
                    int colsToSearchEnemy = i_CurrentPosition.Col + colValues[i];
                    if (!isMoveInsideBoard(new Position(rowsToSearchEnemy, colsToSearchEnemy), i_GameBoard))
                    {
                        continue;
                    }

                    if (i_GameBoard[rowsToSearchEnemy, colsToSearchEnemy].Team == enemy)
                    {
                        int rowsToSearchEmpty = i_CurrentPosition.Row + (toAddRow[j] * 2);
                        int colsToSearchEmpty = i_CurrentPosition.Col + (colValues[i] * 2);
                        if (!isMoveInsideBoard(new Position(rowsToSearchEmpty, colsToSearchEmpty), i_GameBoard))
                        {
                            continue;
                        }

                        if (i_GameBoard[rowsToSearchEmpty, colsToSearchEmpty].Team == eTeam.Empty)
                        {
                            skipPositions.Add(new Position(rowsToSearchEmpty, colsToSearchEmpty));
                        }
                    }
                }
            }

            return skipPositions;
        }

        public override bool HasAvailableSkip(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard)
        {
            List<Position> skipPositions = GetAllSkipMoves(i_CurrentPosition, i_GameBoard);

            return skipPositions.Count > 0;
        }

        public override bool IsMoveLegal(Position i_CurrentPosition, Position i_NextPosition, CheckersGameObject[,] i_GameBoard)
        {
            bool isMoveLegal = false;
            if (Math.Abs(i_CurrentPosition.Row - i_NextPosition.Row) == 2 && Math.Abs(i_CurrentPosition.Col - i_NextPosition.Col) == 2)
            {
                if (i_GameBoard[i_NextPosition.Row, i_NextPosition.Col].Team == eTeam.Empty
                   && i_GameBoard[(i_CurrentPosition.Row + i_NextPosition.Row) / 2,
                       (i_CurrentPosition.Col + i_NextPosition.Col) / 2].Team
                   == OpponentUtils.GetOpponent(this.Team))
                {
                    isMoveLegal = true;
                }
            }
            else if (Math.Abs(i_CurrentPosition.Row - i_NextPosition.Row) == 1 && Math.Abs(i_CurrentPosition.Col - i_NextPosition.Col) == 1)
            {
                if (i_GameBoard[i_NextPosition.Row, i_NextPosition.Col].Team == eTeam.Empty)
                {
                    isMoveLegal = true;
                }
            }

            return isMoveLegal;
        }

        public override string ToString()
        {
            return this.Team == eTeam.Player1 ? "K" : "U";
        }

        public override List<Position> GetAvailableMoves(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard)
        {
            int[] toAddRow = { 1, -1 };
            int[] colValues = { 1, -1 };
            List<Position> availableMoves = new List<Position>();
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    int searchEmptyRow = i_CurrentPosition.Row + toAddRow[j];
                    int searchEmptyCol = i_CurrentPosition.Col + colValues[i];
                    if (!isMoveInsideBoard(new Position(searchEmptyRow, searchEmptyCol), i_GameBoard))
                    {
                        continue;
                    }

                    if (i_GameBoard[searchEmptyRow, searchEmptyCol].Team == eTeam.Empty)
                    {
                        availableMoves.Add(new Position(searchEmptyRow, searchEmptyCol));
                    }
                }
            }

            return availableMoves;
        }
    }
}
