using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
    public class UserRepository
    {
        public UserEntity Authenticate(string userName, string password)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("Select TOP 1 UserId, UserName, Name FROM [User] Where UserName=@username and Password=@password", con))
                    {
                        command.Parameters.Add(new SqlParameter("@username", userName));
                        command.Parameters.Add(new SqlParameter("@password", password));
                        using (SqlDataReader rdr = command.ExecuteReader())
                        {
                            var user = new UserEntity();
                            while (rdr.Read())
                            {
                                var userId = Convert.ToInt32(rdr[0]);
                                var username = Convert.ToString(rdr[1]);
                                var name = Convert.ToString(rdr[2]);
                                user.Name = name; user.UserId = userId; user.UserName = username;
                            }
                            return user;
                        }
                    }
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

        public int InsertToken(TokenEntity t)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("Insert into Tokens(UserId, AuthToken, IssuedOn, ExpiresOn) Values (@UserId, @AuthToken, @IssuedOn, @ExpiresOn)", con))
                    {
                        command.Parameters.Add(new SqlParameter("@UserId", t.UserId));
                        command.Parameters.Add(new SqlParameter("@AuthToken", t.AuthToken));
                        command.Parameters.Add(new SqlParameter("@IssuedOn", t.IssuedOn));
                        command.Parameters.Add(new SqlParameter("@ExpiresOn", t.ExpiresOn));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return 1;
            }
        }

        public List<TokenEntity> GetToken(string tokenId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("Select TokenId, UserId, AuthToken, IssuedOn, ExpiresOn FROM [Tokens] Where AuthToken=@tokenid", con))
                    {
                        command.Parameters.Add(new SqlParameter("@tokenid", tokenId));
                        using (SqlDataReader rdr = command.ExecuteReader())
                        {
                            var tokenList = new List<TokenEntity>();
                            while (rdr.Read())
                            {
                                var t = new TokenEntity();
                                t.TokenId = Convert.ToInt32(rdr[0]);
                                t.UserId = Convert.ToInt32(rdr[1]);
                                t.AuthToken = Convert.ToString(rdr[2]);
                                t.IssuedOn = Convert.ToDateTime(rdr[3]);
                                t.ExpiresOn = Convert.ToDateTime(rdr[4]);
                                tokenList.Add(t);
                            }
                            return tokenList;
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public int UpdateToken(TokenEntity t)
        {
            return 1;
        }

        public int DeleteToken(string tokenId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("Delete from Tokens WHERE TokenId = @TokenId", con))
                    {
                        command.Parameters.Add(new SqlParameter("@TokenId", tokenId));
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    return 0;
                }
                return 1;
            }
        }
    }
}
