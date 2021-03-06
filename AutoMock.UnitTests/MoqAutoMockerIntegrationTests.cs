﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMock.UnitTests.TestClasses;
using Moq;
using NUnit.Framework;

namespace AutoMock.UnitTests
{
    /// <summary>
    /// The MoqAutoMocker class integrates all the components for creating a auto mocker
    /// using Moq as the mocking tool. This test class tests that the integration works
    /// correctly.
    /// </summary>
    [TestFixture]
    public class MoqAutoMockerIntegrationTests
    {
        private MoqAutoMocker _automocker;

        [SetUp]
        public void Setup()
        {
            _automocker = new MoqAutoMocker();
        }

        [Test]
        public void ClassWithDependencyShouldBeConstructedWithMockedDependency()
        {
            // Exercise
            var instance = _automocker.GetInstance<ClassWithSimpleDependency>();

            // Verify
            var dependencyMock = Mock.Get(instance.Dependency);
            Assert.That(dependencyMock, Is.Not.Null);
        }

        [Test]
        public void CreateInstanceAfterGetMockShouldUseSameMock()
        {
            // Setup
            var mock = _automocker.GetMock<ISimpleDependency>();

            // Exercise
            var instance = _automocker.GetInstance<ClassWithSimpleDependency>();

            // Verify
            Assert.That(instance.Dependency, Is.SameAs(mock.Object));
        }


        [Test]
        public void GetMockBeforeCreateInstanceWithNestedDependencyShouldReuseSameMock()
        {
            // Setup
            var dependentMock = _automocker.GetMock<IInterfaceWithDependency>();
            var mock = _automocker.GetMock<ISimpleDependency>();            

            // Exercise
            _automocker.MockDependencies(dependentMock);
            var instance = _automocker.GetInstance<ClassWithNestedDependency>();

            // Veridy
            Assert.That(instance.Dependency.Dependency, Is.SameAs(mock.Object));
        }
    }
}
