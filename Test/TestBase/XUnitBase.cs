using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Dsl;
using AutoFixture.Kernel;
using Moq;

namespace TestBase
{
    /// <summary>
    /// Testler için temel sınıftır.
    /// </summary>
    public abstract class XUnitBase: Xunit.Microsoft.DependencyInjection.Abstracts.TestBedFixture
    {
        public IFixture Fixture { get; protected set; } = CreateFixture();

        protected TPoco GeneratePoco<TPoco>(Func<ICustomizationComposer<TPoco>, IPostprocessComposer<TPoco>> customizationAction = null)
        {
            ICustomizationComposer<TPoco> a = Fixture.Build<TPoco>();

            if (customizationAction != null)
                return customizationAction(a).Create();

            return a.Create();
        }

        protected Mock<TService> GenerateMock<TService>(Func<ICustomizationComposer<Mock<TService>>, ISpecimenBuilder> composerTransformation = null)
          where TService : class
        {
            if (composerTransformation == null)
                return Fixture.Freeze<Mock<TService>>();
            else
                return Fixture.Freeze(composerTransformation);
        }

        public static IFixture CreateFixture()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });

            // client has a circular reference from AutoFixture point of view
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }

    }
}
