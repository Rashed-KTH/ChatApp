using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NATS.Client;

namespace ChatApp.Subscribe
{
    public class Subscriber 
    {
        private string subject {get; set;} = null;

        public void Run (string [] args)
        {
            // For simplicity we assume first argument is 'subject'
            subject = args[0];
            string filePath = Path.Combine(Path.GetTempPath(), "message.txt");

            Options opts = ConnectionFactory.GetDefaultOptions();
            using (IConnection c = new ConnectionFactory().CreateConnection(opts))
            {
                using (ISyncSubscription sub = c.SubscribeSync(subject))
                {
                    while(true)
                    {
                        Msg message = sub.NextMessage();
                        using (StreamWriter sw = File.AppendText(filePath))
                        { 
                            string payload = Encoding.Default.GetString(message.Data);
                            sw.WriteLine(payload);
                            Console.WriteLine($"Message received: {payload}");
                        }
                    }
                }
            }

        }
    }
}