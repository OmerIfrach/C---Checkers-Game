using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Enums;
using Checkers.Utils;

namespace Checkers.model
{
    public class GameBoard
    {
        private readonly CheckersGameObject[,] r_Board;
        public ActionBoardChanged[,] m_BoardDelegates;
        private Position m_HasMultipleSkipsOption;

        public GameBoard(int i_BoardSize)
        {
            this.r_Board = new CheckersGameObject[i_BoardSize, i_BoardSize];
            this.m_BoardDelegates = new ActionBoardChanged[i_BoardSize, i_BoardSize];
            m_HasMultipleSkipsOption = null;
            initBoard();
        }

        public CheckersGameObject[,] Board
        {
            get
            {
                return r_Board;
            }
        }

        public void ResetBoard()
        {
            initBoard();
            notifyBoardReset();
        }

        public bool MoveSoldierInBoard(Position i_Source, Position i_Destination, eTeam i_CurrentTeamMove)
        {
            bool hasMultipleSkips = false;
            CheckersGameObject currentGameObject = this.r_Board[i_Source.Row, i_Source.Col];
            CheckersGameObject desGameObject = this.r_Board[i_Destination.Row, i_Destination.Col];
            this.r_Board[i_Source.Row, i_Source.Col] = desGameObject;
            this.r_Board[i_Destination.Row, i_Destination.Col] = currentGameObject;
            this.m_BoardDelegates[i_Source.Row, i_Source.Col].TriggerChange(desGameObject.ToString());
            this.m_BoardDelegates[i_Destination.Row, i_Destination.Col].TriggerChange(currentGameObject.ToString());
            checkAndUpdateIfHaveNewKings();
            if (Math.Abs(i_Source.Row - i_Destination.Row) == 2 && Math.Abs(i_Source.Col - i_Destination.Col) == 2)
            {
                int eatenPawnRow = (i_Source.Row + i_Destination.Row) / 2;
                int eatenPawnCol = (i_Source.Col + i_Destination.Col) / 2;
                this.r_Board[eatenPawnRow, eatenPawnCol] = new EmptySlot(eTeam.Empty);
                this.m_BoardDelegates[eatenPawnRow, eatenPawnCol].TriggerChange(this.r_Board[eatenPawnRow, eatenPawnCol].ToString());
                if (this.r_Board[i_Destination.Row, i_Destination.Col].HasAvailableSkip(new Position(i_Destination.Row, i_Destination.Col), this.r_Board))
                {
                    this.m_HasMultipleSkipsOption = new Position(i_Destination.Row, i_Destination.Col);
                    hasMultipleSkips = true;
                }
                else
                {
                    this.m_HasMultipleSkipsOption = null;
                }
            }

            return hasMultipleSkips;
        }

        public bool CheckMoveIsValid(Position i_Source, Position i_Destination, eTeam i_CurrentTeamMove, ref string io_Error)
        {
            bool isMoveValid = false;

            if (checkMoveIsMadeOnCurrentPlayerGameObject(i_Source, i_CurrentTeamMove))
            {
                if (checkCurrentPlayerHasSkipOption(i_CurrentTeamMove))
                {
                    isMoveValid = checkIfCurrentMoveIsSkip(i_Source, i_Destination, i_CurrentTeamMove);
                    if (this.m_HasMultipleSkipsOption != null)
                    {
                        if (this.m_HasMultipleSkipsOption.Col != i_Source.Col
                           || this.m_HasMultipleSkipsOption.Row != i_Source.Row)
                        {
                            isMoveValid = false;
                            io_Error = "You have another skip option, please do it!";
                        }
                    }
                    else if(!isMoveValid)
                    {
                        io_Error = "You have a skip option, please do it!";
                    }
                }
                else
                {
                    isMoveValid = checkNextCurrentGameObjectMoveIsLegal(i_Source, i_Destination);
                    if(!isMoveValid)
                    {
                        io_Error = "Move is invalid, you can do a skip or go one step slant!";
                    }
                }
            }
            else
            {
                io_Error = "This is not your pawn, please make a move on yours!";
            }

            return isMoveValid;
        }

