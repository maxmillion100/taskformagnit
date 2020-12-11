using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTaskForMagnit.Models
{
    [Table(Name = "numbersofgame")]
    public class NumberOfGame
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name ="NumberOfGames")]
        public int NumberOfGames { get; set; }

    }
}
