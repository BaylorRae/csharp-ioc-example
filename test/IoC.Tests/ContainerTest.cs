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

        [Test]
        public void ItAllowsAParameterlessConstructor()
        {
            var subject = (C) Container.GetInstance(typeof(C));
            subject.Invoked.Should().BeTrue();
        }

        [Test]
        public void ItAllowsGenericInitialization()
        {
            var subject = Container.GetInstance<A>();
            subject.Should().BeOfType<A>();
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

        class C
        {
            public bool? Invoked { get; set; }

            public C()
            {
                Invoked = true;
            }
        }
    }

    [TestFixture]
    public class Container_Register : ContainerTestBase
    {
        [Test]
        public void RegisterATypeFromAnInterface()
        {
            Container.Register<IMaterial, Plastic>();
            var subject = Container.GetInstance<IMaterial>();
            subject.Should().BeOfType<Plastic>();
        }

        [Test]
        public void InitializeObjectWithDependencies()
        {
            Container.Register<IMaterial, Toy>();
            var subject = (Toy) Container.GetInstance<IMaterial>();
            subject.Material.Should().BeOfType<Plastic>();
        }

        interface IMaterial
        {
            int Weight { get; }
        }

        class Plastic : IMaterial
        {
            public int Weight => 42;
        }

        class Metal : IMaterial
        {
            public int Weight => 84;
        }

        class Toy : IMaterial
        {
            public int Weight => 100;
            public Plastic Material { get; } = null;

            public Toy(Plastic material)
            {
                Material = material;
            }
        }
    }
}