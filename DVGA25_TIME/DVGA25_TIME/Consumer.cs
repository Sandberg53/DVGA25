using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVGA25_TIME
{
    internal class Consumer
    {
        public Consumer() { }
        public Consumer(string name) { }

        public async void Receive()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                //LAB3: använd dina egna uppgifter:
                factory.UserName = "danisand104";
                factory.Password = "danisand104_rmq_password";
                factory.VirtualHost = "vh_danisand104";
                factory.HostName = "vortex.cse.kau.se";
                //skapa connection:
                IConnection conn = await factory.CreateConnectionAsync();
                //skapa channel:
                IChannel channel = await conn.CreateChannelAsync();
                //skapa exchange, köer och bindings:
                //LAB3: använd dina egna uppgifter:
                string exchange_name = "danisand104_ex";
                string queue = "danisand104_TIME";
                string routing_key = "TIME";

                //LAB3: skapa exchange, kö och binding:
                //skriv rätt anrop till funktionerna nedan! ******
                //LAB3: skapa exchange, kö och binding:

                await channel.ExchangeDeclareAsync(
                    exchange: exchange_name,
                    type: ExchangeType.Direct,
                    durable: true
                );

                await channel.QueueDeclareAsync(
                    queue: queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false
                );

                await channel.QueueBindAsync(
                    queue: queue,
                    exchange: exchange_name,
                    routingKey: routing_key
                );
                //************************************************

                var consumer = new AsyncEventingBasicConsumer(channel);
                var message = String.Empty;
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    message = Encoding.UTF8.GetString(body);

                    try
                    {
                        //spara undan filen ("databasen") som skickats från Data transformern. LAB3: ändra sökväg vid behov 
                        StreamWriter sw = new StreamWriter(@"employee_time.xml");
                        sw.Write(message);
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error:" + e.Message);
                    }

                    await channel.BasicAckAsync(ea.DeliveryTag, false);

                };

                await channel.BasicConsumeAsync(queue: queue, autoAck: true, consumer: consumer);
            }

            catch (RabbitMQClientException e)
            {

                MessageBox.Show("Error: " + e.Message.ToString());
                throw;
            }
        }
    }
    
}
