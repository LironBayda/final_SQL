using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL
{
    class Geners
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Geners()
        {

        }


        public static bool operator ==(Geners geners1, Geners geners2)
        {


            if (object.ReferenceEquals(geners1, null) && object.ReferenceEquals(geners2, null))
                return true;
            if (object.ReferenceEquals(geners1, null) || object.ReferenceEquals(geners2, null))
                return false;

            return geners1.ID == geners2.ID;
        }

        public static bool operator !=(Geners geners1, Geners geners2)
        {
            return !(geners1 == geners2);
        }

        public override bool Equals(object obj)
        {
            Geners geners = obj as Geners;
            if (geners != null)
                return this.ID == geners.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

    }
}
