using Cake.Frosting;

namespace CakeBuild
{
    public static class Program
    {
        public static int Main(string[] arguments)
        {
            CakeHost cakeHost = new CakeHost();
            cakeHost.UseContext<BuildContext>();
            return cakeHost.Run(arguments);
        }
    }
}
