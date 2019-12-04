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
        public string Title { get; set; }
        public string Image { get; set; }
        public string Date { get; set; }
        public int TrackCount { get; set; }
        public string Genre { get; set; }
        public string AllMusic { get; set; }
        public string Discogs { get; set; }
        public string RateYourMusic { get; set; }


        public List<ArtistAlbum> ArtistAlbum { get; } = new List<ArtistAlbum>();
    }
}
