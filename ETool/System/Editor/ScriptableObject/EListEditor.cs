using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    [CustomEditor(typeof(EList))]
    [CanEditMultipleObjects]
    public class EListEditor : EToolEditorBase
    {
        public override void DrawEToolInformation()
        {
            EList myTarget = (EList)target;
            List<ListElement> le = myTarget.listBase.listElements;
            VGroupStart();
            myTarget.listType = DrawElementDataType(myTarget.listType, "Data type");
            for(int i = 0; i < le.Count; i++)
            {
                HGroupStart();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(40));
                le[i].target = DrawFieldHelper(le[i].target, myTarget.listType);
                if (GUILayout.Button("-", GUILayout.Width(30)))
                    myTarget.listBase.listElements.RemoveAt(i);
                HGroupEnd();
            }
            if (GUILayout.Button("+"))
                le.Add(new ListElement());
            VGroupEnd();
        }
    }
}
