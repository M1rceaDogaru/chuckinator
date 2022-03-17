using Chuckinator.Models;
using Newtonsoft.Json;
using OneOf;
using OneOf.Types;

namespace Chuckinator.Services
{
    internal class JokeRetriever : IJokeRetriever
    {
        private readonly string _jokesApiUrl;
        private readonly HttpClient _httpClient;

        public JokeRetriever(HttpClient httpClient, Settings settings)
        {
            _jokesApiUrl = settings.JokesApiUrl;
            _httpClient = httpClient;
        }

        public async Task<OneOf<Success<Joke>, Error>> GetJoke()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_jokesApiUrl);
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
