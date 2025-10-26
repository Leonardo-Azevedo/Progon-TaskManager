using Microsoft.Data.SqlClient;
using Progon.Domain.Entities.Interfaces;
using Progon.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progon.Infrastructure.Interfaces
{
    public interface ITaskRepository
    {
        ITask MapTask(SqlDataReader reader);
        void Create(ITask task);
        IEnumerable<ITask> ListAll();
        void Delete(int id);
        void Update(ITask task);
        ITask GetTaskById(int id);
        IEnumerable<ITask> ListWithFilter(string name = null, OrderStatus? status = null, DateTime? startDate = null, DateTime? endDate = null);


    }
}
