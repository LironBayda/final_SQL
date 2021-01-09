using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_SERVER
{
    class DAO : IDAO
    {

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static SQL_SERVER_AppConfig m_config;

        SqlConnection conn;

        public DAO()
        {

            m_config = new SQL_SERVER_AppConfig();
             conn = new SqlConnection(m_config.ConnectionString);




        }

        public void AddCategories(string category)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"INSERT Categories([CategoryName]) VALUES('{category}'); " +
                    $"SELECT SCOPE_IDENTITY();";

                try
                {
                     conn.Open();
                    SqlCommand cmd = new SqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();
                    cmd.CommandType = CommandType.Text;
                    Decimal result = Convert.ToDecimal(cmd.ExecuteScalar());

                    my_logger.Info($"New category {category} was added with id {result}");
                }
                catch (Exception ex)
                {

                    my_logger.Error($"Failed to add category to data base. Error : {ex}");
                    my_logger.Error($"AddCategories: [{query}]");
                }

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }

            conn.Close();
        }



        public void AddStores(Stores stores)
        {
            if (m_config.AllowDBWrite)
            {

                string query = "INSERT Stores([StoresName],[Store_Floor],[category_ID]) " +
                    $"VALUES('{stores.StoresName}',{stores.Store_Floor},{stores.category_ID}); " +
                    "SELECT SCOPE_IDENTITY();";

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();
                    cmd.CommandType = CommandType.Text;
                    Decimal result = Convert.ToDecimal(cmd.ExecuteScalar());
                    cmd.Connection.Close();

                    my_logger.Info($"New stores- {stores.StoresName} was added with id {result}");
                }
                catch (Exception ex)
                {

                    my_logger.Error($"Failed to add store to data base. Error : {ex}");
                    my_logger.Error($"AddStores: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }


        }

        public void DeleteCategories(int id)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"DELETE FROM Stores WHERE category_ID={id};DELETE FROM Categories WHERE ID={id}; ";
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($" category with id= {id} was deleted ");
                }
                catch (Exception ex)
                {

                    my_logger.Error($"Failed to delete category. Error : {ex}");
                    my_logger.Error($"DeleteCategories: [{query}]");

                }
                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }


        }

        public void DeleteCategoriesAll()
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"DELETE FROM Stores; DELETE FROM Categories; ";

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($" delete all feom Categories ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to delete Categories. Error : {ex}");
                    my_logger.Error($"DeleteCategoriesAll: [{query}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }


        }

        public void DeleteStores(int id)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"DELETE FROM Stores WHERE ID={id}; ";

                try
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($"Stores with id= {id} was deleted ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to delete Stores. Error : {ex}");
                    my_logger.Error($"DeleteStores: [{query}]");
                }
                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }

        }


        public void DeleteStoresAll()
        {
            if (m_config.AllowDBWrite)
            {

                string query = $" DELETE FROM Stores; ";

                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($" delete all from Stores ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to delete Stores. Error : {ex}");
                    my_logger.Error($"DeleteStoresAll: [{query}]");
                }
                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }


        }

        public List<Categories> GetAllCategories()
        {
            conn.Open();
            List<Categories> list = new List<Categories>();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Categories", conn))
            {

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);


                while (reader.Read() == true)
                {
                    list.Add(
                        new Categories
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            CategoryName = reader["CategoryName"].ToString()
                        });


                }
                my_logger.Info($" Get All Categories ");





            }
            conn.Close();

            return list;
        }


        public List<Stores> GetAllStores()
        {

            conn.Open();
            List<Stores> list = new List<Stores>();


            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Stores", conn))
            { 

            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);


                while (reader.Read() == true)
                {
                    list.Add(
                        new Stores
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            StoresName = reader["StoresName"].ToString(),
                            Store_Floor = Convert.ToInt32(reader["Store_Floor"]),
                            category_ID = Convert.ToInt32(reader["category_ID"])

                        });



                }
                my_logger.Info($" Get All Stores ");

            }
            conn.Close();

            return list;
        }

        public List<object> GetAllStoresWithCategories()
        {

            conn.Open();
            List<object> list = new List<object>();


            using (SqlCommand cmd = new SqlCommand("SELECT *,s.ID as store_id, c.id as category_id FROM Stores s join Categories c " +
                 "on s.category_ID=c.id ", conn)) 

            { 
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);


                while (reader.Read() == true)
                {
                    list.Add(
                        new
                        {
                            store_id = Convert.ToInt32(reader["store_id"]),
                            StoresName = reader["StoresName"].ToString(),
                            Store_Floor = Convert.ToInt32(reader["Store_Floor"]),
                            category_ID = Convert.ToInt32(reader["category_ID"]),
                            CategoryName = reader["CategoryName"].ToString()


                        });


                }

                my_logger.Info($" Get All stores with Categories ");

            }
            conn.Close();

            return list;
        }

        public Categories GetByIdCategories(int id)
        {
            Categories category = null;
            string query = $"SELECT * FROM Categories where Id={id}";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query,
                        conn);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);


                reader.Read();
                
                     category=
                        new Categories
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            CategoryName = reader["CategoryName"].ToString()
                        };


                my_logger.Info($"get categorey with id= {id} ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get categorey. Error : {ex}");
                my_logger.Error($"GetByIdCategories: [{query}]");
            }

            conn.Close();

            return category;

        }

        public Stores GetByIdStores(int id)
        {
            Stores store = null;
            string query = $"SELECT * FROM Stores where Id={id}";
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query,
                        conn);


                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);


                reader.Read();

                store =
                   new Stores
                   {
                       ID = Convert.ToInt32(reader["ID"]),
                       StoresName = reader["StoresName"].ToString(),
                       Store_Floor = Convert.ToInt32(reader["Store_Floor"]),
                       category_ID = Convert.ToInt32(reader["category_ID"])
                   };


                my_logger.Info($"get store with id= {id} ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get store. Error : {ex}");
                my_logger.Error($"GetByIdStores: [{query}]");
            }


            conn.Close();

            return store;

        }

        public List<Stores> GetSoresFromFloorAndCategories(int category_id, int floor)
        {
            string query = $"SELECT * from Stores where Store_Floor={floor} " +
                $"AND category_ID={category_id} ";

            List<Stores> list = new List<Stores>();

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query,
                        conn);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);


                while (reader.Read() == true)
                {
                    list.Add(

                   new Stores
                   {
                       ID = Convert.ToInt32(reader["ID"]),
                       StoresName = reader["StoresName"].ToString(),
                       Store_Floor = Convert.ToInt32(reader["Store_Floor"]),
                       category_ID = Convert.ToInt32(reader["category_ID"])
                   });

                    my_logger.Info($"get stores with floor= {floor} and category id= {category_id} ");
                }

            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get stores. Error : {ex}");
                my_logger.Error($"GetSoresFromFloorAndCategories: [{query}]");
            }


            conn.Close();

            return list;

        }


        public void ShowCategoriesWithMaxStores()
        {
            string query = $"with  temp as"+
            "(select count(s.[category_ID]) as sumOfStoresPerCategory, c.[CategoryName], " +
                " c.[ID] from[dbo].[Categories] c join[dbo].[Stores] s " +
              "on c.[id] = s.[category_ID] group by c.[CategoryName], c.[ID]) " +
              "select[CategoryName], [ID] from temp " +
               "where sumOfStoresPerCategory = (select max(sumOfStoresPerCategory) from temp);";
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query,
                        conn);


                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                // I use while for the case there is more then one category with the same number of stores
                Console.WriteLine("Categories with max stores:");

                while (reader.Read() == true)
                {
                    Console.WriteLine($"category name: {reader["CategoryName"]} category id: {reader["ID"]} "); 
                }



                my_logger.Info($"get Categories with max stores ");
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Categories with max stores. Error : {ex}");
                my_logger.Error($"GetByIdStores: [{query}]");
            }


            conn.Close();

        }

        public void UpdateCategories(int id, Categories category)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"update Categories set CategoryName='{category.CategoryName}' where id={id};";


                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($"update Categories  set CategoryName= '{category.CategoryName}' where id={id} ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update category. Error : {ex}");
                    my_logger.Error($"UpdateCategories: [{query}]");
                }
                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }


        }

        public void UpdateStores(int id, Stores stores)
        {
            if (m_config.AllowDBWrite)
            {

                string query = $"update Stores set [StoresName]='{stores.StoresName}', " +
                    $"[Store_Floor]={stores.Store_Floor}, [category_ID]={stores.category_ID} where id={id}";
                

                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query,
                            conn);
                    cmd.ExecuteNonQuery();

                    my_logger.Info($"update store- {stores.StoresName} ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update store. Error : {ex}");
                    my_logger.Error($"UpdateStores: [{query}]");
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
