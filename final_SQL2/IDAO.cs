using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalSQL
{
    interface IDAO
    {
        List<Cars> GetCars();
        void AddCars(Cars cars);
        void UpdateCars(int id, Cars cars);
        void DeleteCars(int id);
        void DeleteCarsAll();

        List<Tests> GetTests();
        void AddTests(Tests tests);
        void UpdateTests(int id, Tests tests);
        void DeleteTests(int id);
        void DeleteTestsAll();


        List<Cars> GetCarsFromManufacturer(string Manufacturer);
        void ShowTestsWithCars();

    }
}
