using ETool.ANode;
using System.Collections.Generic;
using UnityEngine;

namespace ETool
{
    public partial class EBlueprint : ScriptableObject
    {
        public void Custom_AddCustomEvent(object customEvent)
        {
            AddCustomEvent ace = (AddCustomEvent)customEvent;
            NodeBase n = Node_AddNode(ace.mousePosition, ace.page, typeof(ACustomEvent));
            ACustomEvent nec = n as ACustomEvent;
            if (nec != null)
            {
                nec.targetEventOrVar = ace.tbp.name + "." + ace.bce.eventName;
                nec.unlocalTitle = ace.bce.eventName;
                nec.SetCustomEvent(ace.bce);
            }
        }


        public void Custom_AddCustomEventCall(object customEvent)
        {
            AddCustomEvent ace = (AddCustomEvent)customEvent;
            NodeBase n = Node_AddNode(ace.mousePosition, ace.page, typeof(ACustomEventCall));
            ACustomEventCall nec = n as ACustomEventCall;
            if (nec != null)
            {
                nec.targetEventOrVar = ace.tbp.name + "." + ace.bce.eventName;
                nec.unlocalTitle = ace.bce.eventName;
                nec.SetCustomEvent(ace.tbp, ace.bce);
            }
        }

        public string[] Custom_GetAllCustomEventName()
        {
            List<string> result = new List<string>();
            foreach(var i in blueprintEvent.customEvent)
            {
                result.Add(name + "." + i.eventName);
            }
            return result.ToArray();
        }

        public AddCustomEvent[] Custom_GetAllCustomAddEvent()
        {
            List<AddCustomEvent> result = new List<AddCustomEvent>();
            foreach (var i in GetPublicEvent())
            {
                result.Add(new AddCustomEvent() {
                    tbp = i.Item2,
                    bce = i.Item1
                });
            }
            return result.ToArray();
        }

        public string[] Custom_GetAllInheritCustomEventName()
        {
            List<string> result = new List<string>();
            foreach (var i in GetInheritEvent())
            {
                if (i.Item2 != this)
                    result.Add(i.Item2.name + "." + i.Item1.eventName);
            }
            return result.ToArray();
        }

        public bool Custom_CheckCustomEventNodeExist(string name, string eventname)
        {
            foreach (var i in nodes)
            {
                if (i.NodeType == typeof(ACustomEvent).FullName)
                {
                    if (i.targetEventOrVar == name + "." + eventname) return true;
                }
            }
            return false;
        }

        public NodeBase[] Custom_GetAllCustomEventCallNode(string name, string eventname)
        {
            List<NodeBase> result = new List<NodeBase>();
            foreach (var i in nodes)
            {
                if (i.NodeType == typeof(ACustomEventCall).FullName)
                {
                    if (i.targetEventOrVar == name + "." + eventname) result.Add(i);
                }
            }
            return result.ToArray();
        }

        public ACustomEvent Custom_GetCustomEventNode(string eventname)
        {
            foreach (var i in nodes)
            {
                if (i.NodeType == typeof(ACustomEvent).FullName)
                {
                    if (i.targetEventOrVar.Split('.')[1] == eventname) return i as ACustomEvent;
                }
            }
            return null;
        }

        /// <summary>
        /// Try to call blueprint custom event <br />
        /// It only search current blueprint and trying to call it by eventName <br />
        /// This method will search inherit blueprint object
        /// </summary>
        /// <param name="data">Input Data</param>
        /// <param name="EventName">Event Name</param>
        /// <param name="_arg">Argument List</param>
        /// <returns>Is Event Exist Calling</returns>
        public bool Custom_CallCustomEvent(string EventName, object[] _arg)
        {
            /* It match ! This mean this event is exist */
            /* Then let's loop all the node search the custom event node */
            foreach (var j in _InputInstance.allNode)
            {
                /* Get the event ACustomEvent and check the index is correct */
                if (j.NodeType == typeof(ACustomEvent).FullName && j.unlocalTitle == EventName)
                {
                    /* Get the subclass of the node */
                    ACustomEvent buffer = (ACustomEvent)j;

                    /* Send arugment */
                    buffer.ReceivedObject(_arg);

                    /* Call the event */
                    buffer.ProcessCalling(_InputInstance);
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// This method is for external blueprint modify public variable <br />
        /// User and see the detail at <see cref="SetOutterVariable"/> and <see cref="GetOutterVariable"/>
        /// </summary>
        /// <param name="label">Variable Label</param>
        /// <param name="type">Field Type</param>
        /// <param name="o">Object</param>
        /// <returns></returns>
        public bool Custom_CallSetVariable(string label, FieldType type, object o)
        {
            foreach (var i in _InputInstance.blueprintVariables)
            {
                if (i.label == label)
                {
                    Field.SetObjectByFieldType(type, i.variable, o);
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// This method is for external blueprint modify public variable <br />
        /// User and see the detail at <see cref="SetOutterVariable"/> and <see cref="GetOutterVariable"/>
        /// </summary>
        /// <param name="label">Variable Label</param>
        /// <param name="type">Field Type</param>
        /// <param name="o">Object</param>
        /// <returns></returns>
        public bool Custom_CallSetVariable_Array(string label, FieldType type, object[] o)
        {
            foreach (var i in _InputInstance.blueprintVariables)
            {
                if (i.label == label)
                {
                    Field.SetObjectArrayByField(type, i.variable_Array, o);
                    return true;
                }
            }
            return false;
        }

        public object Custom_CallGetVariable(string label, FieldType type)
        {
            foreach (var i in _InputInstance.blueprintVariables)
            {
                if (i.label == label)
                {
                    return Field.GetObjectByFieldType(type, i.variable);
                }
            }
            return null;
        }

        public object Custom_CallGetVariableArray(string label, FieldType type)
        {
            foreach (var i in _InputInstance.blueprintVariables)
            {
                if (i.label == label)
                {
                    return Field.GetObjectArrayByFieldType(type, i.variable_Array);
                }
            }
            return null;
        }
    }
}
