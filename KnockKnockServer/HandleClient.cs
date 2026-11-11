using KnockKnockServer.Services;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using KnockKnockServer.Models;

namespace KnockKnockServer
{
    public class ClientHandler
    {
        private readonly JokeProviderService _JokeProviderService;
        private readonly Joke Joke;
        public ClientHandler(JokeProviderService JokeProviderService)
        {
            _JokeProviderService = JokeProviderService;

            Joke = _JokeProviderService.GetJoke();
        }

        public async Task NewClient(TcpClient client)
        {

            ConversationState State = ConversationState.WaitingForWhosThere;

            using (client)
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8)
            {
                AutoFlush = true
            })
            {
                try
                {
                    await writer.WriteLineAsync("Knock Knock!");

                    while (true)
                    {
                        string? message = await reader.ReadLineAsync();
                        Console.WriteLine($"Client: {message}");

                        switch (State)
                        {
                            case ConversationState.WaitingForWhosThere:
                                if (message?.ToLower() == "who's there?")
                                {
                                    await writer.WriteLineAsync(Joke.Who);
                                    State = ConversationState.WaitingForWho;
                                }
                                else
                                {
                                    await writer.WriteLineAsync("You should say 'Who's there?'");
                                }
                                break;

                            case ConversationState.WaitingForWho:
                                if (message?.ToLower() == $"{Joke.Who.ToLower()} who?")
                                {
                                    await writer.WriteLineAsync(Joke.Punchline);
                                    State = ConversationState.Finished;
                                }
                                else
                                {
                                    await writer.WriteLineAsync($"You should say '{Joke.Who} who?'");
                                }
                                break;
                        }

                        if (message == null || State == ConversationState.Finished)
                        {
                            break;
                        }


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Client error: {ex.Message}");
                }
            }

            Console.WriteLine("Client disconnected!");
        }
    }

}
