using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Debit.Models
{
    public partial class User
    {
        public User()
        {
            Accountuser = new HashSet<Accountuser>();
            Bill = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public decimal MoneyAmount { get; set; }
        public bool IsShare { get; set; }
        public long CreateDate { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual ICollection<Accountuser> Accountuser { get; set; }
        [JsonIgnore]
        public virtual ICollection<Bill> Bill { get; set; }
    }
}
