using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Core.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        protected Mock<IServiceProvider> ServiceProviderMock;
        protected FakeContextProvider FakeContextProvider;
        protected IUnitOfWork UnitOfWork;

        [TestInitialize]
        public void Setup()
        {
            this.ServiceProviderMock = new Mock<IServiceProvider>();
            this.ServiceProviderMock.Setup(sp => sp.GetService(typeof(IRepository<BaseModel>))).Returns(new Repository<BaseModel>());

            this.FakeContextProvider = new FakeContextProvider();

            this.UnitOfWork = new UnitOfWork(this.FakeContextProvider, this.ServiceProviderMock.Object);
        }

        [TestMethod]
        public void AddValidEntity()
        {
            var baseModelId = 123; 

            var baseModel = new BaseModel
            {
                Id = baseModelId
            };

            var baseRepository = this.UnitOfWork.GetRepository<BaseModel>();
            baseRepository.Add(baseModel);

            Assert.AreEqual(baseModel.Id, (this.FakeContextProvider.fakeContext.First(c => ((BaseModel)c).Id.Equals(baseModel.Id)) as BaseModel).Id);
        }

        [TestMethod]
        public void AddNullEntity_ThrowsNullException()
        {
            var baseRepository = this.UnitOfWork.GetRepository<BaseModel>();
            Assert.ThrowsException<ArgumentNullException>(() => baseRepository.Add(null));
        }

        [TestMethod]
        public void AddValidEntiyRange()
        {
            var baseModelId1 = 123;

            var baseModel1 = new BaseModel
            {
                Id = baseModelId1
            };

            var baseModelId2 = 321;
            var baseModel2 = new BaseModel
            {
                Id = baseModelId2
            };

            var entityList = new List<BaseModel> { baseModel1, baseModel2 };

            var baseRepository = this.UnitOfWork.GetRepository<BaseModel>();
            baseRepository.AddRange(entityList);

            Assert.AreEqual(2, this.FakeContextProvider.fakeContext.Count);
            Assert.IsTrue(this.FakeContextProvider.fakeContext.Any(e => ((BaseModel)e).Id == baseModelId1));
            Assert.IsTrue(this.FakeContextProvider.fakeContext.Any(e => ((BaseModel)e).Id == baseModelId2));
        }

        [TestMethod]
        public void AddRangeWithEmptyList()
        {
            var emptyList = new List<BaseModel>();
            var baseRepository = this.UnitOfWork.GetRepository<BaseModel>();
            baseRepository.AddRange(emptyList);

            Assert.AreEqual(0, this.FakeContextProvider.fakeContext.Count);
        }

        [TestMethod]
        public void FindEntityById() {
            var baseModelId = 123;

            var baseModel = new BaseModel
            {
                Id = baseModelId
            };

            this.FakeContextProvider.Add(baseModel);

            var findValue = this.UnitOfWork.GetRepository<BaseModel>().Find(o => o.Id == baseModelId);

            Assert.AreEqual(baseModelId, findValue.FirstOrDefault(v => v.Id == baseModelId).Id);
        }

        [TestMethod]
        public void GetEntityById()
        {
            var baseModelId = 123;

            var baseModel = new BaseModel
            {
                Id = baseModelId
            };

            this.FakeContextProvider.Add(baseModel);

            var baseRepository = this.UnitOfWork.GetRepository<BaseModel>();
            var getValue = baseRepository.Get(baseModel.Id);

            Assert.AreEqual(baseModel.Id, getValue.Id);
        }

        [TestMethod]
        public void TryGetEntityWithInvalidId_ReturnsNull()
        {
            var baseRepository = this.UnitOfWork.GetRepository<BaseModel>();
            var getValue = baseRepository.Get(123);

            Assert.IsNull(getValue);
        }

        [TestMethod]
        public void RemoveRange()
        {
            var baseModelId1 = 123;

            var baseModel1 = new BaseModel
            {
                Id = baseModelId1
            };

            var baseModelId2 = 321;
            var baseModel2 = new BaseModel
            {
                Id = baseModelId2
            };

            var baseModelId3 = 567;
            var baseModel3 = new BaseModel
            {
                Id = baseModelId3
            };

            var entityList = new List<BaseModel> { baseModel1, baseModel2, baseModel3 };
            this.FakeContextProvider.fakeContext.AddRange(entityList);

            var entityListItemsToRemove = new List<BaseModel> { baseModel1, baseModel3 };

            var baseRepository = this.UnitOfWork.GetRepository<BaseModel>();
            baseRepository.RemoveRange(entityListItemsToRemove);

            Assert.AreEqual(1, this.FakeContextProvider.fakeContext.Count());
            Assert.IsTrue(this.FakeContextProvider.fakeContext.Any(e => ((BaseModel)e).Id == baseModelId2));
        }

        [TestMethod]
        public void RemoveSingle()
        {
            var baseModelId1 = 123;

            var baseModel1 = new BaseModel
            {
                Id = baseModelId1
            };

            var baseModelId2 = 321;
            var baseModel2 = new BaseModel
            {
                Id = baseModelId2
            };

            var baseModelId3 = 567;
            var baseModel3 = new BaseModel
            {
                Id = baseModelId3
            };

            var entityList = new List<BaseModel> { baseModel1, baseModel2, baseModel3 };
            this.FakeContextProvider.fakeContext.AddRange(entityList);

            var baseRepository = this.UnitOfWork.GetRepository<BaseModel>();
            baseRepository.Remove(baseModel1);

            Assert.AreEqual(2, this.FakeContextProvider.fakeContext.Count());
            Assert.IsTrue(this.FakeContextProvider.fakeContext.Any(e => ((BaseModel)e).Id == baseModelId2));
            Assert.IsTrue(this.FakeContextProvider.fakeContext.Any(e => ((BaseModel)e).Id == baseModelId3));
            Assert.IsFalse(this.FakeContextProvider.fakeContext.Any(e => ((BaseModel)e).Id == baseModelId1));
        }

    }
}