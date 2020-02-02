using UnityEngine;
using System.Collections.Generic;

namespace ETool
{
    [System.Serializable]
    public class ELanguageManager
    {
        [SerializeField] public int node_Index;
        [SerializeField] public List<ELanguageStruct> node_LanguageStructs = new List<ELanguageStruct>();

        [SerializeField] public int field_Index;
        [SerializeField] public List<ELanguageStruct> field_LanguageStructs = new List<ELanguageStruct>();

        [SerializeField] public int custom_Index;
        [SerializeField] public List<ELanguageStruct> custom_LanguageStructs = new List<ELanguageStruct>();
    }
}
