using Progon.Domain.Entities.Enums;

namespace Progon.Domain.Entities.Interfaces
{
    public interface ITask
    {
        int Id { get; set; }
        String Name { get; set; }
        String Description { get; set; }
        TypeTask Type { get; set; }
        DateTime CreateDate { get; set; }
        DateTime StartDate { get; set; }
        DateTime? FinishDate { get; set; }
        OrderStatus Status { get; set; }
    }
}
