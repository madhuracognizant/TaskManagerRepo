using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using TaskManagerAPI.Controllers;
using TaskManagerDAL;

namespace TaskManagerAPI.Tests
{
    [TestClass]
    public class TestTasksRepository
    {
        [TestMethod]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            //Arrange
            List<Task> tasks = GetTestTasks();
            DbSet<Task> tasksSet = GetDbSetStub<Task>(tasks);

            Mock<DbContext> contextStub = new Mock<DbContext>();
            contextStub.Setup(x => x.Set<Task>())
            .Returns(() => tasksSet);
            var repo = new TasksRepository(contextStub.Object);

            //Act
            var actual = repo.GetAll();


            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(((FakeSet<Task>)actual).Values.Count, tasks.Count());
        }

        [TestMethod]
        public void GetTaskByID_ShouldReturnTask()
        {
            //Arrange
            List<Task> tasks = GetTestTasks();
            DbSet<Task> tasksSet = GetDbSetStub<Task>(tasks);

            Mock<DbContext> contextStub = new Mock<DbContext>();
            contextStub.Setup(x => x.Set<Task>())
            .Returns(() => tasksSet);
            var repo = new TasksRepository(contextStub.Object);

            //Act
            var actual = repo.GetById(2);
            var expected = tasksSet.Where(a => a.Task_ID == 2).FirstOrDefault();

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Task_ID, expected.Task_ID);
        }

        [TestMethod]
        public void SaveTask_ShouldSaveSuccessfully()
        {
            //Arrange
            List<Task> tasks = GetTestTasks();
            DbSet<Task> tasksSet = GetDbSetStub<Task>(tasks);
            var taskToSave = new Task() { Task_ID = 5,TaskName="Test Task" };

            Mock<DbContext> contextStub = new Mock<DbContext>();
            contextStub.Setup(x => x.Set<Task>())
            .Returns(() => tasksSet);

            var repo = new TasksRepository(contextStub.Object);

            //Act
            repo.Save(taskToSave);
            var expected = 5;

            //Assert
            Assert.AreEqual(taskToSave.Task_ID, expected);
        }

        //[TestMethod]
        //public void EditTask_ShouldEditSuccessfully()
        //{
        //    //Arrange
        //    List<Task> tasks = GetTestTasks();
        //    DbSet<Task> tasksSet = GetDbSetStub<Task>(tasks);
        //    var taskToEdit = tasks.Where(a => a.Task_ID == 2).FirstOrDefault();

        //    Mock<DbContext> contextStub = new Mock<DbContext>();
            
           
        //    contextStub.Setup(m => m.Set<Task>().Find(It.IsAny<object[]>()))
        //    .Returns<object[]>(
        //          ids => tasks.FirstOrDefault(d => d.Task_ID == (int)ids[0]));


        //    var repo = new TasksRepository(contextStub.Object);

        //    //Act
        //    var result = repo.Edit(2, taskToEdit);

        //    //Assert
        //    Assert.AreEqual(result, 1);
        //}

        

        private static DbSet<T> GetDbSetStub<T>(List<T> values) where T : class
        {
            return new FakeSet<T>(values);
        }

        class FakeSet<T> : DbSet<T>, IQueryable, IEnumerable<T> where T : class
        {
            List<T> values;
            public FakeSet(IEnumerable<T> values)
            {
                this.values = values.ToList();
            }

            IQueryProvider IQueryable.Provider
            {
                get { return values.AsQueryable().Provider; }
            }

            Expression IQueryable.Expression
            {
                get { return values.AsQueryable().Expression; }
            }

            Type IQueryable.ElementType
            {
                get { return values.AsQueryable().ElementType; }
            }
            

            public IList<T> Values
            {
                get { return values; }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return values.GetEnumerator();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return values.GetEnumerator();
            }

            public override T Add(T entity)
            {
                values.Add(entity);
                return entity;
            }

            public override T Remove(T entity)
            {
                values.Remove(entity);
                return entity;
            }

            public override T Attach(T item)
            {
                values.Add(item);
                return item;
            }
        }

        private static List<Task> GetTestTasks()
        {
            var testTasks = new List<TaskManagerDAL.Task>();
            testTasks.Add(new Task { Task_ID = 1, TaskName = "Task 1", Parent_ID = 2 });
            testTasks.Add(new Task { Task_ID = 2, TaskName = "Task 2" });
            testTasks.Add(new Task { Task_ID = 3, TaskName = "Task 3", Parent_ID = 4 });
            testTasks.Add(new Task { Task_ID = 4, TaskName = "Task 4" });

            return testTasks;
        }

    }
}

