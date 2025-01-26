using Confluent.Kafka;
using System;
using System.Threading;

class AccountConsumerService
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9094",
            GroupId = "account_consumer_group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        const string topic = "account_receipts";

        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        using (var consumer = new ConsumerBuilder<string, string>(config).Build())
        {
            consumer.Subscribe(topic);
            try
            {
                while (true)
                {
                    var cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"consumed event from topic {topic}: key = null, value = {cr.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("ctrl-c was pressed");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}