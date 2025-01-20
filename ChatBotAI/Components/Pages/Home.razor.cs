using ChatBotAI.Services;
using Microsoft.AspNetCore.Components;

namespace ChatBotAI.Components.Pages
{
    public partial class Home
    {
        [Inject] public OpenAIService OpenAiService { get; set; }
        public string Answer { get; set; }
        public string Prompt { get; set; }
        public List<string> ConversationHistory = new List<string>();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private async Task Send(string prompt)
        {
            ConversationHistory.Add("Me: " + prompt + "\n");
            Answer = await OpenAiService.RetrieveAnswerAsync(Prompt);
            ConversationHistory.Add("Bot: " + Answer + "\n");

            StateHasChanged();
        }
    }
}