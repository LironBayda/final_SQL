using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storeProcedures
{
    class Program
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static storeProceduresAppConfig m_config;


        static void Main(string[] args)
        {



            my_logger.Info("******************** System startup");

            m_config = new storeProceduresAppConfig();


            Console.WriteLine($"-- Hello App {m_config.AppName}");

            DAO dAO = new DAO();
            Console.WriteLine( dAO.Run_sp_AvgSalaryForRoleId(1));
            Console.ReadLine();


            my_logger.Info("******************** System shutdown");

        }
    }
}
