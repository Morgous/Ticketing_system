using Newtonsoft.Json;
using System.Text;

public class Program
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task Main(string[] args)
    {
        var tasks = new List<Task>();
        var successfulResponses = 0;
        var cartId = Guid.NewGuid();
        var eventId = Guid.Parse("b1e8c82c-736f-4a6b-9f10-15d562ee5692");
        var seatId = Guid.Parse("6d762474-a6c5-49a1-8461-4a93b2fe4c82");
        var version = 0;

        for (int i = 0; i < 3; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                var response = await SendRequestAsync(cartId, eventId, seatId, version, "pessimistic");
                if (response.IsSuccessStatusCode)
                {
                    successfulResponses++;
                }
            }));
        }

        await Task.WhenAll(tasks);

        Console.WriteLine($"Number of successful responses: {successfulResponses}");
    }

    private static async Task<HttpResponseMessage> SendRequestAsync(Guid cartId, Guid eventId, Guid seatId, int version, string strategy)
    {
        var url = $"http://localhost:5023/api/order/carts/{cartId}/{strategy}";
        var newOrder = new
        {
            EventId = eventId,
            SeatId = seatId,
        };

        var json = JsonConvert.SerializeObject(newOrder);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await client.PostAsync(url, content);
    }
}