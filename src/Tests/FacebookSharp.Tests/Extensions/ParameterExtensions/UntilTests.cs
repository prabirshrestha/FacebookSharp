namespace FacebookSharp.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using FacebookSharp.Extensions;
    using Xunit;

    public class UntilTests
    {
        [Fact]
        public void AddUntil_ToExisitingParameter()
        {
            // Arrange
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            // Act
            parameters = parameters.Until("yesterday");

            // Assert
            Assert.Equal(1, parameters.Count);

            Assert.Equal("yesterday", parameters["until"]);
        }

        [Fact]
        public void AddUntil_ToNullParameter()
        {
            // Arrange
            IDictionary<string, string> parameters = null;

            // Act/Assert
            Assert.DoesNotThrow(() =>
                                    {
                                        parameters = parameters.Until("yesterday");
                                    });

            Assert.NotNull(parameters);

            Assert.Equal("yesterday", parameters["until"]);
        }

        [Fact]
        public void AddUntil_ToParameterAlreadyContaininguntil()
        {
            // Arrange
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters = parameters.Until("yesterday");

            // Assert
            Assert.Throws<ArgumentException>(() =>
                                                 {   // Act
                                                     parameters = parameters.Until("yesterday");
                                                 });
        }

        [Fact]
        public void AddUntil_FluentApiTest()
        {
            var parameters = new Dictionary<string, string>().Until("yesterday");

            var expected = "yesterday";
            var actual = parameters["until"];

            Assert.Equal(expected, actual);
        }
    }
}