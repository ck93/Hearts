using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hearts
{
    class Control
    {
        public Control()
        {
            starter = 0;
            for (int i = 0; i < 4; i++)
                grade[i] = 0;
        }
        int starter;//第一个出牌者
        int[] cards = new int[4];//一轮中四名玩家出的牌
        static public int[] grade = new int[4]; //四名玩家的分数
        /*返回第一个出牌者*/
        public int GetStarter()
        {
            return starter;
        }
        /*记录一轮中的牌*/
        public void RecordCards(int cardIndex, int playerNum)
        {
            cards[playerNum] = cardIndex;
        }
        /*一轮完毕结算分数*/
        public void Settle()
        {
            int max = cards[starter];
            int maxer = starter;
            for (int i = 0; i < 4; i++)
            {
                if (i == starter)
                    continue;
                if (cards[i] > max && cards[i] <= ((cards[starter] - 1) / 13 + 1) * 13)
                {
                    maxer = i;
                    max = cards[i];
                }
            }
            int tempGrade = 0;
            for (int i = 0; i < 4; i++)
            {
                if (cards[i] == 12)
                    tempGrade += 13;
                if (cards[i] > 13 && cards[i] < 27)
                    tempGrade++;
            }
            grade[maxer] += tempGrade;
            starter = maxer;
        }
        /*AI出牌函数*/
        public int Play (Cards AI, int indexofAI)
        {
            if (starter != indexofAI)            
            {
                int type = (cards[starter] - 1) / 13;
                for (int i = 0; i < AI.GetCards().Count; i++)
                {
                    int cardIndex = AI.GetCard(i);
                    if (cardIndex > type * 13 && cardIndex <= (type + 1) * 13)
                    {                        
                        AI.Play(i);
                        return cardIndex;
                    }
                }
            }
            return AI.Play();
        }
        
    }
}
