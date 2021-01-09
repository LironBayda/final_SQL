using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalSQL
{
    class Program
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static SQLITEAppConfig m_config;


        static void Main(string[] args)
        {



            my_logger.Info("******************** System startup");

            m_config = new SQLITEAppConfig();

            Console.WriteLine($"-- Hello App {m_config.AppName}");

            DAO dAO = new DAO();
            Cars cars1 = new Cars
            {
                ID = 1,
                Manufacturer = "pejo",
                Model = "ppopl",
                Year = 1987
        };

            Cars cars2 = new Cars
            {
                ID = 2,
                Manufacturer = "pejo",
                Model = "gfgdfc234",
                Year = 1966
            };

            Tests tests1 = new Tests
            {
                ID = 1,
                IsPassed = 0,
                Car_ID = 3,
                Tests_Date = "13.05.2010"
            };

            Tests tests2 = new Tests
            {
                ID = 3,
                IsPassed = 1,
                Car_ID = 4,
                Tests_Date = "13.05.2018"
            };

            dAO.AddCars(cars1);
            dAO.AddTests(tests1);
            

            List<Cars> cars = new List<Cars>();
            List<Tests> tests = new List<Tests>();
          
            Console.WriteLine("GetCars:");
            cars = dAO.GetCars();
            cars.ForEach(c => Console.WriteLine(c.ToString()));
            Console.WriteLine();

            Console.WriteLine("GetTests:");

            tests = dAO.GetTests();
            tests.ForEach(t => Console.WriteLine(t.ToString()));
            Console.WriteLine();

            Console.WriteLine("GetCarsFromManufacturer:");

            cars = dAO.GetCarsFromManufacturer("HONDA");
            cars.ForEach(c => Console.WriteLine(c.ToString()));
            Console.WriteLine();

            dAO.UpdateCars(3, cars2);
            dAO.UpdateTests(3, tests2);

            Console.WriteLine("ShowTestsWithCars:");

            dAO.ShowTestsWithCars();

            dAO.DeleteCars(10);
            dAO.DeleteTests(10);


            Console.WriteLine("ShowTestsWithCars:");

            dAO.ShowTestsWithCars();

            dAO.DeleteCarsAll();
            dAO.DeleteTestsAll();
            Console.ReadLine();

            my_logger.Info("******************** System shotdown");
        }
    }
}
