using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;
using System.Data.SqlClient;

namespace WpfLab04
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-1OE2ISC\SQLEXPRESS; 
                    Initial Catalog=School;Integrated Security=True;");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            
        }

      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>();
            conn.Open();
            SqlCommand cmd = new SqlCommand("FindPerson", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param1 = new SqlParameter();
            param1.SqlDbType = SqlDbType.VarChar;
            param1.Size = 50;
            //param1.Value = txtFirstName.Text.Trim(); 
            param1.Value = "";
            param1.ParameterName = "@FirstName";

            SqlParameter param2 = new SqlParameter();
            param2.SqlDbType = SqlDbType.VarChar;
            param2.Size = 50;
            //param2.Value = txtLastName.Text.Trim(); 
            param2.Value = "";
            param2.ParameterName = "@LastName";

            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Person person = new Person
                {
                    PersonId = dr["PersonID"] != DBNull.Value ? (string)dr["PersonID"] : string.Empty,
                    LastName = dr["LastName"] != DBNull.Value ? (string)dr["LastName"] : string.Empty,
                    FirstName = dr["FirstName"] != DBNull.Value ? (string)dr["FirstName"] : string.Empty,
                    //FullName = dr["FirstName"].ToString(),
                    FullName = string.Concat(dr["FirstName"].ToString(), " ",
                     dr["LastName"].ToString())
                    //Age = DateDiff(DateTime.Now- Convert.ToDateTime( dr[2].ToString()))
                };
                people.Add(person);



                
            }
            conn.Close();
            dgvPeople.ItemsSource = people;
        }

        private void dgvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
