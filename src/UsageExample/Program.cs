
namespace UsageExample
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    static class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<Service1>();
            services.AddTransient<Service2>();
            services.AddHttpClient<MyAPIHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://www.google.com");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
            });
            
            var provider = services.BuildServiceProvider();

            using (var service1 = provider.GetService<Service1>())
            {
                Console.Write(service1.Get().Result);
            }

            Console.WriteLine("\n Pressione qualquer tecla para continuar...");
            Console.Read();
        }

    }

    public class Service1 : IDisposable
    {
        private readonly Service2 _S2;
        private readonly MyAPIHttpClient _http;
        public Service1(Service2 s2, MyAPIHttpClient http)
        {
            Console.WriteLine("Iniciando o Serviço 1");
            _S2 = s2;
            _http = http;
        }

        public async Task<string> Get() => 
            await Task.Run(()=> 
            {
                Console.WriteLine("Realizando a captura dos dados");
                _S2.Print();
                return GetGoogle();
            });

        private async Task<string> GetGoogle() => await _http.Client.GetStringAsync("/");


        public void Dispose()
        {
            Console.WriteLine("Finalizando o Serviço 1");
            _S2.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public class Service2 : IDisposable
    {
        public Service2()
        {
            Console.WriteLine("Iniciando o Serviço 2");
        }

        public void Print() => Console.WriteLine("Obtendo o Serviço 2.");

        public void Dispose()
        {
            Console.WriteLine("Finalizando o Serviço 2");
            GC.SuppressFinalize(this);
        }
    }
}
