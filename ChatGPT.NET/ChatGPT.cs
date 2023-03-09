using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace ChatGPT.NET
{
    public class ChatGPTAPI
    {
        #region Private Instance Variables

        private string apiKey = "";

        #endregion

        private const string API_ENDPOINT 
            = "https://api.openai.com/v1/chat/completions";

        public ChatGPTAPI(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<ChatGPTAPIResponse> GenerateResponseAsync(string prompt)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", apiKey);

                using (HttpRequestMessage httpRequestMessage 
                    = new HttpRequestMessage(HttpMethod.Post, API_ENDPOINT))
                {
                    string model = "gpt-3.5-turbo";
                    var messages = new[] { new { role = "user", content = prompt } };
                    var data = new { model, messages };

                    StringContent content 
                        = new StringContent(JsonSerializer
                        .Serialize(data), System.Text.Encoding.UTF8, 
                        "application/json");

                    httpRequestMessage.Content = content;

                    using (HttpResponseMessage httpResponseMessage 
                        = await httpClient.SendAsync(httpRequestMessage))
                    {
                        if (!httpResponseMessage.IsSuccessStatusCode)
                            return new ChatGPTAPIResponse("", false);

                        Stream streamResponse 
                            = await httpResponseMessage
                            .Content.ReadAsStreamAsync();

                        Root root = await JsonSerializer
                            .DeserializeAsync<Root>(streamResponse);

                        ChatGPTAPIResponse result 
                            = new ChatGPTAPIResponse(root.choices[0]
                            .message.content.Trim(), true);

                        return result;
                    }
                }
            }
        }
    }

    public class ChatGPTAPIResponse
    {
        public string responseText { get; set; }
        private bool successfulRequest { get; set; }

        public ChatGPTAPIResponse(string responseText, bool successfulRequest)
        {
            this.responseText = responseText;
            this.successfulRequest = successfulRequest;
        }

        public bool SuccessfulRequest() => successfulRequest;
    }

    public class Choice
    {
        public int index { get; set; }
        public Message message { get; set; }
        public string finish_reason { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public List<Choice> choices { get; set; }
        public Usage usage { get; set; }
    }

    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
}