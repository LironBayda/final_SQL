using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalSQL
{
    class Tests
    {
        public int ID { get; set; }
        public int Car_ID { get; set; }
        public int IsPassed { get; set; }
        public string Tests_Date { get; set; }
        

        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Tests()
        {

        }


        public static bool operator ==(Tests tests1, Tests tests2)
        {


            if (object.ReferenceEquals(tests1, null) && object.ReferenceEquals(tests2, null))
                return true;
            if (object.ReferenceEquals(tests1, null) || object.ReferenceEquals(tests2, null))
                return false;

            return tests1.ID == tests2.ID;
        }

        public static bool operator !=(Tests tests1, Tests tests2)
        {
            return !(tests1 == tests2);
        }

        public override bool Equals(object obj)
        {
            Tests tests = obj as Tests;
            if (tests != null)
                return this.ID == tests.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

    }

}

