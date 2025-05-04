using MFramework.Services.FakeData;
using System.Threading.Channels;

namespace ChannelsSample;

class Program
{
    static async Task Main(string[] args)
    {
        var channel = Channel.CreateBounded<string>(10);

        var producer = ProduceData(channel.Writer);
        var consumer1 = CreateConsumer(channel.Reader, "consumer1", 1, ConsoleColor.Yellow);
        var consumer2 = CreateConsumer(channel.Reader, "consumer2", 3, ConsoleColor.Cyan);

        await Task.WhenAll(producer, consumer1, consumer2);
    }

    private static async Task ProduceData(ChannelWriter<string> writer)
    {
        for (int i = 0; i < 20; i++)
        {
            await writer.WriteAsync(NameData.GetCompanyName());
            await Task.Delay(500);
        }


        writer.Complete();
    }


    private static async Task CreateConsumer(ChannelReader<string> reader, string name, int priority = 1, ConsoleColor color = ConsoleColor.Yellow)
    {
        // Bu yöntem sayesinde veri geldiğinde tetiklenir, yani:
        // İçeride bir event-based sinyalleşme (semaphore/awaiter) sistemi vardır.
        // Boşta bekler, veri geldiğinde “uyandırılır”. CPU yormaz, yüksek verimli ve event tabanlıdır.
        // Kanal kapandığında veya işlem tamamlandığında otomatik olarak kapanır.
        await foreach (var item in reader.ReadAllAsync())
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{name}] ⮕ {item} (count : {reader.Count})");
            Console.ResetColor();

            if (priority > 0)
                await Task.Delay(500 * (5 - priority));
        }


        //// Alternatif Okuma
        //// Kalan daki verilerin hepsini okur. Fakat okuma işlemi tamamlanınca kanal kapanır.
        //// Dolayısı ile bu kod bir kere çalıştıktan sonra bir daha çalışmaz. (Kanal kapalı olduğu için)
        //while (reader.TryRead(out var item))
        //{
        //    Console.ForegroundColor = color;
        //    Console.WriteLine($"[{name}] ⮕ {item} (count : {reader.Count})");
        //    Console.ResetColor();

        //    if (priority > 0)
        //        await Task.Delay(500 * (5 - priority));
        //}

        Console.WriteLine($"[{name}] : Kanal kapandı veya işlem tamamlandı.");
    }
}
