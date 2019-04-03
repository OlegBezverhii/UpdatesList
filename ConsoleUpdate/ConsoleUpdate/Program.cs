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
using System.IO;

namespace ConsoleUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сейчас будет выводиться список обновлений ПК с именем " + Environment.MachineName);
            Console.WriteLine("для продолжения нажмите любую клавишу");
            //Console.WriteLine("Имя ПК: {0}", Environment.MachineName);
            Console.ReadKey();
            Console.WriteLine("\t");


            string writePath = AppDomain.CurrentDomain.BaseDirectory + "output.txt";
            //Console.WriteLine(writePath);
            if (!File.Exists(writePath))
            {
                Console.WriteLine("Создаю пустой txt файл");
                //File.Create(writePath);
            }
            else
            {
                Console.WriteLine("Файл output.txt существует, он будет перезаписан");
                File.Delete(writePath);
                //File.Create(writePath);
            }
            
            string Key = "Win32_QuickFixEngineering";
            //https://windowsnotes.ru/cmd/poisk-ustanovlennyx-obnovlenij-iz-komandnoj-stroki/
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + Key);

            try
            {
                /*using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }*/
                StreamWriter sw = new StreamWriter(writePath);

                sw.WriteLine("Имя ПК: " + Environment.MachineName + Environment.NewLine);


                foreach (ManagementObject share in searcher.Get())
                {

                    Console.WriteLine("{0,-17} {1}","Обновление: ", share["HotFixID"]);// String
                    Console.WriteLine("{0,-17} {1}", "Тип обновления: ", share["Description"]);
                    Console.WriteLine("{0,-17} {1}", "Ссылка/описание: ", share["Caption"]);
                    Console.WriteLine("{0,-17} {1}", "Дата установки: ", share["InstalledOn"]);
                    Console.WriteLine("");

                    sw.WriteLine("{0,-17} {1}", "Обновление: ", share["HotFixID"]);
                    sw.WriteLine("{0,-17} {1}", "Тип обновления: ", share["Description"]);
                    sw.WriteLine("{0,-17} {1}", "Ссылка: ", share["Caption"]);
                    sw.WriteLine("{0,-17} {1}", "Дата установки: ", share["InstalledOn"]);
                    sw.WriteLine();

                    //Console.WriteLine("{0,-35} {1,-40}","InstallDate",ManagementDateTimeConverter.ToDateTime((string)WmiObject["InstallDate"]));// Datetime
                    //Console.WriteLine("{0,-35} {1,-40}", share["Caption"], share["HotFixID"]);// String
                }

                sw.Close();
            }
            
            catch (Exception exp)
            {
                Console.WriteLine("Ошибка: \n" + exp.Message);
            }
            Console.WriteLine("Для выхода нажмите любую клавишу");
            Console.ReadKey();
        }
    }
}
