using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12_3
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
            MyTree<Vehicle> tree = new MyTree<Vehicle>(0);
            MyTree<Vehicle> findTree = new MyTree<Vehicle>(0);
            int answer = 0;
            do
            {
                try
                {
                    answer = ShowMenu("Menu:", "Неправильно введен пункт меню",
                "Сформировать ИСД",
                "Распечататть полученное дерево",
                "Найти макс. элемент в дереве по году",
                "Преобразовать ИСД в дерево поиска",
                "Распечатать дерево поиска",
                //"Удалить из дерева элемент с заданным ключом",
                "Удалить дерево из памяти",
                "Завершить работу"
                );
                    switch (answer)
                    {
                        case 1:
                            int size = InputData.NumInput("Размер коллекции?", true);
                            tree = new MyTree<Vehicle>(size);
                            findTree = new MyTree<Vehicle>(0);
                            Console.WriteLine("дерево создано");
                            break;
                        case 2:
                            if (tree.Count == 0)
                                Console.WriteLine("дерево пустое");
                            else
                                tree.ShowTree();
                            break;
                        case 3:
                            if (tree.Count == 0)
                                Console.WriteLine("дерево пустое");
                            else if (findTree.Count == 0)
                                Console.WriteLine("Max elem. - " + tree.FindMax());
                            else
                                Console.WriteLine("Max elem. - " + findTree.FindMax());
                            break;
                        case 4:
                            if (tree.Count == 0)
                                Console.WriteLine("исходное дерево пустое");
                            else
                            {
                                findTree = tree.TransformToFindTree();
                                Console.WriteLine("дерево создано");
                            }
                            break;
                        case 5:
                            if (findTree.Count == 0)
                                Console.WriteLine("дерево пустое");
                            else 
                                findTree.ShowTree();
                            break;
                        case 6:
                            if (tree.Count == 0)
                                Console.WriteLine("дерево пустое");
                            else
                            {
                                tree = new MyTree<Vehicle>(0);
                                findTree = new MyTree<Vehicle>(0);
                                Console.WriteLine("Память освобождена");
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