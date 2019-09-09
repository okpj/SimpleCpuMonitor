using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SimpleCpuMonitor.Configuration;
using SimpleCpuMonitor.Container;
using SimpleCpuMonitor.Model;

namespace SimpleCpuMonitor.Worker
{
    public class Sender
    {
        public static IConnection GetConnection()
        {
            return new ConnectionFactory
            {
                HostName = CompositionRoot.Container.GetInstance<RabbitMQConfigurations>().Host,
                UserName = CompositionRoot.Container.GetInstance<RabbitMQConfigurations>().User,
                Password = CompositionRoot.Container.GetInstance<RabbitMQConfigurations>().Password
            }.CreateConnection();
        }


        public static void SendOverloadInfo(float value)
        {
            var message = new CpuMessage
            {
                DateTime = DateTime.Now,
                Usage = value
            };

            Send(JsonConvert.SerializeObject(message));
        }

        public static void Send(string message)
        {
            var exchange = CompositionRoot.Container.GetInstance<RabbitMQConfigurations>().Exchange;
            var routingKey = CompositionRoot.Container.GetInstance<RabbitMQConfigurations>().RoutingKey;
            var type = CompositionRoot.Container.GetInstance<RabbitMQConfigurations>().Type;

            using (var connection = GetConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.QueueDeclare("test_q", false, false, false, null);
                channel.ExchangeDeclare(exchange, type);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);

                Console.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
