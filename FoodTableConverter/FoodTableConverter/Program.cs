/*
 * Created by SharpDevelop.
 * User: ${someguythere}
 * Date: (c)12/22/2014
 * Time: 12:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FoodTableConverter
{
	static class Program
	{
		
		private static Dictionary<int, string> FoodNameList { get; set; }

        private static Dictionary<int, float> ServingSizeList { get; set; }

        private static Dictionary<int, float> CaloriesPerServingList { get; set; }

        private static Dictionary<int, string> DefinersList { get; set; }
		
		private static void Main()
		{
			String line;
			int position = 0;
			Console.Title = "Reading food table.txt...please wait....";
			using (StreamReader sr = new StreamReader ("food table.txt"))
           {
               int[] number = {
                   0,
                   0,
                   0,
                   0
               };
               while (!Equals((line = sr.ReadLine()), null))
               {
                   if (line.Contains("-", StringComparison.CurrentCultureIgnoreCase))
                   {
                       position++;
                   }
                   else
                   {
                       switch (position)
                       {
                           case 0:
                               FoodNameList.Add(number [position], line);
                               break;
                           case 1:
                               ServingSizeList.Add(number [position], float.Parse(line, CultureInfo.InvariantCulture));
                               break;
                           case 2:
                               CaloriesPerServingList.Add(number [position], float.Parse(line, CultureInfo.InvariantCulture));
                               break;
                           case 3:
                               DefinersList.Add(number [position], line);
                               break;
                       }
                       number [position]++;
                   }
               }
               sr.Close();
           }
			
			if (File.Exists(string.Format(CultureInfo.InvariantCulture, "{0}food.table")))
            {
                File.Delete(string.Format(CultureInfo.InvariantCulture, "{0}food.table"));
            }
            string finalstring = null;
            const string seperator = "-------------------------------------------------------------------------\n";
            for (int i = 0; i < FoodNameList.Count; i++)
            {
                finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n", FoodNameList [i]);
                finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n", ServingSizeList [i]);
                finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n", CaloriesPerServingList [i]);
                finalstring += string.Format(CultureInfo.InvariantCulture, "{0}\n", DefinersList [i]);
                finalstring += seperator;
            }
            File.WriteAllText("food.table", finalstring);
		}
		
		internal static bool Contains (this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
	}
}