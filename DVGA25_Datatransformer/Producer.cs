using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DVGA25_Datatransformer
{
    internal class Producer
    {
        public async Task PublishToQueue(string fileName, string queue, string routing_key)
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.UserName = "danisand104";
                factory.Password = "danisand104_rmq_password";
                factory.VirtualHost = "vh_danisand104";
                factory.HostName = "vortex.cse.kau.se";

                IConnection conn = await factory.CreateConnectionAsync();
                IChannel channel = await conn.CreateChannelAsync();

                string exchange_name = "danisand104_ex";

                // 1. Declare exchange
                await channel.ExchangeDeclareAsync(
                    exchange: exchange_name,
                    type: ExchangeType.Direct,
                    durable: true
                );

                // 2. Declare queue
                await channel.QueueDeclareAsync(
                    queue: queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false
                );

                // 3. Bind queue to exchange
                await channel.QueueBindAsync(
                    queue: queue,
                    exchange: exchange_name,
                    routingKey: routing_key
                );

                // Read message from file
                string message = File.ReadAllText(fileName);

                var props = new BasicProperties();
                props.ContentType = "text/xml";

                byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);

                // 4. Publish message
                await channel.BasicPublishAsync(
                    exchange: exchange_name,
                    routingKey: routing_key,
                    mandatory: false,
                    basicProperties: props,
                    body: messageBodyBytes
                );

                await channel.CloseAsync();
                await conn.CloseAsync();
                await channel.DisposeAsync();
                await conn.DisposeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                throw;
            }
        }
    }
}
