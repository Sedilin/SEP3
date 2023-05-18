using System.Text;
using System.Text.Json;
using Domain.DTOs;
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