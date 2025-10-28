using Microsoft.Data.SqlClient;
using Progon.Domain.Entities;
using Progon.Domain.Entities.Interfaces;
using Progon.Domain.Enums;
using Progon.Infrastructure.Interfaces;
using Progon.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Progon.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbConnection _dbConnection;
        public TaskRepository(DbConnection dbConnection) 
        {
            _dbConnection = dbConnection;
        }

        public ITask MapTask(SqlDataReader reader)
        {
            var task = new SimpleTask(
                id: (int)reader["Id"],
                name: reader["Name"].ToString(),
                description: reader["Description"].ToString(),
                type: (TypeTask)(int)reader["TypeTask"],
                createDate: (DateTime)reader["CreateDate"],
                startDate: (DateTime)reader["StartDate"],
                finishDate: reader["FinishDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["FinishDate"],
                status: (OrderStatus)(int)reader["Status"]

            );

            return task;
        }
        public void Create(ITask task)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Tasks (Name, Description, TypeTask, CreateDate, StartDate, Status) VALUES (@Name, @Description, @TypeTask, @CreateDate, @StartDate, @Status)";

            cmd.Parameters.AddWithValue("@Name", task.Name);
            cmd.Parameters.AddWithValue("@Description", task.Description);
            cmd.Parameters.AddWithValue("@TypeTask", (int)task.Type);
            cmd.Parameters.AddWithValue("@CreateDate", task.CreateDate.Date);
            cmd.Parameters.AddWithValue("@StartDate", task.StartDate.Date);
            cmd.Parameters.AddWithValue("@Status", (int)task.Status);

            try
            {
                using(var conn = _dbConnection.connect())
                {
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                //clear parameters
                cmd.Parameters.Clear();
            }

        }

        public IEnumerable<ITask> ListAll()
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT Id, Name, Description, TypeTask, CreateDate, StartDate, FinishDate, Status FROM Tasks ORDER BY Id DESC";
            List<ITask> list = new List<ITask>();

            try
            {
                using (var conn = _dbConnection.connect())
                {
                    cmd.Connection = conn;
                    SqlDataReader reader = cmd.ExecuteReader();
                    //percorrer a listagem
                    while (reader.Read())
                    {
                        list.Add(MapTask(reader));
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                //clear parameters
                cmd.Parameters.Clear();
            }

            return list;
        }

        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "DELETE FROM Tasks WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                using(var conn = _dbConnection.connect())
                {
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
            }
        }

        public void Update(ITask task)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE Tasks SET Name=@Name, Description=@Description, StartDate=@StartDate, FinishDate=@FinishDate, TypeTask=@TypeTask, Status=@Status WHERE Id=@Id";

            cmd.Parameters.AddWithValue("@Name", task.Name);
            cmd.Parameters.AddWithValue("@Description", task.Description);
            cmd.Parameters.AddWithValue("@TypeTask", (int)task.Type);
            cmd.Parameters.AddWithValue("@StartDate", task.StartDate.Date);
            cmd.Parameters.AddWithValue("@FinishDate", task.FinishDate.HasValue ? (object)task.FinishDate.Value.Date : DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", (int)task.Status);
            cmd.Parameters.AddWithValue("@Id", task.Id);

            try
            {
                using (var conn = _dbConnection.connect())
                {
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
            }
        }

        public ITask GetTaskById(int id)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Tasks WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                using(var conn = _dbConnection.connect())
                {
                    cmd.Connection = conn;
                    //executar o comando
                    SqlDataReader reader = cmd.ExecuteReader();
                    //percorrer a listagem
                    if (reader.Read())
                    {
                        ITask task = new SimpleTask();
                        task = MapTask(reader);
                        reader.Close();
                        cmd.Parameters.Clear();
                        return task;

                    }
                    else
                    {
                        cmd.Parameters.Clear();
                        reader.Close();
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                //clear parameters
                cmd.Parameters.Clear();
                return null;
            }

        }

        public IEnumerable<ITask> ListWithFilter(string name = null, OrderStatus? status = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Tasks WHERE 1=1";

            if (name != null) //Filtragem pelo nome
            {
                cmd.CommandText += " AND Name LIKE @Name";

                cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
            }
            if (status != null) //Filtragem pelo status
            {
                //cmd.CommandText = "SELECT * FROM Tasks WHERE Status LIKE @Status";
                cmd.CommandText += " AND Status = @Status";

                cmd.Parameters.AddWithValue("@Status", status);
            }
            if (startDate != null && endDate != null) //Filtragem entre data de inicio e fim selecionada
            {
                //cmd.CommandText = "SELECT * FROM Tasks WHERE StartDate >= @StartDate AND StartDate < @EndDate";
                cmd.CommandText += " AND StartDate >= @StartDate AND StartDate < @EndDate";

                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
            }

            cmd.CommandText += " ORDER BY Id DESC";

            List<ITask> list = new List<ITask>();

            try
            {
                using (var conn = _dbConnection.connect())
                {
                    cmd.Connection = conn;
                    SqlDataReader reader = cmd.ExecuteReader();
                    //percorrer a listagem
                    while (reader.Read())
                    {
                        list.Add(MapTask(reader));
                    }
                    reader.Close();
                    //mostrar mensagem
                }


            }
            catch (Exception ex)
            {
                //clear parameters
                cmd.Parameters.Clear();
            }

            return list;
        }

        //public void ExportToExcel(IEnumerable<ITask> listTasks)
        //{
        //    ExcelPackage.License.SetNonCommercialPersonal("Leonardo");

        //    var file = new FileInfo(@"C:\Users\ADM\Desktop\ExportTasks.xlsx");
        //    using (var archive = new ExcelPackage(file))
        //    {
        //        var sheet = archive.Workbook.Worksheets.Add("Sheet1");
        //        sheet.Cells["A1"].Value = "ID";
        //        sheet.Cells["B1"].Value = "NAME";
        //        sheet.Cells["C1"].Value = "DESCRIPTON";
        //        sheet.Cells["D1"].Value = "CREATE DATE";
        //        sheet.Cells["E1"].Value = "START DATE";
        //        sheet.Cells["F1"].Value = "FINISH DATE";
        //        sheet.Cells["G1"].Value = "STATUS";

        //        int row = 2;

        //        foreach (var task in listTasks)
        //        {


        //            sheet.Cells["A" + row].Value = task.Id;
        //            sheet.Cells["B" + row].Value = task.Name;
        //            sheet.Cells["C" + row].Value = task.Description;
        //            sheet.Cells["D" + row].Value = task.CreateDate;
        //            sheet.Cells["E" + row].Value = task.StartDate;
        //            sheet.Cells["F" + row].Value = task.FinishDate;
        //            sheet.Cells["G" + row].Value = task.Status;

        //            row++;
        //        }

        //        //Estilização das colunas
        //        sheet.Cells["A1:G1"].Style.Font.Bold = true;
        //        sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
        //        sheet.Column(4).Style.Numberformat.Format = "dd/MM/yyyy";
        //        sheet.Column(5).Style.Numberformat.Format = "dd/MM/yyyy";
        //        sheet.Column(6).Style.Numberformat.Format = "dd/MM/yyyy";

        //        archive.Save();
        //    }

        //}

    }

}
