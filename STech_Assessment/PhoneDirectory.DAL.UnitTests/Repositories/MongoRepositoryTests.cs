using NUnit.Framework;
using System;
using Xunit;
using FluentAssertions;
using AutoFixture;
using Moq;
using PhoneDirectory.DAL.Interfaces;
using PhoneDirectory.Entity.Base;

namespace PhoneDirectory.DAL.UnitTests
{
    public class MongoRepositoryTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IMongoRepository<IDocument>> _repositoryMock;

        public MongoRepositoryTests()
        {
            _fixture = new Fixture();
            _repositoryMock = _fixture.Freeze<Mock<IMongoRepository<IDocument>>>();
        }

        [Fact]
        public void Test1()
        {
        }
    }
}
