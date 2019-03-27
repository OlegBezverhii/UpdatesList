using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;


namespace ConsoleUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сейчас будет выводиться список всех обновлений");
            Console.ReadKey();
            Console.WriteLine("\t");

            string Key = "Win32_QuickFixEngineering";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + Key);

            try
            {
                foreach (ManagementObject share in searcher.Get())
                {

                    Console.WriteLine(share["HotFixID"]);// String
                    //Console.WriteLine("{0,-35} {1,-40}", share["Caption"], share["HotFixID"]);// String
                }
            }


            catch (Exception exp)
            {
                Console.WriteLine("can't get data because of the followeing error \n" + exp.Message);
            }

            Console.ReadKey();
        }
    }
}
