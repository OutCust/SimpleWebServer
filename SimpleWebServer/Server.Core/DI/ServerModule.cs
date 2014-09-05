using Ninject.Modules;
using Server.Core.Components;

namespace Server.Core.DI
{
    public class ServerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserClient>().To<UserClient>();
            Bind<IRequestBuilder>().To<RequestBuilder>();
            Bind<IRequestDataSource>().To<RequestDataSource>();
            Bind<IContentTypeDefiner>().To<ContentTypeDefiner>().InSingletonScope();
            Bind<IResponceFactory>().To<ResponceFactory>().InSingletonScope();
        }
    }
}