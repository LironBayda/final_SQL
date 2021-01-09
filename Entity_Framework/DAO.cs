using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework
{
    class DAO : IDAO
    {
        public void AddCity(City city)
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                se.Cities.Add(city);
                se.SaveChanges();

            };
        }

        public void AddDistrict(District district)
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                se.Districts.Add(district);
                se.SaveChanges();


            };
        }

        public List<City> cityWithPopulationBiggerThanMin(int min)
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                return se.Cities.Where(c => c.Population > min).ToList();



            };
        }

        public void DeleteCity(int id)
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                se.Cities.Remove(se.Cities.First(_ => _.ID == id));

                se.SaveChanges();


            };
        }

        public void DeleteCityAll()
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                se.Database.ExecuteSqlCommand
                   ($"delete from Cities ");
                se.SaveChanges();

            };
        }

        public void DeleteDistrict(int id)
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

    
                se.Districts.Remove(se.Districts.First(_ => _.ID == id));

                se.SaveChanges();

            };
        }

        public void DeleteDistrictsAll()
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                se.Database.ExecuteSqlCommand
                  ($"delete from [dbo].[Cities];" +
                  $"delete from [dbo].[Districts]");
                se.SaveChanges();

            };
        }

        public void FillDistrictPopulation(int  districtId)
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                int districtPopulation =se.Cities.Where(c => c.District.ID==districtId).Sum(c => c.Population).Value;
                se.Database.ExecuteSqlCommand
                    ($"update [dbo].[Districts] set [Population]={districtPopulation} where [ID]={districtId}");
                se.SaveChanges();

            };
        }

        public List<City> GetAllCity()
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                return se.Cities.ToList();
            
            };
        }

        public List<City> GetAllCityQuerySyntax()
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {
                var results = from s in se.Cities
                              select s;
                return results.ToList();



            };
            
        }

        public List<District> GetAllDistrict()
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                return se.Districts.ToList();

            };
        }

        public List<City> GetByIdCity(int id)
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                return se.Cities.Where(c => c.ID == id).ToList();

            };
        }

        public List<District> GetByIdDistrict(int id)
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                return se.Districts.Where(c => c.ID == id).ToList();

            };
        }

        public void UpdateCity(int id, City city)
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                city.ID = id;
                se.Cities.AddOrUpdate(city);
                se.SaveChanges();


            };
        }

        public void UpdateDistrict(int id, District district)
        {
            using (finalSQLEntities se = new finalSQLEntities())
            {

                district.ID = id;
                se.Districts.AddOrUpdate(district);
                se.SaveChanges();


            };
        }
    }
}
