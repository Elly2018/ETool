using System.Collections.Generic;

namespace ETool
{
    public class ManagerBase<T> where T : ObjectBase
    {
        private List<T> TargetObject = new List<T>();

        protected T[] GetTargetObjects()
        {
            return TargetObject.ToArray();
        }

        protected string CreateUniqueName(string name)
        {
            if (!IsTargetObjectExist(name)) return name;
            bool Exist = true;
            int Number = 1;
            while (Exist)
            {
                Exist = false;
                foreach (var i in TargetObject)
                {
                    if (i.name == name + Number.ToString())
                    {
                        Number++;
                        Exist = true;
                    }
                }
            }
            return name + Number.ToString();
        }

        protected bool IsTargetObjectIsNull()
        {
            return TargetObject.Count == 0;
        }

        protected bool IsTargetObjectExist(string name)
        {
            foreach (var i in TargetObject)
            {
                if (i.name == name) return true;
            }
            return false;
        }

        protected void AddNewTargetObject(T instance)
        {
            TargetObject.Add(instance);
        }

        protected void RemoveTargetObject(int index) {
            TargetObject.RemoveAt(index);
        }

        protected void RemoveTargetObject(T instance)
        {
            if (TargetObject.Contains(instance))
            {
                TargetObject.Remove(instance);
            }
        }

        protected void RemoveAllTargetObject()
        {
            TargetObject.Clear();
        }

        public T GetTargetObjectByName(string name)
        {
            foreach(var i in TargetObject)
            {
                if (i.name == name) return i;
            }
            return null;
        }

        public T GetTargetObjectByIndex(int index)
        {
            if (index > -1 && index < TargetObject.Count) 
                return TargetObject[index];
            else
                return null;
        }
    }
}