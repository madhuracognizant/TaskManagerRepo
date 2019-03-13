using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDAL
{
    public class TasksRepository : ITasksRepository
    {
        public DbContext _context;
        public IDbSet<Task> _dbset;

        public TasksRepository(DbContext context)
        {
            _context = context;
            _dbset = context.Set<Task>();
        }

        public IEnumerable<Task> GetAll()
        {
            return _dbset.AsEnumerable();
        }

        public Task GetById(int id)
        {
            return _dbset.AsEnumerable().Where(a => a.Task_ID == id).FirstOrDefault();
        }

        public void Save(Task entity)
        {
            _dbset.Add(entity);
            _context.SaveChanges();
        }

        public int Delete(int id)
        {
            try
            {
                Task entity = GetById(id);
                entity.IsFinished = true;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Edit(int id, Task entity)
        {
            try
            {
                var entityToEdit = GetById(id);
                entityToEdit.Task_ID = entity.Task_ID;
                entityToEdit.TaskName = entity.TaskName;
                entityToEdit.Parent_ID = entity.Parent_ID;
                entityToEdit.StartDate = entity.StartDate;
                entityToEdit.EndDate = entity.EndDate;
                entityToEdit.Priority = entity.Priority;
                _context.Entry(entityToEdit).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
