using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Elite_Dangerous_Helper
{
    static class ReadWebsite
    {
        #region ReadCommodities
        public static List<Commodity> MainReadWebsite()
        {
            List<Commodity> tempCommodities = ReadFromLinkAndReturnCommodities();

            for (int i = 0; i < tempCommodities.Count; i++)
            {
                tempCommodities[i].myName.TrimStart();
            }

            return tempCommodities;
        }

        static List<Commodity> ReadFromLinkAndReturnCommodities()
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://inara.cz/galaxy-commodities/");
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string tempResult = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            List<string> tempResultsList = new List<string>();
            string[] tempResultsArray = tempResult.Split('<');
            tempResultsList = tempResultsArray.ToList();

            for (int i = 0; i < 806; i++)
            {
                tempResultsList.RemoveAt(0);
            }

            var tempCharsToRemove = new string[]
            {
                "/td>", "/span>", "/td>", "/a>", "/", "a href=", "galaxy-commodity", "class="+'"'+"inverse"+'"'+">", "" + '"', "td class=lineright alignright>", "span class=minor>Cr", "td class=alignright>",
                "tr>", "td class=lineright alignright collapsible1 collapsible2>", "td class=lineright paddingleft wrap>", "td class=alignright lineright>", "td class=alignright lineright>", "tr class=subheader>",
                " lineright uppercase>", "td class=subheader", "td class=subheader>", "td class=subheader lineright collapsible1 collapsible2>", ">", " lineright>", " lineright collapsible1 collapsible2>", " lineright",
                " collapsible1 collapsible2", " collapsible1 collapsible2", "tr class=advert", "td colspan=7", "div class=advertleaderboardcontainer advertplatform2 ", "div class=advertleaderboard", "div", "script",
                "br", " type=textjavawindow['nitroAds'].createAd('GGAd_Galaxy_commodities_incontent_4_mobile', {refreshLimit: 20, refreshTime: 65, renderVisibleOnly: false, refreshVisibleOnly: true,sizes: [ ['728','90']," +
                " ['320','100'], ['300','50'], ['320','50'] ],report: { enabled: true, wording: Report Ad, position: bottom-right },mediaQuery: (max-width: 1024px),}); ", " id=GGAd_Galaxy_commodities_incontent_4_mobile",
                " id=GGAd_Galaxy_commodities_incontent_8_mobile", " type=textjavawindow['nitroAds'].createAd('GGAd_Galaxy_commodities_incontent_8_mobile', {refreshLimit: 20, refreshTime: 65, renderVisibleOnly: false, " +
                "refreshVisibleOnly: true,sizes: [ ['728','90'], ['320','100'], ['300','50'], ['320','50'] ],report: { enabled: true, wording: Report Ad, position: bottom-right },mediaQuery: (max-width: 1024px),}); ",
                " id=GGAd_Galaxy_commodities_incontent_10_mobile", " type=textjavawindow['nitroAds'].createAd('GGAd_Galaxy_commodities_incontent_10_mobile', {refreshLimit: 20, refreshTime: 65, renderVisibleOnly: false," +
                " refreshVisibleOnly: true,sizes: [ ['728','90'], ['320','100'], ['300','50'], ['320','50'] ],report: { enabled: true, wording: Report Ad, position: bottom-right },mediaQuery: (max-width: 1024px),}); ",
                ",", "commodity "
            };

            for (int i = 0; i < tempResultsList.Count; i++)
            {
                for (int j = 0; j < tempCharsToRemove.Length; j++)
                {
                    tempResultsList[i] = tempResultsList[i].Replace(tempCharsToRemove[j], string.Empty);
                }
            }

            for (int i = 0; i < tempResultsList.Count; i++)
            {
                if (tempResultsList[i] == string.Empty)
                {
                    tempResultsList.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 1631; i < tempResultsList.Count; i++)
            {
                tempResultsList.RemoveAt(i);
                i--;
            }

            tempResultsList.RemoveAt(91);
            tempResultsList.RemoveAt(126);
            tempResultsList.RemoveAt(168);
            tempResultsList.RemoveAt(231);
            tempResultsList.RemoveAt(294);
            tempResultsList.RemoveAt(462);
            tempResultsList.RemoveAt(504);
            tempResultsList.RemoveAt(658);
            tempResultsList.RemoveAt(854);
            tempResultsList.RemoveAt(1386);
            tempResultsList.RemoveAt(1400);
            tempResultsList.RemoveAt(1519);
            tempResultsList.RemoveAt(1554);
            tempResultsList.RemoveAt(1583);


            List<string> tempDigitList = new List<string>();
            for (int i = 0; i < tempResultsList.Count; i++)
            {
                tempDigitList.Add(Regex.Match(tempResultsList[i], @"\d+").Value);
                i += 6;
            }

            for (int i = 0; i < tempResultsList.Count; i++)
            {
                tempResultsList[i] = Regex.Replace(tempResultsList[i], @"[\d]", string.Empty);
                i += 6;
            }

            for (int i = 0; i < tempResultsList.Count; i++)
            {
                tempResultsList[i] = Regex.Replace(tempResultsList[i], @"commodity", string.Empty);
            }

            for (int i = 0; i < tempResultsList.Count; i++)
            {
                tempResultsList[i] = tempResultsList[i].Trim();
            }

            for (int i = 0; i < tempDigitList.Count; i++)
            {
                tempDigitList[i] = tempDigitList[i].Trim();
            }

            List<Commodity> tempCommodityList = new List<Commodity>();

            string tempName = "";
            int tempID = 0;
            int tempAVGSell = 0;
            int tempAVGBuy = 0;
            int tempAVGProfit = 0;
            int tempMaxSell = 0;
            int tempMinBuy = 0;
            int tempMaxProfit = 0;

            for (int i = 0; i < tempResultsList.Count; i++)
            {
                for (int j = 0; j < tempDigitList.Count; j++)
                {
                    tempName = tempResultsList[i];
                    int.TryParse(tempDigitList[j], out tempID);
                    i++;
                    int.TryParse(tempResultsList[i], out tempAVGSell);
                    i++;
                    int.TryParse(tempResultsList[i], out tempAVGBuy);
                    i++;
                    int.TryParse(tempResultsList[i], out tempAVGProfit);
                    i++;
                    int.TryParse(tempResultsList[i], out tempMaxSell);
                    i++;
                    int.TryParse(tempResultsList[i], out tempMinBuy);
                    i++;
                    int.TryParse(tempResultsList[i], out tempMaxProfit);
                    i++;

                    tempCommodityList.Add(new Commodity(tempName, tempID, tempAVGSell, tempAVGBuy, tempAVGProfit, tempMaxSell, tempMinBuy, tempMaxProfit));
                }
            }

            return tempCommodityList;
        }
        #endregion

        #region ReadPosition
        public static void ReadPosition()
        {
            HttpWebRequest tempRequest = (HttpWebRequest)WebRequest.Create("https://inara.cz/cmdr/287933/");
            tempRequest.Method = "GET";
            WebResponse myResponse = tempRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string tempResult = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();
            string[] tempResultsArray = tempResult.Split('<');
            List<string> tempResultList = tempResultsArray.ToList();

            tempResultList.RemoveAt(0);

            var tempStringsToRemove = new string[]
            {
                "/div>", "div>", "br>", "/span>", "span class=", "/a>", "head>", "noscript>", "link rel=" + '"' + "icon" + '"' +" type=" + '"' + "image/png" + '"' + "href=" + '"' + "/", "︎", ">", "/h2>",
                "link rel=", "meta name=", "/", "script type=", "/script", "meta name=", "meta property=", "body", "div style=", "div class=", "a href=" + '"' + "/" + '"', Environment.NewLine
            };

            for (int i = 0; i < tempResultList.Count; i++)
            {
                for (int j = 0; j < tempStringsToRemove.Length; j++)
                {
                    tempResultList[i] = tempResultList[i].Replace(tempStringsToRemove[j], string.Empty);
                }
            }

            for (int i = 0; i < tempResultList.Count; i++)
            {
                if (tempResultList[i] == string.Empty)
                {
                    tempResultList.RemoveAt(i);
                    i--;
                }
            }

            Write_And_Read.WriteTextFileCreate(tempResultList, "ReadPosition.txt");
        }
        #endregion
    }
}