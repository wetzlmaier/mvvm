using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace Common.Reflection
{
    public static class AssemblyHelper
    {
        public static readonly string[] Extensions = { ".dll", ".exe" };

        public static string GetAssemblyFileName(Assembly assembly)
        {
            return new Uri(assembly.Location).LocalPath;
        }

        public static string? GetApplicationDirectory()
        {
            return new FileInfo(GetAssemblyFileName(Assembly.GetExecutingAssembly())).DirectoryName;
        }

        public static string GetAssemblyFileExtension(Assembly assembly)
        {
            return new FileInfo(GetAssemblyFileName(assembly)).Extension;
        }

        public static Icon? LoadApplicationIcon()
        {
            Assembly? assembly = Assembly.GetEntryAssembly();
            if (assembly == null)
                return null;

            using Stream? stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".App.ico");
            return stream == null ? null : new Icon(stream);
        }

        public static string GetAssemblyCompany(Assembly assembly)
        {
            var attribute = (AssemblyCompanyAttribute?)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));
            return attribute != null ? attribute.Company : string.Empty;
        }

        public static string GetAssemblyProduct(Assembly assembly)
        {
            var attribute = (AssemblyProductAttribute?)Attribute.GetCustomAttribute(assembly, typeof(AssemblyProductAttribute));
            return attribute != null ? attribute.Product : string.Empty;
        }

        public static string GetAssemblyCopyright(Assembly assembly)
        {
            var attribute =
                (AssemblyCopyrightAttribute?)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute));
            return attribute != null ? attribute.Copyright : string.Empty;
        }

        public static string GetAssemblyTrademark(Assembly assembly)
        {
            var attribute =
                (AssemblyTrademarkAttribute?)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTrademarkAttribute));
            return attribute != null ? attribute.Trademark : string.Empty;
        }

        public static string GetAssemblyCulture(Assembly assembly)
        {
            var attribute =
                (AssemblyCultureAttribute?)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCultureAttribute));
            return attribute != null ? attribute.Culture : string.Empty;
        }

        public static string GetAssemblyVersion(Assembly assembly)
        {
            var attribute =
                (AssemblyVersionAttribute?)Attribute.GetCustomAttribute(assembly, typeof(AssemblyVersionAttribute));
            return attribute != null ? attribute.Version : string.Empty;
        }

        public static string GetAssemblyFileVersion(Assembly assembly)
        {
            var attribute =
                (AssemblyFileVersionAttribute?)Attribute.GetCustomAttribute(assembly,
                    typeof(AssemblyFileVersionAttribute));
            return attribute != null ? attribute.Version : string.Empty;
        }

        public static string GetPublicKeyToken(Assembly assembly)
        {
            var result = new StringBuilder();
            var pk = assembly.GetName().GetPublicKey();

            if (pk == null)
                return string.Empty;

            for (int i = 0; i < pk.GetLength(0); i++)
                result.AppendFormat("{0:x2}", pk[i]);
            return result.ToString();
        }
    }
}
