using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Albmer.Models
{

    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public string Name;
        public string Type;
        public string Image;
        public string BeginYear;
        public string EndYear;
        public string Origin;
				public string Genre;

        public ICollection<Album> Albums;
    }
}
