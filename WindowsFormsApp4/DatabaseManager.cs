using Oracle.DataAccess.Client;

namespace WindowsFormsApp4
{
    public static class DatabaseManager
    {
        private static readonly string connectionString =
            "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;";

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }
    }
}
