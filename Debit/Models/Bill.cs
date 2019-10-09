using System.Text.Json.Serialization;

namespace Debit.Models
{
    public partial class Bill
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public int? Type { get; set; }
        public bool IsIncome { get; set; }
        public long Date { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Billtype TypeNavigation { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
