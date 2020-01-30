using System;

namespace ETool
{
    /// <summary>
    /// Define node in context menu path string <br />
    /// Adding this in a subclass of "NodeBase"
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NodePath : Attribute
    {
        /// <summary>
        /// Node path string
        /// </summary>
        public string Path;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Context menu path</param>
        public NodePath(string path)
        {
            Path = path;
        }
    }
}
