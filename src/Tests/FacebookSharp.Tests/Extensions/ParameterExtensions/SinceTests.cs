namespace FacebookSharp.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using FacebookSharp.Extensions;
    using Xunit;

    public class SinceTests
    {
        [Fact]
        public void AddSince_ToExisitingParameter()
        {
            // Arrange
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            // Act
            parameters = parameters.Since("yesterday");

            // Assert
            Assert.Equal(1, parameters.Count);

            Assert.Equal("yesterday", parameters["since"]);
        }

        [Fact]
        public void AddSince_ToNullParameter()
        {
            // Arrange
            IDictionary<string, string> parameters = null;

            // Act/Assert
            Assert.DoesNotThrow(() =>
                                    {
                                        parameters = parameters.Since("yesterday");
                                    });

            Assert.NotNull(parameters);

            Assert.Equal("yesterday", parameters["since"]);
        }

        [Fact]
        public void AddSince_ToParameterAlreadyContainingsince()
        {
            // Arrange
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters = parameters.Since("yesterday");

            // Assert
            Assert.Throws<ArgumentException>(() =>
                                                 {   // Act
                                                     parameters = parameters.Since("yesterday");
                                                 });
        }

        [Fact]
        public void AddSince_FluentApiTest()
        {
            var parameters = new Dictionary<string, string>().Since("yesterday");

            var expected = "yesterday";
            var actual = parameters["since"];

            Assert.Equal(expected, actual);
        }
    }
}