using Confluent.Kafka;
using System.Text.Json;
using YYYoinkAPI.Models;

class AccountProducerService
{
    // private readonly string _bootstrapServers;

    // public AccountProducer()
    // {
        // if (string.IsNullOrWhiteSpace(bootstrapServers))
        // {
        //     throw new ArgumentException("Bootstrap servers must be provided.", nameof(bootstrapServers));
        // }
        //
        // _bootstrapServers = bootstrapServers;
    // }
    
    public bool Produce(string email, string password)
    {
        const string topic = "account_actions";

        var accountData = new CreateAccountAction.AccountData(
            email,
            password
        );

        var accountAction = new CreateAccountAction(
            Guid.NewGuid(),
            "create_account",
            // TODO: set valid timezone not utc
            DateTime.UtcNow,
            topic,
            accountData
        );

        var actionMessage = JsonSerializer.Serialize(accountAction, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9094"
        };

        var completionStatus = false;

        using (var producer = new ProducerBuilder<string, string>(config).Build())
        {
            producer.Produce(topic, new Message<string, string>
                {
                    Key = null,
                    Value = actionMessage
                },
                (deliveryReport) =>
                {
                    if (deliveryReport.Error.Code != ErrorCode.NoError)
                    {
                        Console.WriteLine($"failed to deliver message: {deliveryReport.Error.Reason}");
                    }
                    else
                    {
                        completionStatus = true;
                        Console.WriteLine($"produced event to topic {topic}: key = null, value = {actionMessage}");
                    }
                });
            
            producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine($"message produced to topic {topic}");
            return completionStatus;
        }
    }
}