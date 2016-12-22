using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Portal.BLL.DTO
{
    public class ProjectTaskReportViewModel
    {
        public string Title { get; set; }
        public int EstimatedDifficult { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
