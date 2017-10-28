using ModPanel.App.Data.Models;
using ModPanel.App.Models.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Services.Contracts
{
    interface ILogService
    {
        void Create(string admin, LogType type, string additionalInfo);

        IEnumerable<AdminLogModel> All();
    }
}
