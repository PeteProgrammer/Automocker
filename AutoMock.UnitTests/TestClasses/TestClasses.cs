﻿namespace AutoMock.UnitTests.TestClasses
{
    public interface ISimpleDependency { }

    public interface ISimpleDependency2 { }

    public class ClassWithoutDependencies { }

    public class ClassWithSimpleDependency
    {
        public ISimpleDependency Dependency { get; private set; }

        public ClassWithSimpleDependency(ISimpleDependency dependency) { Dependency = dependency; }
    }

    public class ClassWithClassDependency
    {
        public ClassWithSimpleDependency Dependency { get; private set; }

        public ClassWithClassDependency(ClassWithSimpleDependency dependency) { Dependency = dependency; }
    }

    public class ClassWithMultipleConstructors
    {
        public bool WasConstructedUsingGreedyConstructor { get; private set; }

        public ClassWithMultipleConstructors(ISimpleDependency dependency1)
        {
            WasConstructedUsingGreedyConstructor = false;
        }

        public ClassWithMultipleConstructors(ISimpleDependency dependency1, ISimpleDependency2 dependency2)
        {
            WasConstructedUsingGreedyConstructor = true;
        }
    }

    public class ClassWithCircularDependency
    {
        public ClassWithCircularDependency(ClassWithCircularDependency2 dependency) { }
    }

    public class ClassWithCircularDependency2
    {
        public ClassWithCircularDependency2(ClassWithCircularDependency dependency) { }
    }
}
