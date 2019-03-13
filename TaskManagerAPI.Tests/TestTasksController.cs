using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TaskManagerAPI.Controllers;
using TaskManagerDAL;

namespace TaskManagerAPI.Tests
{
    [TestClass]
    public class TestTasksController
    {
        [TestMethod]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testTasks = GetTestTasks();
            mockTasksRepo.Setup(m => m.GetAll()).Returns(testTasks);
            TasksController controller = new TasksController(
                 mockTasksRepo.Object);

            // Act
            var contentResult = controller.GetAllTasks();
          
            //Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(testTasks.Count(), ((System.Web.Http.Results.JsonResult<List<Models.Task>>)contentResult).Content.Count());
        }

        [TestMethod]
        public void GetAllParentTasks_ShouldReturnAllParentTasks()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testParentTasks = GetTestTasks().Where(a=>a.Parent_ID==null);
            mockTasksRepo.Setup(m => m.GetAll()).Returns(testParentTasks);
            TasksController controller = new TasksController(
                 mockTasksRepo.Object);

            // Act
            var contentResult = controller.GetParentTasks();

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(testParentTasks.Count(), ((System.Web.Http.Results.JsonResult<IEnumerable<Task>>)contentResult).Content.Count());
        }


        [TestMethod]
        public void GetAllParentTasks_ShouldReturnZeroParentTasks()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testNonParentTasks = GetTestTasks().Where(a => a.Parent_ID != null);
            mockTasksRepo.Setup(m => m.GetAll()).Returns(testNonParentTasks);
            TasksController controller = new TasksController(
                 mockTasksRepo.Object);

            // Act
            var contentResult = controller.GetParentTasks();

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(0, ((System.Web.Http.Results.JsonResult<IEnumerable<Task>>)contentResult).Content.Count());
        }


        [TestMethod]
        public void GetTaskByID_ShouldReturnCorrectTask()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testTask = GetTestTasks().Where(a=>a.Task_ID==2).FirstOrDefault();
            mockTasksRepo.Setup(m => m.GetById(2)).Returns(testTask);
            TasksController controller = new TasksController(
                 mockTasksRepo.Object);

            // Act
            var contentResult = controller.GetTaskByID(2);

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(testTask.TaskName, ((System.Web.Http.Results.JsonResult<Models.Task>)contentResult).Content.TaskName);
        }

        [TestMethod]
        public void GetTaskByID_ShouldReturnEmptyResultIfTaskNotPresent()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testTask = GetTestTasks().Where(a => a.Task_ID == 5).FirstOrDefault();
            mockTasksRepo.Setup(m => m.GetById(5)).Returns(testTask);
            TasksController controller = new TasksController(
                 mockTasksRepo.Object);

            // Act
            var contentResult = controller.GetTaskByID(5);

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(0, ((System.Web.Http.Results.JsonResult<Models.Task>)contentResult).Content.Task_ID);
        }

        [TestMethod]
        public void SaveTask_ShouldSaveSuccessfully()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testTask = new Task() {Task_ID =5, TaskName ="Task 5" };
            mockTasksRepo.Setup(m => m.Save(testTask));
           TasksController controller = new TasksController(
                 mockTasksRepo.Object);

            // Act
            var contentResult =  controller.SaveTask(testTask);

            //Assert 
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Task 5", ((System.Web.Http.Results.JsonResult<Task>)contentResult).Content.TaskName);
        }

        [TestMethod]
        public void SaveTask_NullObjectPassed_ShouldNotSaveSuccessfully()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testTask = default(Task);
            mockTasksRepo.Setup(m => m.Save(testTask));
            TasksController controller = new TasksController(
                  mockTasksRepo.Object);

            // Act
            var contentResult = controller.SaveTask(testTask);

            //Assert 
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(0, ((System.Web.Http.Results.JsonResult<Task>)contentResult).Content.Task_ID);
        }

        [TestMethod]
        public void EditTask_ShouldEditSuccessfully()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testTaskToEdit = GetTestTasks().Where(a => a.Task_ID == 1).FirstOrDefault();
            testTaskToEdit.TaskName = "Test task 1";
            mockTasksRepo.Setup(m => m.Edit(testTaskToEdit.Task_ID, testTaskToEdit)).Returns(1);
            TasksController controller = new TasksController(
                  mockTasksRepo.Object);

            // Act
            var contentResult = controller.SaveTask(testTaskToEdit);

            //Assert 
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Test task 1", ((System.Web.Http.Results.JsonResult<Task>)contentResult).Content.TaskName);
        }

        [TestMethod]
        public void EditTask_NullObjectPassed_ShouldNotEditSuccessfully()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testTask = default(Task);
            mockTasksRepo.Setup(m => m.Save(testTask));
            TasksController controller = new TasksController(
                  mockTasksRepo.Object);

            // Act
            var contentResult = controller.SaveTask(testTask);

            //Assert 
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(0, ((System.Web.Http.Results.JsonResult<Task>)contentResult).Content.Task_ID);
        }

        [TestMethod]
        public void DeleteTask_ShouldSetIsFinishedToTrue()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testTask = GetTestTasks().Where(a => a.Task_ID == 2).FirstOrDefault();
            testTask.IsFinished = true;
            mockTasksRepo.Setup(m => m.GetById(testTask.Task_ID)).Returns(testTask);

            mockTasksRepo.Setup(m => m.Delete(2)).Returns(1);
            TasksController controller = new TasksController(
                 mockTasksRepo.Object);

            // Act
            controller.Delete(testTask.Task_ID);

            //Assert
            Assert.AreEqual(testTask.IsFinished, true);
        }

        [TestMethod]
        public void DeleteTaskFails_ShouldNotSetIsFinishedToTrue()
        {
            //Arrange
            var mockResult = new Mock<IHttpActionResult>();
            var mockTasksRepo = new Mock<ITasksRepository>();
            var testTask = new Task()
            {
                Task_ID = 5, IsFinished = false
            };
            mockTasksRepo.Setup(m => m.GetById(testTask.Task_ID)).Returns(testTask);

            mockTasksRepo.Setup(m => m.Delete(testTask.Task_ID)).Returns(0);
            TasksController controller = new TasksController(
                 mockTasksRepo.Object);

            // Act
            controller.Delete(testTask.Task_ID);

            //Assert
            Assert.AreEqual(testTask.IsFinished, false);
        }

        private List<Task> GetTestTasks()
        {
            var testTasks = new List<TaskManagerDAL.Task>();
            testTasks.Add(new Task { Task_ID = 1, TaskName = "Task 1", Parent_ID=2});
            testTasks.Add(new Task { Task_ID = 2, TaskName = "Task 2" });
            testTasks.Add(new Task { Task_ID = 3, TaskName = "Task 3", Parent_ID=4 });
            testTasks.Add(new Task { Task_ID = 4, TaskName = "Task 4" });

            return testTasks;
        }
        
    }
}
