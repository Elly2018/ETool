using System;
using UnityEngine;

namespace ETool
{
    [CreateAssetMenu(menuName = "ETool/List")]
    public class EList : ScriptableObject
    {
        public string listType = "System.Int32";
        public ListBase listBase;
    }
}
