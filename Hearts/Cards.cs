using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hearts
{
    class Cards
    {
        /*构造函数*/
        public Cards(List<int> player)
        {
            this.player = new List<int>(player);
        }
        /*私有变量*/
        private List<int> player;
        /*洗牌发牌函数*/
        static public List<int>[] Deal()
        {            
            List<int>[] player = new List<int>[4];//4名玩家所持有的牌
            for (int i = 0; i < 4; i++)
                player[i] = new List<int>();
            Random rd = new Random();
            int[] cards = new int[52];
            List<int> list = new List<int>();
            while (list.Count < 52)//将1到52随机排序
            {
                int temp = rd.Next(1, 53);
                if (!cards.Contains(temp))
                {
                    cards[list.Count] = temp;
                    list.Add(temp);
                }
            }
            for (int i = 0; i < 13; i++)//给四名玩家分发牌
            {
                player[0].Add(cards[i]);
                player[1].Add(cards[i + 13]);
                player[2].Add(cards[i + 26]);
                player[3].Add(cards[i + 39]);
            }
            return player;
        }
        /*出牌函数*/
        public void Play(int index)
        {
            player.RemoveAt(index);
        }
        public void Play()
        {
            Random rd = new Random();
            player.RemoveAt(rd.Next(0, player.Count));
        }
        /*返回所有牌*/
        public List<int> GetCards()
        {
            return player;
        }
        /*返回特定索引的牌*/
        public int GetCard(int index)
        {
            return player[index];
        }
    }
}
