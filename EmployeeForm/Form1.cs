using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace EmployeeForm
{
    public partial class Form1 : Form
    {
        SqlConnection _conn = null;
        //SqlConnection _conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString());
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void EstablishConnection()
        {
            _conn = new SqlConnection();
            _conn.ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Lucy;Integrated Security=True;Encrypt=False";
            if(_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _command = "insert into employees(EmployeeName,Salary,IsActive,CreatedBy,CreatedDate) Values ('"+ textBox1.Text +"','"+ textBox2.Text +"','1','Manual',GETDATE())";
            EstablishConnection();
            SqlCommand sqlCommand = new SqlCommand(_command, _conn);
            int result = sqlCommand.ExecuteNonQuery();
            if(result == 1)
            {
                MessageBox.Show("Inserted record");
            }
            CloseConnection(); 
        }


        private void CloseConnection()
        {
            if(_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _command = "Select EmployeeName,Salary from Employees";
            EstablishConnection();
            SqlCommand sqlCommand = new SqlCommand(_command, _conn);
            //SqlDataReader dataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlCommand.ExecuteReader());
            dataGridView1.DataSource = dt;
            CloseConnection();
        }
    }
}
