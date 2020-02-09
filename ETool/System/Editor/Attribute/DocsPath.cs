using System;

namespace ETool
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DocsPath : Attribute
    {
        public string Path;

        public DocsPath(string path)
        {
            Path = path;
        }
    }
}
