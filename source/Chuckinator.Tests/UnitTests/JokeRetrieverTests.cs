using Chuckinator.Models;
using Chuckinator.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Chuckinator.Tests.UnitTests
{
    [TestFixture]
    public class JokeRetrieverTests
    {
        [Test]
        public async Task ShouldReturnError()
        {
            var jokeRetriever = new JokeRetriever(new HttpClient(), new Settings { JokesApiUrl = "Some malformed url" });
            var jokeResult = await jokeRetriever.GetJoke();

            jokeResult.Switch(success => Assert.Fail("Request should not be successful"), 
                error => Assert.Pass("Request failed as expected"));
        } 

        [Test]
        public async Task ShouldReturnAJoke()
        {
            var expectedJoke = new Joke("someid", "This is funny");
            var httpClient = new HttpClient(new FakeHttpMessageHandler(JsonConvert.SerializeObject(expectedJoke)));
            var jokeRetriever = new JokeRetriever(httpClient, new Settings { JokesApiUrl = "http://localhost" });

            var jokeResult = await jokeRetriever.GetJoke();

            jokeResult.Switch(success => Assert.AreEqual(success.Value, expectedJoke),
                error => Assert.Fail("Request should not fail"));
        }

        internal class FakeHttpMessageHandler : HttpMessageHandler
        {
            private readonly string _returnedContent;

            public FakeHttpMessageHandler(string returnedContent)
            {
                _returnedContent = returnedContent;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(_returnedContent) });
            }
        }
    }
}
