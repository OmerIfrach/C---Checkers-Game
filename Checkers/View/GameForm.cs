using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Checkers.Enums;
using Checkers.model;

namespace Checkers.View
{
    public class GameForm : Form
    {
        private const int k_SpaceSize = 4;
        private readonly int r_BoardDimensions;
        private readonly Size r_ButtonSize = new Size(45, 45);
        private readonly Label r_Player1Score = new Label();
        private readonly Label r_Player2Score = new Label();
        private readonly GameLogic r_GameLogic;
        private PawnButton m_SelectedPawn = null;

        public GameForm(int i_BoardDimensions, GameLogic i_CheckersGame)
        {
            this.r_BoardDimensions = i_BoardDimensions;
            this.r_GameLogic = i_CheckersGame;
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            int boardSize = 2;
            bool evenRow = true;
            bool evenColumn = true;

            initBoardSettings(boardSize);
            this.r_GameLogic.ReportGameError += this.showErrorDialog;
            this.r_GameLogic.GameEnded += this.gameEnded;
            Point buttonLocation = new Point(k_SpaceSize + 10, k_SpaceSize + (r_Player1Score.Height * 2));
            for (int i = 0; i < r_BoardDimensions; i++)
            {
                for (int j = 0; j < r_BoardDimensions; j++)
                {
                    bool buttonEnable = evenColumn != evenRow;
                    PawnButton pawnButton = new PawnButton(i, j, this.r_GameLogic.Board[i, j], buttonEnable);
                    pawnButton.Click += this.pawnButton_Click;
                    this.r_GameLogic.AddListenerToGameObject(i, j, pawnButton.OnChange);
                    pawnButton.Size = r_ButtonSize;
                    pawnButton.Location = buttonLocation;
                    buttonLocation.X += r_ButtonSize.Width;

                    this.Controls.Add(pawnButton);
                    evenColumn = !evenColumn;
                }

                buttonLocation.X = k_SpaceSize + 10;
                buttonLocation.Y += r_ButtonSize.Height;
                evenRow = !evenRow;
            }
        }

        private void initBoardSettings(int i_BoardSize)
        {
            this.Text = "Damka";
            this.BackColor = Color.White;
            i_BoardSize += (k_SpaceSize + this.r_ButtonSize.Height) * this.r_BoardDimensions;
            this.ClientSize = new Size(i_BoardSize, i_BoardSize + (r_Player1Score.Height * 2));
            this.r_Player1Score.Location = new Point((this.Width / 4) - (this.r_Player1Score.Width / 2), r_Player1Score.Height);
            this.r_Player1Score.Text = this.r_GameLogic.GetPlayerNameAndScore(1);
            this.r_Player1Score.Font = new Font(this.Font, this.Font.Style | FontStyle.Bold);
            this.Controls.Add(r_Player1Score);
            this.r_Player2Score.Location = new Point((this.Width / 2) + (this.Width / 4) - (this.r_Player2Score.Width / 2), r_Player2Score.Height);
            this.r_Player2Score.Text = this.r_GameLogic.GetPlayerNameAndScore(2);
            this.r_Player2Score.Font = new Font(this.Font, this.Font.Style | FontStyle.Bold);
            this.Controls.Add(r_Player2Score);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void pawnButton_Click(object sender, EventArgs e)
        {
            PawnButton pawnButton = sender as PawnButton;
            List<string> currentPlayerSymbols = this.r_GameLogic.GetCurrentPlayerSymbol();
            bool isCurrentSymbolMatchButton = false;
            if(pawnButton != null)
            {
                foreach(string symbol in currentPlayerSymbols)
                {
                    if(symbol == pawnButton.Text)
                    {
                        isCurrentSymbolMatchButton = true;
                    }
                }

                if (this.m_SelectedPawn == null && isCurrentSymbolMatchButton)
                {
                    this.m_SelectedPawn = pawnButton;
                    this.m_SelectedPawn.PawnSelectedToggle();
                }
                else if(this.m_SelectedPawn != null && this.m_SelectedPawn == pawnButton)
                {
                    this.m_SelectedPawn.PawnSelectedToggle();
                    this.m_SelectedPawn = null;
                }
                else if(this.m_SelectedPawn != null && pawnButton.Text == ((char)eTeam.Empty).ToString())
                {
                    this.r_GameLogic.MakeMove(
                        this.m_SelectedPawn.Row,
                        this.m_SelectedPawn.Col,
                        pawnButton.Row,
                        pawnButton.Col);
                    this.m_SelectedPawn.PawnSelectedToggle();
                    this.m_SelectedPawn = null;
                }
            }
        }

        private void gameEnded(string i_GameEndedMessage)
        {
            string messageToMessageBox = string.Format(
                "{0}{1}{2}",
                i_GameEndedMessage,
                Environment.NewLine,
                "Another Round?");
            DialogResult dialogResult = MessageBox.Show(messageToMessageBox, "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialogResult == DialogResult.Yes)
            {
                this.r_GameLogic.ResetGame();
                this.r_Player1Score.Text = this.r_GameLogic.GetPlayerNameAndScore(1);
                this.r_Player2Score.Text = this.r_GameLogic.GetPlayerNameAndScore(2);
            }
            else if(dialogResult == DialogResult.No)
            {
                this.Close();
            }
        }

        private void showErrorDialog(string i_ErrorMessage)
        {
            MessageBox.Show(i_ErrorMessage);
        }
    }
}
