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
        List<Box> GetBoxAll();
        Box GetBoxByID(int id);
        List<Box> GetBoxByContainerID(int containerID);
        Box BoxUpdate(Box box);
        Box BoxInsert(Box box);
        void BoxDelete(int id);
        
        List<Container> GetContainerAll();
        Container GetContainerByID(int id);
        Container ContainerUpdate(Container container);
        Container ContainerInsert(Container container);
        void ContainerDelete(int id);

        List<Calculation> GetCalculationAll();
        Calculation GetCalculationByID(int id);
        Calculation CalculationUpdate(Calculation calculation);
        Calculation CalculationInsert(Calculation calculation);
        void CalculationDelete(int id);

        List<User> GetUserAll();
        User GetUserByID(int id);
        void UserUpdate(User user);
        void UserInsert(User user);
        void UserDelete(int id);

        List<Licence> GetLicenceAll();
        Licence GetLicenceByID(int id);
        void LicenceUpdate(Licence licence);
        void LicenceInsert(Licence licence);
        void LicenceDelete(int id);
    }
}
