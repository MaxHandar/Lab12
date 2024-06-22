using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12_4
{
    internal class Program
    {
        static int ShowMenu(string question, string defaultMessage, params string[] answers)
        {
            Console.WriteLine(question);
            for (int i = 0; i < answers.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {answers[i]}");
            }
            int answer = InputData.NumInput();
            return answer;
        }
        static void Main(string[] args)
        {
            MyCollection<Vehicle> c1 = new MyCollection<Vehicle>();
            //foreach (Vehicle v in c1)
            //{
            //    Console.WriteLine(v);
            //}
            c1.ShowTree();
            int answer = 0;
            do
            {
                try
                {
                    answer = ShowMenu("Menu:", "Неправильно введен пункт меню",
                "Сформировать коллекцию",
                "Распечататть полученную коллекцию",
                "Удалить элемент",
                "Нахождение элемента в коллекции",
                "Очистить коллекцию",
                "Завершить работу"
                );
                    switch (answer)
                    {
                        case 1:
                            int size = InputData.NumInput("Размер коллекции?", true);
                            c1 = new MyCollection<Vehicle>(size);
                            Console.WriteLine("коллекция создана");
                            break;
                        case 2:
                            if (c1.Count == 0)
                                Console.WriteLine("коллекция пуста");
                            else
                            {
                                foreach (Vehicle v in c1)
                                {
                                    Console.WriteLine(v);
                                }
                                Console.WriteLine(c1.Count);
                            }
                            Console.WriteLine();
                            break;
                        case 3:
                            if (c1.Count == 0)
                                Console.WriteLine("коллекция пуста");
                            else
                            {
                                Vehicle vehicle = new Vehicle();
                                Console.WriteLine("Введите элемент типа Vehicle");
                                vehicle.Init();
                                if (c1.Remove(vehicle))
                                    Console.WriteLine("Элемент удален");
                                else
                                    Console.WriteLine("Элемент не найден");
                            }   
                            break;
                        case 4:
                            if (c1.Count == 0)
                                Console.WriteLine("коллекция пуста");
                            else
                            {
                                Vehicle vehicle2 = new Vehicle();
                                Console.WriteLine("Введите элемент типа Vehicle");
                                vehicle2.Init();
                                if (c1.Contains(vehicle2))
                                {
                                    Console.WriteLine("Элемент не найден");
                                }
                                else
                                {
                                    Console.WriteLine("Элемент найден");
                                }
                            }
                            break;
                        case 5:
                            if (c1.Count == 0)
                                Console.WriteLine("коллекция пуста");
                            else
                            {
                                c1.Clear();
                                Console.WriteLine("Память освобождена");
                            }
                            break;
                        case 6:
                            Console.WriteLine("Работа завершена");
                            break;
                        default:
                            Console.WriteLine("Неправильно задан пункт меню");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            } while (answer != 6);
        }
    }
}
