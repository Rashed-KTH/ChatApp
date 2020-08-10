using System;
using System.Globalization;
using System.Text;
using NATS.Client;


namespace ChatApp.Publish
{
    public class Publisher
    {   
        public void Run(string []args)
        {
            // avoiding any logic for simplicity we assume first argument
            // is 'subject' and second argument is 'user name'.
            string subject = args[0];
            Console.WriteLine($"Subject: '{subject}' ");
            string userName = args[1];

            Options opts = ConnectionFactory.GetDefaultOptions();
            // opts.Name = "Rashed";
            using (IConnection conn = new ConnectionFactory().CreateConnection(opts))
            {
                while(true)
                {
                    Console.Write("Message to publish: ");
                    string messageToPublish = Console.ReadLine();
                    string dt = DateTime.Now.ToString(CultureInfo.GetCultureInfo("sv-SE"));
                    messageToPublish = $"[{userName}-{dt}] " + messageToPublish;
                    byte [] message = Encoding.Default.GetBytes(messageToPublish);
                    conn.Publish(subject, message);
                }
            }
        }
    }
}
