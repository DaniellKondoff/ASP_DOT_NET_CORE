using ModPanel.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Models.Admin
{
    public class AdminUserModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public PositionType Position { get; set; }

        public int Posts { get; set; }

        public bool IsApproved { get; set; }

    }
}
