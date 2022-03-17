using Chuckinator.Models;
using Chuckinator.Services;
using OneOf;

namespace Chuckinator
{
    internal class Application
    {
        private readonly IJokeService _jokeService;

        public Application(IJokeService jokeService)
        {
            _jokeService = jokeService;
        }

        public async Task Run()
        {
            OneOf<Joke, NoJoke> jokeOption;
            while (true)
            {
                Console.WriteLine($"Press j for new joke, p for previous joke and n for next joke.");
                var key = Console.ReadKey();

                switch ((Models.Action)key.KeyChar)
                {
                    case Models.Action.NewJoke: HandleJoke(await _jokeService.GetRandomJoke()); break;
                    case Models.Action.PreviousJoke:  HandleJoke(await _jokeService.GetNextJoke()); break;
                    case Models.Action.NextJoke: HandleJoke(await _jokeService.GetPreviousJoke()); break;
                };
            }
        }
        private void HandleJoke(OneOf<Joke, NoJoke> jokeOption)
        {
            jokeOption.Switch(joke => Console.WriteLine($"The Chuckinator says: {joke.Value}"),
                noJoke => Console.WriteLine($"The Chuckinator struggled. {noJoke.Reason}"));
        }
    }
}
