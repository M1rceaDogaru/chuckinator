using Chuckinator.Models;
using Newtonsoft.Json;

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

        public async Task<Joke> GetJoke()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await client.GetAsync(_jokesApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Joke>(content);
                }
            } 
            catch (Exception ex)
            {
                //TODO something here
            }            

            return null;
        }
    }
}
