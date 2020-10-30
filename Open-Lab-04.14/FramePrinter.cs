using System;

namespace Open_Lab_04._14
{
    public class FramePrinter
    {
        public void Print(string[] strings)
        {
            int h = 0;
            for (int i = 0; i<strings.Length; i++)
            {
                if (strings[i].Length > h)
                {
                    h = strings[i].Length;
                }
            }
            int g = h + 3;
            //{ "Hello", "World", "in", "a", "frame" };6,9,
            for (int i = 0; i < g + 1; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine("");
            for (int i = 0; i < strings.Length; i++)
            {
                Console.Write("* " + strings[i]);
                int d = h - strings[i].Length;
                for (int b = 0; b < d; b++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(" *");
            }
            for (int i = 0; i < g + 1; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine("");
        }
    }
}