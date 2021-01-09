using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalSQL
{
    class Cars
    {
        public int ID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
       
        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Cars()
        {

        }


        public static bool operator ==(Cars cars1, Cars cars2)
        {


            if (object.ReferenceEquals(cars1, null) && object.ReferenceEquals(cars2, null))
                return true;
            if (object.ReferenceEquals(cars1, null) || object.ReferenceEquals(cars2, null))
                return false;

            return cars1.ID == cars2.ID;
        }

        public static bool operator !=(Cars cars1, Cars cars2)
        {
            return !(cars1 == cars2);
        }

        public override bool Equals(object obj)
        {
            Cars cars = obj as Cars;
            if (cars != null)
                return this.ID == cars.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

    }
}
