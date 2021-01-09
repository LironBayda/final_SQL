using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework
{
    class Program
    {
        static void Main(string[] args)
        {
          

            City city1 = new City
            {
                ID = 4,
                District_ID = 1,
                Mayor = "c",
                Population = 943,
                Name = "naharia"

            };

            City city2 = new City
            {
                ID = 5,
                District_ID = 3,
                Mayor = "c",
                Population = 943,
                Name = "herzelia"

            };

            District district1 = new District
            {
                Name = "a"
            };

            District district2 = new District
            {
                Name = "North1"
            };

            DAO dAO = new DAO();


        /*     dAO.AddCity(city1);
              dAO.AddDistrict(district1);
              dAO.GetAllCity().ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c)));
              dAO.GetAllDistrict().ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c)));
              dAO.GetByIdCity(5).ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c)));
              dAO.GetByIdDistrict(1).ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c)));
             Console.ReadLine();*/

         /*   dAO.cityWithPopulationBiggerThanMin(500).ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c)));
            Console.WriteLine();
            dAO.GetAllCityQuerySyntax().ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c)));
            dAO.FillDistrictPopulation(3);
            Console.ReadLine();
            dAO.DeleteDistrict(5);
            dAO.DeleteCity(12);*/

            dAO.UpdateCity(5,city2);
            dAO.UpdateDistrict(1, district2);
        }
    }
}
