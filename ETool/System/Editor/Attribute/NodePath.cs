using System;

namespace ETool
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodePath : Attribute
    {
        public string Path;

        public NodePath(string path)
        {
            Path = path;
        }
    }
}
