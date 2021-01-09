using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL
{
    class Actors
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string BirtDate { get; set; }


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Actors()
        {

        }


        public static bool operator ==(Actors actors1, Actors actors2)
        {


            if (object.ReferenceEquals(actors1, null) && object.ReferenceEquals(actors2, null))
                return true;
            if (object.ReferenceEquals(actors1, null) || object.ReferenceEquals(actors2, null))
                return false;

            return actors1.ID == actors2.ID;
        }

        public static bool operator !=(Actors actors1, Actors actors2)
        {
            return !(actors1 == actors2);
        }

        public override bool Equals(object obj)
        {
            Actors actors = obj as Actors;
            if (actors != null)
                return this.ID == actors.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

    }
}
