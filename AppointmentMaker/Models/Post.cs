using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppointmentMaker.Models
{
    public class Post : BaseModel
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Creator")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }

        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        [JsonIgnore]
        public virtual Topic Topic { get; set; }

        public DateTime? DatumPostavljana { get; set; }

        public string Title { get; set;}
        public string Body { get; set;}

        public string? postImageString { get; set; }
        public List<PostLikes> Likes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
