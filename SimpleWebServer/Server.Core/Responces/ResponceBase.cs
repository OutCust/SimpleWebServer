using System.Text;
using Server.Interfaces;

namespace Server.Core.Responces
{
    public class ResponceBase : IResponce
    {
        public ResponceBase(IRequest request)
        {
            Request = request;
        }

        public byte[] ResponceData { get; set; }

        public bool IsRedirect { get; private set; }
        public IRequest Request { get; private set; }

        public void Send()
        {
            Request.RequestStream.Write(ResponceData, 0, ResponceData.Length);
            Request.RequestStream.Flush();
        }

        public virtual void Process()
        {
            //нинче не делаем
        }

        public void Redirect(string url)
        {
            IsRedirect = true;
            var resp = string.Format("HTTP/1.1 302 Found \nLocation: {0} \n\n", url);
            ResponceData = Encoding.ASCII.GetBytes(resp);
        }


    }
}