using Incontra.Packer.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Repository
{
    public class BoxRepository: BaseRepository<Box>, IBoxRepository
    {
        public BoxRepository() : base()
        {

        }

        public List<Box> GetByContainerID(int containerID)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "EXEC [incontra_packer].[p_Box_GetByContainerID] @ContainerID";
                command.Parameters.AddWithValue("@ContainerID", containerID);
                return GetModelList(command);
            }
        }
    }
}
