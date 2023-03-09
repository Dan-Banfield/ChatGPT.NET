using System.Threading.Tasks;

namespace ChatGPT.NET
{
    public class ChatGPT
    {
        #region Private Instance Variables

        private string apiKey = "";

        #endregion

        public ChatGPT(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<string> GenerateResponseAsync()
        {
            await Task.Delay(1000);
            return "This is just a test.";
        }
    }
}