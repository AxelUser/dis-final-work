using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Portal.BLL.DTO;
using TaskManager.DAL.Entities;

namespace TaskManager.Portal.BLL.Extentions
{
    public static class ViewModelExtentions
    {
        public static ProjectTaskReportViewModel ToReportViewModel(this ProjectTask entity)
        {
            return new ProjectTaskReportViewModel()
            {
                Title = entity.Title,
                EstimatedDifficult = entity.EstimatedDifficult,
                CreationDate = entity.CreationDate,
                UpdateDate = entity.UpdateDate
            };
        }
    }
}
