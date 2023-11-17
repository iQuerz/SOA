// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcClient;

Console.WriteLine("Press any key to get all readings");
Console.ReadLine();
using var channel = GrpcChannel.ForAddress("http://localhost:5240");
var client = new Greeter.GreeterClient(channel);

var reply = await client.GetAllAsync(new IoTParams { PageNumber = 1, PageSize = 100 });
Console.WriteLine($"List of IoTReadings : {reply.IoTReadings}");
Console.WriteLine("Enter an time");
string Time = Console.ReadLine();
string Device = Console.ReadLine();



var reply2 = await client.GetAsync(new IoTId { Ts = Time, Device = Device });
Console.WriteLine($"List of IoTReadings : {reply2}");
Console.WriteLine("Press any key to create a reading");
Console.ReadLine();

var reply3 = await client.CreateAsync(new IoTReading {
                                                    Ts = "123",
                                                    Device = "22",
                                                    Co = 3,
                                                    Humidity = 4,
                                                    Light = true,
                                                    Lpg = 6,
                                                    Motion = true,
                                                    Smoke = 8,
                                                    Temp = 9, });
Console.WriteLine($"List of IoTReadings : {reply3}");
Console.WriteLine("Press any key to update a reading ");
Console.ReadLine();

var reply4 = await client.UpdateAsync(new IoTReading{
                                                        Ts = "12",
                                                        Device = "22",
                                                        Co = 4,
                                                        Humidity = 5,
                                                        Light = true,
                                                        Lpg = 6,
                                                        Motion = true,
                                                        Smoke = 7,
                                                        Temp = 8,
                                                    });
Console.WriteLine($"List of IoTReadings : {reply4}");
Console.WriteLine("Enter an id");
string TimeD = Console.ReadLine();
string DeviceD = Console.ReadLine();


var reply5 = await client.DeleteAsync(new IoTId { Ts = Time, Device = Device });
Console.WriteLine($"Deleted Reading : {reply5}");