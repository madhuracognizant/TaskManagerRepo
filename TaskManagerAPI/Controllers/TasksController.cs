using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagerDAL;

namespace TaskManagerAPI.Controllers
{
    public class TasksController : ApiController
    {

        private TasksRepository _repository;
        public TasksController(TasksRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/Tasks")]
        public IHttpActionResult GetAllTasks()
        {

            var tasksDAL = _repository.GetAll().ToList();
            var list = new List<Models.Task>();
            foreach (var task in tasksDAL)
            {
                var model = new Models.Task();
                model.TaskName = task.TaskName;
                model.StartDate = task.StartDate;
                model.EndDate = task.EndDate;
                model.Priority = task.Priority;
                model.Task_ID = task.Task_ID;
                model.Parent_ID = task.Parent_ID;
                model.ParentTaskName = GetParentTaskName(task.Parent_ID);
                model.IsFinished = task.IsFinished;
                list.Add(model);
            }
            return Json(list, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        private string GetParentTaskName(int? parentTaskID)
        {
            if (parentTaskID.HasValue)
                return _repository.GetAll().ToList().Where(a => a.Task_ID == parentTaskID.Value).FirstOrDefault().TaskName;
            else
                return "This Task Has NO Parent";
        }

        [HttpGet]
        [Route("api/Tasks/ParentTasks")]
        public IHttpActionResult GetParentTasks()
        {
            var parentTasksDAL = _repository.GetAll().Where(a=>a.Parent_ID==null);
           
            return Json(parentTasksDAL, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        [HttpGet]
        [Route("api/Tasks/{id}")]
        public IHttpActionResult GetTaskByID(int id)
        {
            var taskDAL = _repository.GetById(id);
            var model = new Models.Task();
            //var task = Mapper.Map<Task, Models.Task>(taskDAL);
            if (taskDAL != null)
            {
                model.TaskName = taskDAL.TaskName;
                //model.ParentTaskName = GetParentTaskName(taskDAL.Parent_ID)
                model.Task_ID = taskDAL.Task_ID;
                model.Parent_ID = taskDAL.Parent_ID;
                model.StartDate = taskDAL.StartDate;
                model.EndDate = taskDAL.EndDate;
                model.Priority = taskDAL.Priority;
                model.IsFinished = taskDAL.IsFinished;
                model.ParentTaskName = GetParentTaskName(taskDAL.Parent_ID);
            }
            return Json(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        
        [HttpPost]
        [Route("api/Tasks/SaveTask")]
        public IHttpActionResult SaveTask([FromBody]Task task)
        {
            var taskDAL = new Task();
            //var taskDAL = Mapper.Map<Models.Task, Task>(task);
            if (task != null)
            {
                taskDAL.Task_ID = task.Task_ID;
                taskDAL.TaskName = task.TaskName;
                taskDAL.StartDate = task.StartDate;
                taskDAL.EndDate = task.EndDate;
                taskDAL.Parent_ID = task.Parent_ID;
                taskDAL.Priority = task.Priority;
                taskDAL.IsFinished = false;
                _repository.Save(taskDAL);
            }
            return Json(taskDAL, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/Tasks/EndTask/{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
