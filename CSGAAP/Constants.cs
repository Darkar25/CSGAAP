using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSGAAP
{
    public static class Constants
    {
        public static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name!;

        public static readonly string URIPart_ResourceLocation = $"pack://application:,,,/{AssemblyName};component/Resources/";

        public static readonly string TempDirectory = Path.Combine(Environment.CurrentDirectory, "tmp");
    }
}
