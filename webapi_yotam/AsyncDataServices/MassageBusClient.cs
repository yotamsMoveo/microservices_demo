using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using webapi_yotam.DTO;

namespace webapi_yotam.AsyncDataServices
{
    public class MassageBusClient: IMassageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MassageBusClient(IConfiguration config)
        {
            _config = config;
            var factory = new ConnectionFactory()
            {
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"])

            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;
                Console.WriteLine("--> connected to rabbit");


            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> could not connect to rabbit {ex}");
            }

        }

        public void PublishNewPlatform(PlatformPublishedDTO platformPublishedDTO)
        {
            var massage = JsonSerializer.Serialize(platformPublishedDTO);
            if (_connection.IsOpen)
            {
                Console.WriteLine("-->rabbit is open ,sending massage");
                SendMassage(massage);
            }
            else
            {
                Console.WriteLine("-->rabbit is close ,not sending massage");

            }
        }

        private void SendMassage(string massage)
        {
            var body = Encoding.UTF8.GetBytes(massage);
            _channel.BasicPublish(exchange: "trigger",
                routingKey:"",
                basicProperties: null,
                body: body);
            Console.Write($"--> we have sent {massage}");

        }

        public void Dispose()
        {
            Console.WriteLine("-->rabbit dispose");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("-->Rabbit connection shut down");
        }
    }
}

