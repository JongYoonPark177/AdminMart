﻿using Adminmart.Lib.DataBase;

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
                return db.Query<TicketModel>(sql, new { status = status });
            }
        }

        public int Update()
        {
            using (var db = new MySqlDapperHelper())
            {
                db.BeginTransaction();  
                try
                {
                    int r = 0;
                    string sql = @"
                                UPDATE t_ticket  
                                    Set
	                                title = @title
                                WHERE 
                                    ticket_id = @ticket_id
                                ";
                    r += db.Execute(sql, this);

                    db.Commit();

                    return r;
                }
                catch (Exception ex) 
                {
                    db.Rollback();
                    throw ex;
                }
            }


        }
    }
}
