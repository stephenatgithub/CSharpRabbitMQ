using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri(uriString: "amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Sender App";


IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();

string exchangeName = "ex";
string routingKey = "rk";
string queueName = "qn";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);

// UTF8 version of byte message
//byte[] messageBytes = Encoding.UTF8.GetBytes("hello again");
//channel.BasicPublish(exchangeName, routingKey, null, messageBytes);

for (int i = 0; i < 60; i++)
{
    Console.WriteLine($"Sending message {i}");
    byte[] messageBytes = Encoding.UTF8.GetBytes($"This is message {i}");
    channel.BasicPublish(exchangeName, routingKey, null, messageBytes);

    // wait 1 seconds
    Thread.Sleep(1000);
}

channel.Close();
connection.Close();






