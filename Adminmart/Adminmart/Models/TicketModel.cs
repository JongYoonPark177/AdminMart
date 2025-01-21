using Adminmart.Lib.DataBase;

namespace Adminmart.Models
{
    public class TicketModel
    {
        public int Ticket_id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public static List<TicketModel> GetList(string status)
        {
            using (var db = new MySqlDapperHelper())
            {
                string sql = @"
                                        SELECT 
	                                        A.ticket_id
	                                        ,A.title
	                                        ,A.status
                                         FROM t_ticket A
                                        WHERE A.status = @status
                                        ";
                return db.GetQuery<TicketModel>(sql, new { status = status });
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
            using (var db = new MySqlDapperHelper())
            {
                return db.Execute(sql, this);
            }


        }
    }
}
