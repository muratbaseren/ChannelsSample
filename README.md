# NET Core : Channels Sample

This project demonstrates the use of `System.Threading.Channels` in .NET 9 to implement a producer-consumer pattern. The application showcases how to efficiently handle data streams using bounded channels with multiple consumers and a single producer.

## Features  
- **Producer-Consumer Pattern**  
 A producer generates company names using the `MFramework.Services.FakeData` library and writes them to a bounded channel.  
 Two consumers read from the channel with different priorities and display the data in the console with distinct colors.  

- **Efficient Data Handling**  
 The application leverages asynchronous and event-driven mechanisms provided by `ChannelReader` and `ChannelWriter` for high performance and low CPU usage.  

- **Customizable Behavior**  
 - Adjustable consumer priorities.  
 - Configurable channel capacity.  
 - Colored console output for better visualization.  

## Prerequisites  
- .NET 9 SDK  
- `MFramework.Services.FakeData` NuGet package (version 1.0.23)  

## How to Run  
1. Clone the repository.  
2. Restore dependencies using `dotnet restore`.  
3. Build the project using `dotnet build`.  
4. Run the application using `dotnet run`.  

## Code Overview  

### Producer  
The producer generates 20 random company names and writes them to the channel with a delay of 500ms between each write.  

### Consumers  
Two consumers read data from the channel  
- **Consumer1**: Lower priority, processes data faster.  
- **Consumer2**: Higher priority, processes data slower.  

Both consumers display the data in the console with their respective colors and handle channel completion gracefully.  

## License  
This project is licensed under the MIT License.