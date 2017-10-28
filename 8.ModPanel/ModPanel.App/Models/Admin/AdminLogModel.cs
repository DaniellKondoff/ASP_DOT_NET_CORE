using ModPanel.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Models.Admin
{
    public class AdminLogModel
    {
        public string Admin { get; set; }

        public LogType Type { get; set; }

        public string AdditionalInfo { get; set; }

        public override string ToString()
        {
            string message = null;

            switch (this.Type)
            {
                case LogType.CreatePost:
                    message = $"created the post {this.AdditionalInfo}";
                    break;
                case LogType.EditPost:
                    message = $"edited the post {this.AdditionalInfo}";
                    break;
                case LogType.DeletePost:
                    message = $"deleted the post {this.AdditionalInfo}";
                    break;
                case LogType.UserApproval:
                    message = $"approval the registration of {this.AdditionalInfo}";
                    break;
                case LogType.OpenMenu:
                    message = $"opened {this.AdditionalInfo} menu";
                    break;
                default:
                    throw new InvalidOperationException("Invalid log type");
            }

            return $"{Admin} {message}";
        }
    }
}
