using MySqlConnector;
using System.ComponentModel;

namespace Adminmart.Models.Login
{
    public class UserModel
    {
        public uint User_Seq { get; set; }
        public string User_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string ConvertPassword() 
        {
            var sha = new System.Security.Cryptography.HMACSHA512();
            sha.Key = System.Text.Encoding.UTF8.GetBytes(this.Password.Length.ToString());

            var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(this.Password));
            
            return System.Convert.ToBase64String(hash);
        }

        public  int Register() 
        {
            string sql = @"
                INSERT INTO t_user(
                    user_seq
                    ,user_name
                    ,email
                    ,password
                )
                SELECT 
                    @user_seq
                    ,@user_name
                    ,@email
                    ,@password
            ";

            using (var conn = new MySqlConnection("Server = 127.0.0.1; Port = 3306; Database = adminmart; Uid = root; Pwd = root;"))
            {
                conn.Open();

                return Dapper.SqlMapper.Execute(conn, sql, this);
            }
        }
    }
}
