using Chuckinator.Models;

namespace Chuckinator.Services
{
    internal interface IJokeService
    {
        Task<Joke> GetRandomJoke();
        Task<Joke> GetPreviousJoke();
        Task<Joke> GetNextJoke();
    }
}
