using System.Reflection;

namespace CSGAAP
{
    public static class Constants
    {
        public static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name!;

        public static readonly string URIPart_ResourceLocation = $"pack://application:,,,/{AssemblyName};component/Resources/";

        public static readonly string TempDirectory = Path.Combine(Environment.CurrentDirectory, "tmp");
    }
}
