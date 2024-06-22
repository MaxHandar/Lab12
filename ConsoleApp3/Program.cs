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
        static int GetExactNumber(int min, int max)
        {
            int number;
            do
            {
                Console.WriteLine("Элемент под каким номером надо изменить(для проверки на глубоку копию)?");
                number = InputData.NumInput();
            } while (number < min || number > max);
            return number;
        }
        static int ShowMenu(string question, string defaultMessage, params string[] answers)
        {
            Console.WriteLine(question);
            for (int i = 0; i < answers.Length; i++)
            {
                Console.WriteLine($"{i+1}. {answers[i]}");
            }
            int answer = InputData.NumInput();
            return answer;
        }
        static void Main(string[] args)
        {
            MyList<Vehicle> list = new MyList<Vehicle>();
            int answer = 1;
            do
            {
                try
                {
                    answer = ShowMenu("Menu:", "Неправильно введен пункт меню",
                "Сформировать двунаправленный список",
                "Распечататть полученный список",
                "Глубокое копирование списка + проверка",
                "Добавить в список элементы(с помощью ДСЧ) с номерами 1,3,5 и т.д.",
                "Удалить из списка все элементы, начиная с элемента с заданным названием бренда до конца списка",
                "Удалить список из памяти",
                "Завершить работу");
                    switch (answer)
                    {
                        case 1:
                            int size = InputData.NumInput("Размер коллекции?");
                            list = new MyList<Vehicle>(size);
                            Console.WriteLine("Список создан");
                            break;
                        case 2:
                            list.PrintList();
                            Console.WriteLine($"Count: {list.Count}");
                            break;
                        case 3:
                            Random rnd = new Random();
                            MyList<Vehicle> clone = list.MakeCloneList();
                            Console.WriteLine($"Count(clone): {clone.Count}");
                            clone.ChangeDataOf1stElem();
                            Console.WriteLine("Копия списка с измененным 1-м эл-м:");
                            clone.PrintList();
                            Console.WriteLine("Исходный список без изменений:");
                            list.PrintList();
                            Console.WriteLine($"Count(main): {list.Count}");
                            break;
                        case 4:
                            list.AddOddNumElems();
                            Console.WriteLine("Элементы добавлены");
                            Console.WriteLine($"Count: {list.Count}");
                            break;
                        case 5:
                            if (list.Count == 0)
                                Console.WriteLine("Массив пуст");
                            else
                            {
                                Console.Write("Введите название бренда, с которого нужно начать удаление:");
                                string brand = Console.ReadLine();
                                list.RemoveItemsFromGiven(brand);
                                Console.WriteLine("Элементы удалены");
                                Console.WriteLine($"Count: {list.Count}");
                            }
                            break;
                        case 6:
                            if (list.Count==0)
                                Console.WriteLine("Массив пуст");
                            else
                            {
                                list = new MyList<Vehicle>();
                                Console.WriteLine("Память освобождена");
                                Console.WriteLine($"Count: {list.Count}");
                            }
                            break;
                        case 7:
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
            } while (answer != 7);
            
        }
    }
}
