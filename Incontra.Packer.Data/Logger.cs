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
        private static IExceptionRepository _repository;
        private static IExceptionRepository Repository
        {
            get
            {
                if (_repository == null)
                    _repository = new ExceptionRepository();
                return _repository;
            }            
        }

        public static void LogException(string exceptionMsg, int? userID)
        {
            var exceptionLog = new ExceptionLog();
            exceptionLog.UserID = userID;
            exceptionLog.LogDate = DateTime.Now;
            exceptionLog.Exception = exceptionMsg;
            Repository.Insert(exceptionLog);
        }
    }
}
