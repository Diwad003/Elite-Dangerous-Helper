using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite_Dangerous_Helper
{
    public class Commodity
    {
        public string myName { get; set; }
        public int myID { get; set; }
        public int myAVGSell { get; }
        public int myAVGBuy { get; }
        public int myAVGProfit { get; }
        public int myMaxSell { get; }
        public int myMinBuy { get; }
        public int myMaxProfit { get; }

        public Commodity(string aName, int aID, int aAVGSell, int aAVGBuy, int aAVGProfit, int aMaxSell, int aMinBuy, int aMaxProfit)
        {
            myName = aName;
            myID = aID;
            myAVGSell = aAVGSell;
            myAVGBuy = aAVGBuy;
            myMaxSell = aMaxSell;
            myMinBuy = aMinBuy;

            myMaxProfit = aMaxProfit;
            myAVGProfit = aAVGProfit;
        }
    }
}