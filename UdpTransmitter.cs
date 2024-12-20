using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Scoreboard
{
    public class UdpTransmitter
    {
        // private static string LocalIP = "192.168.50.92"; 
        private static  IPAddress MulticastAddress = IPAddress.Parse("239.0.0.1");
        private static int MulticastPort = 5000;
        private UdpClient UdpSender;

        // Creates a new UdpClient to send multicast data usin IPAdress.Any
        public UdpTransmitter(GameJsonSerializer json)
        {
            UdpSender = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
            UdpSender.EnableBroadcast = true;
            UdpSender.MulticastLoopback = true;
            UdpSender.JoinMulticastGroup(MulticastAddress, IPAddress.Any);

            json.DataSerialized += OnDataSerialized;
        }

        // Run task to send multicast data.
        private void OnDataSerialized(object? sender, string jsonData)
        {
            Task.Run(() => SendMulticast(jsonData));
        }

        // Sends data over Udp.
        private void SendMulticast(string jsonData)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(jsonData);
                UdpSender.Send(data, data.Length, new IPEndPoint(MulticastAddress, MulticastPort));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send data via multicast: {ex.Message}");
            }
        }
    }
}
