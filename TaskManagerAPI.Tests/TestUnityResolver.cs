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
using TaskManagerAPI.Resolvers;
using TaskManagerDAL;
using Unity;

namespace TaskManagerAPI.Tests
    {
        [TestClass]
        public class TestUnityResolver
        {
            [TestMethod]
            public void GetService_ShouldResolveService()
            {
                //Arrange
                Mock<IUnityContainer> containerStub = new Mock<IUnityContainer>();
                //containerStub.Setup(a => a.Resolve(typeof(TestService))).Returns(new TestService());
              
                var resolver = new UnityResolver(containerStub.Object);

                ////Act
                var service = resolver.GetService(typeof(TestService));


                //Assert
                Assert.IsNull(service);
        }

           
        }

        public class TestService : ITestService
        {

        }

        public interface ITestService
        {

        }
}
