using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progon.Application.DTO
{
    public class UpdateTaskRequest
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int Type { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int Status { get; set; }
    }
}
