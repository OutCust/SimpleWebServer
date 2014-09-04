using Server.Interfaces;

namespace Server.Core.Responces
{
    public class PageResponce: ResponceBase
    {
        private readonly IPage _page;

        public PageResponce(IPage page)
        {
            _page = page;
        }

        public override void Process()
        {
            throw new System.NotImplementedException();
        }
    }
}