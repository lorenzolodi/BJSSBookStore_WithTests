using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BookServiceQA.Support_classes
{
    public class DbManager
    {
        string configvalue = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=BookServiceContext-20140613003603; Integrated Security=True; MultipleActiveResultSets=True; AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\BookService\App_Data\BookServiceContext-20140613003603.mdf";
        
        public void ClearDB()
        {
            ClearTable("Books");
            ClearTable("Authors");
            ClearTable("Environments");
        }

        public void PopulateDB()
        {
            createEnvironment();
            createAuthors();
            createBooks();
        }

        public void ClearTable(string tableName)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = configvalue;
                conn.Open();
                SqlCommand sql = new SqlCommand("DELETE FROM " + tableName, conn);
                var reader = sql.ExecuteReader();
                reader.Close();
                conn.Close();
                conn.Dispose();
            }
        }

        public void createEnvironment()
        {
            runQuery("INSERT INTO Environments (Id, SqlInjected) VALUES (CONVERT(uniqueidentifier,'0C6A36BA-10E4-438F-BA86-0D5B68A2BB15'), 'FALSE')");
        }

        public void createAuthors()
        {
            runQuery("SET IDENTITY_INSERT Authors ON; INSERT INTO Authors (Id, Name, EnvironmentId) VALUES(1, 'Dante Alighieri', CONVERT(uniqueidentifier, '0C6A36BA-10E4-438F-BA86-0D5B68A2BB15')); SET IDENTITY_INSERT Authors OFF;");
            runQuery("SET IDENTITY_INSERT Authors ON; INSERT INTO Authors (Id, Name, EnvironmentId) VALUES(2, 'Giovanni Boccaccio', CONVERT(uniqueidentifier, '0C6A36BA-10E4-438F-BA86-0D5B68A2BB15')); SET IDENTITY_INSERT Authors OFF;");
            runQuery("SET IDENTITY_INSERT Authors ON; INSERT INTO Authors (Id, Name, EnvironmentId) VALUES(3, 'Alessandro Manzoni', CONVERT(uniqueidentifier, '0C6A36BA-10E4-438F-BA86-0D5B68A2BB15')); SET IDENTITY_INSERT Authors OFF;");
        }

        public void createBooks()
        {
            runQuery("SET IDENTITY_INSERT Books ON; INSERT INTO Books(Id, Title, Year, Price, Genre, AuthorId, EnvironmentId) VALUES(1, 'Divina Commedia', 1307, 200, 'Poetry', 1, CONVERT(uniqueidentifier, '0C6A36BA-10E4-438F-BA86-0D5B68A2BB15')); SET IDENTITY_INSERT Books OFF;");
            runQuery("SET IDENTITY_INSERT Books ON; INSERT INTO Books(Id, Title, Year, Price, Genre, AuthorId, EnvironmentId) VALUES(2, 'Decameron', 1351, 100.99, 'Novels', 2, CONVERT(uniqueidentifier, '0C6A36BA-10E4-438F-BA86-0D5B68A2BB15')); SET IDENTITY_INSERT Books OFF;");
            runQuery("SET IDENTITY_INSERT Books ON; INSERT INTO Books(Id, Title, Year, Price, Genre, AuthorId, EnvironmentId) VALUES(3, 'I promessi sposi', 1827, 160, 'Novel', 3, CONVERT(uniqueidentifier, '0C6A36BA-10E4-438F-BA86-0D5B68A2BB15')); SET IDENTITY_INSERT Books OFF;");
        }

        public void runQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = configvalue;
                conn.Open();
                SqlCommand sql = new SqlCommand(query, conn);
                var reader = sql.ExecuteReader();
                reader.Close();
                conn.Close();
                conn.Dispose();
            }
        }

        public bool detach()
        {
            using (var connection = new SqlConnection(configvalue))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = String.Format("exec sp_detach_db '{0}', 'true'", connection.Database);
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch(Exception err)
                {
                    Console.WriteLine("Could not detach" + err.Message);
                    return false;
                }
            }
        }
    }
}
