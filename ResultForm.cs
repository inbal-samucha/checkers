using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Damka
{
    class ResoltForm : Form
    {
        private Button m_ButtonNO;
        private Button m_ButtonYes;
        private Label m_Label1;
        private PictureBox m_PictureBox1;

        public ResultForm(string i_Name)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResoltForm));
            this.m_ButtonNO = new System.Windows.Forms.Button();
            this.m_ButtonYes = new System.Windows.Forms.Button();
            this.m_Label1 = new System.Windows.Forms.Label();
            this.m_PictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox1)).BeginInit();
            this.SuspendLayout();
            
            this.m_ButtonNO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ButtonNO.CausesValidation = false;
            this.m_ButtonNO.DialogResult = System.Windows.Forms.DialogResult.No;
            this.m_ButtonNO.Location = new System.Drawing.Point(217, 122);
            this.m_ButtonNO.Name = "ButtonNO";
            this.m_ButtonNO.Size = new System.Drawing.Size(90, 34);
            this.m_ButtonNO.TabIndex = 0;
            this.m_ButtonNO.Text = "No";
            this.m_ButtonNO.UseVisualStyleBackColor = true;
           
            this.m_ButtonYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ButtonYes.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_ButtonYes.Location = new System.Drawing.Point(121, 122);
            this.m_ButtonYes.Name = "buttonYes";
            this.m_ButtonYes.Size = new System.Drawing.Size(90, 34);
            this.m_ButtonYes.TabIndex = 1;
            this.m_ButtonYes.Text = "Yes";
            this.m_ButtonYes.UseVisualStyleBackColor = true;
           
            this.m_Label1.AutoSize = true;
            this.m_Label1.Font = new System.Drawing.Font("Narkisim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_Label1.Location = new System.Drawing.Point(142, 43);
            this.m_Label1.Name = "label1";
            this.m_Label1.Size = new System.Drawing.Size(155, 40);
            this.m_Label1.TabIndex = 2;
            this.m_Label1.Text = string.Format("{0} Won!\r\nAnother round?", i_Name);
          
            this.m_PictureBox1.Image = global::Damka.Properties.Resources.question_mark_1829459_640;
            this.m_PictureBox1.Location = new System.Drawing.Point(12, 21);
            this.m_PictureBox1.Name = "pictureBox1";
            this.m_PictureBox1.Size = new System.Drawing.Size(83, 89);
            this.m_PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_PictureBox1.TabIndex = 3;
            this.m_PictureBox1.TabStop = false;
          
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 168);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.m_PictureBox1);
            this.Controls.Add(this.m_Label1);
            this.Controls.Add(this.m_ButtonYes);
            this.Controls.Add(this.m_ButtonNO);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WinForm";
            this.Text = "Checkers";
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
