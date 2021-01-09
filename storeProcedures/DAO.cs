using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storeProcedures
{
    class DAO:IDAO
    {

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static storeProceduresAppConfig m_config;

        NpgsqlConnection conn;

        public DAO()
        {

            m_config = new storeProceduresAppConfig();
            conn = new NpgsqlConnection(m_config.ConnectionString);




        }
       public float Run_sp_AvgSalaryForRoleId(int id)
        {
            string sp_name = "avg_salary_for_role_id";

            try
            {
                
                    conn.Open();
                 

                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure; 

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("chosen_roled_id", id)
                    });

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                    float avg = Convert.ToInt64( reader["avg"]);
                        // ....
                        return avg;
                    }
                    my_logger.Error($"Function not returned value!");
                
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get avg salary for roleId. Error : {ex}");
                my_logger.Error($"Run_sp_AvgSalaryForRoleId" +
                    $": [{sp_name}]");
                return 0;
            }

            return 0;

        }

    }
}
