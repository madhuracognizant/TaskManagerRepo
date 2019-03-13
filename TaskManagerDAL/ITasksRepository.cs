using System.Collections.Generic;

namespace TaskManagerDAL
{
    public interface ITasksRepository : IRepository<Task>
    {
        IEnumerable<Task> GetAll();

        Task GetById(int id);
        void Save(Task entity);
        int Delete(int id);

        int Edit(int id, Task entity);
    }
}
