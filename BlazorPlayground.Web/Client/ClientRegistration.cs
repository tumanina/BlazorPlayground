namespace BlazorPlayground.Web.Client
{
    public static class ClientRegistration
    {
        public static void RegisterClient(this IServiceCollection serviceCollection, ApiClientConfiguration clientConfiguration)
        {
            serviceCollection.AddScoped<IClient>(c => new Client(clientConfiguration));
        }
    }
}
