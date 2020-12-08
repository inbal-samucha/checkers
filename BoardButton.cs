using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Damka
{
    class BoardButton : Button
    {
        private int m_BoardPlaceX;
        private int m_BoardPlaceY;

        private Logics m_Logics = new Logics();

        private readonly List<BoardButton> m_OptionalMoves = new List<BoardButton>();
        private readonly List<BoardButton> m_EatingPossibilities = new List<BoardButton>();

        public List<BoardButton> EatingPossibilities { get { return m_EatingPossibilities; } }
        public List<BoardButton> OptionalMoves { get { return m_OptionalMoves; } }

        public enum eButtonStatus { White=1, Grey, Blue };

        private eButtonStatus m_ButtonStatus;

        private Logics.eTurn m_MyTurn;

        public Logics.eTurn MyTurn { set { m_MyTurn = value; } get { return m_MyTurn; } }

        public eButtonStatus ButtonStatus { set { m_ButtonStatus = value; } get { return m_ButtonStatus; } }

        public int BoardPlaceX { set { m_BoardPlaceX = value; } get { return m_BoardPlaceX; } }

        public int BoardPlaceY { set { m_BoardPlaceY = value; } get { return m_BoardPlaceY; } }

        public void CheckAllOptions(BoardButton i_destination,BoardButton i_eaten)
        {

            if (m_Logics.IsSingleMovePossible(i_destination, this))
            {
                m_OptionalMoves.Add(i_destination);
            }
            if (m_Logics.IsEatingMovePossible(i_destination, this, i_eaten))
            {
                m_EatingPossibilities.Add(i_destination);
            }
        }

        public bool CheckSingleMove(BoardButton i_destination)
        {
            bool check = false;

            foreach (BoardButton button in m_OptionalMoves)
            {
                if (button == i_destination)
                {
                    check = true;
                }
            }

            return check;
        }

        public bool CheckEatingMove(BoardButton i_destination)
        {
            bool check = false;

            foreach (BoardButton button in m_EatingPossibilities)
            {
                if (button == i_destination)
                {
                    check = true;
                }
            }

            return check;
        }

        public void CleanOptions()
        {
            m_OptionalMoves.Clear();
            m_EatingPossibilities.Clear();
        }


        public override string ToString()
        {
            return string.Format("{0},{1}", m_BoardPlaceX, m_BoardPlaceY);
        }
    }
}
