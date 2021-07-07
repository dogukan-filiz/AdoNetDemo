using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo
{
    public class ProductDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=ETrade;integrated security=true");
        public List<Product> GetAll()
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("SELECT * FROM Products", _connection);
            var reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    StockAmount = (int)reader["StockAmount"],
                    UnitPrice = (decimal)reader["UnitPrice"]
                };
                products.Add(product);
            }

            reader.Close();
            _connection.Close();

            return products;
        }
        public void Add(Product product)
        {
            ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Products values(@name,@unitPrice,@stockAmount)", _connection);
            sqlCommand.Parameters.AddWithValue("@name", product.Name);
            sqlCommand.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            sqlCommand.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            sqlCommand.ExecuteNonQuery();

            _connection.Close();
        }
        public void Update(Product product)
        {
            ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Update Products Set Name = @name, UnitPrice = @unitPrice, StockAmount = @stockAmount Where Id = @id", _connection);
            sqlCommand.Parameters.AddWithValue("@name", product.Name);
            sqlCommand.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            sqlCommand.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            sqlCommand.Parameters.AddWithValue("@id", product.Id);
            sqlCommand.ExecuteNonQuery();

            _connection.Close();
        }
        public void Delete(Product product)
        {
            ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Delete from Products Where Id = @id", _connection);
            sqlCommand.Parameters.AddWithValue("@id", product.Id);
            sqlCommand.ExecuteNonQuery();

            _connection.Close();
        }
        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public DataTable GetAll2()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

            SqlCommand command = new SqlCommand("SELECT * FROM T_ILCELER", _connection);
            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            reader.Close();
            _connection.Close();

            return dataTable;
        }

    }
}
