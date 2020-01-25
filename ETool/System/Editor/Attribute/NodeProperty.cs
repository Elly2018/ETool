using System;

namespace ETool
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NodePropertyGet : Attribute
    {
        public Type type;
        public int index;

        public NodePropertyGet(Type type, int index)
        {
            this.type = type;
            this.index = index;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class NodePropertyGet2 : Attribute
    {
        public int index;
        public int index2;

        public NodePropertyGet2(int index, int index2)
        {
            this.index = index;
            this.index2 = index2;
        }
    }
}
