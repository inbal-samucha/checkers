using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Damka
{
    public  class GameSetting : Form
    {
        Label m_LabelBoardSize = new Label();
        Label m_LabelPlayers = new Label();
        Label m_LabelPlayerOne = new Label();
        RadioButton m_RadioButtonSizeMinimum = new RadioButton();
        RadioButton m_RadioButtonSizeMedium = new RadioButton();
        RadioButton m_RadioButtonSizeBig = new RadioButton();
        TextBox m_TextBoxPlayerOneText = new TextBox();
        TextBox m_TextBoxPlayerTwoText = new TextBox();
        CheckBox m_CheckBoxPlayerTwo = new CheckBox();
        Button m_ButtonDone = new Button();

        int m_BoardSize;

        public GameSetting()
        {
            this.Size = new Size(300, 250);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "GameSetting";

            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Location = new Point(10, 20);

            m_RadioButtonSizeMinimum.Text = "6 X 6";
            m_RadioButtonSizeMinimum.Location = new Point(m_LabelBoardSize.Left + 8,m_LabelBoardSize.Top + m_LabelBoardSize.Height );

            m_RadioButtonSizeMedium.Text = "8 X 8";
            m_RadioButtonSizeMedium.Location = new Point(m_RadioButtonSizeMinimum.Right , m_RadioButtonSizeMinimum.Top );
            
            m_RadioButtonSizeBig.Text = "10 X 10";
            m_RadioButtonSizeBig.Location = new Point(m_RadioButtonSizeMedium.Right , m_RadioButtonSizeMinimum.Top);

            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.Location = new Point(m_LabelBoardSize.Left, m_RadioButtonSizeMinimum.Height + 60);

            m_LabelPlayerOne.Text = "PlayerOne";
            m_LabelPlayerOne.Location = new Point(m_LabelPlayers.Left + 5 , m_LabelPlayers.Top + m_LabelPlayers.Height );

            int textBoxPlayerOneTop = m_LabelPlayerOne.Top + m_LabelPlayerOne.Height / 2; // לשנות לא ממורכז
            textBoxPlayerOneTop -=  m_TextBoxPlayerOneText.Height / 2;

            m_TextBoxPlayerOneText.Location = new Point(m_LabelPlayerOne.Right + 8, textBoxPlayerOneTop);
            m_TextBoxPlayerOneText.Enabled = false;
            

            m_CheckBoxPlayerTwo.Text = "PlayerTwo";
            m_CheckBoxPlayerTwo.Location = new Point(m_LabelPlayerOne.Left,m_LabelPlayerOne.Top + m_LabelPlayerOne.Height);

            int textBoxPlayerTwoTop = m_CheckBoxPlayerTwo.Top + m_CheckBoxPlayerTwo.Height / 2;
            textBoxPlayerTwoTop -=  m_TextBoxPlayerTwoText.Height / 2;

            m_TextBoxPlayerTwoText.Location = new Point(m_TextBoxPlayerOneText.Left ,
                 textBoxPlayerTwoTop);
            m_TextBoxPlayerTwoText.Text = "computer";
            m_TextBoxPlayerTwoText.Enabled = false;

            m_ButtonDone.Text = "Done";
            m_ButtonDone.Location = new Point(this.ClientSize.Width - m_ButtonDone.Width - 8, this.ClientSize.Height - m_ButtonDone.Height - 8);
            m_ButtonDone.Enabled = false;

            this.Controls.AddRange(new Control[] { m_LabelBoardSize, m_LabelPlayers, m_LabelPlayerOne, m_RadioButtonSizeMinimum, m_RadioButtonSizeMedium,
                m_RadioButtonSizeBig, m_TextBoxPlayerOneText, m_TextBoxPlayerTwoText, m_CheckBoxPlayerTwo, m_ButtonDone});

            this.m_ButtonDone.Click += m_ButtonDone_Click;
            this.m_CheckBoxPlayerTwo.Click += m_CheckBoxPlayerTwo_Click;
            this.m_RadioButtonSizeBig.Click += textFill_Click;
            this.m_RadioButtonSizeMedium.Click += textFill_Click;
            this.m_RadioButtonSizeMinimum.Click += textFill_Click;
            this.m_TextBoxPlayerOneText.TextChanged += M_TextBoxPlayerText_TextChanged;
            this.m_TextBoxPlayerTwoText.TextChanged += M_TextBoxPlayerText_TextChanged;
        }

        private void M_TextBoxPlayerText_TextChanged(object sender, EventArgs e)
        {
            if (m_TextBoxPlayerOneText.Text == "" || m_TextBoxPlayerTwoText.Text == "")
            {
                m_ButtonDone.Enabled = false;
            }
            else
                m_ButtonDone.Enabled = true;
        }

        private void m_CheckBoxPlayerTwo_Click(object sender, EventArgs e)
        {
            m_TextBoxPlayerTwoText.Enabled = m_CheckBoxPlayerTwo.Checked;
            if (m_CheckBoxPlayerTwo.Checked)
                m_TextBoxPlayerTwoText.Text = "";
            else
                m_TextBoxPlayerTwoText.Text = "Computer";
        }

        private void textFill_Click(object sender, EventArgs e)
        {
            m_TextBoxPlayerOneText.Enabled = true;
            //if(m_TextBoxPlayerOneText.Text.Length > 0)
            //{
                //m_ButtonDone.Enabled = true;
            //}
        }

        private void m_ButtonDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string PlayerOne
        {
            get { return m_TextBoxPlayerOneText.Text; }
            set { m_TextBoxPlayerOneText.Text = value; }
        }

        public string PlayerTwo
        {
            get { return m_TextBoxPlayerTwoText.Text; }
            set { m_TextBoxPlayerTwoText.Text = value; }
        }

        public int boardSize()
        {
            if(m_RadioButtonSizeBig.Checked)
            {
                m_BoardSize = 10;
            }
            if(m_RadioButtonSizeMedium.Checked)
            {
                m_BoardSize = 8;
            }
            if(m_RadioButtonSizeMinimum.Checked)
            {
                m_BoardSize = 6;
            }

            return m_BoardSize;
        }

        public CheckBox CheckBoxPlayerTwo { get { return m_CheckBoxPlayerTwo; } }
        
    }
}
