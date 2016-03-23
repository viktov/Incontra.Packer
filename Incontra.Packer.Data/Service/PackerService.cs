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
        private IBoxRepository _boxRepository;
        private IContainerRepository _containerRepository;
        private ICalculationRepository _calculationRepository;
        private IUserRepository _userRepository;
        private ILicenceRepository _licenceRepository;

        public PackerService(IBoxRepository boxRepository,
            IContainerRepository containerRepository,
            ICalculationRepository calculationRepository,
            IUserRepository userRepository,
            ILicenceRepository licenceRepository)
        {
            _boxRepository = boxRepository;
            _containerRepository = containerRepository;
            _calculationRepository = calculationRepository;
            _userRepository = userRepository;
            _licenceRepository = licenceRepository;
        }

        public List<Box> GetBoxAll()
        {
            return _boxRepository.GetAll();
        }

        public Box GetBoxByID(int id)
        {
            return _boxRepository.GetByID(id);
        }

        public List<Box> GetBoxByContainerID(int containerID)
        {
            return _boxRepository.GetByContainerID(containerID);
        }

        public Box BoxUpdate(Box box)
        {
            return _boxRepository.Update(box);
        }

        public Box BoxInsert(Box box)
        {
            return _boxRepository.Insert(box);
        }

        public void BoxDelete(int id)
        {
            _boxRepository.Delete(id);
        }

        public List<Container> GetContainerAll()
        {
            return _containerRepository.GetAll();
        }

        public Container GetContainerByID(int id)
        {
            return _containerRepository.GetByID(id);
        }

        public Container ContainerUpdate(Container container)
        {
            return _containerRepository.Update(container);
        }

        public Container ContainerInsert(Container container)
        {
            return _containerRepository.Insert(container);
        }

        public void ContainerDelete(int id)
        {
            _containerRepository.Delete(id);
        }

        public List<Calculation> GetCalculationAll()
        {
            return _calculationRepository.GetAll();
        }

        public Calculation GetCalculationByID(int id)
        {
            return _calculationRepository.GetByID(id);
        }

        public Calculation CalculationUpdate(Calculation calculation)
        {
            return _calculationRepository.Update(calculation);
        }

        public Calculation CalculationInsert(Calculation calculation)
        {
            return _calculationRepository.Insert(calculation);
        }

        public void CalculationDelete(int id)
        {
            _calculationRepository.Delete(id);
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

        public List<Licence> GetLicenceAll()
        {
            throw new NotImplementedException();
        }

        public Licence GetLicenceByID(int id)
        {
            throw new NotImplementedException();
        }

        public void LicenceUpdate(Licence licence)
        {
            throw new NotImplementedException();
        }

        public void LicenceInsert(Licence licence)
        {
            throw new NotImplementedException();
        }

        public void LicenceDelete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
