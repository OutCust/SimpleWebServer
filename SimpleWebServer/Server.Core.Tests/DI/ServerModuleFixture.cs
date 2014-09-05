using Ninject;
using NUnit.Framework;
using Server.Core.DI;

namespace Server.Core.Tests.DI
{
    [TestFixture]
    public class ServerModuleFixture
    {
        private StandardKernel _target;


        [SetUp]
        public void SetUp()
        {
            _target = new StandardKernel();
            _target.Load<ServerModule>();
        }


        [Test]
        public void CanResolveUserClient()
        {
            var uc = _target.Get<IUserClient>();
        }
    }
}