using System;
using System.Collections.Generic;

namespace Elite_Dangerous_Helper
{
    class Program
    {
        public static List<Commodity> myCommodity { get; set; }
        public static List<Archive> myArchive { get; set; }

        public static void Main()
        {
            myCommodity = ReadWebsite.MainReadWebsite();
            myArchive = Write_And_Read.ReadArchives();


            Console.Clear();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Add To Archive");
            Console.WriteLine("2. Graph Of The Commodities");
            Console.WriteLine("3. ReadPosition");
            Console.WriteLine("4. Exit");
            string tempPlayerChoose = Console.ReadLine();

            Console.Clear();
            switch (tempPlayerChoose)
            {
                case "1":
                    if (myArchive[myArchive.Count-1] != new Archive(DateTime.Today, myCommodity))
                    {
                        myArchive.Add(new Archive(DateTime.Today, myCommodity));
                    }

                    myArchive[myArchive.Count - 1].ArchiveIntoTxt();
                    Console.WriteLine("Its done");
                    Console.ReadKey();
                    Main();
                    break;

                case "2":
                    GraphOfCommoditiesStart();
                    break;

                case "3":
                    GetCurrentSystem();
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

                default:
                    Main();
                    break;
            }
        }

        public static void GetCurrentSystem()
        {
            ReadWebsite.ReadPosition();
        }

        public static void GraphOfCommoditiesStart()
        {
            #region CommodityLists
            List<Commodity> tempChemicals = new List<Commodity>();
            List<Commodity> tempConsumerItems = new List<Commodity>();
            List<Commodity> tempLegalDrugs = new List<Commodity>();
            List<Commodity> tempFoods = new List<Commodity>();
            List<Commodity> tempIndustrialMaterials = new List<Commodity>();
            List<Commodity> tempMachinery = new List<Commodity>();
            List<Commodity> tempMedicines = new List<Commodity>();
            List<Commodity> tempMetals = new List<Commodity>();
            List<Commodity> tempMinerals = new List<Commodity>();
            List<Commodity> tempSalvage = new List<Commodity>();
            List<Commodity> tempSlaves = new List<Commodity>();
            List<Commodity> tempTechnology = new List<Commodity>();
            List<Commodity> tempTextiles = new List<Commodity>();
            List<Commodity> tempWaste = new List<Commodity>();
            List<Commodity> tempWeapons = new List<Commodity>();
            #endregion

            for (int i = 0; i < myCommodity.Count; i++)
            {
                if (i >= 0 && i < 13)
                {
                    tempChemicals.Add(myCommodity[i]);
                }
                else if (i >= 13 && i < 18)
                {
                    tempConsumerItems.Add(myCommodity[i]);
                }
                else if (i >= 18 && i < 24)
                {
                    tempLegalDrugs.Add(myCommodity[i]);
                }
                else if (i >= 24 && i < 33)
                {
                    tempFoods.Add(myCommodity[i]);
                }
                else if (i >= 33 && i < 42)
                {
                    tempIndustrialMaterials.Add(myCommodity[i]);
                }
                else if (i >= 42 && i < 86)
                {
                    tempMachinery.Add(myCommodity[i]);
                }
                else if (i >= 86 && i < 89)
                {
                    tempMedicines.Add(myCommodity[i]);
                }
                else if (i >= 89 && i < 111)
                {
                    tempMetals.Add(myCommodity[i]);
                }
                else if (i >= 111 && i < 139)
                {
                    tempMinerals.Add(myCommodity[i]);
                }
                else if (i >= 139 && i < 187)
                {
                    tempSalvage.Add(myCommodity[i]);
                }
                else if (i >= 187 && i < 189)
                {
                    tempSlaves.Add(myCommodity[i]);
                }
                else if (i >= 189 && i < 206)
                {
                    tempTechnology.Add(myCommodity[i]);
                }
                else if (i >= 206 && i < 211)
                {
                    tempTextiles.Add(myCommodity[i]);
                }
                else if (i >= 211 && i < 220)
                {
                    tempWaste.Add(myCommodity[i]);
                }
                else if (i >= 220 && i < 234)
                {
                    tempWeapons.Add(myCommodity[i]);
                }
            }

            Console.Clear();
            Console.WriteLine("1. Chemicals");
            Console.WriteLine("2. Consumer Items");
            Console.WriteLine("3. Legal Drugs");
            Console.WriteLine("4. Foods");
            Console.WriteLine("5. Industrial Materials");
            Console.WriteLine("6. Machinery");
            Console.WriteLine("7. Medicines");
            Console.WriteLine("8. Metals");
            Console.WriteLine("9. Minerals");
            Console.WriteLine("10. Salvage");
            Console.WriteLine("11. Slaves");
            Console.WriteLine("12. Technology");
            Console.WriteLine("13. Textiles");
            Console.WriteLine("14. Waste");
            Console.WriteLine("15. Weapons");
            Console.WriteLine("16. Exit To Main Menu");

            string tempChooseString = Console.ReadLine();
            int tempCommodityIndex = 0;
            List<Commodity> tempCommodity = new List<Commodity>();
            switch (tempChooseString)
            {
                case "1":
                    GraphCommodityWriteOut(tempChemicals);
                    tempCommodity = tempChemicals;
                    break;
                case "2":
                    GraphCommodityWriteOut(tempConsumerItems);
                    tempCommodity = tempConsumerItems;
                    break;
                case "3":
                    GraphCommodityWriteOut(tempLegalDrugs);
                    tempCommodity = tempLegalDrugs;
                    break;
                case "4":
                    GraphCommodityWriteOut(tempFoods);
                    tempCommodity = tempFoods;
                    break;
                case "5":
                    GraphCommodityWriteOut(tempIndustrialMaterials);
                    tempCommodity = tempIndustrialMaterials;
                    break;
                case "6":
                    GraphCommodityWriteOut(tempMachinery);
                    tempCommodity = tempMachinery;
                    break;
                case "7":
                    GraphCommodityWriteOut(tempMedicines);
                    tempCommodity = tempMedicines;
                    break;
                case "8":
                    GraphCommodityWriteOut(tempMetals);
                    tempCommodity = tempMetals;
                    break;
                case "9":
                    GraphCommodityWriteOut(tempMinerals);
                    tempCommodity = tempMinerals;
                    break;
                case "10":
                    GraphCommodityWriteOut(tempSalvage);
                    tempCommodity = tempSalvage;
                    break;
                case "11":
                    GraphCommodityWriteOut(tempSlaves);
                    tempCommodity = tempSlaves;
                    break;
                case "12":
                    GraphCommodityWriteOut(tempTechnology);
                    tempCommodity = tempTechnology;
                    break;
                case "13":
                    GraphCommodityWriteOut(tempTextiles);
                    tempCommodity = tempTextiles;
                    break;
                case "14":
                    GraphCommodityWriteOut(tempWaste);
                    tempCommodity = tempWaste;
                    break;
                case "15":
                    GraphCommodityWriteOut(tempWeapons);
                    tempCommodity = tempWeapons;
                    break;
                case "16":
                    Main();
                    break;

                default:
                    GraphOfCommoditiesStart();
                    break;
            }

            if (tempCommodityIndex < tempCommodity.Count)
            {
                GraphMaker.MakeAGraph(tempCommodity[tempCommodityIndex], myArchive);
            }
        }

        static int GraphCommodityWriteOut(List<Commodity> aCommodities)
        {
            Console.Clear();
            for (int i = 0; i < aCommodities.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i, aCommodities[i].myName);
            }
            return int.Parse(Console.ReadLine());
        }
    }
}