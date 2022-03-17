using Chuckinator.Models;
using OneOf;
using OneOf.Types;

namespace Chuckinator.Services
{
    internal interface IJokeRetriever
    {
        Task<OneOf<Success<Joke>, Error>> GetJoke();
    }
}
