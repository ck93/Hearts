using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hearts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        
        Cards human;
        Cards[] AI = new Cards[3];
        Control control;
        /*绘制出电脑玩家的牌*/
        private void DrawCardsofAI(int n, PictureBox picbox, bool isHorizontal)
        {
            picbox.Refresh();
            Graphics g = picbox.CreateGraphics();
            Bitmap back = new Bitmap("./img/back.jpg");
            if (isHorizontal)
                for (int i = 0; i < n; i++)
                {
                    Rectangle r = new Rectangle(i * 30, 0, 105, 150);                              
                    g.DrawImage(back, r);
                }
            else
                for (int i = 0; i < n; i++)
                {
                    Rectangle r = new Rectangle(0, 20 * i, 105, 150);
                    g.DrawImage(back, r);
                }
            g.Dispose();
        }
        /*绘制出人类玩家的牌*/
        private void DrawCardsofPlayer(List<int> player)
        {
            pictureBox1.Refresh();
            int n = player.Count;
            Graphics g = pictureBox1.CreateGraphics();
            for (int i = 0; i < n; i++)
            {
                Bitmap card = new Bitmap("./img/" + player[i] + ".jpg");
                Rectangle r = new Rectangle(i * 50, 0, 105, 150);
                g.DrawImage(card, r);
            }
            g.Dispose();
        }
        private void DrawPlayedCards(int x, int y, int cardIndex)
        {
            Graphics g = pictureBox5.CreateGraphics();
            Bitmap card = new Bitmap("./img/" + cardIndex + ".jpg");
            Rectangle r = new Rectangle(x, y, 105, 150);
            g.DrawImage(card, r);
            g.Dispose();
        }
        /*开局*/
        private void 新游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            List<int>[] players = new List<int>[4];
            players = Cards.Deal();
            players[0].Sort();//使玩家的牌可以有序显示
            players[1].Sort();
            players[2].Sort();
            players[3].Sort();
            human = new Cards(players[0]);
            AI[0] = new Cards(players[1]);
            AI[1] = new Cards(players[2]);
            AI[2] = new Cards(players[3]);
            control = new Control();           
            DrawCardsofAI(13, pictureBox2, true);
            DrawCardsofAI(13, pictureBox3, false);
            DrawCardsofAI(13, pictureBox4, false);
            DrawCardsofPlayer(human.GetCards());
            
        }
        /*单击出牌*/
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int numofCards = human.GetCards().Count;
            int index;
            if (e.X > numofCards * 50 + 55)//若点击坐标在所有牌的右边，则忽略
                return;
            else if (e.X > numofCards * 50)//若点击坐标在最后一张牌的右半边，则返回最后一张牌的位置
                index = numofCards - 1;
            else//否则返回点击的牌的位置
                index = e.X / 50;
            int cardIndex = human.GetCard(index);
            DrawPlayedCards(75, 100, cardIndex);
            human.Play(index);           
            DrawCardsofPlayer(human.GetCards());
            control.RecordCards(cardIndex, 0);
            int starter = control.GetStarter();
            if (starter == 0)
                starter = 4;
            for (int i = 1; i < starter; i++)
                PlayAndDraw(i);
            control.Settle();
            UpdateGrades();
            Delay(500);
            pictureBox5.Refresh();
            if (human.GetCards().Count == 0)
            {
                MessageBox.Show("结束");
                return;
            }
            if (control.GetStarter() == 0)
                return;
            for (int i = control.GetStarter(); i < 4; i++)
                PlayAndDraw(i);
        }
        /*刷新分数*/
        private void UpdateGrades()
        {
            label2.Text = Control.grade[0].ToString();
            label3.Text = Control.grade[1].ToString();
            label5.Text = Control.grade[2].ToString();
            label7.Text = Control.grade[3].ToString();
        }
        /*延时函数*/
        private void Delay(int ms)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(ms) > DateTime.Now)
            {
                Application.DoEvents();
            }
            return;
        }
        /*AI出牌并绘制*/
        private void PlayAndDraw(int i)
        {
            int cardIndex = control.Play(AI[i - 1], i);
            control.RecordCards(cardIndex, i);
            switch (i)
            {
                case 1:
                    DrawPlayedCards(150, 50, cardIndex);
                    DrawCardsofAI(AI[i - 1].GetCards().Count, pictureBox3, false);
                    break;
                case 2:
                    DrawPlayedCards(75, 0, cardIndex);
                    DrawCardsofAI(AI[i - 1].GetCards().Count, pictureBox2, true);
                    break;
                case 3:
                    DrawPlayedCards(0, 50, cardIndex);
                    DrawCardsofAI(AI[i - 1].GetCards().Count, pictureBox4, false);
                    break;
            }
        }
    }
}
