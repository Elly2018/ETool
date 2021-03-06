﻿using ETool.ANode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ETool
{
    [System.Serializable]
    public class NodeBase : Node, INodeStyle
    {
        public int normal;
        public int select;
        public int in_point;
        public int out_point;

        public NodeBase(Vector2 position, float width, float height) : base(position, width, height) 
        {
            StyleInitialize();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Draw on graph method
        /// </summary>
        public void Draw()
        {
            if (nodeErrors.Count != 0)
            {
                if (isSelected)
                {
                    GUI.Box(GetBoxRect(), "", StyleUtility.GetStyle(StyleType.Select_Error_Node));
                }
                else
                {
                    GUI.Box(GetBoxRect(), "", StyleUtility.GetStyle(StyleType.Error_Node));
                }
                DrawField(fields);
                return;
            }
            if (isSelected)
            {
                GUI.Box(GetBoxRect(), "", StyleUtility.GetStyle((StyleType)select));
            }
            else if(!isSelected)
            {
                GUI.Box(GetBoxRect(), "", StyleUtility.GetStyle((StyleType)normal));
            }
            DrawField(fields);
        }

        public Rect GetBoxRect()
        {
            ZoomData zoomLevel = NodeBasedEditor.Editor_Instance.GetZoomLevel();
            float height = rect.height + ((fields.Count + 1) * PropertiesHeight);
            float padding = 7;
            return new Rect(
                (rect.x - padding) * zoomLevel.ratio,
                (rect.y) * zoomLevel.ratio,
                (rect.width + padding * 2) * zoomLevel.ratio,
                (height) * zoomLevel.ratio);
        }
#endif

        /// <summary>
        /// This will initialize style <br />
        /// Call all the override style method <br />
        /// Store the enum number into local int
        /// </summary>
        protected void StyleInitialize()
        {
            normal = (int)GetNodeStyle();
            select = (int)GetNodeSelectStyle();
            in_point = (int)GetInPointStyle();
            out_point = (int)GetOutPointStyle();
        }

        ///
        /// User can override the style method in subclass
        ///
        #region Style Virtual Function
        /// <summary>
        /// When normal state <br />
        /// What style id it will be
        /// </summary>
        /// <returns></returns>
        public virtual StyleType GetNodeStyle()
        {
            return StyleType.Default_Node;
        }

        /// <summary>
        /// When selected state <br />
        /// What style id it will be
        /// </summary>
        /// <returns></returns>
        public virtual StyleType GetNodeSelectStyle()
        {
            return StyleType.Select_Default_Node;
        }

        /// <summary>
        /// In point style id
        /// </summary>
        /// <returns></returns>
        public virtual StyleType GetInPointStyle()
        {
            return StyleType.In_Point;
        }

        /// <summary>
        /// Out point style id
        /// </summary>
        /// <returns></returns>
        public virtual StyleType GetOutPointStyle()
        {
            return StyleType.Out_Point;
        }

        public StyleType GetInPointArrayStyle()
        {
            return StyleType.In_Point_Array;
        }

        public StyleType GetOutPointArrayStyle()
        {
            return StyleType.Out_Point_Array;
        }
        #endregion

        /// <summary>
        /// Get the node next connected node <br />
        /// This usually only use for event type <br />
        /// Don not use this on data type field
        /// </summary>
        /// <param name="fieldIndex">Field index</param>
        /// <param name="data">BP input data</param>
        /// <returns></returns>
        protected NodeBase GetNextNode(int fieldIndex, BlueprintInput data)
        {
            List<NodeBase> nl = data.allNode.ToList();
            int myIndex = nl.IndexOf(this);
            Vector2Int outNode = new Vector2Int(myIndex, fieldIndex);
            for (int i = 0; i < data.allConnection.Length; i++)
            {
                if (data.allConnection[i].outPointMark == outNode)
                {
                    return data.allNode[data.allConnection[i].inPointMark.x];
                }
            }

            return null;
        }

        /// <summary>
        /// Calling the next node processing method <br />
        /// This usually only use for event type <br />
        /// Don not use this on data type field
        /// </summary>
        /// <param name="fieldIndex">Field index</param>
        /// <param name="data">BP input data</param>
        public void ActiveNextEvent(int fieldIndex, BlueprintInput data)
        {
            try
            {
                GetNextNode(fieldIndex, data).ProcessCalling(data);
            }
            catch { }
        }

        protected void ActiveCustomEvent(BlueprintInput data, string eventName, object[] obj)
        {
            foreach(var i in data.allNode)
            {
                if(i.targetEventOrVar.Split('.').Length > 1)
                {
                    if (i.targetEventOrVar.Split('.')[1] == eventName && i.NodeType == typeof(ACustomEvent).FullName)
                    {
                        ACustomEvent target = (ACustomEvent)i;
                        target.ReceivedObject(obj);
                        target.ProcessCalling(data);
                    }
                }
            }

            DebugHelper.Log("Cannot find custom event node\n" +
                "eventName: " + eventName,
                this);
        }

        protected void ActiveInheritCustomEvent(BlueprintInput data, object[] obj, string blueprintName)
        {
            BlueprintInput refer = FindBlueprintInher(data, blueprintName);
            if (refer == null) Debug.LogWarning("Cannot find event for calling method");
            bool eventExist = refer.self.Custom_CallCustomEvent(targetEventOrVar.Split('.')[1], obj);
            if (!eventExist) Debug.LogWarning("Cannot find event for calling method");
        }

        private BlueprintInput FindBlueprintInher(BlueprintInput data, string Targetname)
        {

            if (data == null) return null;

            if (data.self.name == Targetname) return data;

            if (data.inherit != null)
            {
                return FindBlueprintInher(data.inherit._InputInstance, Targetname);
            }
            else
            {
                return null;
            }
        }

        protected bool CheckIfConnectionExist(int fieldIndex, BlueprintInput data, bool input)
        {
            List<NodeBase> nl = data.allNode.ToList();
            int myIndex = nl.IndexOf(this);
            Vector2Int target = new Vector2Int(myIndex, fieldIndex);
            for (int i = 0; i < data.allConnection.Length; i++)
            {
                if (input)
                {
                    if (data.allConnection[i].inPointMark == target)
                        return true;
                }
                else
                {
                    if (data.allConnection[i].outPointMark == target)
                        return true;
                }
            }
            return false;
        }

        protected object GetFieldOrLastInputField(int fieldIndex, BlueprintInput data)
        {
            if (CheckIfConnectionExist(fieldIndex, data, true))
                return GetFieldInputValue(fieldIndex, data);
            else
                return Field.GetObjectByFieldType(fields[fieldIndex].fieldType, fields[fieldIndex].target);
        }

        protected T GetFieldOrLastInputField<T>(int fieldIndex, BlueprintInput data)
        {
            if (CheckIfConnectionExist(fieldIndex, data, true))
                return (T)GetFieldInputValue(fieldIndex, data);
            else
                return (T)Field.GetObjectByFieldType(fields[fieldIndex].fieldType, fields[fieldIndex].target);
        }

        protected object GetFieldInputValue(int fieldIndex, BlueprintInput data)
        {
            List<NodeBase> nl = data.allNode.ToList();
            int myIndex = nl.IndexOf(this);
            Vector2Int target = new Vector2Int(myIndex, fieldIndex);
            for (int i = 0; i < data.allConnection.Length; i++)
            {
                if(data.allConnection[i].inPointMark == target)
                {
                    return data.allNode[data.allConnection[i].outPointMark.x].ReceiveData(data.allConnection[i].outPointMark.y, data);
                }
            }
            return null;
        }

        public virtual object ReceiveData(int index, BlueprintInput data)
        {
            if (fields[index].connectionType == ConnectionType.DataBoth || fields[index].connectionType == ConnectionType.DataOutput)
            {
                MethodInfo[] mif = GetType().GetMethods();
                foreach (var i in mif)
                {
                    NodePropertyGet npg = (NodePropertyGet)i.GetCustomAttribute(typeof(NodePropertyGet));
                    if(npg != null)
                    {
                        if (npg.index == index)
                            return i.Invoke(this, new object[] { data });
                    }

                    NodePropertyGet2 npg2 = (NodePropertyGet2)i.GetCustomAttribute(typeof(NodePropertyGet2));
                    if(npg2 != null)
                    {
                        if (index >= npg2.index && index <= npg2.index2)
                            return i.Invoke(this, new object[] { data, index });
                    }
                }
                return Field.GetObjectByFieldType(fields[index].fieldType, fields[index].target);
            }
            return null;
        }

        protected void SetVariable(BlueprintInput data, FieldType ft, object target)
        {
            BlueprintInput refer = FindBlueprintInher(data, targetEventOrVar.Split('.')[0]);
            refer.self.Custom_CallSetVariable(targetEventOrVar.Split('.')[1], ft, target);
        }

        protected void SetVariableArray(BlueprintInput data, FieldType ft, object[] target)
        {
            BlueprintInput refer = FindBlueprintInher(data, targetEventOrVar.Split('.')[0]);
            refer.self.Custom_CallSetVariable_Array(targetEventOrVar.Split('.')[1], ft, target);
        }

        protected object GetVarialbe(BlueprintInput data, FieldType ft)
        {
            BlueprintInput refer = FindBlueprintInher(data, targetEventOrVar.Split('.')[0]);
            return refer.self.Custom_CallGetVariable(targetEventOrVar.Split('.')[1], ft);
        }

        protected object[] GetVarialbeArray(BlueprintInput data, FieldType ft)
        {
            BlueprintInput refer = FindBlueprintInher(data, targetEventOrVar.Split('.')[0]);
            return refer.self.Custom_CallGetVariableArray(targetEventOrVar.Split('.')[1], ft);
        }

        #region Field modify

        protected bool CheckFieldType(int index, FieldType ft)
        {
            if(index < 0 || index >= fields.Count)
            {
                Debug.LogWarning("Index out of range");
                return false;
            }
            return fields[index].fieldType == ft;
        }

        protected bool CheckContainerType(int index, FieldContainer fc)
        {
            if (index < 0 || index >= fields.Count)
            {
                Debug.LogWarning("Index out of range");
                return false;
            }
            return fields[index].fieldContainer == fc;
        }

        protected void InsertField(Field field, int index)
        {
            fields.Insert(index, field);
            for(int i = index; i < fields.Count; i++)
            {
                if(i != fields.Count - 2)
                {
                    if (fields[i].fieldType != fields[i + 1].fieldType)
                    {
                        EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[i]);
                    }
                }
            }
        }

        protected void DeleteLastField()
        {
            EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[fields.Count - 1]);
            fields.RemoveAt(fields.Count - 1);
        }

        #endregion
    }
}
