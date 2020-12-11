using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
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
using WpfTaskForMagnit.Models;

namespace WpfTaskForMagnit
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page 
    {
        public Page1()
        {
            InitializeComponent();


            

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Открываем подключение
                connection.Open();
                Console.WriteLine("Подключение открыто");
                DataContext db = new DataContext(connectionString);

                Table<CellModel> cellModels = db.GetTable<CellModel>();

                //foreach (var user in cellModels)
                //{
                //    var a = user.Id;
                //    var b = user.Column;
                //    var c = user.Row;
                //    var d = user.ValueOfCell;
                //    var e = user.Sosedi;

                //    Console.WriteLine("{0} \t{1} \t{2}", user.Id, user.Column, user.Row);
                //}

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //finally
            //{
            //    // закрываем подключение
            //    connection.Close();
            //    Console.WriteLine("Подключение закрыто...");
            //}



            DataSet bookStore = new DataSet("BookStore");
            DataTable booksTable = new DataTable("Books");
            // добавляем таблицу в dataset
            bookStore.Tables.Add(booksTable);

            // создаем столбцы для таблицы Books
            DataColumn idColumn = new DataColumn("Id", Type.GetType("System.Int32"));
            idColumn.Unique = true; // столбец будет иметь уникальное значение
            idColumn.AllowDBNull = false; // не может принимать null
            idColumn.AutoIncrement = true; // будет автоинкрементироваться
            idColumn.AutoIncrementSeed = 1; // начальное значение
            idColumn.AutoIncrementStep = 1; // приращении при добавлении новой строки

            for (int i = 0; i < column; i++)
            {
                DataColumn nameOfColumn = new DataColumn(i.ToString(), Type.GetType("System.String"));
                booksTable.Columns.Add(nameOfColumn);
                booksTable.PrimaryKey = new DataColumn[] { booksTable.Columns["Id"] };
            }

            for (int i = 0; i < 10; i++)
            {
                forRows[i] = "1";
            }

            for (int i = 0; i < qrow; i++)
            {
                DataRow dataRow = booksTable.NewRow();
                dataRow.ItemArray = forRows;
                booksTable.Rows.Add(dataRow);
            }

            //gridEmployees.DataContext = booksTable.DefaultView;


            var list = booksTable.Select().SelectMany(row => row.ItemArray).Select(x => (string)x).ToList();
        }
        public static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private void ButtonNext_OnClick(object sender, RoutedEventArgs e)
        {
            App.CurrentApp.ProductModel.TextColumn = textForColumn.Text;
            
            App.CurrentApp.ProductModel.TextRow = textForRow.Text;
            column = int.Parse(App.CurrentApp.ProductModel.TextColumn);
            qrow = int.Parse(App.CurrentApp.ProductModel.TextRow);
            

            

            DataContext db = new DataContext(connectionString);

            Table<NumberOfGame> number = db.GetTable<NumberOfGame>();

            
            NumberOfGame numberOfGame = new NumberOfGame();

            //получение количества элементов в таблице
            var nnnn = number.ToList<NumberOfGame>();

            var ab = nnnn.Count;

            numberOfGame.NumberOfGames = ab + 1;

            NumberOfGame numberOfGame1 = new NumberOfGame { NumberOfGames = (ab + 1) };
            db.GetTable<NumberOfGame>().InsertOnSubmit(numberOfGame1);
            db.SubmitChanges();
            //List<NumberOfGame> cellModels1 = cellModels.ToList();

            NavigationService.Navigate(new CustomPlace(column, qrow));
        }


        public static List<string> GetTabless(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable schema = connection.GetSchema("Tables");
                List<string> TableNames = new List<string>();
                foreach (DataRow row in schema.Rows)
                {
                    TableNames.Add(row[2].ToString());
                }
                return TableNames;
            }
        }



        public static int column = 10;
        public static int qrow = 10;

        object[] forRows = new object[column];


    }
}