        public void RunComputerMove(eTeam i_CurrentTeamMove)
        {
            List<Position> teamPositions = getCurrentPlayerGameObjects(i_CurrentTeamMove);
            Position currentPosition = null;
            Position suggestedPosition = null;
            foreach (Position position in teamPositions)
            {
                bool hasSkipOption = r_Board[position.Row, position.Col].HasAvailableSkip(position, this.r_Board);
                if (hasSkipOption)
                {
                    suggestedPosition = r_Board[position.Row, position.Col].SuggestComputerPosition(position, this.r_Board);
                    currentPosition = position;
                    break;
                }
            }

            if (suggestedPosition == null)
            {
                foreach (Position position in teamPositions)
                {
                    List<Position> availablePositions = r_Board[position.Row, position.Col].GetAvailableMoves(position, this.r_Board);
                    if (availablePositions.Count > 0)
                    {
                        suggestedPosition = availablePositions[0];
                        currentPosition = position;
                        break;
                    }
                }
            }

            MoveSoldierInBoard(currentPosition, suggestedPosition, i_CurrentTeamMove);
            if(m_HasMultipleSkipsOption != null)
            {
                this.RunComputerMove(i_CurrentTeamMove);
            }
        }

        public eGameState CalcGameState(eTeam i_CurrentTeamMove)
        {
            eGameState gameStateResult = gameHasWinner(i_CurrentTeamMove);
            if (eGameState.GameIsInProgress == gameStateResult)
            {
                bool player1HasMoves = checkPlayerHasAvailableMove(eTeam.Player1);
                bool player2HasMoves = checkPlayerHasAvailableMove(eTeam.Player2);

                if (!player1HasMoves && !player2HasMoves)
                {
                    gameStateResult = eGameState.Tie;
                }
                else if (!player2HasMoves)
                {
                    gameStateResult = eGameState.Player1Won;
                }
                else if (!player1HasMoves)
                {
                    gameStateResult = eGameState.Player2Won;
                }
            }

            return gameStateResult;
        }

        public int CalcScore()
        {
            int score = 0;

            for (int x = 0; x < this.r_Board.GetLength(1); x++)
            {
                for (int y = 0; y < this.r_Board.GetLength(1); y++)
                {
                    if (this.r_Board[x, y].Team == eTeam.Player1)
                    {
                        if (this.r_Board[x, y].ToString() == "K")
                        {
                            score += 4;
                        }
                        else
                        {
                            score += 1;
                        }
                    }
                    else if (this.r_Board[x, y].Team == eTeam.Player2)
                    {
                        if (this.r_Board[x, y].ToString() == "U")
                        {
                            score -= 4;
                        }
                        else
                        {
                            score -= 1;
                        }
                    }
                }
            }

            return score;
        }

        private List<Position> getCurrentPlayerGameObjects(eTeam i_CurrentTeamMove)
        {
            List<Position> teamPositions = new List<Position>();

            for (int x = 0; x < this.r_Board.GetLength(1); x++)
            {
                for (int y = 0; y < this.r_Board.GetLength(1); y++)
                {
                    if (i_CurrentTeamMove == this.r_Board[x, y].Team)
                    {
                        teamPositions.Add(new Position(x, y));
                    }
                }
            }

            return teamPositions;
        }

        private void checkAndUpdateIfHaveNewKings()
        {
            for (int i = 0; i < r_Board.GetLength(1); i++)
            {
                if (r_Board[0, i].Team == eTeam.Player1)
                {
                    r_Board[0, i] = new King(r_Board[0, i].Team);
                    this.m_BoardDelegates[0, i].TriggerChange(r_Board[0, i].ToString());
                }

                if (r_Board[r_Board.GetLength(1) - 1, i].Team == eTeam.Player2)
                {
                    r_Board[r_Board.GetLength(1) - 1, i] = new King(r_Board[r_Board.GetLength(1) - 1, i].Team);
                    this.m_BoardDelegates[r_Board.GetLength(1) - 1, i].TriggerChange(r_Board[r_Board.GetLength(1) - 1, i].ToString());
                }
            }
        }

        private bool checkMoveIsMadeOnCurrentPlayerGameObject(
            Position i_CurrentPosition,
            eTeam i_CurrentTeamMove)
        {
            eTeam movedItemTeam = this.r_Board[i_CurrentPosition.Row, i_CurrentPosition.Col].Team;
            return i_CurrentTeamMove == movedItemTeam;
        }

        private bool checkCurrentPlayerHasSkipOption(eTeam i_CurrentTeamMove)
        {
            bool hasSkipOption = false;

            for (int x = 0; x < this.r_Board.GetLength(1); x++)
            {
                for (int y = 0; y < this.r_Board.GetLength(1); y++)
                {
                    if (this.r_Board[x, y].Team == i_CurrentTeamMove)
                    {
                        if (this.r_Board[x, y].HasAvailableSkip(new Position(x, y), this.r_Board))
                        {
                            hasSkipOption = true;
                            break;
                        }
                    }
                }

                if (hasSkipOption)
                {
                    break;
                }
            }

            return hasSkipOption;
        }

