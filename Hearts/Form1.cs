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
        Cards human, AI1, AI2, AI3;
        /*绘制出电脑玩家的牌*/
        private void DrawCardsofAI(int n, PictureBox picbox, bool isHorizontal)
        {
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
        }
        /*绘制出人类玩家的牌*/
        private void DrawCardsofPlayer(List<int> player)
        {           
            int n = player.Count;
            Graphics g = pictureBox1.CreateGraphics();
            for (int i = 0; i < n; i++)
            {
                Bitmap card = new Bitmap("./img/" + player[i] + ".jpg");
                Rectangle r = new Rectangle(i * 50, 0, 105, 150);
                g.DrawImage(card, r);
            }
        }
        private void DrawPlayedCards(int x, int y, int cardIndex)
        {
            Graphics g = pictureBox5.CreateGraphics();
            Bitmap card = new Bitmap("./img/" + cardIndex + ".jpg");
            Rectangle r = new Rectangle(x, y, 105, 150);
            g.DrawImage(card, r);
        }
        /*开局*/
        private void 新游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int>[] players = new List<int>[4];
            players = Cards.Deal();
            human = new Cards(players[0]);
            AI1 = new Cards(players[1]);
            AI2 = new Cards(players[2]);
            AI3 = new Cards(players[3]);
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
            DrawPlayedCards(75, 100, human.GetCard(index));
            human.Play(index);
            pictureBox1.Refresh();
            DrawCardsofPlayer(human.GetCards());
            
        }


    }
}
