# ChatGPT.NET

<h4 align="center">An unofficial .NET implementation of OpenAI's ChatGPT API.</h4>

<div align="center">

[![NuGet Package](https://img.shields.io/nuget/v/)](https://www.nuget.org/packages/ChatGPT.NET.API.DanBanfield.1.0.0)

</div>  

## Installation
Stable builds are available through [NuGet](https://www.nuget.org/packages/ChatGPT.NET.API.DanBanfield.1.0.0).  
```
Install-Package ChatGPT.NET.API.DanBanfield.1.0.0
```

## Usage
You can use the ChatGPT API as shown:
```csharp
using ChatGPT.NET;

ChatGPTAPI chatGPTAPI = new ChatGPTAPI("YOUR_API_KEY");
ChatGPTAPIResponse chatGPTAPIResponse = await chatGPTAPI.GenerateResponseAsync("YOUR_PROMPT_HERE");

//Check for errors!
if (chatGPTAPIResponse.SuccessfulRequest()) { Console.WriteLine(chatGPTAPIResponse.responseText); }
```

## Documentation

View the documentation on [OpenAI's website](https://platform.openai.com/docs/api-reference/chat).

## Credits
OpenAI - For their ChatGPT API [OpenAI](https://openai.com/).
