using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YamahaMusicCast;

namespace YamahaReceiverTest
{
    class Program
    {
        static readonly string _ip = "127.0.0.1"; // The ip-address of your MusicCast device here

        static void Main(string[] args)
        {
            IMusicCastClient client = new MusicCastClient(IPAddress.Parse(_ip));

            Console.ReadLine();

            client.ChangePower(YamahaMusicCast.Enums.PowerMode.On).Wait();

            Console.ReadLine();
        }
    }
        
}
