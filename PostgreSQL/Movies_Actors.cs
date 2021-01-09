using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL
{
    class Movies_Actors
    {
        public int ID { get; set; }
        public int Movie_ID { get; set; }
        public int Actor_ID { get; set; }

        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Movies_Actors()
        {

        }


        public static bool operator ==(Movies_Actors movies_Actors1, Movies_Actors movies_Actors2)
        {


            if (object.ReferenceEquals(movies_Actors1, null) && object.ReferenceEquals(movies_Actors2, null))
                return true;
            if (object.ReferenceEquals(movies_Actors1, null) || object.ReferenceEquals(movies_Actors2, null))
                return false;

            return movies_Actors1.ID == movies_Actors2.ID;
        }

        public static bool operator !=(Movies_Actors movies_Actors1, Movies_Actors movies_Actors2)
        {
            return !(movies_Actors1 == movies_Actors2);
        }

        public override bool Equals(object obj)
        {
            Movies_Actors movies_Actors = obj as Movies_Actors;
            if (movies_Actors != null)
                return this.ID == movies_Actors.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

    }
}
