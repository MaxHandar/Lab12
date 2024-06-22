using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        //static int GetExactNumber(int min, int max)
        //{
        //    int number;
        //    do
        //    {
        //        Console.WriteLine("Элемент под каким номером надо изменить(для проверки на глубоку копию)?");
        //        number = InputData.NumInput();
        //    } while (number < min || number > max);
        //    return number;
        //}
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
            MyHashTable<Vehicle> table = new MyHashTable<Vehicle>();
            int answer = 1;
            do
            {
                try
                {
                    answer = ShowMenu("Menu:", "Неправильно введен пункт меню",
                "Сформировать хэш-таблицу",
                "Распечататть полученную хеш-таблицу",
                "Поиск эл-та в хэш-таблице",
                "Удаление элемента в хэш-таблмице",
                "Удаление хэш-таблицы из памяти",
                "Завершить работу"
                //"Удалить из списка все элементы, начиная с элемента с заданным названием бренда до конца списка",
                //"Удалить список из памяти",
                //"Завершить работу");
                );
                    switch (answer)
                    {
                        case 1:
                            int size = InputData.NumInput("Размер коллекции?", true);
                            table = new MyHashTable<Vehicle>(size);
                            table.FillHashTableRand();
                            Console.WriteLine("таблица создана");
                            break;
                        case 2:
                            table.PrintTable();
                            Console.WriteLine($"Capacity: {table.Capacity}");
                            break;
                        case 3:
                            Vehicle vehicle = new Vehicle();
                            Console.WriteLine("Введите элемент для поиска:");
                            vehicle.Init();
                            int index;
                            if (table.Contains(vehicle, out index))
                            {
                                Console.WriteLine($"Элемент найден, позиция = {index+1}");
                            }
                            else
                            {
                                Console.WriteLine("Элемент не найден");
                            }
                            break;
                        case 4:
                            Vehicle vehicleToDelete = new Vehicle();
                            Console.WriteLine("Введите элемент для поиска:");
                            vehicleToDelete.Init();
                            if (table.RemoveData(vehicleToDelete))
                                Console.WriteLine("Элемент удален");
                            else
                                Console.WriteLine("Элемент не найден");
                            break;
                        //case 5:
                        //    if (list.Count == 0)
                        //        Console.WriteLine("Массив пуст");
                        //    else
                        //    {
                        //        Console.Write("Введите название бренда, с которого нужно начать удаление:");
                        //        string brand = Console.ReadLine();
                        //        list.RemoveItemsFromGiven(brand);
                        //        Console.WriteLine("Элементы удалены");
                        //        Console.WriteLine($"Count: {list.Count}");
                        //    }
                        //    break;
                        case 5:
                            if (table.Capacity == 0)
                                Console.WriteLine("Таблица пуста");
                            else
                            {
                                table = new MyHashTable<Vehicle>();
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