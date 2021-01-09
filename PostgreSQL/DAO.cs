using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL
{
    class DAO : IDAO
    {
            

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static PostgresSQL_AppConfig m_config;

        NpgsqlConnection conn;

        public DAO()
        {

            m_config = new PostgresSQL_AppConfig();
            conn = new NpgsqlConnection(m_config.ConnectionString);

        }

   

        public void AddActors(Actors actors)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"insert  into actors (name,birth_date) " +
                    $"values ('{actors.Name}','{actors.BirtDate}'); SELECT LASTVAL();";

                try
                {
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    cmd.CommandType = CommandType.Text;
                    Decimal result = Convert.ToDecimal(cmd.ExecuteScalar());

                    

                    my_logger.Info($"New actor {actors.Name} was added with id {result}");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to add category to data base. Error : {ex}");
                    my_logger.Error($"AddActors: [{query}]");
                }
                conn.Close();
            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

        public void AddGeneres(Geners geners)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"insert  into generes (name) " +
                    $"values ('{geners.Name}'); SELECT LASTVAL();";

                try
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();
                    cmd.CommandType = CommandType.Text;
                    Decimal result = Convert.ToDecimal(cmd.ExecuteScalar());

                    my_logger.Info($"New gener {geners.Name} was added  with id {result}");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to add gener to data base. Error : {ex}");
                    my_logger.Error($"AddGeneres: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

        public void AddMovies(Movies movies)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"insert  into movies (name, release_date, genre_id) " +
                    $"values ('{movies.Name}','{movies.releaseDate}',{movies.genre_ID});" +
                    $" SELECT LASTVAL();";

                try
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();
                    cmd.CommandType = CommandType.Text;
                    Decimal result = Convert.ToDecimal(cmd.ExecuteScalar());

                    my_logger.Info($"New movie {movies.Name} was added  with id {result}");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to add movie to data base. Error : {ex}");
                    my_logger.Error($"AddMovies: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

        public void AddMoviesWithActors(Movies_Actors movies_actors)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"insert  into movies_actors (movie_id,actor_id ) " +
                    $"values ({movies_actors.Movie_ID},{movies_actors.Actor_ID}); " +
                    $" SELECT LASTVAL();";

                try
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();
                    cmd.CommandType = CommandType.Text;
                    Decimal result = Convert.ToDecimal(cmd.ExecuteScalar());

                    my_logger.Info($"New movies_actors was added  with id {result}");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to add movies_actors to data base. Error : {ex}");
                    my_logger.Error($"AddMoviesWithActors: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }


        public void DeleteActors(int id)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"DELETE FROM movies_actors WHERE actor_id={id};" +
                    $"DELETE FROM actors WHERE ID={id}; ";

                try
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($" actor with id= {id} was deleted ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to delete actor. Error : {ex}");
                    my_logger.Error($"DeleteActors: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

        public void deleteAllDataInDatabase()
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"DELETE FROM movies_actors;" +
                    $"DELETE FROM movies;" +
                    $"DELETE FROM actors;" +
                    $"DELETE FROM generes ;";

                try
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($"delete all ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to delete. Error : {ex}");
                    my_logger.Error($"deleteAllDataInDatabase: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

        public void DeleteGeneres(int id)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"with movie_to_delete as (select id from movies where  genre_id={ id}) " +
                    $"delete from movies_actors where movie_id in (select  id from movies); " +
                    $"delete from movies where genre_id={id}; " +
                    $"delete from generes where id={id}; ";

                try
                {
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($" genere with id= {id} was deleted ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to delete genere. Error : {ex}");
                    my_logger.Error($"DeleteGeneres: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

        public void DeleteMovies(int id)
        {
            if (m_config.AllowDBWrite)
            {
                string query = $"DELETE FROM movies_actors WHERE movie_id={id}; " +
                                    $"DELETE FROM movies WHERE id={id}; ";

                try
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($" movie with id= {id} was deleted ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to delete movie. Error : {ex}");
                    my_logger.Error($"DeleteMovies: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

        public void DeleteMoviesWithActors(int id)
        {
            if (m_config.AllowDBWrite)
            {
                string query = $"DELETE FROM movies_actors WHERE id={id};";
                                   

                try
                {
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($" movies_actors with id= {id} was deleted ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to delete movies_actors. Error : {ex}");
                    my_logger.Error($"DeleteMoviesWithActors: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }


        public Actors GetActorsById(int id)
        {
            Actors actor  = null;
            string query = $"SELECT * FROM actors where id={id}";
            try
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    actor =
                   new Actors
                   {
                       ID = Convert.ToInt32(reader["id"]),
                       Name = reader["name"].ToString(),
                       BirtDate = reader["birth_date"].ToString()
                   };
                }
                cmd.Connection.Close();

                my_logger.Info($"get actor with id= {id} ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get actor. Error : {ex}");
                my_logger.Error($"GetActorsById: [{query}]");
            }

            conn.Close();

            return actor;

        }

        public List<Actors> GetActorWithMaxMovies()
        {
            List<Actors> list = new List<Actors>();
            string query = $"with temp as (select  a.name as actor_name,a.birth_date ,a.id, "+
                        "COUNT(movie_id) OVER(PARTITION BY a.name) AS count_of_movies "+
                        "from actors a join movies_actors ma on a.id = ma.actor_id join movies m on ma.movie_id = m.id) "+
                        "select actor_name, birth_date, id from temp group by actor_name,birth_date,id , count_of_movies "+
                        "having count_of_movies = (select max(count_of_movies) from temp); ";
            try
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(
                       new Actors
                       {
                           ID = Convert.ToInt32(reader["id"]),
                           Name = reader["actor_name"].ToString(),
                           BirtDate = reader["birth_date"].ToString()
                       });
                }
                cmd.Connection.Close();

                my_logger.Info($"get actors with max movie ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get actors. Error : {ex}");
                my_logger.Error($"GetActorWithMaxMovies: [{query}]");
            }

            conn.Close();


            return list;
        }

        public List<Actors> GetAllActors()
        {
            List<Actors> list = new List<Actors>();
            string query = $"SELECT * FROM actors";
            try
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();

                while( reader.Read())
                {
                    list.Add(
                       new Actors
                       {
                           ID = Convert.ToInt32(reader["id"]),
                           Name = reader["name"].ToString(),
                           BirtDate = reader["birth_date"].ToString()
                       });
                }
                cmd.Connection.Close();

                my_logger.Info($"get all actors ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get actors. Error : {ex}");
                my_logger.Error($"GetAllActors: [{query}]");
            }

            conn.Close();

            return list;

        }

        public List<Geners> GetAllGeneres()
        {
            List<Geners> list = new List<Geners>();
            string query = $"SELECT * FROM generes";
            try
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(
                       new Geners
                       {
                           ID = Convert.ToInt32(reader["id"]),
                           Name = reader["name"].ToString()
                       });
                }
                cmd.Connection.Close();

                my_logger.Info($"get all generes ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get generes. Error : {ex}");
                my_logger.Error($"GetAllGeneres: [{query}]");
            }

            conn.Close();

            return list;

        }

        public List<Movies> GetAllMovies()
        {
            List<Movies> list = new List<Movies>();
            string query = $"SELECT * FROM movies";
            try
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(
                       new Movies
                       {
                           ID = Convert.ToInt32(reader["id"]),
                           Name = reader["name"].ToString(),
                           releaseDate = reader["release_date"].ToString(),
                           genre_ID = Convert.ToInt32(reader["genre_id"])
                       });
                }
                cmd.Connection.Close();

                my_logger.Info($"get all movies ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get movies. Error : {ex}");
                my_logger.Error($"GetAllMovies: [{query}]");
            }

            conn.Close();


            return list;

        }

        public List<Movies> GetAllMoviesWithActorBornBefore1972()
        {
            List<Movies> list = new List<Movies>();
            string query = $"with  actors_with_movies as(select m.name as movis_name,m.id,m.release_date,a.birth_date, m.genre_id " +
                           "from actors a join movies_actors ma on a.id = ma.actor_id join movies m on ma.movie_id = m.id) "+
                           "select movis_name, id, release_date,genre_id from actors_with_movies " +
                           " where birth_date < CAST('01.01.1972' as date) "+
                            " group by movis_name,id,release_date,genre_id; ";
            try
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("here");
                    list.Add(
                       new Movies
                       {
                           ID = Convert.ToInt32(reader["id"]),
                           Name = reader["movis_name"].ToString(),
                           releaseDate = reader["release_date"].ToString(),
                           genre_ID = Convert.ToInt32(reader["genre_id"])
                       });
                }

                my_logger.Info($"get all getAll movies with actor born before 1972 ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get movies. Error : {ex}");
                my_logger.Error($"GetAllMoviesWithActorBornBefore1972: [{query}]");
            }


            conn.Close();

            return list;

        }

        public List<Movies_Actors> GetAllMoviesWithActors()
        {
            List<Movies_Actors> list = new List<Movies_Actors>();
            string query = $"SELECT * FROM movies_actors";
            try
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(
                       new Movies_Actors
                       {
                           ID = Convert.ToInt32(reader["id"]),
                           Actor_ID = Convert.ToInt32(reader["actor_id"]),
                           Movie_ID = Convert.ToInt32(reader["movie_id"])

                       });
                }
                cmd.Connection.Close();

                my_logger.Info($"get all movies_actors ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get movies_actors. Error : {ex}");
                my_logger.Error($"GetAllMoviesWithActors: [{query}]");
            }

            conn.Close();

            return list;

        }

       

        public List<Movies> GetFristMoviesInEveryYesr()
        {
            List<Movies> list = new List<Movies>();
            string query = $"with temp as(with moviesTableOrderByRelease_date as (select * from movies order by release_date) "+
                           "select name, release_date, id,genre_id, " +
                            "ROW_NUMBER () OVER(PARTITION BY(select substr(cast(release_date AS text), 1, 4))) as placeInTheYear "+
                            "from moviesTableOrderByRelease_date) "+
                            "select name, release_date,genre_id, id from temp where placeInTheYear = 1 ";
            try
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(
                       new Movies
                       {
                           ID = Convert.ToInt32(reader["id"]),
                           Name = reader["name"].ToString(),
                           releaseDate = reader["release_date"].ToString(),
                           genre_ID = Convert.ToInt32(reader["genre_id"])
                       });
                }
                cmd.Connection.Close();

                my_logger.Info($"get all movies ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get movies. Error : {ex}");
                my_logger.Error($"GetAllMovies: [{query}]");
            }

            conn.Close();

            return list;

        }

        public Geners GetGeneresById(int id)
        {
            Geners gener = null;
            string query = $"SELECT * FROM generes where Id={id}";
            try
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    gener =
                   new Geners
                   {
                       ID = Convert.ToInt32(reader["id"]),
                       Name = reader["name"].ToString()
                   };
                }
                cmd.Connection.Close();

                my_logger.Info($"get gener with id= {id} ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get gener. Error : {ex}");
                my_logger.Error($"GetGeneresById: [{query}]");
            }


            conn.Close();

            return gener;

        }

        public Movies GetMoviesById(int id)
        {
            Movies movie = null;
            string query = $"SELECT * FROM movies where id={id}";
            try
            {

                conn.Open();
     

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {


                    movie =
                       new Movies
                       {
                           ID = Convert.ToInt32(reader["id"]),
                           Name = reader["name"].ToString(),
                           releaseDate = reader["release_date"].ToString(),
                           genre_ID = Convert.ToInt32(reader["genre_id"])
                       };

                }
                cmd.Connection.Close();

                my_logger.Info($"get movie with id= {id} ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get movie. Error : {ex}");
                my_logger.Error($"GetMoviesById: [{query}]");
            }

            conn.Close();


            return movie;

        }


        public Movies_Actors GetMoviesWithActorsById(int id)
        {
            Movies_Actors movies_actors = null;
            string query = $"SELECT * FROM movies_actors where id={id}";
            try
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query,
                        conn);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    movies_actors =
                   new Movies_Actors
                   {
                       ID = Convert.ToInt32(reader["id"]),
                       Actor_ID = Convert.ToInt32(reader["actor_id"]),
                       Movie_ID = Convert.ToInt32(reader["movie_id"])

                   };
                }
                cmd.Connection.Close();

                my_logger.Info($"get movies_actors with id= {id} ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get movies_actors. Error : {ex}");
                my_logger.Error($"GetMoviesWithActorsById: [{query}]");
            }
            conn.Close();


            return movies_actors;

        }


        public void UpdateActors(int id, Actors actors)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"update actors set name='{actors.Name}',birth_date='{actors.BirtDate}' where id= {id}";


                try
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($"update actor- {actors.Name} ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update actor. Error : {ex}");
                    my_logger.Error($"UpdateActors: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }


        public void UpdateGeneres(int id, Geners geners)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"update generes set name='{geners.Name}' where id= {id}";


                try
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($"update gener= '{geners.Name}' ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update gener. Error : {ex}");
                    my_logger.Error($"UpdateGeneres: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

            public void UpdateMovies(int id, Movies movies)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"update movies set name='{movies.Name}', " +
                    $" release_date='{movies.releaseDate}', genre_id={movies.genre_ID}  " +
                    $" where id= {id}";


                try
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($"update movie= {movies.Name} ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update movie. Error : {ex}");
                    my_logger.Error($"UpdateMovies: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

        public void UpdateMoviesWithActors(int id, Movies_Actors movies_actors)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"update movies_actors set actor_id={movies_actors.Actor_ID}, movie_id={movies_actors.Movie_ID}" +
                    $" where id= {id}";


                try
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($"update movies_actors  ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update movies_actors. Error : {ex}");
                    my_logger.Error($"UpdateMoviesWithActors: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

    }
}
