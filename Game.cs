using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace Damka
{
    public class Game
    {
        private BoardForm m_Board = null;
        private GameSetting m_LoginForm;
        //private bool m_GameVsCp = false;
        private int m_PlayerOneScore;
        private int m_PlayerTwoScore;
        private ResoltForm m_ResoultForm;

        public Game()
        {
            Application.EnableVisualStyles();
        }

        private void M_Board_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ((sender as BoardForm).GameOver)
            {
                if ((sender as BoardForm).GetWiner() == Logics.eTurn.PlayerOne)
                {
                    m_PlayerOneScore++;
                }
                else if ((sender as BoardForm).GetWiner() == Logics.eTurn.PlayerTwo)
                {
                    m_PlayerTwoScore++;
                }


                m_ResoultForm = new ResoltForm((sender as BoardForm).GetWiner().ToString());
                m_ResoultForm.ShowDialog();

                if(m_ResoultForm.DialogResult == DialogResult.OK)
                {
                    m_Board = new BoardForm(m_LoginForm.boardSize(), m_LoginForm.PlayerOne, m_LoginForm.PlayerTwo, m_PlayerOneScore, m_PlayerTwoScore, !m_LoginForm.CheckBoxPlayerTwo.Checked);
                    m_Board.FormClosed += M_Board_FormClosed;
                    m_Board.ShowDialog();
                }
            }
        }

        public void Run()
        {
            m_LoginForm = new GameSetting();
            m_LoginForm.ShowDialog();

            if (m_LoginForm.DialogResult == DialogResult.OK)
            {
                
                m_Board = new BoardForm(m_LoginForm.boardSize(), m_LoginForm.PlayerOne, m_LoginForm.PlayerTwo, m_PlayerOneScore, m_PlayerTwoScore ,!m_LoginForm.CheckBoxPlayerTwo.Checked);
               
                m_Board.FormClosed += M_Board_FormClosed;
                m_Board.ShowDialog();

            }
        }//מריץ את המשחק
    }
}
