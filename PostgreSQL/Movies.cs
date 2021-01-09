using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL
{
    class Movies
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string releaseDate { get; set; }
        public int genre_ID { get; set; }

        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Movies()
        {

        }


        public static bool operator ==(Movies movies1, Movies movies2)
        {


            if (object.ReferenceEquals(movies1, null) && object.ReferenceEquals(movies2, null))
                return true;
            if (object.ReferenceEquals(movies1, null) || object.ReferenceEquals(movies2, null))
                return false;

            return movies1.ID == movies2.ID;
        }

        public static bool operator !=(Movies movies1, Movies movies2)
        {
            return !(movies1 == movies2);
        }

        public override bool Equals(object obj)
        {
            Movies movies = obj as Movies;
            if (movies != null)
                return this.ID == movies.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

    }
}
