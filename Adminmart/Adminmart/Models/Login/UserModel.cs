﻿using Adminmart.Lib.DataBase;
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

        public void ConvertPassword() 
        {
            var sha = new System.Security.Cryptography.HMACSHA512();
            sha.Key = System.Text.Encoding.UTF8.GetBytes(this.Password.Length.ToString());

            var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(this.Password));
            
            this.Password =  System.Convert.ToBase64String(hash);
        }
 
        public  int Register() 
        {
            //중복 이메일이 있는지
            //검증로직

            string sql = @"
                INSERT INTO t_user(
                    user_name
                    ,email
                    ,password
                )
                SELECT 
                    @user_name
                    ,@email
                    ,@password
            ";

           
            using (var db = new MySqlDapperHelper())
            {
                return db.Execute(sql, this);
            }
        }

        internal UserModel GetLoginUser()
        {

            string sql = @"
                SELECT 
                    user_seq
                    ,user_name
                    ,email
                    ,password
                FROM t_user
                Where  user_name = @user_name
                ";

            UserModel user;
            using (var db = new MySqlDapperHelper())
            {
                user = db.QuerySingle<UserModel>(sql, this);
            }
            if(user == null) 
            {
                throw new Exception("사용자가 존재 하지않습니다.");
            }

            if(user.Password != this.Password) 
            {
                throw new Exception("비밀번호가 틀렸습니다.");
            }

            return user;
        }
    }
}
