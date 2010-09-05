namespace FacebookSharp.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using FacebookSharp.Extensions;
    using Xunit;

    public class OffsetTests
    {
        [Fact]
        public void AddOffset_ToExisitingParameter()
        {
            // Arrange
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            // Act
            parameters = parameters.Offset(10);

            // Assert
            Assert.Equal(1, parameters.Count);

            Assert.Equal("10", parameters["offset"]);
        }

        [Fact]
        public void AddOffset_ToNullParameter()
        {
            // Arrange
            IDictionary<string, string> parameters = null;

            // Act/Assert
            Assert.DoesNotThrow(() =>
                                    {
                                        parameters = parameters.Offset(10);
                                    });

            Assert.NotNull(parameters);

            Assert.Equal("10", parameters["offset"]);
        }

        [Fact]
        public void AddOffset_ToParameterAlreadyContainingoffset()
        {
            // Arrange
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters = parameters.Offset(10);

            // Assert
            Assert.Throws<ArgumentException>(() =>
                                                 {   // Act
                                                     parameters = parameters.Offset(20);
                                                 });
        }

        [Fact]
        public void AddOffset_FluentApiTest()
        {
            var parameters = new Dictionary<string, string>().Offset(3);

            var expected = "3";
            var actual = parameters["offset"];

            Assert.Equal(expected, actual);
        }
    }
}