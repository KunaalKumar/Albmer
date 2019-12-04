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
        public string Name { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string BeginYear { get; set; }
        public string EndYear { get; set; }
        public string Origin { get; set; }
        public string Genre { get; set; }

        public List<ArtistAlbum> Albums { get; set; }
    }
}