        private bool checkIfCurrentMoveIsSkip(Position i_Source, Position i_Destination, eTeam i_CurrentTeamMove)
        {
            bool isMoveLegal = r_Board[i_Source.Row, i_Source.Col].IsMoveLegal(
                new Position(i_Source.Row, i_Source.Col),
                new Position(i_Destination.Row, i_Destination.Col),
                this.r_Board);

            return r_Board[i_Destination.Row, i_Destination.Col].Team == eTeam.Empty && r_Board[(i_Source.Row + i_Destination.Row) / 2, (i_Source.Col + i_Destination.Col) / 2].Team == OpponentUtils.GetOpponent(i_CurrentTeamMove) && isMoveLegal;
        }

        private bool checkNextCurrentGameObjectMoveIsLegal(Position i_Source, Position i_Destination)
        {
            return r_Board[i_Source.Row, i_Source.Col].IsMoveLegal(i_Source, i_Destination, this.r_Board);
        }

        private void initBoard()
        {
            fillEmptySlots();
            initPlayer1Soldier();
            initPlayer2Soldier();
        }

        private void fillEmptySlots()
        {
            for (int x = 0; x < this.r_Board.GetLength(1); x++)
            {
                for (int y = 0; y < this.r_Board.GetLength(1); y++)
                {
                    this.r_Board[x, y] = new EmptySlot(eTeam.Empty);
                }
            }
        }

        private void initPlayer1Soldier()
        {
            bool toEvenSlot = true;
            for (int x = this.r_Board.GetLength(1) - 1; x >= (this.r_Board.GetLength(1) / 2) + 1; x--)
            {
                for (int y = 0; y < this.r_Board.GetLength(1); y++)
                {
                    if (toEvenSlot && y % 2 == 0)
                    {
                        this.r_Board[x, y] = new Soldier(eTeam.Player1);
                    }

                    if (!toEvenSlot && y % 2 == 1)
                    {
                        this.r_Board[x, y] = new Soldier(eTeam.Player1);
                    }
                }

                toEvenSlot = !toEvenSlot;
            }
        }

        private void initPlayer2Soldier()
        {
            bool toEvenSlot = false;
            for (int x = 0; x < (this.r_Board.GetLength(1) / 2) - 1; x++)
            {
                for (int y = 0; y < this.r_Board.GetLength(1); y++)
                {
                    if (toEvenSlot && y % 2 == 0)
                    {
                        this.r_Board[x, y] = new Soldier(eTeam.Player2);
                    }

                    if (!toEvenSlot && y % 2 == 1)
                    {
                        this.r_Board[x, y] = new Soldier(eTeam.Player2);
                    }
                }

                toEvenSlot = !toEvenSlot;
            }
        }

        private bool checkPlayerHasAvailableMove(eTeam i_CurrentTeamMove)
        {
            List<Position> teamPositions = getCurrentPlayerGameObjects(i_CurrentTeamMove);
            bool hasMove = false;

            foreach (Position position in teamPositions)
            {
                bool hasSkipOption = r_Board[position.Row, position.Col].HasAvailableSkip(position, this.r_Board);
                if (hasSkipOption)
                {
                    hasMove = true;
                    break;
                }
            }

            if (!hasMove)
            {
                foreach (Position position in teamPositions)
                {
                    List<Position> availablePositions = r_Board[position.Row, position.Col].GetAvailableMoves(position, this.r_Board);
                    if (availablePositions.Count > 0)
                    {
                        hasMove = true;
                        break;
                    }
                }
            }

            return hasMove;
        }

        private eGameState gameHasWinner(eTeam i_CurrentTeamMove)
        {
            eGameState gameStateResult = eGameState.GameIsInProgress;
            eTeam enemy = OpponentUtils.GetOpponent(i_CurrentTeamMove);
            int enenyCount = 0;
            for (int x = 0; x < this.r_Board.GetLength(1); x++)
            {
                for (int y = 0; y < this.r_Board.GetLength(1); y++)
                {
                    if (this.r_Board[x, y].Team == enemy)
                    {
                        enenyCount++;
                    }
                }
            }

            if (enenyCount == 0)
            {
                if (enemy == eTeam.Player1)
                {
                    gameStateResult = eGameState.Player2Won;
                }
                else
                {
                    gameStateResult = eGameState.Player1Won;
                }
            }

            return gameStateResult;
        }

        private void notifyBoardReset()
        {
            for(int x = 0; x < this.r_Board.GetLength(1); x++)
            {
                for(int y = 0; y < this.r_Board.GetLength(1); y++)
                {
                    this.m_BoardDelegates[x, y].TriggerChange(this.r_Board[x, y].ToString());
                }
            }
        }
    }
}
