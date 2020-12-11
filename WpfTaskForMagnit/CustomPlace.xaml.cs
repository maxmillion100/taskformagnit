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
    /// Логика взаимодействия для CustomPlace.xaml
    /// </summary>
    public partial class CustomPlace : Page
    {
        public CustomPlace(int column, int qrow)
        {
            InitializeComponent();

            

            object[] forRows = new object[column];
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

            for (int i = 0; i < column; i++)
            {
                forRows[i] = "";

            }

            for (int i = 0; i < qrow; i++)
            {
                DataRow dataRow = booksTable.NewRow();
                dataRow.ItemArray = forRows;
                booksTable.Rows.Add(dataRow);

            }

            for(int i = 0; i < qrow; i++)
            {
                for(int b = 0; b < column; b++)
                {
                    DataContext db = new DataContext(connectionString);

                    Table<CellModel> cellModels = db.GetTable<CellModel>();

                    Table<NumberOfGame> numbers = db.GetTable<NumberOfGame>();
                    var number = numbers.ToList();
                    var ab = number.Count();
                    List<CellModel> cellModels1 = cellModels.ToList();

                    CellModel cell1 = new CellModel { Column=b,Row = i, /*ValueOfCell = "",*/ NumberOfGame = ab };


                    db.GetTable<CellModel>().InsertOnSubmit(cell1);
                    db.SubmitChanges();
                }
            }

            if (cellModels != null)
            {
                foreach (var p in cellModels)
                {
                    booksTable.Rows[p.Row][p.Column] = 1;
                }
            }
            

            dt = booksTable;
            gridEmployees.DataContext = booksTable.DefaultView;
            _column = column;
            _qrow = qrow;

            var list = booksTable.Select().SelectMany(row => row.ItemArray).Select(x => (string)x).ToList();
        }

        public static DataTable dt = new DataTable();

        public static int _column;
        public static int _qrow;

        public int column;
        public int qrow;

        public int columnCell;
        public int qrowCell;

        public static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public  static List<CellModel> cellModels = new List<CellModel>();
        //private void ButtonNext_OnClick(object sender, RoutedEventArgs e)
        //{
        //    App.CurrentApp.ProductModel.TextColumn = textForColumn.Text;

        //    App.CurrentApp.ProductModel.TextRow = textForRow.Text;
        //    column = int.Parse(App.CurrentApp.ProductModel.TextColumn);
        //    qrow = int.Parse(App.CurrentApp.ProductModel.TextRow);
        //    NavigationService.Navigate(new CustomPlace(column, qrow));
        //}
        
        private void ButtonSubmitCell_OnClick(object sender, RoutedEventArgs e)
        {
            DataTable booksTable = new DataTable();

            booksTable = dt;


            App.CurrentApp.ProductModel.TextColumnCell = textForColumnCell.Text;

            App.CurrentApp.ProductModel.TextRowCell = textForRowCell.Text;
            columnCell = int.Parse(App.CurrentApp.ProductModel.TextColumnCell);
            qrowCell = int.Parse(App.CurrentApp.ProductModel.TextRowCell);
            // NavigationService.Navigate(new CustomPlace(column, qrow));



            CellModel cell = new CellModel();

            
            //p.ValueOfCell = 1;
            dt = booksTable;

            DataContext db = new DataContext(connectionString);

            Table<CellModel> cellModels = db.GetTable<CellModel>();

            Table<NumberOfGame> numbers = db.GetTable<NumberOfGame>();
            var number = numbers.ToList();
            var ab = number.Count();
            List<CellModel> cellModels1 = cellModels.ToList();

            CellModel cell1 = new CellModel { Column = columnCell, Row = qrowCell, ValueOfCell = 1, NumberOfGame = ab };

            CellModel cellfordelete = new CellModel();

            booksTable.Rows[qrowCell][columnCell] = cell1.ValueOfCell;
            var listNeedCell = db.GetTable<CellModel>().ToList();

            var numberss = (from t in listNeedCell where t.NumberOfGame == ab&&t.Column==columnCell&&t.Row==qrowCell select t).ToList();
            //db.GetTable<CellModel>().Select()

            


            foreach (var p in numberss)
            {
              
                cellfordelete = p;
            }

            db.GetTable<CellModel>().DeleteOnSubmit(cellfordelete);

            db.GetTable<CellModel>().InsertOnSubmit(cell1);
            db.SubmitChanges();



            gridEmployees.DataContext = booksTable.DefaultView;


            var list = booksTable.Select().SelectMany(row => row.ItemArray).Select(x => (string)x).ToList();

            
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
        private void ButtonStart_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Start(dt,cellModels, _column, _qrow));
        }
        //	booksTable.Rows[0][2] = 300
    }
}
