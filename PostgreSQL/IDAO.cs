using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL
{
    interface IDAO
    {
          List<Movies_Actors>  GetAllMoviesWithActors();
        List<Movies> GetAllMovies();
        List<Actors> GetAllActors();
        List<Geners> GetAllGeneres();

        Movies_Actors GetMoviesWithActorsById(int id);
        Movies GetMoviesById(int id);
        Actors GetActorsById(int id);
        Geners GetGeneresById(int id);

        void AddMoviesWithActors(Movies_Actors movies_actors);
        void AddMovies(Movies movies);
        void AddActors(Actors actors);
        void AddGeneres(Geners geners);

        void UpdateMoviesWithActors(int id,Movies_Actors movies_actors);
        void UpdateMovies(int id, Movies movies);
        void UpdateActors(int id, Actors actors);
        void UpdateGeneres(int id, Geners geners);

        void DeleteMoviesWithActors(int id);
        void DeleteMovies(int id);
        void DeleteActors(int id);
        void DeleteGeneres(int id );

        void deleteAllDataInDatabase();

        List<Movies> GetAllMoviesWithActorBornBefore1972();
        List<Movies> GetFristMoviesInEveryYesr();

        // the function return list becuase maybe in the top there more then one  actor with max number of movies
        List<Actors> GetActorWithMaxMovies();


    }
}
