using System;

namespace ETool
{
    /// <summary>
    /// Node method that use for property getter method <br />
    /// Method format: <br />
    /// public T MethodName(BlueprintInput data)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class NodePropertyGet : Attribute
    {
        /// <summary>
        /// Return type
        /// </summary>
        public Type type;

        /// <summary>
        /// Index of field
        /// </summary>
        public int index;

        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="type">Return Type</param>
        /// <param name="index">Field Index</param>
        public NodePropertyGet(Type type, int index)
        {
            this.type = type;
            this.index = index;
        }
    }

    /// <summary>
    /// Node method that use for property getter method <br />
    /// When index is in the range, it will trigger this method <br />
    /// Method format: <br />
    /// public T MethodName(BlueprintInput data, int index)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class NodePropertyGet2 : Attribute
    {
        /// <summary>
        /// Minimum index range
        /// </summary>
        public int index;

        /// <summary>
        /// Maximum index range
        /// </summary>
        public int index2;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index">Minimum index range</param>
        /// <param name="index2">Maximum index range</param>
        public NodePropertyGet2(int index, int index2)
        {
            this.index = index;
            this.index2 = index2;
        }
    }
}
