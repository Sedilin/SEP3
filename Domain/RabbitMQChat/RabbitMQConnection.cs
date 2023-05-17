using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Domain.RabbitMQChat;

public class RabbitMQConnection
{
    public IConnection GetConnection()
    {
        ConnectionFactory factory = new ConnectionFactory();
        factory.HostName = "localhost";
        return factory.CreateConnection();
    }

    public bool send(IConnection con, string message, string friendqueue)
    {
        try
        {
            IModel channel = con.CreateModel();
            channel.ExchangeDeclare("messageexchange", ExchangeType.Direct);
            channel.QueueDeclare(friendqueue, true, false, false, null);
            channel.QueueBind(friendqueue, "messageexchange", friendqueue, null);
            var msg = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("messageexchange", friendqueue, null, msg);
        }
        catch (Exception)
        {
            throw new Exception("RabbitMQ send gone wrong");
        }
        return true;
    }

    public string receive(IConnection con, string myqueue)
    {
        try
        {
            string queue = myqueue;
            IModel channel = con.CreateModel();
            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            BasicGetResult result = channel.BasicGet(queue: queue, autoAck: true);
            if (result != null)
            {
                return Encoding.UTF8.GetString(result.Body.ToArray());
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}