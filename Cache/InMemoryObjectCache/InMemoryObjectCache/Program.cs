using System;
using System.Collections.Generic;

namespace InMemoryObjectCache
{
    using System.Globalization;

    using ObjectCache;

    class Program
    {
        private static void Main(string[] args)
        {
            InMemory myNewCache = new InMemory();

            string strFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "../../XmlFiles/ListOfUsers.xml");
            var lstFiles = new List<string>();
            lstFiles.Add(strFilePath);

            //for (int i = 1; i <= 100; i++)
            //{
            //    string cacheItem1 = "Steve Smith-" + i;
            //    myNewCache.AddToMyCache(i.ToString(CultureInfo.InvariantCulture), cacheItem1, MyCachePriority.NotRemovable, lstFiles);
            //}

            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            for (int i = 1; i <= 250000; i++)
            {
                var car = new Car("Blue" + i, i);
                myNewCache.AddToMyCache(
                    i.ToString(CultureInfo.InvariantCulture), car, MyCachePriority.NotRemovable, lstFiles);
            }

            var myItem10 = myNewCache.GetMyCachedItem("10") as Car;
            var myItem100 = myNewCache.GetMyCachedItem("100") as Car;
            var myItem1000 = myNewCache.GetMyCachedItem("1000") as Car;
            var myItem10000 = myNewCache.GetMyCachedItem("10000") as Car;
            var myItem100000 = myNewCache.GetMyCachedItem("100000") as Car;
            var myItem150000 = myNewCache.GetMyCachedItem("150000") as Car;
            var myItem200000 = myNewCache.GetMyCachedItem("200000") as Car;
            var myItem250000 = myNewCache.GetMyCachedItem("250000") as Car;

            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " (" + myNewCache.Count() + ")");
            while (Console.ReadLine() != "quit")
            {
            }
        }
    }
}
