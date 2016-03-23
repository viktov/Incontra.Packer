using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Web;

namespace Incontra.Packer.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class, new()
	{
        private string _connectionString;
        protected string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    var connStrings = System.Web.Configuration.WebConfigurationManager.ConnectionStrings;
                    System.Configuration.ConnectionStringSettings connString;
                    if (connStrings.Count > 0)
                    {
                        connString = connStrings["DefaultConnection"];
                        if (connString != null)
                            _connectionString = connString.ConnectionString;
                    }                    
                }
                return _connectionString;
            }
        }

        public BaseRepository()
        {
            
        }
        
        private T FillModel(SqlDataReader reader, T t)
        {
            for (int j = 0; j < reader.FieldCount; j++)
            {
                var columnName = reader.GetName(j);
                var type = reader.GetFieldType(j);
                PropertyInfo p = t.GetType().GetProperty(columnName);
                if (type == typeof(string))
                {
                    p.SetValue(t, reader.GetString(reader.GetOrdinal(columnName)), null);
                }
                if (type == typeof(int))
                {
                    try
                    {
                        p.SetValue(t, reader.GetInt32(reader.GetOrdinal(columnName)), null);
                    }
                    catch
                    {
                        p.SetValue(t, null, null);
                    }
                }
                if (type == typeof(bool))
                {
                    p.SetValue(t, reader.GetBoolean(reader.GetOrdinal(columnName)), null);
                }
                if (type == typeof(float))
                {
                    p.SetValue(t, reader.GetFloat(reader.GetOrdinal(columnName)), null);
                }
                if (type == typeof(double))
                {                    
                    try
                    {                        
                        p.SetValue(t, reader.GetDecimal(reader.GetOrdinal(columnName)), null);
                    }
                    catch
                    {
                        p.SetValue(t, null, null);
                    }                    
                }
                if (type == typeof(Guid))
                {
                    p.SetValue(t, reader.GetGuid(reader.GetOrdinal(columnName)), null);
                }
            }
            return t;
        }

        protected List<T> GetModelList(SqlCommand command)
        {
            List<T> list = new List<T>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T model = FillModel(reader, new T());
                    list.Add(model);
                }
                return list;
            }
        }

        protected T GetModel(SqlCommand command)
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T model = FillModel(reader, new T());
                    return model;
                }
            }
            return null;
        }

		public List<T> GetAll()
		{            
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = String.Format("EXEC [incontra_packer].[p_{0}_GetAll]", typeof(T).Name);
                return GetModelList(command);             
            }            
		}

        public T GetByID(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = String.Format("EXEC [incontra_packer].[p_{0}_GetByID] @ID", typeof(T).Name);
                command.Parameters.AddWithValue("@ID", id);
                return GetModel(command);                
            }            
        }

        public T Insert(T t)
		{
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var command = conn.CreateCommand();

                PropertyInfo[] properties = t.GetType().GetProperties();
                StringBuilder parameters = new StringBuilder();
                foreach (PropertyInfo property in properties)
                {
                    if (!property.Name.Equals("ID") && !property.PropertyType.Name.Contains("List"))
                    {
                        string parameter = String.Format("@{0}", property.Name);
                        parameters.AppendFormat("{0}{1}", !String.IsNullOrEmpty(parameters.ToString()) ? "," : String.Empty, parameter);
                        command.Parameters.AddWithValue(parameter, property.GetValue(t, null) ?? DBNull.Value);
                    }
                }
                command.CommandText = String.Format("EXEC [incontra_packer].[p_{0}_Insert] {1}", typeof(T).Name, parameters.ToString());
                return GetModel(command);
            }
		}

        public T Update(T t)
		{
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var command = conn.CreateCommand();

                PropertyInfo[] properties = t.GetType().GetProperties();
                StringBuilder parameters = new StringBuilder();
                foreach (PropertyInfo property in properties)
                {                    
                    if(!property.GetType().IsGenericType) 
                    {
                        string parameter = String.Format("@{0}", property.Name);
                        parameters.AppendFormat("{0}{1}", !String.IsNullOrEmpty(parameters.ToString()) ? "," : String.Empty, parameter);
                        command.Parameters.AddWithValue(parameter, property.GetValue(t, null));
                    }                    
                }
                command.CommandText = String.Format("EXEC [incontra_packer].[p_{0}_Update] {1}", typeof(T).Name, parameters.ToString());
                return GetModel(command);
            }
		}

        public void Delete(int id)
		{
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = String.Format("EXEC [incontra_packer].[p_{0}_Delete] @ID", typeof(T).Name);
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
		}
	}
}
