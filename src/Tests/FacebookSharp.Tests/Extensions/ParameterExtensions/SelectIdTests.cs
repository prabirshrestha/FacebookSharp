namespace FacebookSharp.Tests.Extensions
{
    using System.Collections.Generic;
    using FacebookSharp.Extensions;
    using Xunit;

    public class SelectIdTests
    {
        [Fact]
        public void SelectId_InNullParameter()
        {
            IDictionary<string, string> parameters = null;

            Assert.DoesNotThrow(
                () =>
                {
                    parameters = parameters.SelectId("1");
                });

            Assert.Equal(1, parameters.Count);
            Assert.Equal("1", parameters["ids"]);
        }

        [Fact]
        public void SelectId_NewEmptyParameter()
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            parameters = parameters.SelectId("1");

            Assert.Equal(1, parameters.Count);

            Assert.Equal("1", parameters["ids"]);
        }

        [Fact]
        public void SelectId_AddToExisitingIdParameter()
        {
            IDictionary<string, string> parameters = new Dictionary<string, string> { { "ids", "1" } };

            Assert.DoesNotThrow(
                () =>
                {
                    parameters = parameters.SelectId("2");
                });

            Assert.Equal(1, parameters.Count);

            Assert.Equal(3, parameters["ids"].Length);
        }

        [Fact]
        public void SelectId_DoesNotEndInComma_WhenMoreParametersAdded()
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>().SelectId("1");

            // assert that it doesn't end with ,
            Assert.NotEqual(',', parameters["ids"][parameters["ids"].Length - 1]);

            parameters = parameters.SelectId("2");

            // assert it contains , at 1st index -> 1,2
            Assert.Equal(',', parameters["ids"][1]);

            // assert that it doesn't end with ,
            Assert.NotEqual(',', parameters["ids"][parameters["ids"].Length - 1]);
        }
    }
}