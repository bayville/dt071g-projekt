using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Scoreboard
{
    public class UdpTransmitter
    {
        private static string LocalIP = "192.168.50.92"; 
        private static  IPAddress MulticastAddress = IPAddress.Parse("239.255.255.250");
        private static int MulticastPort = 5000;
        private UdpClient UdpSender;

        public UdpTransmitter(GameJsonSerializer json)
        {
            UdpSender = new UdpClient(new IPEndPoint(IPAddress.Parse(LocalIP), 0));
            UdpSender.EnableBroadcast = true;
            UdpSender.MulticastLoopback = true;
            UdpSender.JoinMulticastGroup(MulticastAddress, IPAddress.Parse(LocalIP));

            json.DataSerialized += OnDataSerialized;
        }

        private void OnDataSerialized(object? sender, string jsonData)
        {
            Task.Run(() => SendMulticast(jsonData));
        }

        private void SendMulticast(string jsonData)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(jsonData);
                UdpSender.Send(data, data.Length, new IPEndPoint(MulticastAddress, MulticastPort));
                Console.WriteLine("Data sent via multicast: " + jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send data via multicast: {ex.Message}");
            }
        }
    }
}
