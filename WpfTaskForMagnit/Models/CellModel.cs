using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTaskForMagnit.Models
{
    [Table(Name = "forcell")]
    public class CellModel
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "ColumnOfCell")]
        public int Column { get; set; }
        [Column(Name = "RowOfCell")]
        public int Row { get; set; }
        [Column(Name = "ValueOfCell")]
        public int ValueOfCell { get; set; }
        [Column(Name = "Sosedi")]
        public int Sosedi { get; set; }
        [Column(Name = "NumberOfGame")]
        public int NumberOfGame { get; set; }
        //[Column(Name = "Id")]
        //public int IdOFSosed { get; set; }
    }
}
