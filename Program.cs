using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication176
{
    class Program
    {
        const string FILENAME = @"C:\Users\Tomas\source\repos\ConsoleApplication176\ConsoleApplication176\CSVFiles\CSVFiles.csv";
        static void Main(string[] args)
        {
            Menu menu = new Menu(FILENAME);

            List<Menu> sortedRows = Menu.items.OrderBy(x => x).ToList();
            menu.Print(sortedRows);
            Console.ReadLine();
        }
    }
    public class Menu : IComparable
    {
        public static List<Menu> items { get; set; }
        public int ID { get; set; }
        public string name { get; set; }
        public int? parent { get; set; }
        public Boolean hidden { get; set; }
        public string[] linkUrl { get; set; }

        public Menu() { }
        public Menu(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            string line = "";
            int rowCount = 0;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length > 0)
                {
                    if (++rowCount == 1)
                    {
                        items = new List<Menu>();
                    }
                    else
                    {
                        Menu newMenu = new Menu();
                        items.Add(newMenu);
                        string[] splitArray = line.Split(new char[] { ';' }).ToArray();
                        newMenu.ID = int.Parse(splitArray[0]);
                        newMenu.name = splitArray[1];
                        newMenu.parent = (splitArray[2] == "NULL") ? null : (int?)int.Parse(splitArray[2]);
                        newMenu.hidden = Boolean.Parse(splitArray[3]);
                        newMenu.linkUrl = splitArray[4].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    }

                }
            }
        }
        public int CompareTo(object obj)
        {
            Menu other = (Menu)obj;
            int min = Math.Min(this.linkUrl.Length, other.linkUrl.Length);

            for (int i = 0; i < min; i++)
            {
                int compare = this.linkUrl[i].CompareTo(other.linkUrl[i]);
                if (compare != 0) return compare;
            }
            return this.linkUrl.Length.CompareTo(other.linkUrl.Length);
        }
        public void Print(List<Menu> rows)
        {
            foreach (Menu menu in rows)
            {
                if (!menu.hidden)
                {
                    int length = menu.linkUrl.Length - 1;
                    Console.WriteLine(".{0} {1}", new string('.', 3 * length), menu.name);
                }
            }
        }
    }

}