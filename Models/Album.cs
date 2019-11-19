using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Albmer.Models
{
    public class Album
    {
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public string Title;
        public string Image;
        public string Date;
	    public int TrackCount;
        public string Genre;

        ICollection<Artist> Artists;
    }
}
