using RabbitMQ.Client;
using RabbitMQ_Sender.Models;
using System;
using System.Text;

// https://csharp.hotexamples.com/examples/RabbitMQ.Client/ConnectionFactory/-/php-connectionfactory-class-examples.html
namespace RabbitMQ_Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" }; //, Port = 15672

            //IConnection conn = factory.CreateConnection();
            //IModel ch = conn.CreateModel();
            //ch.Close(Constants.ReplySuccess, "Closing the channel"); 
            //conn.Close(Constants.ReplySuccess, "Closing the connection");

            Category _cat = Category.GetInstance();
            _cat.Fill(_cat);

          

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    while (true)
                    {
                        #region string continuously sending msg
                        string messagex = Console.ReadLine();
                        var body = Helper<string>.ObjectToByteArray(messagex); // Encoding.UTF8.GetBytes(messagex); 
                        #endregion

                        //var body = Helper<Category>.ObjectToByteArray(_cat);

                        channel.QueueDeclare(queue: "modal",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);


                        //string message = $"\n Category Name : { _cat.Name}. \n Description   : {_cat.Description}.";
                       
                        channel.BasicPublish(exchange: "",
                                             routingKey: "modal",
                                             basicProperties: null,
                                             body: body);

                        Console.WriteLine("Sending Message : {0}" , messagex);
                        //Console.WriteLine(" [x] Sent => {0}", message);
                    }

                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
