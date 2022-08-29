using System;
using System.Text;
using CommandsService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandsService.AsyncDataServices
{
    public class MassageBusSubscriber:BackgroundService
    {
        private readonly IConfiguration _config;
        private readonly IEventProcessor _eventProcessor;
        private IConnection _connection;
        private IModel _chanel;
        private string _queueName;

        public MassageBusSubscriber(IConfiguration config,IEventProcessor eventProcessor)
        {
            _config = config;
            _eventProcessor = eventProcessor;
            InitializeRabbitMq();
        }

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"])


            };
            _connection = factory.CreateConnection();
            _chanel = _connection.CreateModel();
            _chanel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queueName = _chanel.QueueDeclare().QueueName;
            _chanel.QueueBind(queue: _queueName,
                exchange: "trigger",
                routingKey: "");

            Console.WriteLine("-->listening on massage bus...");
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;


        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_chanel);
            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine("-->event received!");
                var body = ea.Body;
                var notificationMassage = Encoding.UTF8.GetString(body.ToArray());
                _eventProcessor.ProcessEvent(notificationMassage);

            };

            _chanel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

        private void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> connecrion shut down");
        }

        public override void Dispose()
        {
            if (_chanel.IsOpen)
            {
                _chanel.Close();
                _connection.Close();
            }
            base.Dispose();
        }
    }
}

