using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Victuz
{
    public class APIclass
    {
        private readonly HttpClient httpClient;

        public APIclass()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://official-joke-api.appspot.com/random_joke");

        }

        public async Task<List<Post>> GetPostsAsync(); 
        {
            var response = await HttpClient.GetStringAsync("/posts");
            return JsonSerializer.Deserialize<List<Post>>(response);
        }

    }
}
