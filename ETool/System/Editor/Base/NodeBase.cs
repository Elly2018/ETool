using ETool.ANode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ETool
{
    [System.Serializable]
    public class NodeBase : Node, INode
    {
        public int normal;
        public int select;
        public int in_point;
        public int out_point;

        public int targetPage;

        public NodeBase(Vector2 position, float width, float height) : base(position, width, height) 
        {
            StyleInitialize();
        }

        /// <summary>
        /// Draw on graph method
        /// </summary>
        public void Draw()
        {
            if(isSelected)
                GUI.Box(new Rect(rect.x, rect.y, rect.width, rect.height + ((fields.Count + 1) * PropertiesHeight)), "", StyleUtility.GetStyle((StyleType)select));
            else
                GUI.Box(new Rect(rect.x, rect.y, rect.width, rect.height + ((fields.Count + 1) * PropertiesHeight)), "", StyleUtility.GetStyle((StyleType)normal));
            DrawField(fields);
        }

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
        protected void ActiveNextEvent(int fieldIndex, BlueprintInput data)
        {
            try
            {
                GetNextNode(fieldIndex, data).ProcessCalling(data);
            }
            catch { }
        }

        protected void ActiveCustomEvent(BlueprintInput data, int page, object[] obj)
        {
            foreach(var i in data.allNode)
            {
                if(i.page == page && i.GetType() == typeof(ACustomEvent))
                {
                    ACustomEvent target = (ACustomEvent)i;
                    target.ReceivedObject(obj);
                    target.ProcessCalling(data);
                }
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
                return Casting(fields[index].target, fields[index].fieldType);
            }
            return null;
        }

        private object Casting(GenericObject target, FieldType ft)
        {
            switch (ft)
            {
                case FieldType.Event:
                    return null;
                case FieldType.Int:
                    return target.target_Int;
                case FieldType.Float:
                    return target.target_Float;
                case FieldType.String:
                    return target.target_String;
                case FieldType.Boolean:
                    return target.target_Boolean;
                case FieldType.Double:
                    return target.target_Boolean;
                case FieldType.GameObject:
                    return target.target_GameObject;
                case FieldType.Vector2:
                    return target.target_Vector2;
                case FieldType.Vector3:
                    return target.target_Vector3;
                case FieldType.Vector4:
                    return target.target_Vector4;
                case FieldType.Rect:
                    return target.target_Rect;
                case FieldType.Color:
                    return target.target_Color;
                case FieldType.Type:
                    return target.target_Type;
                case FieldType.Variable:
                    break;
            }
            return null;
        }
    }
}
