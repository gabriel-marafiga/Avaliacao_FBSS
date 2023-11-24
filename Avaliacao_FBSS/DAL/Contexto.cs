using Dapper;
using log4net;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebGrease;
using LogManager = log4net.LogManager;

namespace Avaliacao_FBSS.DAL
{
    public class Contexto
    {
        public static NpgsqlConnection SimpleDbConnection()
        {
            return new NpgsqlConnection("Server=localhost;Port=5432;User Id=AgileSystem;Password=123;Database=avaliacao;");
        }

        static readonly ILog log = LogManager.GetLogger(typeof(Contexto));
        private static Contexto instance;
        private NpgsqlConnection connection;

        public static Contexto Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Contexto();
                }
                return instance;
            }
        }

        public IDbConnection Connection
        {
            get
            {
                if (connection == null)
                    //connection = new NpgsqlConnection(new AppSettingsReader().GetValue("ConnectionString", typeof(string)).ToString());
                    connection = SimpleDbConnection();
                return connection;
            }
        }

        public static bool CloseDatabase()
        {
            try
            {

                log.Info("Fechando conexão com banco de dados.");
                if (Contexto.Instance.Connection.State == ConnectionState.Open)
                {
                    Contexto.Instance.Connection.Close();
                    log.Info("Conexão fechada com sucesso.");
                    return true;
                }
                else if (Contexto.Instance.Connection.State == ConnectionState.Closed)
                {
                    log.Debug("Conexão já estava fechada.");
                    return true;
                }


            }
            catch (Exception ex)
            {
                log.Error("Erro ao fechar conexão com banco de dados: " + ex.Message);
                return false;
            }

            return false;
        }

        public static bool OpenDatabase()
        {
            try
            {
                if (Contexto.Instance.Connection.State == ConnectionState.Closed)
                {
                    Contexto.Instance.Connection.Open();
                    log.Info("Conexão aberta com sucesso.");
                    return true;
                }
                else if (Contexto.Instance.Connection.State == ConnectionState.Open)
                {
                    log.Info("Conexão já estava aberta.");
                    return true;
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Executa um comando mandado por parametro, sem retornos, indicado para updates, insert´s e deletes.
        /// </summary>
        /// <param name="sql"> comando SQL</param>
        public static void ExecutarQuery(string sql)
        {
            OpenDatabase();
            log.InfoFormat("Status da conexão {0}", Contexto.Instance.Connection.State);
            log.Info("Executando a query: " + sql);
            var comand = Instance.Connection.CreateCommand();
            comand.CommandText = sql;
            var ret = comand.ExecuteReader();
            comand.Dispose();
            ret.Dispose();

            CloseDatabase();
            log.InfoFormat("Status da conexão {0}", Instance.Connection.State);

        }

        public static T ExecutarQueryDapper<T>(string sql)
        {
            using (NpgsqlConnection cnn = SimpleDbConnection())
            {
                cnn.Open();
                log.Info("Executando a query: " + sql);
                log.InfoFormat("Status da conexão {0}", cnn.FullState);
                return cnn.QueryFirstOrDefault<T>(sql);
            }
        }

        public static List<T> ExecutarQueryDapperList<T>(string sql)
        {
            using (NpgsqlConnection cnn = SimpleDbConnection())
            {
                cnn.Open();
                log.Info("Executando a query: " + sql);
                log.InfoFormat("Status da conexão {0}", cnn.FullState);
                return cnn.Query<T>(sql).ToList();
            }
        }
    }
}