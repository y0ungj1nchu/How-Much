using System;
using System.Data;
using Oracle.DataAccess.Client;

namespace WindowsFormsApp4
{
    public class TransactionRepository
    {
        public DataTable GetTransactions(DateTime start, DateTime end)
        {
            string sql =
                "SELECT " +
                "T.TX_ID AS 거래ID, " +
                "T.METHOD_ID AS METHOD_ID_RAW, " +
                "C.CATEGORY_ID AS CAT_ID_RAW, " +
                "S.SUB_ID AS SUB_ID_RAW, " +
                "TO_CHAR(T.TX_DATE, 'YYYY-MM-DD') AS 날짜, " +
                "M.NAME AS 거래수단, " +
                "S.NAME AS 카테고리, " +
                "T.AMOUNT AS 금액, " +
                "T.MEMO AS 메모, " +
                "CASE WHEN C.TYPE = 'EXPENSE' THEN '지출' ELSE '수입' END AS 수입지출 " +
                "FROM TRANSACTIONS T " +
                "JOIN PAY_METHODS M ON M.METHOD_ID = T.METHOD_ID " +
                "JOIN SUB_CATEGORIES S ON S.SUB_ID = T.SUB_ID " +
                "JOIN CATEGORIES C ON C.CATEGORY_ID = S.CATEGORY_ID " +
                "WHERE T.TX_DATE BETWEEN :s AND :e " +
                "ORDER BY T.TX_DATE DESC";

            using (OracleConnection conn = DatabaseManager.GetConnection())
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add(":s", start);
                cmd.Parameters.Add(":e", end);

                DataTable dt = new DataTable();
                conn.Open();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }

        public void Insert(int methodId, int subId, int amount, string memo)
        {
            using (OracleConnection conn = DatabaseManager.GetConnection())
            using (OracleCommand cmd = new OracleCommand(
                "INSERT INTO TRANSACTIONS " +
                "(TX_ID, METHOD_ID, SUB_ID, AMOUNT, TX_DATE, MEMO) " +
                "VALUES (SEQ_TX.NEXTVAL, :m, :s, :a, SYSDATE, :memo)", conn))
            {
                cmd.Parameters.Add(":m", methodId);
                cmd.Parameters.Add(":s", subId);
                cmd.Parameters.Add(":a", amount);
                cmd.Parameters.Add(":memo", memo);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(int txId)
        {
            using (OracleConnection conn = DatabaseManager.GetConnection())
            using (OracleCommand cmd = new OracleCommand(
                "DELETE FROM TRANSACTIONS WHERE TX_ID = :id", conn))
            {
                cmd.Parameters.Add(":id", txId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
