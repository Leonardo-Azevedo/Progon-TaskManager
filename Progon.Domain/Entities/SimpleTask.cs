using Progon.Domain.Entities.Interfaces;
using Progon.Domain.Enums;

namespace Progon.Domain.Entities
{
    public class SimpleTask : ITask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TypeTask Type { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public OrderStatus Status { get; set; }

        public SimpleTask() { }
        public SimpleTask(string name, string description, TypeTask type, DateTime createDate, DateTime startDate, DateTime? finishDate, OrderStatus status)
        {
            Name = name;
            Description = description;
            Type = type;
            CreateDate = createDate;
            StartDate = startDate;
            FinishDate = finishDate;
            Status = status;
        }

        public SimpleTask(int id, string name, string description, TypeTask type, DateTime createDate, DateTime startDate, DateTime? finishDate, OrderStatus status)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            CreateDate = createDate;
            StartDate = startDate;
            FinishDate = finishDate;
            Status = status;
        }
    }
}
