using Ninject;

namespace GuestBook
{
    public class NinjectServiceLocator
    {
        private readonly IKernel _kernel;
        private NinjectServiceLocator()
        {
            _kernel = new StandardKernel();
        }

        private static readonly NinjectServiceLocator Instance = new NinjectServiceLocator();
        public static NinjectServiceLocator GetInstance()
        {
            return Instance;
        }

        public IKernel GetKernel()
        {
            return _kernel;
        }
        public T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}