using GuestBook.Datalayer;
using Ninject;
using Server.Interfaces;

namespace GuestBook
{
    public class SiteInitializer : ISiteInitializer
    {
        public void Initialize(string siteConfigPath)
        {
            var configLoader = new ConfigLoader();
            
            var config = configLoader.Load<GuestBookConfig>(siteConfigPath);
            
            var locator = NinjectServiceLocator.GetInstance();

            IKernel kernel = locator.GetKernel();
            if (config.RepositoryType.Equals("SQLite"))
            {
                kernel.Bind<IGuestBookRepository>()
                    .To<GuestBookSqLiteRepository>()
                    .WithConstructorArgument("dbFilePath", config.RepositoryPath);
            }

            if (config.RepositoryType.Equals("XML"))
            {
                kernel.Bind<IGuestBookRepository>()
                    .To<GuestBookXmlRepository>()
                    .WithConstructorArgument("filePath", config.RepositoryPath);
            }
        }
    }
}
