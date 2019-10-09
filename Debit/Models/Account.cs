using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Debit.Models
{
    public partial class Account
    {
        public Account()
        {
            Accountuser = new HashSet<Accountuser>();
            Bill = new HashSet<Bill>();
            Billtype = new HashSet<Billtype>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string NickName { get; set; }
        public long RegisterTime { get; set; }
        [JsonIgnore]
        public virtual ICollection<Accountuser> Accountuser { get; set; }
        [JsonIgnore]
        public virtual ICollection<Bill> Bill { get; set; }
        [JsonIgnore]
        public virtual ICollection<Billtype> Billtype { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> User { get; set; }
    }
}
