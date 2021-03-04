using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gubanc.include;

namespace Gubanc
{
    class Program
    {
        static void Main(string[] args)
        {
            CardCollections cardCollections = null;
            RubikGubanc rubikGubanc = new RubikGubanc();
            bool result = rubikGubanc.Gubancol(0);

            if (result)
            {
                Console.WriteLine("");
                Console.WriteLine("Kirakva!");
                Console.WriteLine("");

                cardCollections = CardCollections.GetCardCollections();

                foreach (Card item in cardCollections.Solution)
                {
                    Console.WriteLine("      {0} - tájolás: {1}", item.Name, item.Orientation);
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Nincs megoldás!");
            }

            Console.ReadKey();
        }
    }
}
