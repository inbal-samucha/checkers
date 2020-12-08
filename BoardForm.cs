using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Damka
{
    class BoardForm : Form
    {
        private Label m_LabelFirsePlayer = new Label();
        private Label m_LabelSecondPlayer = new Label();
        private Label m_LabelWhoTurn = new Label();
        private Logics m_GameRules = new Logics();

        private int m_BoardSize;
        private bool m_EatInARow = false;

        private struct listMove
        {
            BoardButton button;

            public BoardButton Button { get { return button; } set { button = value; } }
        }

        private readonly List<listMove> r_VaildEatingnMove = new List<listMove>();
        private readonly List<listMove> r_Vaild = new List<listMove>();

        private System.ComponentModel.CancelEventArgs e;
        private bool m_GameOver ;

        private int m_PlayerOneScore = 0;
        private int m_PlayerTwoScore = 0;
        public int PlayerOneScore { get { return m_PlayerOneScore; } set { m_PlayerOneScore = value; } }
        public int PlayerTwoScore { get { return m_PlayerTwoScore; } set { m_PlayerTwoScore = value; } }

        private string m_PlayerOneName;
        private string m_PlayerTwoName;
        
        private bool m_IsComputerMove = false;

        public BoardForm(int i_Size, string i_FirstPlayer, string i_SecondPlayer, int i_PlayerOneScore, int i_PlayerTwoScore ,bool i_VsCp)
        {
            m_GameOver = false;

            m_BoardSize = i_Size;

            m_PlayerOneName = i_FirstPlayer;
            m_PlayerTwoName = i_SecondPlayer;

            this.Text = "Checkers";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Gray;
            this.ClientSize = new Size(i_Size * 50, i_Size * 55);
            this.m_GameRules.Turn = Logics.eTurn.PlayerOne;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            setBoardSize();
            m_IsComputerMove = i_VsCp;

            this.m_LabelFirsePlayer.Text = string.Format(i_FirstPlayer + ": {0}", i_PlayerOneScore);
            this.m_LabelFirsePlayer.Location = new Point(i_Size * 10, 5);

            this.m_LabelSecondPlayer.Text = string.Format(i_SecondPlayer + ": {0}", i_PlayerTwoScore);
            this.m_LabelSecondPlayer.Location = new Point(m_LabelFirsePlayer.Location.X + 100, m_LabelFirsePlayer.Location.Y);

            this.m_LabelWhoTurn.Text = string.Format("{0} Turn's ", m_PlayerOneName);
            this.m_LabelWhoTurn.Location = new Point(m_LabelWhoTurn.Location.X + 20, this.ClientSize.Height - 30);

            this.Controls.Add(m_LabelFirsePlayer);
            this.Controls.Add(m_LabelSecondPlayer);
            this.Controls.Add(m_LabelWhoTurn);
        }

        private void setBoardSize()
        {
            for(int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    BoardButton boardButton = new BoardButton();
                    boardButton.Size = new Size(40, 40);
                    boardButton.Location = new Point(boardButton.Width * (j+1) , (i+1)  * (boardButton.Height));

                    if ((j + i) % 2 == 0)
                    {
                        boardButton.BackColor = Color.White;
                        boardButton.ButtonStatus = BoardButton.eButtonStatus.White;

                        if (i < (m_BoardSize / 2) - 1)
                        {
                            boardButton.Text = "O";
                            boardButton.MyTurn = Logics.eTurn.PlayerTwo;
                            m_PlayerTwoScore++;
                        }
                        else if (i > (m_BoardSize / 2))
                        {
                            boardButton.Text = "X";
                            boardButton.MyTurn = Logics.eTurn.PlayerOne;
                            m_PlayerOneScore++;
                        }
                        else
                        {
                            boardButton.Text = "";
                            boardButton.MyTurn = Logics.eTurn.Empty;
                        }
                    }
                    else
                    {
                        boardButton.BackColor = Color.Black;
                        boardButton.ButtonStatus = BoardButton.eButtonStatus.Grey;
                        boardButton.MyTurn = Logics.eTurn.Empty;
                    }

                    boardButton.BoardPlaceX = i;
                    boardButton.BoardPlaceY = j;


                    boardButton.Click += BoardButton_Click;


                    this.Controls.Add(boardButton);
                }
            }

            refreshMoves();
        }


        private void BoardButton_Click(object sender, EventArgs e)
        {
            BoardButton pointerToButton = (sender as BoardButton);
            
            if (isThereAnotherBlue() == true && m_EatInARow == false)
            {
                BoardButton pressedButton = getPressedButton();

                if (pressedButton.CheckEatingMove(pointerToButton))
                {
                    makeEatingMove(pointerToButton, pressedButton, getEatenButton(pointerToButton, pressedButton));
                }
                else if (pressedButton.CheckSingleMove(pointerToButton))
                {
                    makeSingleMove(pointerToButton, getPressedButton());
                }
                else if (pointerToButton.ButtonStatus == BoardButton.eButtonStatus.Blue)
                {
                    pointerToButton.BackColor = Color.White;
                    pointerToButton.ButtonStatus = BoardButton.eButtonStatus.White;
                }
                else if (pointerToButton.MyTurn != Logics.eTurn.Empty && (pointerToButton.ButtonStatus == BoardButton.eButtonStatus.White) && pointerToButton.MyTurn == m_GameRules.Turn)
                {
                    cleanOtherBlue(pointerToButton);
                    pointerToButton.BackColor = Color.Blue;
                    pointerToButton.ButtonStatus = BoardButton.eButtonStatus.Blue;

                }

                if (pointerToButton.MyTurn == Logics.eTurn.PlayerOne)
                {
                    m_LabelWhoTurn.Text = string.Format("{0} Turn's ", m_PlayerTwoName);
                }
                else
                {
                    m_LabelWhoTurn.Text = string.Format("{0} Turn's ", m_PlayerOneName);
                }
            }
            else if (m_EatInARow)
            {
                BoardButton pressedButton = getPressedButton();

                if (pressedButton.CheckEatingMove(pointerToButton))
                {
                    makeEatingMove(pointerToButton, pressedButton, getEatenButton(pointerToButton, pressedButton));
                }
            }
            else
            {
                if (pointerToButton.MyTurn == m_GameRules.Turn)
                {
                    if (pointerToButton.ButtonStatus == BoardButton.eButtonStatus.White && pointerToButton.MyTurn != Logics.eTurn.Empty)
                    {
                        pointerToButton.BackColor = Color.Blue;
                        pointerToButton.ButtonStatus = BoardButton.eButtonStatus.Blue;
                    }
                }
            }

            
        }

        private void makeEatingMove(BoardButton i_Destination, BoardButton i_Source, BoardButton i_Eaten)
        {
            i_Destination.Text = i_Source.Text;
            i_Destination.MyTurn = m_GameRules.Turn;


            i_Source.Text = "";
            i_Source.BackColor = Color.White;
            i_Source.ButtonStatus = BoardButton.eButtonStatus.White;
            i_Source.MyTurn = Logics.eTurn.Empty;


            i_Eaten.Text = "";
            i_Eaten.BackColor = Color.White;
            i_Eaten.MyTurn = Logics.eTurn.Empty;

            if (m_GameRules.BecomeKing(i_Destination, m_BoardSize)) ;

            updateScore();

            refreshMoves();

            if (isOtherEatPossible(i_Destination))
            {
                i_Destination.BackColor = Color.Blue;
                i_Destination.ButtonStatus = BoardButton.eButtonStatus.Blue;
                m_EatInARow = true;
            }
            else
            {
                m_EatInARow = false;
                m_GameRules.SwitchTurn();
            }

            this.m_LabelWhoTurn.Text = string.Format("'{0}' Turn's now.", m_GameRules.Turn.ToString());

            if (m_IsComputerMove == true && m_GameRules.Turn == Logics.eTurn.PlayerTwo)
            {
                ComputerMove();
                refreshMoves();
            }

            
        }//מבצע צעד אכילה
        
        
        

        private void makeSingleMove(BoardButton i_Destination, BoardButton i_Source)
        {
            i_Destination.Text = i_Source.Text;
            i_Destination.MyTurn = m_GameRules.Turn;

            i_Source.Text = "";
            i_Source.ButtonStatus = BoardButton.eButtonStatus.White;
            i_Source.BackColor = Color.White;
            i_Source.MyTurn = Logics.eTurn.Empty;

            m_GameRules.BecomeKing(i_Destination, m_BoardSize);
            refreshMoves();
            m_GameRules.SwitchTurn();

            this.m_LabelWhoTurn.Text = string.Format("'{0}' Turn's now.", m_GameRules.Turn.ToString());


            if (m_IsComputerMove == true && m_GameRules.Turn == Logics.eTurn.PlayerTwo)
            {
                ComputerMove();
                refreshMoves();
            }
        }//מבצע צעד רגיל

        private void cleanOtherBlue(BoardButton i_Button)
        {
            BoardButton button = null;

            foreach (object obj in this.Controls)
            {
                if (obj is BoardButton)
                {
                    button = (obj as BoardButton);
                    if (button != i_Button && button.ButtonStatus == BoardButton.eButtonStatus.Blue)
                    {
                        button.ButtonStatus = BoardButton.eButtonStatus.White;
                        button.BackColor = Color.White;
                    }
                }
            }
        }//מאפס את הלוח מכפתוים לחוצים

        private bool isThereAnotherBlue()
        {
            bool anotherBlue = false;
            BoardButton button = null;

            foreach (object obj in this.Controls)
            {
                if (obj is BoardButton)
                {
                    button = (obj as BoardButton);
                    if (button.ButtonStatus == BoardButton.eButtonStatus.Blue)
                    {
                        anotherBlue = true;
                        break;
                    }
                }
            }

            return anotherBlue;
        }//בודק האם קיימים כפתורים לחוצים

        private BoardButton getPressedButton()
        {
            BoardButton button = null;
            BoardButton pointerToButton = null;

            foreach (object obj in this.Controls)
            {
                if (obj is BoardButton)
                {
                    button = (obj as BoardButton);
                    if (button.ButtonStatus == BoardButton.eButtonStatus.Blue)
                    {
                        pointerToButton = button;
                    }
                }
            }

            return pointerToButton;
        }//מחזיר את הריבוע הלחוץ

        private BoardButton getEatenButton(BoardButton i_Destination, BoardButton i_Source)
        {
            BoardButton button = null;
            BoardButton pointerToButton = null;

            if (i_Source.Text == "O")
            {
                foreach (object obj in this.Controls)
                {
                    if (obj is BoardButton)
                    {
                        button = (obj as BoardButton);
                        if (i_Destination.BoardPlaceY > i_Source.BoardPlaceY)
                        {
                            if ((button.BoardPlaceY == i_Source.BoardPlaceY + 1 && button.BoardPlaceY == i_Destination.BoardPlaceY - 1) && (button.BoardPlaceX == i_Source.BoardPlaceX + 1 && button.BoardPlaceX == i_Destination.BoardPlaceX - 1))
                            {
                                pointerToButton = button;
                            }
                        }
                        else if (i_Destination.BoardPlaceY < i_Source.BoardPlaceY)
                        {
                            if ((button.BoardPlaceY == i_Source.BoardPlaceY - 1 && button.BoardPlaceY == i_Destination.BoardPlaceY + 1) && (button.BoardPlaceX == i_Source.BoardPlaceX + 1 && button.BoardPlaceX == i_Destination.BoardPlaceX - 1))
                            {
                                pointerToButton = button;
                            }
                        }
                    }
                }
            }
            else if (i_Source.Text == "X")
            {
                foreach (object obj in this.Controls)
                {
                    if (obj is BoardButton)
                    {
                        button = (obj as BoardButton);
                        if (i_Destination.BoardPlaceY > i_Source.BoardPlaceY)
                        {
                            if ((button.BoardPlaceY == i_Source.BoardPlaceY + 1 && button.BoardPlaceY == i_Destination.BoardPlaceY - 1) && (button.BoardPlaceX == i_Source.BoardPlaceX - 1 && button.BoardPlaceX == i_Destination.BoardPlaceX + 1))
                            {
                                pointerToButton = button;
                            }
                        }
                        else if (i_Destination.BoardPlaceY < i_Source.BoardPlaceY)
                        {
                            if ((button.BoardPlaceY == i_Source.BoardPlaceY - 1 && button.BoardPlaceY == i_Destination.BoardPlaceY + 1) && (button.BoardPlaceX == i_Source.BoardPlaceX - 1 && button.BoardPlaceX == i_Destination.BoardPlaceX + 1))
                            {
                                pointerToButton = button;
                            }
                        }
                    }
                }
            }
            else if (i_Source.Text == "K" || i_Source.Text == "U")
            {
                foreach (object obj in this.Controls)
                {
                    if (obj is BoardButton)
                    {
                        button = (obj as BoardButton);
                        if (i_Destination.BoardPlaceX > i_Source.BoardPlaceX)
                        {
                            if (i_Destination.BoardPlaceY > i_Source.BoardPlaceY)
                            {
                                if ((button.BoardPlaceY == i_Source.BoardPlaceY + 1 && button.BoardPlaceY == i_Destination.BoardPlaceY - 1) && (button.BoardPlaceX == i_Source.BoardPlaceX + 1 && button.BoardPlaceX == i_Destination.BoardPlaceX - 1))
                                {
                                    pointerToButton = button;
                                }
                            }
                            else if (i_Destination.BoardPlaceY < i_Source.BoardPlaceY)
                            {
                                if ((button.BoardPlaceY == i_Source.BoardPlaceY - 1 && button.BoardPlaceY == i_Destination.BoardPlaceY + 1) && (button.BoardPlaceX == i_Source.BoardPlaceX + 1 && button.BoardPlaceX == i_Destination.BoardPlaceX - 1))
                                {
                                    pointerToButton = button;
                                }
                            }
                        }
                        else
                        {
                            if (i_Destination.BoardPlaceY > i_Source.BoardPlaceY)
                            {
                                if ((button.BoardPlaceY == i_Source.BoardPlaceY + 1 && button.BoardPlaceY == i_Destination.BoardPlaceY - 1) && (button.BoardPlaceX == i_Source.BoardPlaceX - 1 && button.BoardPlaceX == i_Destination.BoardPlaceX + 1))
                                {
                                    pointerToButton = button;
                                }
                            }
                            else if (i_Destination.BoardPlaceY < i_Source.BoardPlaceY)
                            {
                                if ((button.BoardPlaceY == i_Source.BoardPlaceY - 1 && button.BoardPlaceY == i_Destination.BoardPlaceY + 1) && (button.BoardPlaceX == i_Source.BoardPlaceX - 1 && button.BoardPlaceX == i_Destination.BoardPlaceX + 1))
                                {
                                    pointerToButton = button;
                                }
                            }
                        }
                    }
                }
            }

            return pointerToButton;
        }//מחזיר את הריבוע שפוטנציאל לאכילה

        private void setOptionalMove(BoardButton i_Button)
        {
            BoardButton pointerToButon = null;

            foreach (object obj in this.Controls)
            {
                if (obj is BoardButton)
                {
                    pointerToButon = obj as BoardButton;

                    if (pointerToButon.MyTurn == Logics.eTurn.Empty && pointerToButon.ButtonStatus != BoardButton.eButtonStatus.Grey)
                    {
                        i_Button.CheckAllOptions(pointerToButon, getEatenButton(pointerToButon, i_Button));
                    }
                }
            }
        }//מאתחל את כל אופציות המהלכים

        private void refreshMoves()
        {
            foreach (object button in this.Controls)
            {
                if (button is BoardButton)
                {
                        (button as BoardButton).CleanOptions();
                        setOptionalMove((button as BoardButton));
                }
            }
        }//מחשב מחדש את כל אופציות המהלכים

        private bool isOtherEatPossible(BoardButton i_Button)
        {
            bool check = false;

            if (i_Button.EatingPossibilities.Count > 0)
            {
                check = true;
            }

            return check;
        }//בודק אופציות אכילה ברצף

        public void checkResulet()//בודק אם המשחק נגמר
        {
            if (m_PlayerOneScore == 0 || m_PlayerTwoScore == 0)
            {
                m_GameOver = true;
                MessageBox.Show("finish!");
                this.OnClosed(e);
            }
        }

        public Logics.eTurn GetWiner()
        {
            Logics.eTurn winner = new Logics.eTurn();

            if (m_PlayerOneScore == 0)
            {
                winner = Logics.eTurn.PlayerTwo;
            }
            else if (m_PlayerTwoScore == 0)
            {
                winner = Logics.eTurn.PlayerOne;
            }

            return winner;
        }

        public bool GameOver { get { return m_GameOver; } }

        private void updateScore()
        {
            if (m_GameRules.Turn == Logics.eTurn.PlayerOne)
            {
                m_PlayerTwoScore--;
            }
            if (m_GameRules.Turn == Logics.eTurn.PlayerTwo)
            { 
                m_PlayerOneScore--;
            }
            checkResulet();
        }//מעדכן את התןצאה

        protected override void OnClosed(EventArgs e)
        {
            this.Hide();
            base.OnClosed(e);
            this.Close();
        }


        public void ComputerMove()
        {
            listMove move = new listMove();
            Random rnd = new Random();

            int randomIndex;

            BoardButton button = null;
            BoardButton pointerToButton = null;

            foreach (object obj in this.Controls)
            {
                if (obj is BoardButton)
                {
                    button = (obj as BoardButton);
                    if (button.MyTurn == Logics.eTurn.PlayerTwo)
                    {
                        move.Button = button;

                        if (move.Button.EatingPossibilities.Count > 0)
                        {
                            r_VaildEatingnMove.Add(move);
                        }
                        else if (move.Button.OptionalMoves.Count > 0)
                        {
                            r_Vaild.Add(move);
                        }
                    }
                }
            }
            if (m_EatInARow)
            {
                button = getPressedButton();
                if (button.EatingPossibilities.Count == 1)
                {
                    pointerToButton = button.EatingPossibilities[0];
                }
                else if (button.EatingPossibilities.Count > 1)
                {
                    randomIndex = rnd.Next(button.EatingPossibilities.Count - 1);
                    pointerToButton = button.EatingPossibilities[randomIndex];
                }

                pointerToButton.PerformClick();
                r_VaildEatingnMove.Clear();
            }

            else if (r_VaildEatingnMove.Count > 0)
            {
                randomIndex = rnd.Next(r_VaildEatingnMove.Count - 1);
                button = r_VaildEatingnMove[randomIndex].Button;
                button.PerformClick();
                if (button.EatingPossibilities.Count == 1)
                {
                    pointerToButton = button.EatingPossibilities[0];
                }
                else if (button.EatingPossibilities.Count > 1)
                {
                    randomIndex = rnd.Next(button.EatingPossibilities.Count - 1);
                    pointerToButton = button.EatingPossibilities[randomIndex];
                }

                pointerToButton.PerformClick();
                r_VaildEatingnMove.Clear();
            }

            else if (r_Vaild.Count > 0 )
            {
                randomIndex = rnd.Next(r_Vaild.Count - 1);
                button = r_Vaild[randomIndex].Button;
                button.PerformClick();

                if (button.EatingPossibilities.Count > 0)
                {
                    randomIndex = rnd.Next(button.EatingPossibilities.Count - 1);
                    pointerToButton = button.EatingPossibilities[randomIndex];
                }
                else if (button.OptionalMoves.Count > 0)
                {
                    randomIndex = rnd.Next(button.OptionalMoves.Count - 1);
                    pointerToButton = button.OptionalMoves[randomIndex];
                }

                pointerToButton.PerformClick();
                r_Vaild.Clear();
            }
            else
            {
                this.OnClosed(e);
            }
        }//מהלכי מחשב
    }
}
