namespace FacebookSharp.Tests.Extensions
{
    using System.Collections.Generic;
    using FacebookSharp.Extensions;
    using Xunit;

    public class SelectFieldTests
    {
        [Fact]
        public void SelectField_InNullParameter()
        {
            IDictionary<string, string> parameters = null;

            Assert.DoesNotThrow(
                () =>
                {
                    parameters = parameters.SelectField("1");
                });

            Assert.Equal(1, parameters.Count);
            Assert.Equal("1", parameters["fields"]);
        }

        [Fact]
        public void SelectField_NewEmptyParameter()
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            parameters = parameters.SelectField("1");

            Assert.Equal(1, parameters.Count);

            Assert.Equal("1", parameters["fields"]);
        }

        [Fact]
        public void SelectField_AddToExisitingFieldParameter()
        {
            IDictionary<string, string> parameters = new Dictionary<string, string> { { "fields", "1" } };

            Assert.DoesNotThrow(
                () =>
                {
                    parameters = parameters.SelectField("2");
                });

            Assert.Equal(1, parameters.Count);

            Assert.Equal(3, parameters["fields"].Length);
        }

        [Fact]
        public void SelectField_DoesNotEndInComma_WhenMoreParametersAdded()
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>().SelectField("1");

            // assert that it doesn't end with ,
            Assert.NotEqual(',', parameters["fields"][parameters["fields"].Length - 1]);

            parameters = parameters.SelectField("2");

            // assert it contains , at 1st index -> 1,2
            Assert.Equal(',', parameters["fields"][1]);

            // assert that it doesn't end with ,
            Assert.NotEqual(',', parameters["fields"][parameters["fields"].Length - 1]);
        }
    }
}