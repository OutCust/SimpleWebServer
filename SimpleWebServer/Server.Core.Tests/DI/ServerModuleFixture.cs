using Ninject;
using NUnit.Framework;
using Server.Core.DI;
using Server.Interfaces;
using Ninject.Extensions.Conventions;

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

            _target.Bind(x => x.FromAssembliesInPath("./")
                .SelectAllClasses().InheritedFrom<IPage>()
                .BindAllInterfaces());
        }


        [Test]
        public void CanResolveUserClient()
        {
            var uc = _target.Get<IUserClient>();
        }

        [Test]
        public void CanResolvePages()
        {
            var pages = _target.GetAll<IPage>();
        }
    }
}