using FluentAssertions;
using NUnit.Framework;

namespace IoC.Tests
{
    public class ContainerTestBase
    {
        protected Container Container;

        [SetUp]
        public void BeforeEach()
        {
            Container = new Container();
        }

        [TearDown]
        public void AfterEach()
        {
            Container = null;
        }
    }

    [TestFixture]
    public class Container_GetInstance : ContainerTestBase
    {
        [Test]
        public void CreatesAnInstanceWithNoParams()
        {
            var subject = (A) Container.GetInstance(typeof(A));
            subject.Should().BeOfType<A>();
        }

        [Test]
        public void CreatesAnInstanceWithParams()
        {
            var subject = (B) Container.GetInstance(typeof(B));
            subject.A.Should().BeOfType<A>();
        }

        class A
        { }

        class B
        {
            public A A { get; }

            public B()
            {
            }

            public B(A a)
            {
                A = a;
            }
        }
    }
}