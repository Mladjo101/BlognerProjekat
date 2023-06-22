using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppointmentMaker.Models
{
    public class PostLikes
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Creator")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        [JsonIgnore]
        public virtual Post Post { get; set; }
    }
}
