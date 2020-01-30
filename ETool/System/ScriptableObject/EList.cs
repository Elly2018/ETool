using System;
using UnityEngine;

namespace ETool
{
    [CreateAssetMenu(menuName = "ETool/List")]
    public class EList : ScriptableObject
    {
        public FieldType listType;
        public ListBase listBase;
    }
}
