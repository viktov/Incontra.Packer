using Incontra.Packer.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public bool IsLoginValid(User user)
        {            
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "EXEC [incontra_packer].[p_User_GetByUserNameAndPassword] @UserName, @Password";
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", user.Password);
                user = GetModel(command);
                return user != null;
            }
        }    
    }
}
