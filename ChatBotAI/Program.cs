using ChatBotAI.Components;
using ChatBotAI.Services;
using MudBlazor.Services;

namespace ChatBotAI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddMudServices();

            var openAIApiKey = builder.Configuration["OpenAI:ApiKey"] ?? throw new ArgumentNullException("ApiKey not found!");
            builder.Services.AddHttpClient("OpenAI");
            builder.Services.AddScoped<OpenAIService>(op =>
            {
                var httpFactory = op.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpFactory.CreateClient("OpenAI");
                return new OpenAIService(httpClient, openAIApiKey);
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}