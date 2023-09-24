using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace RepoLayer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollabId { get; set; }
        public string Email { get; set; }

        [ForeignKey("Notes")]
        public int NoteId { get; set; }

        [JsonIgnore]
        public Notes Notes { get; set; }

        [ForeignKey("users")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
