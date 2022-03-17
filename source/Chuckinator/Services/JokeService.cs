using Chuckinator.Models;
using OneOf;

namespace Chuckinator.Services
{
    internal class JokeService : IJokeService
    {
        private readonly IJokeRetriever _jokeRetriever;
        private readonly List<Joke> _jokeCache;
        private Joke _lastJoke;


        public JokeService(IJokeRetriever jokeRetriever)
        {
            _jokeRetriever = jokeRetriever;
            _jokeCache = new List<Joke>();
        }

        public async Task<OneOf<Joke, NoJoke>> GetNextJoke()
        {
            if (_lastJoke == null || _jokeCache.Count == 0)
            {
                return await Task.FromResult(new NoJoke("No jokes retrieved"));
            }

            var newJokeIndex = _jokeCache.IndexOf(_lastJoke) + 1;
            if (newJokeIndex >= _jokeCache.Count)
            {
                return await Task.FromResult(new NoJoke("No more jokes, you're at the end"));
            }

            _lastJoke = _jokeCache[newJokeIndex];

            return await Task.FromResult(_lastJoke);
        }

        public async Task<OneOf<Joke, NoJoke>> GetPreviousJoke()
        {
            if (_lastJoke == null || _jokeCache.Count == 0)
            {
                return await Task.FromResult(new NoJoke("No jokes retrieved"));
            }

            var newJokeIndex = _jokeCache.IndexOf(_lastJoke) - 1;
            if (newJokeIndex < 0)
            {
                return await Task.FromResult(new NoJoke("No more jokes, you're at the beginning"));
            }

            _lastJoke = _jokeCache[newJokeIndex];

            return await Task.FromResult(_lastJoke);
        }

        public async Task<OneOf<Joke, NoJoke>> GetRandomJoke()
        {
            var jokeResult = await _jokeRetriever.GetJoke();
            return jokeResult.Match<OneOf<Joke, NoJoke>>(success =>
            {
                _lastJoke = success.Value;
                if (!_jokeCache.Contains(_lastJoke))
                {
                    _jokeCache.Add(_lastJoke);
                }

                return _lastJoke;
            }, error => new NoJoke("There was an error retrieving a new joke"));
        }
    }
}
