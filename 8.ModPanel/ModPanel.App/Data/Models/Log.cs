using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Data.Models
{
    public class Log
    {
        public int Id { get; set; }

        public string Admin { get; set; }

        public LogType Type { get; set; }

        public string AdditionalInformation { get; set; }
    }
}
