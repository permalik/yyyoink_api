using Confluent.Kafka;
using System;

class AccountProducer
{
    static void Main(string[] args)
    {
        const string topic = "account_events";

        string[] accounts =
        [
            "user_one@gmail.com;qwer1234!",
            "user_two@gmail.com;qwer1234!",
            "user_three@gmail.com;qwer1234!"
        ];

        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9094"
        };

        using (var producer = new ProducerBuilder<string, string>(config).Build())
        {
            var numProduced = 0;
            const int numMessages = 3;

            for (int i = 0; i < numMessages; i++)
            {
                var account = accounts[i];

                producer.Produce(topic, new Message<string, string>
                    {
                        Key = null,
                        Value = account
                    },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"produced event to topic {topic}: key = null, value = {account}");
                            numProduced += 1;
                        }
                    });
            }

            producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
        }
    }
}