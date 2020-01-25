using System;
using System.Reflection;
using UnityEngine;

namespace ETool
{
    [System.Serializable]
    public class GenericObject
    {
        public int target_Int = 0;
        public float target_Float = 0.0f;
        public string target_String = "";
        public bool target_Boolean = true;
        public double target_Double = 0;

        public GameObject target_GameObject = null;
        public Transform target_Transform = null;
        public Vector2 target_Vector2 = Vector2.zero;
        public Vector3 target_Vector3 = Vector3.zero;
        public Vector4 target_Vector4 = Vector4.zero;
        public Rect target_Rect = Rect.zero;

        public Color target_Color = Color.white;
        public int target_Type = 0;
        public UnityEngine.Object target_UnityObject = null;
        public Quaternion target_Quaternion = Quaternion.identity;

        public GenericObject()
        {
            target_String = "";
        }

        public GenericObject(GenericObject reference)
        {
            target_Int = reference.target_Int;
            target_Float = reference.target_Float;
            target_String = reference.target_String;
            target_Boolean = reference.target_Boolean;
            target_Double = reference.target_Double;

            target_GameObject = reference.target_GameObject;
            target_Vector2 = reference.target_Vector2;
            target_Vector3 = reference.target_Vector3;
            target_Vector4 = reference.target_Vector4;
            target_Rect = reference.target_Rect;

            target_Color = reference.target_Color;
            target_Type = reference.target_Type;
            target_UnityObject = reference.target_UnityObject;
        }
    }

    public class TypeUtility
    {
        public static Type GetTypeByName(string name)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            foreach(var i in ass.GetTypes())
            {
                if (i.FullName == name) return i;
            }
            return typeof(Nullable);
        }
    }
}
