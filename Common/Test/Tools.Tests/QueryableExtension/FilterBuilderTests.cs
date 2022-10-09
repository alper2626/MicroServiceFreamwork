using AutoMapper;
using AutoMapperAdapter;
using EntityBase.Abstract;
using EntityBase.Concrete;
using EntityBase.Exceptions;
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
        public void ApplyAllFilter_WhenFilterModelNull_ThrowException()
        {
            var queryable = new List<TestClass>() { new TestClass { Name = "testtest" } }.AsQueryable();
            Assert.Throws<CustomErrorException>(() => FilterBuilder.ApplyAllFilter<TestClassTwo, TestClass>(queryable, null));
        }

        [Fact]
        public void ApplyAllFilter_WhenFilterModelNull_ReturnFilteredItems()
        {
            var queryable = new List<TestClass>() { new TestClass { Name = "testtest" } }.AsQueryable();
            var result = FilterBuilder.ApplyAllFilter<TestClassTwo, TestClass>(queryable, FilterModel.Get(nameof(TestClass.Name), EntityBase.Enum.FilterOperator.Contains, "test"));
            Assert.True(result.Items.Any());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("NotFoundProp")]
        public void ApplyOrderFilter_WhenOrderStringNotContainingFromModel_ReturnSameQueryable(string? order)
        {
            var queryable = new List<TestClass>() { new TestClass { Name = "testtest" } }.AsQueryable();
            Assert.StrictEqual(queryable, FilterBuilder.ApplyOrderFilter<TestClass>(queryable, order, false));
        }

        [Theory]
        [InlineData("Name")]
        public void ApplyOrderFilter_WhenOrderStringContainingFromModel_ReturnOrderedQueryable(string? order)
        {
            var queryable = new List<TestClass>() { new TestClass { Name = "testtest" } }.AsQueryable();
            Assert.NotStrictEqual(queryable, FilterBuilder.ApplyOrderFilter<TestClass>(queryable, order, false));
        }

        [Theory]
        [InlineData("Name")]
        public void ApplyDynamicFilter_WhenFiltersContainingFromModel_ReturnQueryable(string prop)
        {
            var queryable = new List<TestClass>() { new TestClass { Name = "testtest" } }.AsQueryable();
            var filter = new List<FilterItem> { new FilterItem { Prop = prop, Operator = EntityBase.Enum.FilterOperator.Equals, Value = "test" } };
            Assert.NotNull(FilterBuilder.ApplyDynamicFilter(queryable,filter));
        }

        [Theory]
        [InlineData("test")]
        public void ApplyDynamicFilter_WhenFiltersNotContainingFromModel_ThrowException(string prop)
        {
            var queryable = new List<TestClass>() { new TestClass { Name = "testtest" } }.AsQueryable();
            Assert.Throws<CustomErrorException>(() => FilterBuilder.ApplyDynamicFilter<TestClass>(queryable, new List<FilterItem> { new FilterItem { Prop = prop, Operator = EntityBase.Enum.FilterOperator.Equals, Value = "test" } }));
        }



    }

    public class TestMapProfile : Profile
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
