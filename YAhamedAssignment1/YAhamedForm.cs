using System;
using System.Windows.Forms;

namespace YAhamedAssignment1
{
    public partial class YAhamedForm : Form
    {
        private char currentPlayer = 'X';
        private char[,] board = new char[3, 3];
        private PictureBox[,] pictureBoxes = new PictureBox[3, 3];
        private const char Empty = ' ';

        public YAhamedForm()
        {
            InitializeComponent();
            InitializePictureBoxes();
            InitializeGame();
        }

        private void InitializePictureBoxes()
        {
            pictureBoxes[0, 0] = pictureBox00;
            pictureBoxes[0, 1] = pictureBox01;
            pictureBoxes[0, 2] = pictureBox02;
            pictureBoxes[1, 0] = pictureBox10;
            pictureBoxes[1, 1] = pictureBox11;
            pictureBoxes[1, 2] = pictureBox12;
            pictureBoxes[2, 0] = pictureBox20;
            pictureBoxes[2, 1] = pictureBox21;
            pictureBoxes[2, 2] = pictureBox22;

            foreach (PictureBox pictureBox in pictureBoxes)
            {
                pictureBox.Click += PictureBox_Click;
            }

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InitializeGame()
        {
            currentPlayer = 'X';

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = Empty;
                }
            }

            foreach (PictureBox pictureBox in pictureBoxes)
            {
                pictureBox.Image = Properties.Resources.Bg;
                pictureBox.Enabled = true;
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            int row = int.Parse(pictureBox.Name.Substring(10, 1));
            int col = int.Parse(pictureBox.Name.Substring(11, 1));

            if (board[row, col] == Empty)
            {
                board[row, col] = currentPlayer;
                pictureBox.Image = (currentPlayer == 'X') ? Properties.Resources.Cross : Properties.Resources.Ring;
                pictureBox.Enabled = false;

                if (CheckForWin())
                {
                    MessageBox.Show(currentPlayer + " wins!");
                    InitializeGame();
                }
                else if (IsBoardFull())
                {
                    MessageBox.Show("Tie!");
                    InitializeGame();
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }
        }

        private bool CheckForWin()
        {
            for (int idx = 0; idx < 3; idx++)
            {
                // Check rows
                if (board[idx, 0] == currentPlayer && board[idx, 1] == currentPlayer && board[idx, 2] == currentPlayer)
                    return true;
                // Check columns
                if (board[0, idx] == currentPlayer && board[1, idx] == currentPlayer && board[2, idx] == currentPlayer)
                    return true;
            }

            // Check diagonals for a win
            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
                return true;
            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (char cell in board)
            {
                if (cell == Empty)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
