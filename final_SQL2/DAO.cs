using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace finalSQL
{
    class DAO : IDAO
    {
        SqliteConnection conn;

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static SQLITEAppConfig m_config;


        public DAO()
        {
            m_config = new SQLITEAppConfig();

            conn = new SqliteConnection(m_config.ConnectionString);

        }


        public void AddCars(Cars cars)
        {
            if (m_config.AllowDBWrite)
            {

                try
                {
                    conn.Open();

                    SqliteCommand cmd = new SqliteCommand($"INSERT INTO  Cars (Manufacturer,Model,Year)" +
                $" VALUES('{cars.Manufacturer}', '{cars.Model}','{cars.Year}') ", conn);



               cmd.ExecuteNonQuery();



                    my_logger.Info($"New cars {cars.Model} was added ");
                }
                catch (Exception ex)
                {

                    my_logger.Error($"Failed to add car to data base. Error : {ex}");
                }

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }

            conn.Close();
        }


        public void AddTests(Tests tests)
        {
            if (m_config.AllowDBWrite)
            {

      
               try
                {
                    conn.Open();

                    SqliteCommand cmd = new SqliteCommand($"INSERT INTO  Tests (Car_ID,IsPassed,Tests_Date)" +
                $" VALUES('{tests.Car_ID}', '{tests.IsPassed}','{tests.Tests_Date}') ", conn);


                    cmd.ExecuteNonQuery();



                    my_logger.Info($"New test {tests.ID} was added ");
                }
                catch (Exception ex)
                {

                    my_logger.Error($"Failed to add test to data base. Error : {ex}");
                }

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }

            conn.Close();
        }


        public void DeleteCars(int id)
        {
            if (m_config.AllowDBWrite)
            {

                try
                {
                    conn.Open();

                    SqliteCommand cmd = new SqliteCommand($"DELETE FROM Tests WHERE Car_ID={id}; DELETE FROM Cars WHERE ID={id}; ", conn);
           
                        cmd.ExecuteNonQuery();

                    my_logger.Info($" car with id= {id} was deleted ");
                }
                catch (Exception ex)
                {

                    my_logger.Error($"Failed to delete car. Error : {ex}");

                }
                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }


        }
        public void DeleteCarsAll()
        {
            if (m_config.AllowDBWrite)
            {

                try
                {
                    conn.Open();
                    SqliteCommand cmd = new SqliteCommand($"DELETE FROM Tests; DELETE FROM Cars; ", conn);
            
                        cmd.ExecuteNonQuery();

                        my_logger.Info($" delete all cars");
                }
                catch (Exception ex)
                {

                    my_logger.Error($"Failed to delete cars. Error : {ex}");

                }
                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }


        }

        public void DeleteTests(int id)
        {
            if (m_config.AllowDBWrite)
            {

                try
                {
                    conn.Open();

                    SqliteCommand cmd = new SqliteCommand($"DELETE FROM Tests WHERE ID={id}; ", conn);
                        cmd.ExecuteNonQuery();

                    my_logger.Info($" test with id= {id} was deleted ");
                }
                catch (Exception ex)
                {

                    my_logger.Error($"Failed to delete test. Error : {ex}");

                }
                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }


        }


        public void DeleteTestsAll()
        {
            if (m_config.AllowDBWrite)
            {

                try
                {
                    conn.Open();
                    SqliteCommand cmd = new SqliteCommand($"DELETE FROM Tests; ", conn);


                        cmd.ExecuteNonQuery();

                    my_logger.Info($" delete all tests");
                }
                catch (Exception ex)
                {

                    my_logger.Error($"Failed to delete tests. Error : {ex}");

                }
                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }


        }

      
        public List<Cars> GetCars()
        {
            List<Cars> cars = new List<Cars>();
            conn.Open();

            using (SqliteCommand cmd = new SqliteCommand("SELECT * FROM Cars", conn))
            {

                using (SqliteDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read() == true)
                    {

                        Cars c = new Cars
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Manufacturer = (string)reader["Manufacturer"],
                            Model = (string)reader["Model"],
                            Year = Convert.ToInt32(reader["Year"]),
                          
                        };

                        cars.Add(c);

                    }


                }


            }

            conn.Close();
            my_logger.Info($" Get All cars ");

            return cars;

        }

        public List<Cars> GetCarsFromManufacturer(string Manufacturer)
        {
            List<Cars> cars = new List<Cars>();
            conn.Open();

            using (SqliteCommand cmd = new SqliteCommand($"SELECT * FROM Cars WHERE Manufacturer='{Manufacturer}' ", conn))
            {

                using (SqliteDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read() == true)
                    {

                        Cars c = new Cars
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Manufacturer = (string)reader["Manufacturer"],
                            Model = (string)reader["Model"],
                            Year = Convert.ToInt32(reader["Year"]),

                        };

                        cars.Add(c);

                    }

                }
            }

            conn.Close();
            my_logger.Info($" Get cars from manufacturer ");

            return cars;

        }

        public List<Tests> GetTests()
        {
            List<Tests> tests = new List<Tests>();
            conn.Open();

            using (SqliteCommand cmd = new SqliteCommand("SELECT * FROM Tests", conn))
            {

                using (SqliteDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read() == true)
                    {

                        Tests t = new Tests
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Car_ID = Convert.ToInt32(reader["Car_ID"]),
                            IsPassed = Convert.ToInt32(reader["IsPassed"]),
                            Tests_Date = (string)reader["Tests_Date"],
                            
                        };

                        tests.Add(t);

                    }

                }
            }

            conn.Close();
            my_logger.Info($" Get Tests ");
            return tests;

        }

        public void ShowTestsWithCars()
        {
            conn.Open();

            using (SqliteCommand cmd = new SqliteCommand("SELECT *, C.ID as Cars_ID, C.ID as Tests_ID  FROM Tests T JOIN Cars C on T.Car_ID=C.ID", conn))
            {

                using (SqliteDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read() == true)
                    {

                        var carsAndTests = new 
                        {
                            Cars_ID = Convert.ToInt32(reader["Cars_ID"]),
                            Car_ID = Convert.ToInt32(reader["Car_ID"]),
                            IsPassed = Convert.ToInt32(reader["IsPassed"]),
                            Tests_Date = (string)reader["Tests_Date"],

                            Tests_ID = Convert.ToInt32(reader["Tests_ID"]),
                            Manufacturer = (string)reader["Manufacturer"],
                            Model = (string)reader["Model"],
                            Year = Convert.ToInt32(reader["Year"])
                        };


                         Console.WriteLine(JsonConvert.SerializeObject(carsAndTests, Formatting.Indented));
                    }

                }
            }

            conn.Close();
            Console.WriteLine();
            my_logger.Info($" show tests with cars ");


        }



        public void  UpdateCars(int id, Cars cars)
        { 
            if (m_config.AllowDBWrite)
            {
     
                try
                {
                    conn.Open();

                    SqliteCommand cmd = new SqliteCommand($"UPDATE Cars SET " +
                 $" Manufacturer='{cars.Manufacturer}', Model='{cars.Model}',Year='{cars.Year}'" +
                  $"WHERE ID={id} ", conn);

                    cmd.ExecuteNonQuery();

                    my_logger.Info($"update car- {cars.Model} ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update car. Error : {ex}");
                  
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }



        }

        public void UpdateTests(int id, Tests tests)
        {
            if (m_config.AllowDBWrite)
            {

                try
                {
                    conn.Open();

                    SqliteCommand cmd = new SqliteCommand($"UPDATE Tests SET " +
                  $" Car_ID='{tests.Car_ID}', IsPassed='{tests.IsPassed}',Tests_Date='{tests.Tests_Date}'" +
                  $"WHERE ID={id} ", conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($"update test- {tests.ID} ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update store. Error : {ex}");

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
