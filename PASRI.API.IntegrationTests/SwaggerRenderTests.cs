using NUnit.Framework;
using System.Threading.Tasks;

namespace PASRI.API.IntegrationTests
{
    public class SwaggerRenderTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task Index_Get_ReturnsSwaggerUIBundle()
        {
            var response = await Client.GetAsync("/index.html");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseString.Contains("SwaggerUIBundle"));
        }
    }
}
