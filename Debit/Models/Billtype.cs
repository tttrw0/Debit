using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Debit.Models
{
    public partial class Billtype
    {
        public Billtype()
        {
            Bill = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual ICollection<Bill> Bill { get; set; }
    }
}
