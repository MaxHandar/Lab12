using ClassLibraryLab10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab12_4
{
    public static class VehicleExtensions
    {
        // Метод для определения коэффициента в зависимости от наличия полного привода
        public static double AllWDCoefficient(this OffRoad offRoad)
        {
            return offRoad.AllWD ? 1.0 : 0.5;
        }

        // Метод для "выпрямления" очереди списков автомобилей в один перечень
        public static IEnumerable<Vehicle> Flatten(this Queue<List<Vehicle>> vehicleQueue)
        {
            return vehicleQueue.SelectMany(vehicleList => vehicleList);
        }

        // Метод для выборки внедорожников с информацией о типе бездорожья
        public static IEnumerable<OffRoadInfo> SelectOffRoadVehicles(this IEnumerable<Vehicle> vehicles)
        {
            return vehicles.OfType<OffRoad>()
                           .Select(offRoad => new OffRoadInfo
                           {
                               Name = offRoad.Brand,
                               Price = offRoad.Price,
                               GroundType = offRoad.GroundType
                           });
        }

        // Метод для выборки автомобилей с расчетом соотношения цена-качество
        public static IEnumerable<VehiclePriceQuality> SelectVehiclesWithPriceQualityRatio(this IEnumerable<Vehicle> vehicles)
        {
            return vehicles.OfType<OffRoad>()
                           .Select(offRoad => new VehiclePriceQuality
                           {
                               Name = offRoad.Brand,
                               ID = offRoad.id.Number,
                               PriceQualityRatio = (offRoad.Price / 500) / offRoad.AllWDCoefficient()
                           })
                           .OrderByDescending(item => item.PriceQualityRatio);
        }

        // Метод для группировки по типу бездорожья
        public static IEnumerable<IGrouping<string, OffRoad>> GroupByGroundType(this IEnumerable<Vehicle> vehicles)
        {
            return vehicles.OfType<OffRoad>()
                           .GroupBy(offRoad => offRoad.GroundType);
        }

        // Метод для соединения автомобилей с группами
        public static IEnumerable<VehicleGroupInfo> JoinWithGroups(this IEnumerable<Vehicle> vehicles, IEnumerable<Group> groups)
        {
            return vehicles.Join(groups,
                                 vehicle => vehicle.Brand,
                                 group => group.Brand,
                                 (vehicle, group) => new VehicleGroupInfo
                                 {
                                     Name = vehicle.Brand,
                                     Country = group.Country,
                                     ID = vehicle.id.Number,
                                 });
        }
    }

    // Классы для возвращаемых типов методов расширения
    public class OffRoadInfo
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string GroundType { get; set; }
    }

    public class VehiclePriceQuality
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public double PriceQualityRatio { get; set; }
    }

    public class VehicleGroupInfo
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int ID { get; set; }
    }

    public class Group 
    {
        Random rnd = new Random();
        public string Country { get; set; }
        public string Brand { get; set; }
        string[] countries = { "Россия", "Беларусь", "Франция", "Китай", "Япония", "Германия", "Корея"};
        
        public Group()
        {
            Country = countries[rnd.Next(countries.Length)];
        } 
        public Group(string brand, string country)
        {
            Brand = brand;
            Country = country;
        }
    }

    public class Program
    {

        Random rnd = new Random();
        public static List<Vehicle> MakeCollection(int length, Random rnd)
        {
            var list = new List<Vehicle>();
            for (int i = 0; i < length; i++)
            {
                
                OffRoad offRoad = new OffRoad();
                offRoad.RandomInit(rnd);
                list.Add(offRoad);
            }
            return list;
        }
        public static void PrintCollection(List<Vehicle> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Empty collection");
                return;
            }
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Queue<List<Vehicle>> vehicleQueue = new Queue<List<Vehicle>>();
            

            // Создаем несколько списков с автомобилями
            List<Vehicle> list1 = MakeCollection(10, rnd);
            List<Vehicle> list2 = MakeCollection(10, rnd);
            List<Vehicle> list3 = MakeCollection(10, rnd);
            list1.Add(new PassengerCar("Лада", 2000, "yellow", 100, 100, 100, 4, 100));
            list2.Add(new PassengerCar("Лада", 2000, "yellow", 101, 101, 101, 4, 101));
            list3.Add(new PassengerCar("Лада", 2000, "yellow", 102, 102, 102, 4, 102));

            // Добавляем списки в очередь
            vehicleQueue.Enqueue(list1);
            vehicleQueue.Enqueue(list2);
            vehicleQueue.Enqueue(list3);

            Console.WriteLine("Full factory");
            //var flatvehicles = vehicleQueue.flatten();
            var res1Extension = vehicleQueue
                .SelectMany(vehicle => vehicle)
                .Select(vehicle => vehicle);
            var result1Linq = from workshop in vehicleQueue
                              from vehicle in workshop
                              select vehicle;
            foreach (var item in result1Linq)
            {
                Console.WriteLine(item);
            }

            //проекция
            //var offRoadVehicles = flatVehicles.SelectOffRoadVehicles();
            var res2Extensions = vehicleQueue       //расширение
                .SelectMany(vehicle => vehicle)
                .OfType<OffRoad>()
                .Select(vehicle => new
                {
                    Name = vehicle.Brand,
                    Price = vehicle.Price,
                    GroundType = (vehicle as OffRoad).GroundType
                });
            var result2Linq = from workshop in vehicleQueue     //линку
                              from vehicle in workshop
                              where vehicle is OffRoad
                              select new { Name = vehicle.Brand, Price = vehicle.Price,
                                  GroundType = (vehicle as OffRoad).GroundType };
            Console.WriteLine("\nВнедорожники с типом бездорожья");
            foreach (var item in result2Linq)
            {
                Console.WriteLine(item);
            }
            //foreach (var item in res2Extensions)
            //{
            //    Console.WriteLine(item);
            //}

            double isAllWD(OffRoad offRoad)
            {
                if (offRoad.AllWD)
                    return 1.0;
                else return 0.5;
            }
            //доп вычисления
            //var vehiclesWithRatio = flatVehicles.SelectVehiclesWithPriceQualityRatio();
            //foreach (var item in vehiclesWithRatio)
            //{
            //    Console.WriteLine("---");
            //    Console.WriteLine(item);
            //    Console.WriteLine("---");
            //}
            var res3Extensions = vehicleQueue             //расширение
                .SelectMany(vehicle => vehicle)
                .OfType<OffRoad>()
                .Select(vehicle => new
                {
                    Vehicle = vehicle,
                    PriceQualityRatio = (vehicle.Price / 500) / isAllWD(vehicle)
                })
                .OrderByDescending(item => item.PriceQualityRatio)
                .Select(item => new
                {
                    Name = item.Vehicle.Brand,
                    ID = item.Vehicle.id.Number,
                    item.PriceQualityRatio
                });
            var result3Linq = from workshop in vehicleQueue //линку
                              from vehicle in workshop
                              where vehicle is OffRoad
                              let priceQualityRatio = (vehicle.Price/500) / isAllWD((OffRoad)vehicle) 
                              orderby priceQualityRatio descending
                              select new { Name = vehicle.Brand, ID = vehicle.id.Number, priceQualityRatio };
            Console.WriteLine("\nВнедорожники с соотношением цена-качество");
            foreach (var item in result3Linq)
            {
                Console.WriteLine(item);
            }
            //foreach (var item in res3Extensions)
            //{
            //    Console.WriteLine(item);
            //}

            //group
            //var groupedByGroundType = flatVehicles.GroupByGroundType();
            var res4Extensions = vehicleQueue.SelectMany(x => x) //расширение
                .Where(x => x is OffRoad)
                .GroupBy(vehicle => ((OffRoad)vehicle).GroundType);
            var result4Linq = from workshop in vehicleQueue             //линку
                       from vehicle in workshop
                       where vehicle is OffRoad
                       group vehicle by ((OffRoad)vehicle).GroundType;
            Console.WriteLine("\nGroup");
            foreach (IGrouping<string, Vehicle> group in result4Linq)
            {
                Console.WriteLine(group.Key + " : " + group.Count());
                foreach (var elem in group)
                    Console.WriteLine(elem);
            }
            

            List<Group> groups = new List<Group>();
            Group g1 = new Group("Газель", "Россия");
            Group g2 = new Group("Пежо", "Франция");
            Group g3 = new Group("Лада", "Россия");
            Group g4 = new Group("Ауди", "Германия");
            Group g5 = new Group("Фольцваген", "Германия");
            groups.Add(g1);
            groups.Add(g2);
            groups.Add(g3);
            groups.Add(g4);
            groups.Add(g5);

            //join
            //var joinedWithGroups = flatVehicles.JoinWithGroups(groups);
            var res5Extension = vehicleQueue.SelectMany(workshop => workshop)
                       .Join(groups,
                             vehicle => vehicle.Brand,
                             t => t.Brand,
                             (vehicle, t) => new
                             {
                                 Name = vehicle.Brand,
                                 Country = t.Country,
                                 ID = vehicle.id.Number,
                             });
            var res5 = from workshop in vehicleQueue
                       from vehicle in workshop
                       join t in groups on vehicle.Brand equals t.Brand
                       select new
                       {
                           Name = vehicle.Brand,
                           Country = t.Country,
                           ID = vehicle.id.Number,
                       };
            Console.WriteLine("\nJoin");
            foreach (var item in res5)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n\nFrom Lab12:");
            Console.WriteLine("Full workshop");
            MyObservableCollection<Vehicle> workShop = new MyObservableCollection<Vehicle>(10);
            var res1Ext = workShop.Select(vehicle => vehicle);
            var res1Linq = from vehicle in workShop
                           select vehicle;
            foreach (var item in res1Ext)
                Console.WriteLine(item);

            Console.Write("\nTruck count = ");
            var res2Ext = workShop.Select(vehicle => vehicle)
                .OfType<Truck>()
                .Count();
            var res2Linq = (from vehicle in workShop
                            where vehicle is Truck
                            select vehicle).Count();
            Console.WriteLine(res2Ext);
            //Console.WriteLine(res2Linq);

            //group
            var res3Ext = workShop.Select(x => x) //расширение
                .GroupBy(vehicle => (vehicle).Brand);
            var res3Linq = from vehicle in workShop//линку
                           group vehicle by vehicle.Brand;
            Console.WriteLine("\nGroup");
            foreach (IGrouping<string, Vehicle> group in res3Ext)
            {
                Console.WriteLine(group.Key + " : " + group.Count());
                foreach (var elem in group)
                    Console.WriteLine(elem);
            }

            Console.Write("PassengerCar Average MaxSpeed = ");
            var res4Ext = workShop.Select(x => x)
                .OfType<PassengerCar>()
                .Select(x => x.MaxSpeed)
                .Average();
            var res4Linq = (from vehicle in workShop
                           where vehicle is PassengerCar
                           select ((PassengerCar)vehicle).MaxSpeed).Average();
            Console.WriteLine(res4Ext);
            //Console.WriteLine(res4Lin);

        }
    }
}
