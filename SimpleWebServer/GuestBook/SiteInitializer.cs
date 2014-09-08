using GuestBook.Datalayer;
using Ninject;
using Server.Interfaces;

namespace GuestBook
{
    public class SiteInitializer : ISiteInitializer
    {
        public void Initialize()
        {
            var locator = NinjectServiceLocator.GetInstance();

            IKernel kernel = locator.GetKernel();
            //kernel.Bind<IGuestBookRepository>().To<GuestBookXmlRepository>().WithConstructorArgument("filePath", "./repo.xml");

            kernel.Bind<IGuestBookRepository>().To<GuestBookSqLiteRepository>().WithConstructorArgument("connectionString", @"Data Source=./mydb.db;Version=3");

        }
    }
}
