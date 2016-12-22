using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Utils.Reports
{
    public class ExcelReporterFactory
    {
        public static CustomReport<T> CreateCustomReport<T>(string title) where T : new()
        {
            return new CustomReport<T>()
            {
                ReportTitle = title
            };
        }
    }
}
