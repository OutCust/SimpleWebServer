using System.Net.Sockets;
using Server.Interfaces;

namespace Server.Core.Responces
{
    public class ResponceBase : IResponce
    {
        public byte[] ResponceData { get; set; }

        public void Send(NetworkStream stream)
        {
            stream.Write(ResponceData, 0, ResponceData.Length);
            stream.Flush();
        }

        public virtual void Process(IRequest request)
        {
            //нинче не делаем
        }
    }
}