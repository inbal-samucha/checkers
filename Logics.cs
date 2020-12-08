using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace Damka
{
    class Logics
    {

        public enum eTurn { PlayerOne = 1, PlayerTwo, Empty };//מצביע על תור השחקן
        private eTurn m_Turn;

        public eTurn Turn { set { m_Turn = value; } get { return m_Turn; } }
        bool m_BecomeKing = false;

        public Logics()
        {
            m_Turn = eTurn.PlayerOne;
        }

        public bool IsSingleMovePossible(BoardButton i_Destination, BoardButton i_Source)
        {
            bool check = true;

            if (i_Destination.MyTurn != eTurn.Empty || (i_Destination.ButtonStatus == BoardButton.eButtonStatus.Grey) || Math.Abs(i_Destination.BoardPlaceY - i_Source.BoardPlaceY) != 1)
            {
                check = false;
            }
            else
            {
                if (i_Source.Text == "X")
                {
                    if ((i_Destination.BoardPlaceX - i_Source.BoardPlaceX) != -1)
                    {
                        check = false;
                    }
                }

                else if (i_Source.Text == "O")
                {
                    if ((i_Destination.BoardPlaceX - i_Source.BoardPlaceX) != 1)
                    {
                        check = false;
                    }
                }

                else if (i_Source.Text == "K" || i_Source.Text == "U")
                {
                    if (Math.Abs(i_Destination.BoardPlaceX - i_Source.BoardPlaceX) != 1)
                    {
                        check = false;
                    }
                }
            }

            return check;
        }//בודק חוקיות של מהלך רגיל

        public bool IsEatingMovePossible(BoardButton i_Destination, BoardButton i_Source, BoardButton i_EatenSoldiar)
        {
            bool check = true;

            if (i_EatenSoldiar == null)
            {
                check = false;
            }

            else
            {
                //MessageBox.Show(string.Format("{0}, {1}, {2}, {6}\n {3},{4},{5},", Math.Abs(i_destination.BoardPlaceY - i_source.BoardPlaceY), i_destination.MyTurn.ToString(), i_eatenSoldiar.MyTurn.ToString(), i_source.ToString(), i_eatenSoldiar.ToString(), i_destination.ToString(), m_Turn.ToString()));

                if ((Math.Abs(i_Destination.BoardPlaceY - i_Source.BoardPlaceY) == 2) && i_Destination.MyTurn == eTurn.Empty && i_EatenSoldiar.MyTurn == getOpositeTurn(i_Source))
                {
                    //MessageBox.Show(string.Format(i_source.ToString() + i_eatenSoldiar.ToString() + i_destination.ToString() + " After"));

                    if (i_Source.Text == "O")
                    {
                        if ((i_Destination.BoardPlaceX - i_Source.BoardPlaceX) != 2)
                        {
                            check = false;
                        }
                    }
                    else if (i_Source.Text == "X")
                    {
                        if ((i_Destination.BoardPlaceX - i_Source.BoardPlaceX) != -2)
                        {
                            check = false;
                        }
                    }
                    else if (i_Source.Text == "K" || i_Source.Text == "U")
                    {
                        if (Math.Abs(i_Destination.BoardPlaceX - i_Source.BoardPlaceX) != 2)
                        {
                            check = false;
                        }
                    }
                }
                else
                {
                    check = false;
                }
            }

            return check;
        }//בודק חוקיות של ניסיון אכילה

        private eTurn getOpositeTurn(BoardButton i_Button)
        {
            eTurn opossiteTurn = new eTurn();

            if (i_Button.MyTurn == eTurn.PlayerOne)
            {
                opossiteTurn = eTurn.PlayerTwo;
            }
            else if (i_Button.MyTurn == eTurn.PlayerTwo)
            {
                opossiteTurn = eTurn.PlayerOne;
            }

            return opossiteTurn;
        }

        public void SwitchTurn()
        {
            
            if(m_Turn == eTurn.PlayerOne)
            { 
                m_Turn = eTurn.PlayerTwo;
            }
            else if(m_Turn == eTurn.PlayerTwo)
            {
                m_Turn = eTurn.PlayerOne;
            }
        }//מתודה שמחליפה תור בין שחקן לשחקן

        public bool BecomeKing(BoardButton i_Button, int i_Size)
        {
            m_BecomeKing = false;

            if (i_Button.Text == "X" && i_Button.BoardPlaceX == 0)
            {
                i_Button.Text = "K";
                i_Button.MyTurn = eTurn.PlayerOne;
                m_BecomeKing = true;
            }

            else if (i_Button.Text == "O" && i_Button.BoardPlaceX == i_Size-1)
            {
                i_Button.Text = "U";
                i_Button.MyTurn = eTurn.PlayerTwo;
                m_BecomeKing = true;
            }

            return m_BecomeKing;
        }//מתודה שהופכת חייל למלך
    }

}
