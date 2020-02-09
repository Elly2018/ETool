using System;
using UnityEngine;

namespace ETool
{
    [CreateAssetMenu(menuName = "ETool/Docs")]
    public class EDocs : ScriptableObject
    {
        [SerializeField] public string Path = "Home";
    }
}
