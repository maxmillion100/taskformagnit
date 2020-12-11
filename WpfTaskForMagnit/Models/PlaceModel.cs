using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTaskForMagnit.Models
{
    [Table(Name = "place")]
    public class PlaceModel
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int ColumnCountt { get; set; }
        [Column]
        public int RowCountt { get; set; }
        [Column]
        public int NumberOfGames { get; set; }
    }
}
