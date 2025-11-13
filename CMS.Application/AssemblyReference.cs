using System.Reflection;

namespace CMS.Application
{
    public static class AssemblyReference
    {
        public static Assembly Assembly
        {
            get
            {
                return typeof(AssemblyReference).Assembly;
            }
        }
    }
}
