using AutoMapper;
using AutoMapperAdapter;
using EntityBase.Abstract;
using EntityBase.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.QueryableExtension;
using Xunit;

namespace Tools.Tests.QueryableExtension
{
    public class FilterBuilderTests
    {
        public FilterBuilderTests()
        {
            AutoMapperWrapper.Configure();
        }

        [Fact]
        public void ApplyAllFilterWhenFilterModelNull_ReturnEmptyItems()
        {
            var queryable = new List<TestClass>() { new TestClass { Name="testtest"} }.AsQueryable();
            var result = FilterBuilder.ApplyAllFilter<TestClassTwo, TestClass>(queryable, null);
            Assert.False(result.Items.Any());
        }

        [Fact]
        public void ApplyAllFilterWhenFilterModelNull_ReturnFilteredItems()
        {
            var queryable = new List<TestClass>() { new TestClass { Name = "testtest" } }.AsQueryable();
            var result = FilterBuilder.ApplyAllFilter<TestClassTwo, TestClass>(queryable, FilterModel.Get(nameof(TestClass.Name),EntityBase.Enum.FilterOperator.Contains,"test"));
            Assert.True(result.Items.Any());
        }
    }

    public class TestMapProfile: Profile
    {
        public TestMapProfile()
        {
            CreateMap<TestClass, TestClassTwo>().ReverseMap();
        }
    }
    public class TestClass : Entity
    {
        public string Name { get; set; }
    }

    public class TestClassTwo
    {

        public string Name { get; set; }
    }
}
