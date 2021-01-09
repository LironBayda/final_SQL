using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_SERVER
{
    class Categories
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
     

        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Categories()
        {

        }


        public static bool operator ==(Categories categories1, Categories categories2)
        {


            if (object.ReferenceEquals(categories1, null) && object.ReferenceEquals(categories2, null))
                return true;
            if (object.ReferenceEquals(categories1, null) || object.ReferenceEquals(categories2, null))
                return false;

            return categories1.ID == categories2.ID;
        }

        public static bool operator !=(Categories categories1, Categories categories2)
        {
            return !(categories1 == categories2);
        }

        public override bool Equals(object obj)
        {
            Categories categories = obj as Categories;
            if (categories != null)
                return this.ID == categories.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

    }
}
