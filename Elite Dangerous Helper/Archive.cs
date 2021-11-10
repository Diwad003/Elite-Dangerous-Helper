using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite_Dangerous_Helper
{
    public class Archive
    {
        DateTime myDayOfRecording;
        List<Commodity> myCommodies;

        public List<Commodity> AccessCommodities { get => myCommodies; }
        public DateTime AccessDayOfRecording { get => myDayOfRecording; }

        public Archive(DateTime aDate, List<Commodity> aCommodityList)
        {
            myDayOfRecording = aDate;
            myCommodies = aCommodityList;
        }

        public void ArchiveIntoTxt()
        {
            List<string> tempString = new List<string>();

            for (int i = 0; i < myCommodies.Count; i++)
            {
                tempString.Add(myCommodies[i].myName);
                tempString.Add(myCommodies[i].myID.ToString());
                tempString.Add(myCommodies[i].myAVGSell.ToString());
                tempString.Add(myCommodies[i].myAVGBuy.ToString());
                tempString.Add(myCommodies[i].myAVGProfit.ToString());
                tempString.Add(myCommodies[i].myMaxSell.ToString());
                tempString.Add(myCommodies[i].myMinBuy.ToString());
                tempString.Add(myCommodies[i].myMaxProfit.ToString());
            }

            Write_And_Read.AddNextArchiveIndex();
            Write_And_Read.WriteTextFileCreate(tempString, myDayOfRecording.ToString("yyyy/MM/dd") + ".txt");
        }
    }
}