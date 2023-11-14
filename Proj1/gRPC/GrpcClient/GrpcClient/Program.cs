// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcClient;

Console.WriteLine("Hello, World!");
Console.ReadLine();
using var channel = GrpcChannel.ForAddress("http://localhost:5240");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient"});
Console.WriteLine($"Greetings : {reply.Message}");
Console.WriteLine("Press any key");
Console.ReadLine();