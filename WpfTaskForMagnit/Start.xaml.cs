using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
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
    /// Логика взаимодействия для Start.xaml
    /// </summary>
    public partial class Start : Page
    {
        public Start(DataTable dt, List<CellModel> cellModels, int column, int qrow)
        {
            InitializeComponent();
            gridEmployees.DataContext = dt.DefaultView;

            _cellModels = cellModels;

            var list = dt.Select().SelectMany(row => row.ItemArray).Select(x => (string)x).ToList();
            dtdt = dt;
            _list = list;
            _column = column - 1;
            _qrow = qrow - 1;
        }
        public static DataTable dtdt = new DataTable();
        public static List<string> _list;
        public List<CellModel> _cellModels = new List<CellModel>();
        private void ButtonAutoStart_OnClick(object sender, RoutedEventArgs e)
        {

        }
        public static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static int _column;
        public static int _qrow;
        public static List<CellModel> listlist = new List<CellModel>();
        private void ButtonSteps_OnClick(object sender, RoutedEventArgs e)
        {

            DataContext dataContext = new DataContext(connectionString);
            Table<CellModel> tableCellModels = dataContext.GetTable<CellModel>();
            Table<NumberOfGame> numberOfGames = dataContext.GetTable<NumberOfGame>();
            var listNumber = numberOfGames.ToList();
            int ab = listNumber.Count();

            var listNeedCell = tableCellModels.ToList();

            var number = (from t in listNeedCell where t.NumberOfGame == ab select t).ToList<CellModel>();
            var donumber = (from t in listNeedCell where t.NumberOfGame == ab select t).ToList<CellModel>();
            //проходимся по всем строкам
            //foreach (DataRow row in dtdt.Rows)
            //{
            //    // получаем все ячейки строки
            //    foreach (DataRow rowrow in dtdt.Rows)
            //    {
            //        // получаем все ячейки строки
            //        var cells = row.ItemArray;

            //        foreach (object cell in cells)

            //    }

            //}



            foreach (var item in number)
            {
                var forcolumn = item.Column;
                var forrow = item.Row;
                var fornumber = item.NumberOfGame;

                //циклы проверки соседей
                //var testColumn1 = p.Column - 1;
                //var testRow1 = p.Row - 1;
                //var testCR = dtdt.Rows[testColumn1][testRow1];




                var testColumn_1 = forcolumn - 1;
                var testRow_1 = forrow - 1;

                var testColumn__1 = forcolumn + 1;
                var testRow__1 = forrow + 1;

                //для ячейки: колонка 0 строка 0, проверка соседей  
                if (testColumn_1 < 0 && testRow_1 < 0)
                {
                    //колонка та же, строка +
                    var testCR__11 = dtdt.Rows[forrow + 1][forcolumn];
                    if (testCR__11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    
                    //колонка +, строка +
                    var testCR__1__1 = dtdt.Rows[forrow + 1][forcolumn + 1];
                    if (testCR__1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //колонка +, строка та же
                    var testCR1__1 = dtdt.Rows[forrow][forcolumn + 1];
                    if (testCR1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                }
                // нулевая колонка, но не нулевая строка, при этом не крайняя колонка или не крайняя строка
                if (testColumn_1 < 0 && testRow_1 >= 0 && testRow__1 <= _qrow)
                {
                    //колонка та же, строка -     1
                    var testCR_11 = dtdt.Rows[forrow - 1][forcolumn];
                    if (testCR_11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //колонка та же, строка +             2
                    var testCR__11 = dtdt.Rows[forrow + 1][forcolumn];
                    if (testCR__11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //колонка +, строка-                3
                    var testCR_1__1 = dtdt.Rows[forrow - 1][forcolumn + 1];
                    if (testCR_1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка та же, колонка +                 4
                    var testCR1__1 = dtdt.Rows[forrow][forcolumn + 1];
                    if (testCR1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка +, колонка +              5
                    var testCR__1__1 = dtdt.Rows[forrow + 1][forcolumn + 1];
                    if (testCR__1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                }
                // строка нулевая, при этом колонка не нулевая, но и не крайняя колонка
                if (testRow_1 < 0 && testColumn_1 >= 0 && testColumn__1 <= _column)
                {
                    //строка та же, колонка -              1
                    var testCR1_1 = dtdt.Rows[forrow][forcolumn - 1];
                    if (testCR1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка +, колонка -                     2
                    var testCR__1_1 = dtdt.Rows[forrow + 1][forcolumn - 1];
                    if (testCR__1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка +, колонка та же                3
                    var testCR__11 = dtdt.Rows[forrow + 1][forcolumn];
                    if (testCR__11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка +, колонка +                    4
                    var testCR__1__1 = dtdt.Rows[forrow + 1][forcolumn + 1];
                    if (testCR__1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка та же, колонка +                 5
                    var testCR1__1 = dtdt.Rows[forrow][forcolumn + 1];
                    if (testCR1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                }
                //крайняя строка, крайняя колонка
                if (testRow__1 > _qrow && testColumn__1 > _column)
                {
                    //строка та же, колонка -
                    var testCR1_1 = dtdt.Rows[forrow][forcolumn - 1];
                    if (testCR1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка -, колонка -
                    var testCR_1_1 = dtdt.Rows[forrow - 1][forcolumn - 1];
                    if (testCR_1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка -, колонка та же
                    var testCR_11 = dtdt.Rows[forrow - 1][forcolumn];
                    if (testCR_11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                }
                // крайняя строка, но не крайняя колонка и не нулевая колонка
                if (testRow__1 > _qrow && testColumn__1 <= _column && testColumn_1 >= 0)
                {
                    //строка та же, колонка -         1
                    var testCR1_1 = dtdt.Rows[forrow][forcolumn - 1];
                    if (testCR1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка -, колонка -               2
                    var testCR_1_1 = dtdt.Rows[forrow - 1][forcolumn - 1];
                    if (testCR_1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка -, колонка та же            3
                    var testCR_11 = dtdt.Rows[forrow - 1][forcolumn];
                    if (testCR_11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка -, колонка +                 4
                    var testCR_1__1 = dtdt.Rows[forrow - 1][forcolumn + 1];
                    if (testCR_1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка та же, колонка +                5
                    var testCR1__1 = dtdt.Rows[forrow][forcolumn + 1];
                    if (testCR1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                }
                //нулевая колонка, крайняя строка
                if (testColumn_1 < 0 && testRow__1 > _qrow)
                {
                    //строка -, колонка та же
                    var testCR_11 = dtdt.Rows[forrow - 1][forcolumn];
                    if (testCR_11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }

                    //строка -, колонка +
                    var testCR_1__1 = dtdt.Rows[forrow - 1][forcolumn + 1];
                    if (testCR_1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();

                    }
                    //строка та же, колонка +
                    var testCR1__1 = dtdt.Rows[forrow][forcolumn + 1];
                    if (testCR1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                }
                //крайняя колонка, нулевая строка
                if (testColumn__1 > _column && testRow_1 < 0)
                {
                    //строка та же, колонка -
                    var testCR1_1 = dtdt.Rows[forrow][forcolumn - 1];
                    if (testCR1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка +, колонка -
                    var testCR__1_1 = dtdt.Rows[forrow + 1][forcolumn - 1];
                    if (testCR__1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка +, колонка та же
                    var testCR__11 = dtdt.Rows[forrow + 1][forcolumn];
                    if (testCR__11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                }
                //крайняя колонка но не нулевая строка и не крайняя строка
                if(testColumn__1 > _column && testRow_1 >= 0 && testRow__1 <= _qrow)
                {
                    //строка -, колонка та же            1
                    var testCR_11 = dtdt.Rows[forrow - 1][forcolumn];
                    if (testCR_11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка -, колонка -                   2
                    var testCR_1_1 = dtdt.Rows[forrow - 1][forcolumn - 1];
                    if (testCR_1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка та же, колонка -                  3
                    var testCR1_1 = dtdt.Rows[forrow][forcolumn - 1];
                    if (testCR1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка +, колонка -                     4
                    var testCR__1_1 = dtdt.Rows[forrow + 1][forcolumn - 1];
                    if (testCR__1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    // строка +, колонка та же                        5
                    var testCR__11 = dtdt.Rows[forrow + 1][forcolumn];
                    if (testCR__11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                }
                if(testColumn_1 >= 0 && testRow_1 >=0 && testColumn__1 <= _column && testRow__1 <= _qrow)
                {
                    //строка -, колонка -
                    var testCR_1_1 = dtdt.Rows[forrow - 1][forcolumn - 1];
                    if (testCR_1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка -, колонка та же
                    var testCR_11 = dtdt.Rows[forrow - 1][forcolumn];
                    if (testCR_11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка -, колонка +
                    var testCR_1__1 = dtdt.Rows[forrow - 1][forcolumn + 1];
                    if (testCR_1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка та же, колонка -
                    var testCR1_1 = dtdt.Rows[forrow][forcolumn - 1];
                    if (testCR1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка та же, колонка +
                    var testCR1__1 = dtdt.Rows[forrow][forcolumn + 1];
                    if (testCR1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка +, колонка -
                    var testCR__1_1 = dtdt.Rows[forrow + 1][forcolumn - 1];
                    if (testCR__1_1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка +, колонка та же
                    var testCR__11 = dtdt.Rows[forrow + 1][forcolumn];
                    if (testCR__11.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                    //строка +, колонка +
                    var testCR__1__1 = dtdt.Rows[forrow + 1][forcolumn + 1];
                    if (testCR__1__1.ToString() != "")
                    {
                        item.Sosedi = item.Sosedi + 1;
                        CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                        cell1.Sosedi = item.Sosedi;
                        dataContext.SubmitChanges();
                    }
                }


                ////////////////////////////////////////////////////////////
                //////////////////////////////////////////
                ////////////////////////////

            }
            var numberfor = (from t in listNeedCell where t.NumberOfGame == ab select t).ToList<CellModel>();
            foreach (var item in numberfor)
            {
                //var itemstring = item.ValueOfCell.ToString();
                if(item.ValueOfCell == 0 && item.Sosedi == 3)
                {
                    dtdt.Rows[item.Row][item.Column] = "1";
                    item.ValueOfCell = 1;
                    CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                    cell1.ValueOfCell = item.ValueOfCell;
                    dataContext.SubmitChanges();
                }
                if(item.ValueOfCell != 0 && (item.Sosedi < 2|| item.Sosedi > 3))
                {
                    dtdt.Rows[item.Row][item.Column] = "";
                    item.ValueOfCell = 0;
                    CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Id == item.Id);
                    cell1.ValueOfCell = item.ValueOfCell;
                    dataContext.SubmitChanges();
                }
            }


            DataTable booksTable = new DataTable();
            //DataContext db = new DataContext(connectionString);
            //Table<CellModel> cellModelTable = db.GetTable<CellModel>();

            //booksTable = cellModelTable;
            //// NavigationService.Navigate(new CustomPlace(column, qrow));
            //CellModel cell = new CellModel();
            //p.ValueOfCell = 1;
            booksTable = dtdt;
            
            gridEmployees.DataContext = booksTable.DefaultView;
            var list = booksTable.Select().SelectMany(row => row.ItemArray).Select(x => (string)x).ToList();

            dtdt = booksTable;
            var qrowsindtdt = dtdt.Rows.Count;
            var columnindtdt = dtdt.Columns.Count;

            for (int i = 0; i < qrowsindtdt; i++)
            {
                for (int b = 0; b < columnindtdt; b++)
                {
                    //dtdt.Rows[b][i] = "";

                    CellModel cell1 = dataContext.GetTable<CellModel>().FirstOrDefault(p => p.Column == b && p.Row == i && p.NumberOfGame == ab);

                    cell1.Sosedi = 0;

                    dataContext.SubmitChanges();


                }
            }



        }
        
    }
}