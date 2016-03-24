using Incontra.Packer.Core.Model;
using Incontra.Packer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Service
{
    public interface IPackerService
    {
        List<PackRequest> GetPackRequestAll();
        PackRequest GetPackRequestByID(int id);
        PackRequest PackRequestUpdate(PackRequest calculation);
        PackRequest PackRequestInsert(PackRequest calculation);
        void PackRequestDelete(int id);

        List<User> GetUserAll();
        User GetUserByID(int id);
        void UserUpdate(User user);
        void UserInsert(User user);
        void UserDelete(int id);

        List<License> GetLicenseAll();
        License GetLicenseByID(int id);
        void LicenseUpdate(License licence);
        void LicenseInsert(License licence);
        void LicenseDelete(int id);
    }
}
