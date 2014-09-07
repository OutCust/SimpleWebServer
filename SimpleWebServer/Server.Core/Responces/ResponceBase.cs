using System.Net.Sockets;
using System.Text;
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

        public void Redirect(string url)
        {
            var resp = string.Format("HTTP/1.1 302 Found \nLocation: {0} \n\n", url);
            ResponceData = Encoding.ASCII.GetBytes(resp);
        }

        public virtual void Process(IRequest request)
        {
            //нинче не делаем
        }
    }
}