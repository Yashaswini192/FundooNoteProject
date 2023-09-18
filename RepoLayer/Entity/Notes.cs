using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepoLayer.Entity
{
    
    public class Notes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Remainder { get; set; }

        public string BgColor { get; set; }

        public string Image {get ; set; }

        public bool Archive { get; set; }

        public bool PinNote { get; set; }

        public bool Trash { get; set; }

        [ForeignKey("users")]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User users { get; set;}
        
    }
}
