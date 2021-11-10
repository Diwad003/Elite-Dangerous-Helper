using System;
using System.Collections.Generic;
using System.Linq;

namespace Elite_Dangerous_Helper
{
    public static class GraphMaker
    {
        public static void MakeAGraph(Commodity aCommodity, List<Archive> aArchive)
        {
            List<Commodity> tempCommoditites = new List<Commodity>();
            List<int> tempDateTime = new List<int>();
            for (int i = 0; i < aArchive.Count; i++)
            {
                for (int j = 0; j < aArchive[i].AccessCommodities.Count; j++)
                {
                    if (aArchive[i].AccessCommodities[j].myName == aCommodity.myName)
                    {
                        tempCommoditites.Add(aArchive[i].AccessCommodities[j]);
                    }
                }

                tempDateTime.Add(aArchive[i].AccessDayOfRecording.Day);
            }

            #region Choose And Write Out All The Options
            Console.Clear();
            Console.WriteLine("1. AVGBUY");
            Console.WriteLine("2. AVGSELL");
            Console.WriteLine("3. AVGPROFIT");
            Console.WriteLine("4. MAXPROFIT");
            Console.WriteLine("5. MAXSELL");
            Console.WriteLine("6. MINBUY");
            Console.WriteLine("7. EXIT TO Graph Choose Menu");
            string tempPlayerChoose = Console.ReadLine();

            var tempChartList = new List<Point>();
            switch (tempPlayerChoose)
            {
                case "1":
                    for (int i = 0; i < tempCommoditites.Count; i++)
                    {
                        tempChartList.Add(new Point(tempDateTime[i], tempCommoditites[i].myAVGBuy));
                    }
                    break;

                case "2":
                    for (int i = 0; i < tempCommoditites.Count; i++)
                    {
                        tempChartList.Add(new Point(tempDateTime[i], tempCommoditites[i].myAVGSell));
                    }
                    break;

                case "3":
                    for (int i = 0; i < tempCommoditites.Count; i++)
                    {
                        tempChartList.Add(new Point(tempDateTime[i], tempCommoditites[i].myAVGProfit));
                    }
                    break;

                case "4":
                    for (int i = 0; i < tempCommoditites.Count; i++)
                    {
                        tempChartList.Add(new Point(tempDateTime[i], tempCommoditites[i].myMaxProfit));
                    }
                    break;

                case "5":
                    for (int i = 0; i < tempCommoditites.Count; i++)
                    {
                        tempChartList.Add(new Point(tempDateTime[i], tempCommoditites[i].myMaxSell));
                    }
                    break;

                case "6":
                    for (int i = 0; i < tempCommoditites.Count; i++)
                    {
                        tempChartList.Add(new Point(tempDateTime[i], tempCommoditites[i].myMinBuy));
                    }
                    break;

                case "7":
                    Program.GraphOfCommoditiesStart();
                    break;

                default:
                    MakeAGraph(aCommodity, aArchive);
                    break;
            }
            #endregion

            DrawChart(tempChartList);
            Console.ReadKey();
            MakeAGraph(aCommodity, aArchive);
        }

        public static void DrawChart(List<Point> aPointList)
        {
            int consoleWidth = 78;
            int consoleHeight = 20;
            int actualConsoleHeight = consoleHeight * 2;
            var minX = aPointList.Min(c => c.X);
            var minY = aPointList.Min(c => c.Y);
            var maxX = aPointList.Max(c => c.X);
            var maxY = aPointList.Max(c => c.Y);

            Console.WriteLine(maxX);
            // normalize points to new coordinates
            var normalized = aPointList.
                Select(c => new Point(c.X - minX, c.Y - minY)).
                Select(c => new Point((int)Math.Round((float)(c.X) / (maxX - minX) * (consoleWidth - 1)), (int)Math.Round((float)(c.Y) / (maxY - minY) * (actualConsoleHeight - 1)))).ToArray();
            Func<int, int, bool> IsHit = (hx, hy) =>
            {
                return normalized.Any(c => c.X == hx && c.Y == hy);
            };

            for (int y = actualConsoleHeight - 1; y > 0; y -= 2)
            {
                Console.Write(y == actualConsoleHeight - 1 ? '┌' : '│');
                for (int x = 0; x < consoleWidth; x++)
                {
                    bool hitTop = IsHit(x, y);
                    bool hitBottom = IsHit(x, y - 1);
                    if (hitBottom && hitTop)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write('█');
                    }
                    else if (hitTop)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write('▀');
                    }
                    else if (hitBottom)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write('▀');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write('▀');
                    }
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.WriteLine('└' + new string('─', (consoleWidth / 2) - 1) + '┴' + new string('─', (consoleWidth / 2) - 1) + '┘');
            Console.Write((aPointList.Min(x => x.X) + "/" + aPointList.Min(x => x.Y)).PadRight(consoleWidth / 3));
            Console.Write((aPointList.Max(x => x.Y) / 2).ToString().PadLeft(consoleWidth / 3 / 2).PadRight(consoleWidth / 3));
            Console.WriteLine(aPointList.Max(x => x.Y).ToString().PadLeft(consoleWidth / 3));
        }

        public struct Point
        {
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }
    }
}