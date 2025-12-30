using System.Data;

namespace WindowsFormsApp4
{
    public static class TransactionCalculator
    {
        public static (decimal income, decimal expense) Calculate(DataView view)
        {
            decimal income = 0, expense = 0;

            foreach (DataRowView row in view)
            {
                decimal amt = (decimal)row["금액"];
                string type = row["수입지출"].ToString();

                if (type == "지출") expense += amt;
                else income += amt;
            }

            return (income, expense);
        }
    }
}
