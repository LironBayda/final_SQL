using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_SERVER
{
    class Program
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static SQL_SERVER_AppConfig m_config;


        static void Main(string[] args)
        {



            my_logger.Info("******************** System startup");

            m_config = new SQL_SERVER_AppConfig();

      
            Console.WriteLine($"-- Hello App {m_config.AppName}");



            Categories category1 = new Categories
            {
                ID = 3,
                CategoryName = "computer"

            };

            Categories category2 = new Categories
            {
                ID = 4,
                CategoryName = "cellular"

             };

             Stores store1 = new Stores
             {
                    ID = 10,
                   StoresName = "bug",
                    Store_Floor = 2,
                    category_ID = 4

              };

                 Stores store2 = new Stores
                 {
                       ID = 11,
                       StoresName = "yosi cumputers",
                     Store_Floor = 1,
                    category_ID = 3
          
                  };

                DAO dAO = new DAO();
               dAO.AddCategories(category1.CategoryName);
               dAO.AddStores(store1);

               Console.WriteLine("GetAllCategories: ");
               dAO.GetAllCategories().ForEach(c => Console.WriteLine(c.ToString()));
               Console.WriteLine();

               Console.WriteLine("GetAllStores: ");
               dAO.GetAllStores().ForEach(s => Console.WriteLine(s.ToString()));
               Console.WriteLine();

               Console.WriteLine("GetAllStoresWithCategories: ");
               dAO.GetAllStoresWithCategories().ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c, Formatting.Indented).ToString()));
               Console.WriteLine();


               Console.WriteLine("GetByIdCategories: ");
               Console.WriteLine(dAO.GetByIdCategories(1).ToString());
               Console.WriteLine();

               Console.WriteLine("GetByIdStores: ");
               Console.WriteLine(dAO.GetByIdStores(1).ToString());
               Console.WriteLine();



               Console.WriteLine("GetSoresFromFloorAndCategories: ");
               dAO.GetSoresFromFloorAndCategories(1,1).ForEach(c => Console.WriteLine(c.ToString()));
               Console.WriteLine();

               dAO.UpdateCategories(8,category2);

               dAO.UpdateStores(12, store2);

               Console.WriteLine("ShowCategoriesWithMaxStores: ");
               dAO.ShowCategoriesWithMaxStores();
               Console.WriteLine();
               
               Console.ReadLine();


             dAO.DeleteCategories(7);
               dAO.DeleteStores(9);

            my_logger.Info("******************** System shutdown");



        }
    }
}
