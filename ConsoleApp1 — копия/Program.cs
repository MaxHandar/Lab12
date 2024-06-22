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
        static void Main(string[] args)
        {
            var collection1 = new MyObservableCollection<Vehicle>(3);
            var collection2 = new MyObservableCollection<Vehicle>(4);
            Console.WriteLine($"c1: Count={collection1.Count}");
            foreach (Vehicle v1 in collection1)
            {
                Console.WriteLine(v1);
            }
            Console.WriteLine($"c2: Count={collection2.Count}");
            foreach (Vehicle v2 in collection2)
            {
                Console.WriteLine(v2);
            }

            var journal1 = new Journal();
            var journal2 = new Journal();

            collection1.Name = "collection1";
            collection2.Name = "collection2";

            // Подписка на события
            collection1.CollectionCountChanged += journal1.CollectionChanged;
            collection1.CollectionReferenceChanged += journal1.CollectionChanged;
            

            collection1.CollectionReferenceChanged += journal2.CollectionChanged;
            collection2.CollectionReferenceChanged += journal2.CollectionChanged;

            // Изменения в коллекциях
            var item1 = new Vehicle();
            var item2 = new Vehicle();

            Random rnd = new Random();
            item1.RandomInit(rnd);
            item2.RandomInit(rnd);

            collection1.Add(item1);
            collection1[0] = item2;
            collection1.Remove(collection1[collection1.Count-1]);

            collection2.Add(item1);
            collection2[0] = item2;
            collection2.Remove(collection2[collection2.Count-1]);
            
            Console.WriteLine("\nResult:");
            Console.WriteLine($"c1: Count={collection1.Count}");
            foreach (Vehicle v1 in collection1)
            {
                Console.WriteLine(v1);
            }
            Console.WriteLine($"c2: Count={collection2.Count}");
            foreach (Vehicle v2 in collection2)
            {
                Console.WriteLine(v2);
            }
            // Вывод данных журналов
            Console.WriteLine("\nJournals:");
            Console.WriteLine("1: ");
            journal1.PrintJournal();
            Console.WriteLine("2: ");
            journal2.PrintJournal();
        }
    }
}
