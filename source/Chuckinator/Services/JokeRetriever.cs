using Chuckinator.Models;
using Newtonsoft.Json;
using OneOf;
using OneOf.Types;

namespace Chuckinator.Services
{
    internal class JokeRetriever : IJokeRetriever
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _jokesApiUrl;

        public JokeRetriever(IHttpClientFactory httpClientFactory, Settings settings)
        {
            _httpClientFactory = httpClientFactory;
            _jokesApiUrl = settings.JokesApiUrl;
        }

        public async Task<OneOf<Success<Joke>, Error>> GetJoke()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await client.GetAsync(_jokesApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return new Success<Joke>(JsonConvert.DeserializeObject<Joke>(content));
                }
            } 
            catch
            {
                //TODO log something for posterity
            }            

            // if we got this far something went wrong
            return new Error();
        }
    }
}
