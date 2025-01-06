using MySqlConnector;

namespace Adminmart.Models
{
    public class TicketModel
    {
        public int Ticket_id {  get; set; }
        public string Title {  get; set; }
        public string Status {  get; set; }

        public static List<TicketModel> GetList( string status) 
        {
            using (var conn = new MySqlConnection("Server = 127.0.0.1; Port = 3306; Database = adminmart; Uid = root; Pwd = root;"))
            {
                conn.Open();
                string sql = @"
                                        SELECT 
	                                        A.ticket_id
	                                        ,A.title
	                                        ,A.status
                                         FROM t_ticket A
                                        WHERE A.status = @status
                                        ";

                return Dapper.SqlMapper.Query<TicketModel>(conn, sql, new { status = status }).ToList();
            }
        }

        public int Update() 
        {
            string sql = @"
                        UPDATE t_ticket  
                            Set
	                        title = @title
                        WHERE 
                            ticket_id = @ticket_id
                        ";
            using (var conn = new MySqlConnection("Server = 127.0.0.1; Port = 3306; Database = adminmart; Uid = root; Pwd = root;"))
            {
                conn.Open();

                return Dapper.SqlMapper.Execute(conn, sql, this);
            }

            
        }
    }
}
