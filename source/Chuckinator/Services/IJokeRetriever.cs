using Chuckinator.Models;

namespace Chuckinator.Services
{
    internal interface IJokeRetriever
    {
        Task<Joke> GetJoke();
    }
}
