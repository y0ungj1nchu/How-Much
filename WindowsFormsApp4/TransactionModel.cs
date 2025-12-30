using System;

namespace WindowsFormsApp4
{
    public class TransactionModel
    {
        public int TxId { get; set; }
        public int MethodId { get; set; }
        public int SubId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public string Type { get; set; } // 수입 / 지출
    }
}
