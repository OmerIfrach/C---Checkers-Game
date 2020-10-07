using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Enums;
using Checkers.Utils;

namespace Checkers.model
{
    public class Soldier : CheckersGameObject
    {
        public Soldier(eTeam i_Team)
            : base(i_Team)
        {
        }

        public override bool HasAvailableSkip(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard)
        {
            List<Position> skipPositions = GetAllSkipMoves(i_CurrentPosition, i_GameBoard);

            return skipPositions.Count > 0;
        }

        public override List<Position> GetAllSkipMoves(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard)
        {
            int toAddRow = i_GameBoard[i_CurrentPosition.Row, i_CurrentPosition.Col].Team == eTeam.Player2 ? 1 : -1;
            int[] colValues = { 1, -1 };
            eTeam enemy = OpponentUtils.GetOpponent(this.Team);
            List<Position> skipPositions = new List<Position>();

            for (int i = 0; i < 2; i++)
            {
                int rowsToSearchEnemy = i_CurrentPosition.Row + toAddRow;
                int colsToSearchEnemy = i_CurrentPosition.Col + colValues[i];
                if (!isMoveInsideBoard(new Position(rowsToSearchEnemy, colsToSearchEnemy), i_GameBoard))
                {
                    continue;
                }

                if (i_GameBoard[rowsToSearchEnemy, colsToSearchEnemy].Team == enemy)
                {
                    int rowsToSearchEmpty = i_CurrentPosition.Row + (toAddRow * 2);
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

            return skipPositions;
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

        public override List<Position> GetAvailableMoves(Position i_CurrentPosition, CheckersGameObject[,] i_GameBoard)
        {
            int toAddRow = i_GameBoard[i_CurrentPosition.Row, i_CurrentPosition.Col].Team == eTeam.Player2 ? 1 : -1;
            int[] colValues = { 1, -1 };
            List<Position> availableMoves = new List<Position>();

            for (int i = 0; i < 2; i++)
            {
                int searchEmptyRow = i_CurrentPosition.Row + toAddRow;
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

            return availableMoves;
        }

        public override bool IsMoveLegal(Position i_CurrentPosition, Position i_NextPosition, CheckersGameObject[,] i_GameBoard)
        {
            bool isNextMoveEmpty = i_GameBoard[i_NextPosition.Row, i_NextPosition.Col].Team == eTeam.Empty;

            if (!isMoveInsideBoard(new Position(i_NextPosition.Row, i_NextPosition.Col), i_GameBoard) || !isNextMoveEmpty)
            {
                return false;
            }

            int toAddRow = i_GameBoard[i_CurrentPosition.Row, i_CurrentPosition.Col].Team == eTeam.Player2 ? 1 : -1;
            bool isDistanceIsTwo = isDistanceIs(i_CurrentPosition, i_NextPosition, 2);
            if (isDistanceIsTwo)
            {
                if ((i_CurrentPosition.Row + (toAddRow * 2)) == i_NextPosition.Row && ((i_CurrentPosition.Col + 2) == i_NextPosition.Col || (i_CurrentPosition.Col - 2) == i_NextPosition.Col))
                {
                    if ((i_CurrentPosition.Row + i_NextPosition.Row) % 2 == 1 || (i_CurrentPosition.Col + i_NextPosition.Col) % 2 == 2)
                    {
                        return false;
                    }

                    int toCheckRowEnemy = (i_CurrentPosition.Row + i_NextPosition.Row) / 2;
                    int toCheckColEnemy = (i_CurrentPosition.Col + i_NextPosition.Col) / 2;
                    if (i_GameBoard[toCheckRowEnemy, toCheckColEnemy].Team != this.Team && i_GameBoard[toCheckRowEnemy, toCheckColEnemy].Team != eTeam.Empty)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

            bool isDistanceIsOne = isDistanceIs(i_CurrentPosition, i_NextPosition, 1);
            if (isDistanceIsOne && i_CurrentPosition.Row + toAddRow == i_NextPosition.Row)
            {
                return true;
            }

            return false;
        }

        private bool isDistanceIs(Position i_CurrentPosition, Position i_NextPosition, int i_Distance)
        {
            bool flag = Math.Abs(i_CurrentPosition.Row - i_NextPosition.Row) == i_Distance && Math.Abs(i_CurrentPosition.Col - i_NextPosition.Col) == i_Distance;

            return flag;
        }
    }
}
