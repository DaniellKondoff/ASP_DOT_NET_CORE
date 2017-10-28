using ModPanel.App.Models.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Infrastructure
{
    public static class HtmlHelper
    {
        public static string ToHtml(this AdminLogModel log)
        => $@"<div class=""card border-{log.Type.ToViewClassName()} mb - 1"">
                                    <div class=""card-body"">
                                        <p class=""card-text"">{log.ToString()}</p>
                                    </div>
                            </div>";
    }
}
