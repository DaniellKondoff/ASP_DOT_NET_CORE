using ModPanel.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Infrastructure
{
    public static class Helpers
    {
        public static string ToFriendlyName(this PositionType posistion)
        {
            switch (posistion)
            {
                case PositionType.Developer:
                case PositionType.Designer:
                case PositionType.HR:
                    return posistion.ToString();
                case PositionType.TechnicalSupport:
                    return "Technical Support";
                case PositionType.TechnicalTrainer:
                    return "Technical Trainer";
                case PositionType.MarketingSpecialist:
                    return "Marketing Specialist";
                default:
                    throw new InvalidOperationException($"Invalid Position type {posistion}");
            }
        }

        public static string ToViewClassName(this LogType type)
        {
            switch (type)
            {
                case LogType.CreatePost:
                    return "success";          
                case LogType.EditPost:
                    return "warning";
                case LogType.DeletePost:
                    return "danger";
                case LogType.UserApproval:
                    return "success";
                case LogType.OpenMenu:
                    return "primary";
                default:
                    throw new InvalidOperationException("Invalid log type");
            }
        }
    }
}
