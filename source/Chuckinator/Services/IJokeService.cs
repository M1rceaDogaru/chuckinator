using Chuckinator.Models;
using OneOf;

namespace Chuckinator.Services
{
    internal interface IJokeService
    {
        Task<OneOf<Joke, NoJoke>> GetRandomJoke();
        Task<OneOf<Joke, NoJoke>> GetPreviousJoke();
        Task<OneOf<Joke, NoJoke>> GetNextJoke();
    }
}
