using EntityBase.Enum;
using ServerBaseContract.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Attributes;

namespace DbAdaptersTests.Mssql
{
    [TestCaseOrderer("Xunit.Microsoft.DependencyInjection.TestsOrder.TestPriorityOrderer", "Xunit.Microsoft.DependencyInjection")]
    public class MsSqlRepositoryBaseTests : MssqlTestBase
    {
        ITestOutputHelper _testOutputHelper;
        IEntityRepositoryBase<TestEntity> _entityRepositoryBase;
        readonly Guid _guid;
        public MsSqlRepositoryBaseTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _entityRepositoryBase = GetService<IEntityRepositoryBase<TestEntity>>(_testOutputHelper);
            _guid = Guid.NewGuid();
        }

        [Fact, TestOrder(1)]
        public void SetState_WhenCreateSuccess_ReturnObject()
        {
            var ent = new TestEntity { Id = _guid, CreateTime = DateTime.Now, Name = "Test", UpdateTime = DateTime.Now };
            Assert.Equal(ent, _entityRepositoryBase.SetState(ent, OperationType.Create));
        }

        [Fact, TestOrder(2)]
        public void SetState_WhenRemove_ReturnObjectIsRemovedPropertySetsTrue()
        {
            var ent = new TestEntity { Id = _guid, CreateTime = DateTime.Now, Name = "Test 2", UpdateTime = DateTime.Now,IsRemoved = false };
            _entityRepositoryBase.SetState(ent, OperationType.Create);
            Assert.NotEqual(ent.IsRemoved, _entityRepositoryBase.SetState(ent, OperationType.Remove).IsRemoved);
        }

        [Fact, TestOrder(3)]
        public async Task GetAsync_WhenCall_ReturnData()
        {
            var ent = new TestEntity { Id = _guid, CreateTime = DateTime.Now, Name = "Test 2", UpdateTime = DateTime.Now, IsRemoved = false };
            _entityRepositoryBase.SetState(ent, OperationType.Create);
            var exception = await Record.ExceptionAsync(async() =>
            {
                var obj = await _entityRepositoryBase.GetAsync();
                Assert.NotNull(obj);
                Assert.IsType<TestEntity>(obj);
            });

            Assert.Null(exception);
        }

        [Fact, TestOrder(4)]
        public async Task GetAsync_WhenCallWithFilter_ReturnData()
        {
            var ent = new TestEntity { Id = _guid, CreateTime = DateTime.Now, Name = "Test 2", UpdateTime = DateTime.Now, IsRemoved = false };
            _entityRepositoryBase.SetState(ent, OperationType.Create);
            var exception =await Record.ExceptionAsync(async() =>
            {
                var obj = await _entityRepositoryBase.GetAsync(s => s.IsRemoved == false);
                Assert.NotNull(obj);
                Assert.IsType<TestEntity>(obj);
            });

            Assert.Null(exception);
        }


        [Fact, TestOrder(5)]
        public async Task GetListAsync_WhenCall_ReturnData()
        {
            var ent = new TestEntity { Id = _guid, CreateTime = DateTime.Now, Name = "Test 2", UpdateTime = DateTime.Now, IsRemoved = false };
            _entityRepositoryBase.SetState(ent, OperationType.Create);
            var exception =await Record.ExceptionAsync(async() =>
            {
                var obj =await _entityRepositoryBase.GetListAsync(s => s.IsRemoved == false);
                Assert.NotNull(obj);
                Assert.IsType<List<TestEntity>>(obj);
            });

            Assert.Null(exception);
        }

        [Fact, TestOrder(6)]
        public async Task GetListAsync_WhenCallWithFilter_ReturnData()
        {
            var ent = new TestEntity { Id = _guid, CreateTime = DateTime.Now, Name = "Test 2", UpdateTime = DateTime.Now, IsRemoved = false };
            _entityRepositoryBase.SetState(ent, OperationType.Create);
            var exception =await Record.ExceptionAsync(async () =>
            {
                var obj = await _entityRepositoryBase.GetListAsync(s => s.IsRemoved == false);
                Assert.NotNull(obj);
                Assert.IsType<List<TestEntity>>(obj);
            });

            Assert.Null(exception);
        }


    }
}
