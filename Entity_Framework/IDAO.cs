using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework
{
    interface IDAO
    {
        List<City> GetAllCity();
        List<City> GetByIdCity(int id);
        void AddCity(City city);
        void UpdateCity(int id, City city);
        void DeleteCity(int id);
        void DeleteCityAll();

        List<District> GetAllDistrict();
        List<District> GetByIdDistrict(int id);
        void AddDistrict(District district);
        void UpdateDistrict(int id, District district);
        void DeleteDistrict(int id);
        void DeleteDistrictsAll();

        List<City> cityWithPopulationBiggerThanMin(int min);
        void FillDistrictPopulation(int districtId);
        List<City> GetAllCityQuerySyntax ();



    }
}
