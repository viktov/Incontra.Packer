using Incontra.Packer.Data.Model;
using Incontra.Packer.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data
{
    public static class Logger
    {
        private static ISystemLogRepository _repository;
        private static ISystemLogRepository Repository
        {
            get
            {
                if (_repository == null)
                    _repository = new SystemLogRepository();
                return _repository;
            }            
        }

        public static void LogException(string exceptionMsg, int? userID)
        {
            var exceptionLog = new SystemLog();
            exceptionLog.UserID = userID;
            exceptionLog.LogDate = DateTime.Now;
            exceptionLog.Message = exceptionMsg;
            exceptionLog.LogType = 2;
            Repository.Insert(exceptionLog);
        }
    }
}
