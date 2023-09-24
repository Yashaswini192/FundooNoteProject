using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepoLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int LabelId { get; set; }

        public string LabelName { get; set; }

        [ForeignKey("users")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User users { get; set; }

        [ForeignKey("Notes")]
        public int NoteId { get; set; }
        [JsonIgnore]
        public Notes Notes { get; set; }

    }
}
