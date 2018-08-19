
namespace UsageExample
{
    using System.Net.Http;

    public class MyAPIHttpClient
    {
        public HttpClient Client { get; set; }
        public MyAPIHttpClient(HttpClient client)
        {
            Client = client;
        }

    }
}
