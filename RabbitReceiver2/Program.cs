using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri(uriString: "amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Receiver2 App";


IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();

string exchangeName = "ex";
string routingKey = "rk";
string queueName = "qn";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);

// prefetchSize: limit the message size
// prefetchCount: limit the number of message
channel.BasicQos(0, 1, false);



var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    Task.Delay(TimeSpan.FromSeconds(3)).Wait();

    var body = args.Body.ToArray();

    string message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Message received {message}");

    // positively acknowledge a single delivery, the message will be discarded
    channel.BasicAck(args.DeliveryTag, false);
};


string consumerTag = channel.BasicConsume(queueName, false, consumer);

// keep console running to receive message 
Console.ReadLine();

channel.BasicCancel(consumerTag);

channel.Close();
connection.Close();



