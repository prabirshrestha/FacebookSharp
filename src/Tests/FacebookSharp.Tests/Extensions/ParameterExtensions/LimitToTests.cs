namespace FacebookSharp.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using FacebookSharp.Extensions;
    using Xunit;

    public class LimitToTests
    {
        [Fact]
        public void AddLimitTo_ToExisitingParameter()
        {
            // Arrange
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            // Act
            parameters = parameters.LimitTo(10);

            // Assert
            Assert.Equal(1, parameters.Count);

            Assert.Equal("10", parameters["limit"]);
        }

        [Fact]
        public void AddLimitTo_ToNullParameter()
        {
            // Arrange
            IDictionary<string, string> parameters = null;

            // Act/Assert
            Assert.DoesNotThrow(() =>
                                    {
                                        parameters = parameters.LimitTo(10);
                                    });

            Assert.NotNull(parameters);

            Assert.Equal("10", parameters["limit"]);
        }

        [Fact]
        public void AddLimitTo_ToParameterAlreadyContainingLimit()
        {
            // Arrange
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters = parameters.LimitTo(10);

            // Assert
            Assert.Throws<ArgumentException>(() =>
                                                 {   // Act
                                                     parameters = parameters.LimitTo(20);
                                                 });
        }

        [Fact]
        public void AddLimitTo_FluentApiTest()
        {
            var parameters = new Dictionary<string, string>().LimitTo(3);

            var expected = "3";
            var actual = parameters["limit"];

            Assert.Equal(expected, actual);
        }
    }
}