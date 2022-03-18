using Chuckinator.Models;
using Chuckinator.Services;
using NSubstitute;
using NUnit.Framework;
using OneOf;
using OneOf.Types;
using System.Threading.Tasks;

namespace Chuckinator.Tests.UnitTests
{
    [TestFixture]
    public class JokeServiceTests
    {
        private IJokeRetriever _jokeRetriever;
        private JokeService _jokeService;

        [SetUp]
        public void Setup()
        {
            _jokeRetriever = Substitute.For<IJokeRetriever>();
            _jokeService = new JokeService(_jokeRetriever);
        }

        [Test]
        public async Task ShouldGetNewJokeUsingRetriever()
        {
            var expectedJoke = new Joke("someid", "This is funny");
            _jokeRetriever.GetJoke().Returns(Task.FromResult<OneOf<Success<Joke>, Error>>(new Success<Joke>(expectedJoke)));
            
            var jokeResult = await _jokeService.GetRandomJoke();
            jokeResult.Switch(joke => Assert.AreEqual(expectedJoke, joke), 
                noJoke => Assert.Fail("Should return a valid joke"));
        }

        [Test]
        public async Task ShouldReturnNoJokeWhenRetrieverFails()
        {
            _jokeRetriever.GetJoke().Returns(Task.FromResult<OneOf<Success<Joke>, Error>>(new Error()));

            var jokeResult = await _jokeService.GetRandomJoke();
            jokeResult.Switch(joke => Assert.Fail("No joke should be returned as retriever failed"),
                noJoke => Assert.Pass("No joke returned as expected"));
        }

        //TODO add more tests here
    }
}
