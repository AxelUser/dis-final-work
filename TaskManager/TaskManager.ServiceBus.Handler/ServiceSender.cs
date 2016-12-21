using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.ServiceBus.Handler
{
    public class ServiceSender
    {
        private string key;
        private string hostname;
        private int port;
        public ServiceSender(string key, string hostname, int servicePort)
        {            
            this.key = key;
        }

        public bool Send(IDictionary<string, object> headers)
        {
            using(var client = new TcpClient(hostname, port))
            {
                using(var stream = client.GetStream())
                {
                    try
                    {
                        using (BinaryWriter writer = new BinaryWriter(stream))
                        {
                            writer.Write(headers[key].ToString());
                            return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }                        
                }
            }
        }
    }
}
