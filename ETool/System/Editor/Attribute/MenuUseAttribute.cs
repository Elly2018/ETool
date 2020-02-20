using System;

namespace ETool
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Constant_Menu : Attribute
    {
        public int priority = 10;

        public Constant_Menu() { }

        public Constant_Menu(int priority)
        {
            this.priority = priority;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Casting_Menu : Attribute
    {
        public string source;

        public Casting_Menu(string source)
        {
            this.source = source;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Self_Menu : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Math_Menu : Attribute
    {
        public string source;

        public Math_Menu(string source)
        {
            this.source = source;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Logic_Menu : Attribute
    {
        public string source;

        public Logic_Menu(string source)
        {
            this.source = source;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class GameObject_Menu : Attribute
    {
        public string source;

        public GameObject_Menu(string source)
        {
            this.source = source;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Transform_Menu : Attribute
    {
        public string source;

        public Transform_Menu(string source)
        {
            this.source = source;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Component_Menu : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Input_Menu : Attribute
    {
        public string source;

        public Input_Menu(string source)
        {
            this.source = source;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ETool_Menu : Attribute
    {
        public string source;

        public ETool_Menu(string source)
        {
            this.source = source;
        }
    }
}

