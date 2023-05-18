using System.Text;
using System.Text.Json;
using Domain.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Domain.RabbitMQChat;

public sealed class RabbitMQConnection
{
    private static RabbitMQConnection instance = null;
    private static readonly object padlock = new object();

    private static IConnection connection;
    
    private RabbitMQConnection()
    {
    }

    public static RabbitMQConnection Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new RabbitMQConnection();
                    ConnectionFactory factory = new ConnectionFactory();
                    factory.HostName = "localhost";
                    connection = factory.CreateConnection();
                }
            }

            return instance;
        }
    }
    
    public IConnection GetConnection()
    {
        return connection;
    }

    public bool send(IConnection con, MessageDto dto)
    {
        try
        {
            IModel channel = con.CreateModel();
            channel.ExchangeDeclare("messageexchange", ExchangeType.Direct);
            channel.QueueDeclare(dto.Receiver.UserName, true, false, false, null);
            channel.QueueBind(dto.Receiver.UserName, "messageexchange", dto.Receiver.UserName, null);
            var serialized = JsonSerializer.Serialize(dto);
            var msg = Encoding.UTF8.GetBytes(serialized);
            channel.BasicPublish("messageexchange", dto.Receiver.UserName, null, msg);
            channel.Close();
        }
        catch (Exception)
        {
            throw new Exception("RabbitMQ send gone wrong");
        }
        return true;
    }

    public MessageDto receive(IConnection con, string myqueue)
    {
        try
        {
            string queue = myqueue;
            IModel channel = con.CreateModel();
            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            BasicGetResult result = channel.BasicGet(queue: queue, autoAck: true);
            if (result != null)
            {
                var message = Encoding.UTF8.GetString(result.Body.ToArray());
                channel.Close(); 
                return JsonSerializer.Deserialize<MessageDto>(message);
            }
            channel.Close();
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}