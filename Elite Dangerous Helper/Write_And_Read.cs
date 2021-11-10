using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite_Dangerous_Helper
{
    public static class Write_And_Read
    {
        public static void WriteTextFileCreate(List<string> aResultsList, string aPath)
        {
            using (TextWriter aTextWriter = new StreamWriter(aPath))
            {
                for (int i = 0; i < aResultsList.Count; i++)
                {
                    foreach (String tempString in aResultsList)
                        aTextWriter.WriteLine(tempString);
                }
            }
        }

        public static void AddNextArchiveIndex()
        {
            File.AppendAllLines("ArchiveIndexes.txt", new[] { DateTime.Today.ToString("yyyy/MM/dd" + ';') });
        }

        public static string[] ReadArchiveIndexes()
        {
            string[] tempDatesArray;
            using (FileStream tempFileStream = File.OpenRead(@"ArchiveIndexes.txt"))
            {
                byte[] tempByteArray = new byte[tempFileStream.Length];
                UTF8Encoding tempTextStandard = new UTF8Encoding(true);
                tempFileStream.Read(tempByteArray, 0, tempByteArray.Length);
                string tempAllText = tempTextStandard.GetString(tempByteArray);
                tempDatesArray = tempAllText.Split(';');
            }
            return tempDatesArray;
        }

        public static List<Archive> ReadArchives()
        {
            List<string> tempDateEntries = ReadArchiveIndexes().ToList();
            tempDateEntries.RemoveAt(tempDateEntries.Count - 1);
            for (int i = 0; i < tempDateEntries.Count; i++)
            {
                tempDateEntries[i] = tempDateEntries[i].Replace("\r\n", string.Empty);
            }


            DateTime[] tempDates = new DateTime[tempDateEntries.Count];
            for (int i = 0; i < tempDates.Length; i++)
            {
                tempDates[i] = DateTime.Parse(tempDateEntries[i]);
            }

            List<List<string>> tempCommodityString = new List<List<string>>();
            for (int i = 0; i < tempDateEntries.Count; i++)
            {
                using (FileStream tempFileStream = File.OpenRead(tempDateEntries[i] + @".txt"))
                {
                    byte[] tempByteArray = new byte[tempFileStream.Length];
                    UTF8Encoding tempTextStandard = new UTF8Encoding(true);
                    tempFileStream.Read(tempByteArray, 0, tempByteArray.Length);
                    string tempAllText = tempTextStandard.GetString(tempByteArray);
                    string[] tempAllCommodityStrings = tempAllText.Split(';');
                    tempCommodityString.Add(tempAllCommodityStrings.ToList());
                }
            }




            List<Archive> tempArchive = new List<Archive>();
            for (int i = 0; i < tempCommodityString.Count; i++)
            {
                tempArchive.Add(CreateArchives(tempCommodityString[i], DateTime.Parse(tempDateEntries[i])));
            }

            return tempArchive;
        }

        static Archive CreateArchives(List<string> aCommodityStrings, DateTime aDate)
        {
            string tempName = "";
            int tempID = 0;
            int tempAVGSell = 0;
            int tempAVGBuy = 0;
            int tempAVGProfit = 0;
            int tempMaxSell = 0;
            int tempMinBuy = 0;
            int tempMaxProfit = 0;

            List<Commodity> tempCommodity = new List<Commodity>();
            for (int i = 0; i < aCommodityStrings.Count; i++)
            {
                if (aCommodityStrings[i] != "")
                {
                    tempName = aCommodityStrings[i];
                    i++;
                    int.TryParse(aCommodityStrings[i], out tempID);
                    i++;
                    int.TryParse(aCommodityStrings[i], out tempAVGSell);
                    i++;
                    int.TryParse(aCommodityStrings[i], out tempAVGBuy);
                    i++;
                    int.TryParse(aCommodityStrings[i], out tempAVGProfit);
                    i++;
                    int.TryParse(aCommodityStrings[i], out tempMaxSell);
                    i++;
                    int.TryParse(aCommodityStrings[i], out tempMinBuy);
                    i++;
                    int.TryParse(aCommodityStrings[i], out tempMaxProfit);

                    tempCommodity.Add(new Commodity(tempName, tempID, tempAVGSell, tempAVGBuy, tempAVGProfit, tempMaxSell, tempMinBuy, tempMaxProfit));
                }
            }

            Archive tempArchive = new Archive(aDate, tempCommodity);
            return tempArchive;
        }
    }
}