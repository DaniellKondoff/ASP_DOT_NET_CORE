using ModPanel.App.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using ModPanel.App.Data.Models;
using ModPanel.App.Data;
using ModPanel.App.Models.Admin;
using System.Linq;

namespace ModPanel.App.Services
{
    public class LogService : ILogService
    {
        public void Create(string admin, LogType type, string additionalInfo)
        {
            using (var db = new ModePanelDbContext())
            {
                var log = new Log
                {
                    Admin = admin,
                    Type = type,
                    AdditionalInformation = additionalInfo
                };

                db.Logs.Add(log);
                db.SaveChanges();
            }
        }

        public IEnumerable<AdminLogModel> All()
        {
            using (var db = new ModePanelDbContext())
            {
                return db.Logs
                    .OrderByDescending(l=>l.Id)
                    .Select(l => new AdminLogModel
                    {
                        Admin = l.Admin,
                        Type = l.Type,
                        AdditionalInfo = l.AdditionalInformation
                    })
                    .ToList();
            }
        }
    }
}
