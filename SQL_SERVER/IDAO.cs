using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_SERVER
{
    interface IDAO
    {

        List<Categories> GetAllCategories();
        Categories GetByIdCategories(int id);
        void AddCategories(string category);
        void UpdateCategories(int id, Categories category);
        void DeleteCategories(int id);
        void DeleteCategoriesAll();

        List<Stores> GetAllStores();
        Stores GetByIdStores(int id);
        void AddStores(Stores stores);
        void UpdateStores(int id, Stores stores);
        void DeleteStores(int id);
        void DeleteStoresAll();

        List<object> GetAllStoresWithCategories();
        List<Stores> GetSoresFromFloorAndCategories(int category_id, int floor);
        void ShowCategoriesWithMaxStores();

    }
}
