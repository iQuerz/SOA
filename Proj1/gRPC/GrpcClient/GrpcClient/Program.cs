// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcClient;

Console.WriteLine("Press any key to get all readings");
Console.ReadLine();
using var channel = GrpcChannel.ForAddress("http://localhost:5240");
var client = new Greeter.GreeterClient(channel);

var reply = await client.GetAllAsync(new IoTParams { });
Console.WriteLine($"List of IoTReadings : {reply.IoTReadings.Count()}");
Console.WriteLine("Enter an id");
int.TryParse(Console.ReadLine(), out int IdToUpdate);


var reply2 = await client.GetAsync(new IoTId { Id = IdToUpdate });
Console.WriteLine($"List of IoTReadings : {reply2}");
Console.WriteLine("Press any key to create a reading");
Console.ReadLine();

var reply3 = await client.CreateAsync(new IoTReading { 
                                                        Temperature = 10, 
                                                        PH = 10, 
                                                        Turbidity = 10, 
                                                        BOD = 10, 
                                                        FecalColiform = 10, 
                                                        DisolvedOxygen = 10, 
                                                        Nitratenans = 10, 
                                                        Conductivity = 10 });
Console.WriteLine($"List of IoTReadings : {reply3}");
Console.WriteLine("Press any key to update a reading ");
Console.ReadLine();

var reply4 = await client.UpdateAsync(new IoTReading{
                                                        Id = 7,
                                                        Temperature = 10,
                                                        PH = 10,
                                                        Turbidity = 10,
                                                        BOD = 10,
                                                        FecalColiform = 10,
                                                        DisolvedOxygen = 10,
                                                        Nitratenans = 10,
                                                        Conductivity = 10
                                                    });
Console.WriteLine($"List of IoTReadings : {reply4}");
Console.WriteLine("Enter an id");
int.TryParse(Console.ReadLine(), out int IdToDelete);


var reply5 = await client.DeleteAsync(new IoTId { Id = IdToDelete });
Console.WriteLine($"Deleted Reading : {reply5}");