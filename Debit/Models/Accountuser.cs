using System.Text.Json.Serialization;

namespace Debit.Models
{
    public partial class Accountuser
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
