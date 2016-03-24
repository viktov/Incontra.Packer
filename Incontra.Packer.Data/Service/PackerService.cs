using Incontra.Packer.Data.Repository;
using Incontra.Packer.Data.Model;
using Incontra.Packer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Service
{
    public class PackerService : IPackerService
    {        
        private IPackRequestRepository _packRequestRepository;
        private IUserRepository _userRepository;
        private ILicenseRepository _licenseRepository;

        public PackerService(
            IPackRequestRepository calculationRepository,
            IUserRepository userRepository,
            ILicenseRepository licenceRepository)
        {            
            _packRequestRepository = calculationRepository;
            _userRepository = userRepository;
            _licenseRepository = licenceRepository;
        }

        public List<PackRequest> GetPackRequestAll()
        {
            return _packRequestRepository.GetAll();
        }

        public PackRequest GetPackRequestByID(int id)
        {
            return _packRequestRepository.GetByID(id);
        }

        public PackRequest PackRequestUpdate(PackRequest calculation)
        {
            return _packRequestRepository.Update(calculation);
        }

        public PackRequest PackRequestInsert(PackRequest calculation)
        {
            return _packRequestRepository.Insert(calculation);
        }

        public void PackRequestDelete(int id)
        {
            _packRequestRepository.Delete(id);
        }

        public List<User> GetUserAll()
        {
            throw new NotImplementedException();
        }

        public User GetUserByID(int id)
        {
            throw new NotImplementedException();
        }

        public void UserUpdate(User user)
        {
            throw new NotImplementedException();
        }

        public void UserInsert(User user)
        {
            throw new NotImplementedException();
        }

        public void UserDelete(int id)
        {
            throw new NotImplementedException();
        }

        public List<License> GetLicenseAll()
        {
            throw new NotImplementedException();
        }

        public License GetLicenseByID(int id)
        {
            throw new NotImplementedException();
        }

        public void LicenseUpdate(License licence)
        {
            throw new NotImplementedException();
        }

        public void LicenseInsert(License licence)
        {
            throw new NotImplementedException();
        }

        public void LicenseDelete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
