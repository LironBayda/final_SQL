using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_SERVER
{
    class Stores
    {
        public int ID { get; set; }
        public string StoresName { get; set; }
        public int Store_Floor { get; set; }
        public int category_ID { get; set; }

        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Stores()
        {

        }


        public static bool operator ==(Stores stores1, Stores stores2)
        {


            if (object.ReferenceEquals(stores1, null) && object.ReferenceEquals(stores2, null))
                return true;
            if (object.ReferenceEquals(stores1, null) || object.ReferenceEquals(stores2, null))
                return false;

            return stores1.ID == stores2.ID;
        }

        public static bool operator !=(Stores stores1, Stores stores2)
        {
            return !(stores1 == stores2);
        }

        public override bool Equals(object obj)
        {
            Stores stores = obj as Stores;
            if (stores != null)
                return this.ID == stores.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

    }
}
