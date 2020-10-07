using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Enums;
using Checkers.Utils;

namespace Checkers.model
{
    public class GameLogic
    {
        private readonly int r_BoardDimensions;
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;
        private readonly bool r_IsPlayer2Computer;
        private readonly GameBoard r_GameBoard;
        private int m_Player1Score = 0;
        private int m_Player2Score = 0;
        private eTeam m_PlayerTurn;
        private eGameState m_GameState;
        private bool m_NewGame;

        public event Action<string> ReportGameError;

        public event Action<string> GameEnded; 

        public GameLogic(int i_BoardDimensions, string i_Player1Name, string i_Player2Name, bool i_IsPlayer2Computer)
        {
            this.r_BoardDimensions = i_BoardDimensions;
            this.r_GameBoard = new GameBoard(i_BoardDimensions);
            this.r_Player1Name = i_Player1Name;
            this.r_Player2Name = i_Player2Name;
            this.m_PlayerTurn = eTeam.Player1;
            this.m_GameState = eGameState.GameIsInProgress;
            this.r_IsPlayer2Computer = i_IsPlayer2Computer;
            this.m_NewGame = true;
        }

        public string[,] Board
        {
            get
            {
                string[,] board = new string[this.r_BoardDimensions, this.r_BoardDimensions];
                for(int x = 0; x < r_BoardDimensions; x++)
                {
                    for(int y = 0; y < r_BoardDimensions; y++)
                    {
                        board[x, y] = this.r_GameBoard.Board[x, y].ToString();
                    }
                }

                return board;
            }
        }

        public void MakeMove(int i_SourceRow, int i_SourceCol, int i_DestinationRow, int i_DestinationCol)
        {
            Position sourcePosition = new Position(i_SourceRow, i_SourceCol);
            Position destinationPosition = new Position(i_DestinationRow, i_DestinationCol);
            string error = string.Empty;
            if(this.r_GameBoard.CheckMoveIsValid(sourcePosition, destinationPosition, this.m_PlayerTurn, ref error))
            {
                bool hasAvailableSkipMove = this.r_GameBoard.MoveSoldierInBoard(sourcePosition, destinationPosition, this.m_PlayerTurn);
                this.m_NewGame = false;
                getUpdatedGameState();
                if (!hasAvailableSkipMove && this.m_GameState == eGameState.GameIsInProgress && this.m_NewGame == false)
                {
                    this.m_PlayerTurn = OpponentUtils.GetOpponent(this.m_PlayerTurn);
                    if (this.r_IsPlayer2Computer && this.m_PlayerTurn == eTeam.Player2)
                    {
                        this.r_GameBoard.RunComputerMove(this.m_PlayerTurn);
                        this.m_PlayerTurn = OpponentUtils.GetOpponent(this.m_PlayerTurn);
                        getUpdatedGameState();
                    }
                }
            }
            else
            {
                if(this.ReportGameError != null)
                {
                    this.ReportGameError.Invoke(error);
                }
            }
        }

        public void AddListenerToGameObject(int i_Row, int i_Col, Action<string> listener)
        {
            this.r_GameBoard.m_BoardDelegates[i_Row, i_Col] = new ActionBoardChanged(listener);
        }

        public void ResetGame()
        {
            this.calcScore();
            this.r_GameBoard.ResetBoard();
            this.m_PlayerTurn = eTeam.Player1;
            this.m_GameState = eGameState.GameIsInProgress;
            this.m_NewGame = true;
        }

        public List<string> GetCurrentPlayerSymbol()
        {
            List<string> symbols = new List<string>();
            string basicSymbol = ((char)this.m_PlayerTurn).ToString();
            symbols.Add(basicSymbol);
            symbols.Add(OpponentUtils.GetSecondTeamSymbol(basicSymbol));
            return symbols;
        }

        public string GetPlayerNameAndScore(int i_PlayerNumber)
        {
            string playerAndScoreStr = string.Empty;
            if(i_PlayerNumber == 1)
            {
                playerAndScoreStr = string.Format("{0}: {1}", this.r_Player1Name, this.m_Player1Score);
            }
            else if(i_PlayerNumber == 2)
            {
                playerAndScoreStr = string.Format("{0}: {1}", this.r_Player2Name, this.m_Player2Score);
            }

            return playerAndScoreStr;
        }

        private void getUpdatedGameState()
        {
            this.m_GameState = this.r_GameBoard.CalcGameState(this.m_PlayerTurn);
            if(this.m_GameState != eGameState.GameIsInProgress)
            {
                string gameResultMsg = string.Empty;
                if (this.m_GameState == eGameState.Player1Won)
                {
                    gameResultMsg = string.Format("{0} Won!", this.r_Player1Name);
                }
                else if(this.m_GameState == eGameState.Player2Won)
                {
                    gameResultMsg = string.Format("{0} Won!", this.r_Player2Name);
                }
                else
                {
                    gameResultMsg = "Tie";
                }

                if(this.GameEnded != null)
                {
                    this.GameEnded.Invoke(gameResultMsg);
                }
            }
        }

        private void calcScore()
        {
            int score = this.r_GameBoard.CalcScore();

            if (score > 0)
            {
                this.m_Player1Score += score;
            }
            else if (score < 0)
            {
                this.m_Player2Score += score * -1;
            }
        }
    }
}
