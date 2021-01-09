using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL
{
    class Program
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static PostgresSQL_AppConfig m_config;


        static void Main(string[] args)
        {



           my_logger.Info("******************** System startup");

           m_config = new PostgresSQL_AppConfig();

            string m_conn = m_config.ConnectionString;

            Console.WriteLine($"-- Hello App {m_config.AppName}");

            Actors actor1 = new Actors
            {
                ID = 11,
                Name = "adi chen",
                BirtDate = "13.04.1956"

            };

            Actors actor2 = new Actors
            {
                ID = 12,
                Name = "adi levi",
                BirtDate = "13.04.1966"

            };

            Geners gener1 = new Geners
            {
                ID = 9,
                Name = "horror"
            };


            Geners gener2 = new Geners
            {
                ID = 9,
                Name = "france"
            };

            Movies movie1 = new Movies
            {
                Name = "fast and angery",
                genre_ID = 1,
                ID = 9,
                releaseDate = "12.05.2000"

            };

            Movies movie2 = new Movies
            {
                Name = "rain man",
                genre_ID = 2,
                ID = 19,
                releaseDate = "12.05.2011"

            };

            Movies_Actors movies_actors1 = new Movies_Actors
            {
                ID=33,
                Actor_ID=21,
                Movie_ID=1
            };

            Movies_Actors movies_actors2 = new Movies_Actors
            {
                ID = 34,
                Actor_ID = 14,
                Movie_ID = 2
            };

            DAO dAO = new DAO();

             dAO.AddActors(actor1);
             dAO.AddGeneres(gener1);
             dAO.AddMovies(movie1);
             dAO.AddMoviesWithActors(movies_actors1);



            Console.WriteLine("GetActorsById: ");
            Console.WriteLine( dAO.GetActorsById(3).ToString());
            Console.WriteLine();




            Console.WriteLine("GetMoviesById: ");
            Console.WriteLine(dAO.GetMoviesById(3).ToString());
            Console.WriteLine();


            Console.WriteLine("GetMoviesWithActorsById: ");
            Console.WriteLine(dAO.GetMoviesWithActorsById(51).ToString());
            Console.WriteLine();


            Console.WriteLine("GetGeneresById: ");
            Console.WriteLine(dAO.GetGeneresById(3).ToString());
            Console.WriteLine();

            Console.WriteLine("GetAllGeneres: ");
            dAO.GetAllGeneres().ForEach(s => Console.WriteLine(s.ToString()));
            Console.WriteLine();

            Console.WriteLine("GetAllActors: ");
            dAO.GetAllActors().ForEach(s => Console.WriteLine(s.ToString()));
            Console.WriteLine();

            Console.WriteLine("GetAllMovies: ");
            dAO.GetAllMovies().ForEach(s => Console.WriteLine(s.ToString()));
            Console.WriteLine();

            Console.WriteLine("GetAllMoviesWithActors: ");
            dAO.GetAllMoviesWithActors().ForEach(s => Console.WriteLine(s.ToString()));
            Console.WriteLine();

            Console.WriteLine("GetAllMoviesWithActorBornBefore1972: ");
            dAO.GetAllMoviesWithActorBornBefore1972().ForEach(s => Console.WriteLine(s.ToString()));
            Console.WriteLine();

     

            Console.WriteLine("GetActorWithMaxMovies: ");
            dAO.GetActorWithMaxMovies().ForEach(s => Console.WriteLine(s.ToString())); ;
            Console.WriteLine();

            Console.WriteLine("GetFristMoviesInEveryYesr: ");
            dAO.GetFristMoviesInEveryYesr().ForEach(s => Console.WriteLine(s.ToString())); ;
            Console.WriteLine();

            Console.ReadLine();
          /*    dAO.UpdateActors(11, actor2);
              dAO.UpdateGeneres(11, gener2);
              dAO.UpdateMovies(11, movie2);
              dAO.UpdateMoviesWithActors(11,movies_actors2);


             dAO.DeleteActors(5);
            dAO.DeleteGeneres(5);
           dAO.DeleteMovies(5);
           dAO.DeleteMoviesWithActors(5);*/

           my_logger.Info("******************** System shutdown");


        }
    }
}
