using System;
using System.Text;
using RabbitMQ.Client;

namespace SimpleCpuMonitor.Worker
{
    public static class Sender
    {
        static ConnectionFactory _connectionFactory;
        public static ConnectionFactory ConnectionFactory
        {
            get
            {
                if (_connectionFactory == null)
                {
                    _connectionFactory = new ConnectionFactory { HostName = "localhost" };
                }
                return _connectionFactory;
            }
        }


        public static void SendOverloadInfo(float value)
        {
            Send(value.ToString());
        }

        public static void Send(string message)
        {
            using (var connection = ConnectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.QueueDeclare("test_q", false, false, false, null);
                channel.ExchangeDeclare("test1", "topic");
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "test1", routingKey: "test1", basicProperties: null, body: body);

                Console.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
