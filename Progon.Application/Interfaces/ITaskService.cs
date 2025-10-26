using Progon.Application.Helpers;
using Progon.Domain.Entities.Interfaces;
using Progon.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progon.Application.Interfaces
{
    public interface ITaskService
    {
        ServiceResult CreateTask(ITask task);
        IEnumerable<ITask> ListAll();
        void DeleteTask(int id);
        ServiceResult UpdateTask(ITask task);
        ITask GetTaskById(int id);
        IEnumerable<ITask> ListWithFilter(string name = null, OrderStatus? status = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
