using ETool.ANode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool
{
    public partial class EBlueprint : ScriptableObject
    {
        #region Unity Pipeline Event
        /// <summary>
        /// This happen when user trying to change the unity pipeline toggle list
        /// </summary>
        public void Editor_ChangeEventToggleList()
        {
            List<Tuple<bool, EventNodeType, Type, int>> buffer = new List<Tuple<bool, EventNodeType, Type, int>>();
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.startEvent, EventNodeType.Start, typeof(AStart), 0));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.updateEvent, EventNodeType.Update, typeof(AUpdate), 0));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.fixedUpdateEvent, EventNodeType.FixedUpdate, typeof(AFixedUpdate), 0));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.onDestroyEvent, EventNodeType.OnDestory, typeof(AOnDestory), 0));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.lateUpdateEvent, EventNodeType.LateUpdate, typeof(ALateUpdate), 0));

            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onCollisionEnter, EventNodeType.OnCollisionEnter, typeof(AOnCollisionEnter), 1));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onCollisionExit, EventNodeType.OnCollisionExit, typeof(AOnCollisionExit), 1));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onCollisionStay, EventNodeType.OnCollisionStay, typeof(AOnCollisionStay), 1));

            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onCollisionEnter2D, EventNodeType.OnCollisionEnter2D, typeof(AOnCollisionEnter2D), 1));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onCollisionExit2D, EventNodeType.OnCollisionExit2D, typeof(AOnCollisionExit2D), 1));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onCollisionStay2D, EventNodeType.OnCollisionStay2D, typeof(AOnCollisionStay2D), 1));

            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onTriggerEnter, EventNodeType.OnTriggerEnter, typeof(AOnTriggerEnter), 1));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onTriggerExit, EventNodeType.OnTriggerExit, typeof(AOnTriggerExit), 1));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onTriggerStay, EventNodeType.OnTriggerStay, typeof(AOnTriggerStay), 1));

            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onTriggerEnter2D, EventNodeType.OnTriggerEnter2D, typeof(AOnTriggerEnter2D), 1));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onTriggerExit2D, EventNodeType.OnTriggerExit2D, typeof(AOnTriggerExit2D), 1));
            buffer.Add(new Tuple<bool, EventNodeType, Type, int>(blueprintEvent.physicsEvent.onTriggerStay2D, EventNodeType.OnTriggerStay2D, typeof(AOnTriggerStay2D), 1));

            foreach (var i in buffer)
            {
                if (i.Item1 && !Check_EventNodeExist(i.Item2))
                {
                    Node_AddNode(new Vector2(40, 40), i.Item4, i.Item3);
                }
                else if (!i.Item1 && Check_EventNodeExist(i.Item2))
                {
                    Node_RemoveNodes(Check_GetEventNode(i.Item2));
                }
            }

            if (!Check_EventNodeExist(EventNodeType.Constructor)) GUI_OnClickAddNode(new AddClickEvent() { add = typeof(AConstructor), mousePosition = Vector2.zero, page = 1 });
        }
        #endregion


        #region Custom Event
        /// <summary>
        /// Adding a new custom event into event list <br />
        /// Adding a custom event node into nodes list
        /// </summary>
        public void Editor_CustomEvent_AddCustomEvent()
        {
            BlueprintCustomEvent buffer = new BlueprintCustomEvent();
            buffer.eventName = Editor_CustomEvent_GetUniqueName("New Event");
            blueprintEvent.customEvent.Add(buffer);
            Custom_AddCustomEvent(new AddCustomEvent() { tbp = this, bce = buffer, page = EBlueprint.DefaultPageCount + blueprintEvent.customEvent.Count - 1});
        }


        /// <summary>
        /// Clean all the relationship of this event <br />
        /// Delete all the <see cref="ACustomEvent"/> & <see cref="ACustomEventCall"/> & <see cref="AOutterEventCall"/> that target these events
        /// </summary>
        public void Editor_CustomEvent_CleanCustomEvent()
        {
            int count = blueprintEvent.customEvent.Count;
            for(int i = 0; i < count; i++)
            {
                Editor_CustomEvent_DeleteCustomEvent(0);
            }
        }


        public void Editor_CustomEvent_ChangeEvent_All()
        {
            /* Loop all blueprint */
            foreach (var i in GetAllBlueprint)
            {
                List<NodeBase> nb = new List<NodeBase>();

                /* Loop all nodes */
                foreach (var j in i.nodes)
                {
                    /* Call type */
                    if (j.NodeType == typeof(AOutterEventCall).FullName && j.targetEventOrVar.Split('.')[0] == name)
                    {
                        (j as AOutterEventCall).FieldUpdate();
                    }

                    /* Call type */
                    if (j.NodeType == typeof(ACustomEventCall).FullName && j.targetEventOrVar.Split('.')[0] == name)
                    {
                        bool exist = false;
                        foreach(var k in blueprintEvent.customEvent)
                        {
                            if(k.eventName == j.targetEventOrVar.Split('.')[1])
                            {
                                exist = true;
                            }
                        }

                        if (!exist)
                        {
                            nb.Add(j);
                        }
                    }
                }

                for(int j = 0; j < nb.Count; j++)
                {
                    i.Node_RemoveNode(nb[j]);
                }
            }
        }


        /// <summary>
        /// Change all the nodes which have relationship with these event <br />
        /// Modify content all the <see cref="ACustomEvent"/> & <see cref="ACustomEventCall"/> & <see cref="AOutterEventCall"/> that target these events
        /// </summary>
        /// <param name="oldname">Old Name</param>
        /// <param name="newname">New Name</param>
        public void Editor_CustomEvent_ChangeEventName(string oldname, string newname)
        {
            /* Loop all blueprint */
            foreach (var i in GetAllBlueprint)
            {
                /* Loop all nodes */
                foreach (var j in i.nodes)
                {
                    /* Call type */
                    if (j.NodeType == typeof(ACustomEventCall).FullName && j.targetEventOrVar == name + "." + oldname)
                    {
                        j.targetEventOrVar = name + "." + newname;
                        j.unlocalTitle = newname;
                    }

                    /* Method type */
                    if (j.NodeType == typeof(ACustomEvent).FullName && j.targetEventOrVar == name + "." + oldname)
                    {
                        j.targetEventOrVar = name + "." + newname;
                        j.unlocalTitle = newname;
                    }

                    /* External type */
                    if (j.NodeType == typeof(AOutterEventCall).FullName && j.targetEventOrVar == name + "." + oldname)
                    {
                        j.targetEventOrVar = name + "." + newname;
                        j.unlocalTitle = newname;
                        (j as AOutterEventCall).FieldUpdate();
                    }
                }
            }
        }


        /// <summary>
        /// When user delete custom event <br />
        /// It remove any relate event node and relate connection
        /// </summary>
        /// <param name="index">Index Of Custom Event</param>
        public void Editor_CustomEvent_DeleteCustomEvent(string varName)
        {
            for(int i = 0; i < blueprintEvent.customEvent.Count; i++)
            {
                if(blueprintEvent.customEvent[i].eventName == varName)
                {
                    Editor_CustomEvent_DeleteCustomEvent(i);
                    return;
                }
            }
        }


        /// <summary>
        /// When user delete custom event <br />
        /// It remove any relate event node and relate connection
        /// </summary>
        /// <param name="index">Index Of Custom Event</param>
        public void Editor_CustomEvent_DeleteCustomEvent(int index)
        {
            /* Loop all blueprint, well including this one */
            foreach (var i in GetAllBlueprint)
            {
                List<NodeBase> buffer = new List<NodeBase>();
                /* Loop all nodes */
                foreach (var j in i.nodes)
                {
                    /* Call type */
                    if (j.NodeType == typeof(ACustomEventCall).FullName && j.targetEventOrVar == name + "." + blueprintEvent.customEvent[index].eventName)
                    {
                        buffer.Add(j);
                    }

                    /* Method type */
                    if (j.NodeType == typeof(ACustomEvent).FullName && j.targetEventOrVar == name + "." + blueprintEvent.customEvent[index].eventName)
                    {
                        buffer.Add(j);
                    }

                    /* External type */
                    if (j.NodeType == typeof(AOutterEventCall).FullName && j.targetEventOrVar == name + "." + blueprintEvent.customEvent[index].eventName)
                    {
                        buffer.Add(j);
                    }
                }

                for (int j = 0; j < buffer.Count; j++)
                {
                    i.Node_RemoveNode(buffer[j]);
                }
            }

            blueprintEvent.customEvent.RemoveAt(index);

            foreach (var i in nodes)
            {
                if (i.page > index + EBlueprint.DefaultPageCount) i.page--;
            }
        }

        /// <summary>
        /// Get the unique name of event name
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Editor_CustomEvent_GetUniqueName(string input)
        {
            int count = 0;
            bool repeat = true;

            while (repeat)
            {
                repeat = false;

                foreach (var i in blueprintEvent.customEvent)
                {
                    if(count == 0)
                    {
                        if(i.eventName == input)
                        {
                            count++;
                            repeat = true;
                        }
                    }
                    else
                    {
                        if(i.eventName == input + (count - 1).ToString())
                        {
                            count++;
                            repeat = true;
                        }
                    }
                }
            }

            if (count == 0) return input;
            else return input + (count - 1).ToString();
        }


        /// <summary>
        /// Check if the name is repeat in event list, if repeat then given a unique name
        /// </summary>
        /// <param name="input">Event Name</param>
        /// <param name="index">Event Index</param>
        /// <returns></returns>
        public string Editor_CustomEvent_GetUniqueName_Field(string input, int index)
        {
            for(int i = 0; i < blueprintEvent.customEvent.Count; i++)
            {
                if(i != index)
                {
                    if (blueprintEvent.customEvent[i].eventName == input) return Editor_CustomEvent_GetUniqueName(input);
                }
            }
            return input;
        }


        /// <summary>
        /// When the return type is change
        /// </summary>
        /// <param name="eventIndex">Event Index</param>
        public void Editor_ChangeCustomEventReturnTypeOrContainer(int eventIndex)
        {
            /*
             * This part change all the blueprint calling node information
            */

            /* Loop all blueprint, well including this one */
            foreach (var i in GetAllBlueprint)
            {
                /* Loop all nodes */
                foreach (var j in i.nodes)
                {
                    /* If the event is targeting the event which just change return type */
                    if(j.NodeType == typeof(AOutterEventCall).FullName && j.targetEventOrVar == name + "." + blueprintEvent.customEvent[eventIndex].eventName)
                    {
                        (j as AOutterEventCall).SetCustomEvent(this, blueprintEvent.customEvent[eventIndex]);
                    }

                    if (j.NodeType == typeof(ACustomEventCall).FullName && j.targetEventOrVar == name + "." + blueprintEvent.customEvent[eventIndex].eventName)
                    {
                        (j as ACustomEventCall).SetCustomEvent(this, blueprintEvent.customEvent[eventIndex]);
                    }
                }
            }

            /*
             * This part change current blueprint custom event return object and local custom call
            */

            /* Loop all local nodes */
            foreach (var j in nodes)
            {
                if (j.page == eventIndex + EBlueprint.DefaultPageCount && j.NodeType == typeof(AReturn).FullName)
                {
                    if (j.returnType != blueprintEvent.customEvent[eventIndex].returnType || j.returnContainer != blueprintEvent.customEvent[eventIndex].returnContainer)
                    {
                        j.returnType = blueprintEvent.customEvent[eventIndex].returnType;
                        j.returnContainer = blueprintEvent.customEvent[eventIndex].returnContainer;
                        (j as AReturn).SetReturnType();

                        foreach (var i in connections)
                        {
                            if (i.outPointMark.x == nodes.IndexOf(j) || i.inPointMark.x == nodes.IndexOf(j))
                            {
                                if (i.outPointMark.y == 1 || i.inPointMark.y == 1)
                                {
                                    connections.Remove(i);
                                    break;
                                }
                            }
                        }
                    }
                }

                if (j.NodeType == typeof(ACustomEventCall).FullName)
                {
                    if (j.unlocalTitle == blueprintEvent.customEvent[eventIndex].eventName)
                    {
                        foreach (var i in connections)
                        {
                            if (i.outPointMark.x == nodes.IndexOf(j) || i.inPointMark.x == nodes.IndexOf(j))
                            {
                                if (i.outPointMark.y == 1 || i.inPointMark.y == 1)
                                {
                                    connections.Remove(i);
                                    break;
                                }
                            }
                        }
                        (j as ACustomEventCall).UpdateField();
                    }
                }
            }
        }
        #endregion


        #region Custom Event Argument

        public void Editor_CustomEventArgument_AddArgument(int eventIndex)
        {
            BlueprintVariable buffer = new BlueprintVariable();
            buffer.label = Editor_CustomEventArgument_GetUniqueName("New Argument", eventIndex);
            blueprintEvent.customEvent[eventIndex].arugments.Add(buffer);

            Editor_CustomEventArgument_ChangeCustomEventArugment(eventIndex);
        }


        public void Editor_CustomEventArgument_CleanArgument(int eventIndex)
        {
            blueprintEvent.customEvent[eventIndex].arugments.Clear();
            Editor_CustomEventArgument_ChangeCustomEventArugment(eventIndex);
        }

        /// <summary>
        /// This action will search all related event <br />
        /// Then trying to modify the node state
        /// </summary>
        /// <param name="eventIndex">CustomEvent Index</param>
        public void Editor_CustomEventArgument_ChangeCustomEventArugment(int eventIndex)
        {
            /* Loop all blueprint, well including this one */
            foreach (var i in GetAllBlueprint)
            {
                List<ACustomEventCall> changeNodeBuffer_Call = new List<ACustomEventCall>();
                List<ACustomEvent> changeNodeBuffer_Method = new List<ACustomEvent>();

                /* Loop all node in every blueprint */
                foreach (var j in i.nodes)
                {
                    /* Finding the node that targeting the event which change argument */
                    if (j.targetEventOrVar == name + "." + blueprintEvent.customEvent[eventIndex].eventName)
                    {
                        /* Call type */
                        if (j.NodeType == typeof(ACustomEventCall).FullName)
                        {
                            (j as ACustomEventCall).SetCustomEvent(this, blueprintEvent.customEvent[eventIndex]);
                            changeNodeBuffer_Call.Add(j as ACustomEventCall);
                        }

                        /* Method type */
                        if (j.NodeType == typeof(ACustomEvent).FullName)
                        {
                            (j as ACustomEvent).SetCustomEvent(blueprintEvent.customEvent[eventIndex]);
                            changeNodeBuffer_Method.Add(j as ACustomEvent);
                        }
                    }
                }

                /* Loop all connection in every blueprint */
                foreach (var j in connections)
                {
                    /* Modify the call's connections that just been update */
                    foreach (var k in changeNodeBuffer_Call)
                    {
                        /* Kill above 0 connection */
                        /* Because the 0 index always event input */
                        for (int l = 1; l < k.fields.Count; l++)
                        {
                            i.Connection_RemoveRelateConnectionInField(k.fields[l]);
                        }
                    }

                    /* Modify the method's connections that just been update */
                    foreach (var k in changeNodeBuffer_Method)
                    {
                        /* Kill above 0 connection */
                        /* Because the 0 index always event input */
                        for (int l = 1; l < k.fields.Count; l++)
                        {
                            i.Connection_RemoveRelateConnectionInField(k.fields[l]);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Get the unique name of event argument label
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Editor_CustomEventArgument_GetUniqueName(string input, int eventIndex)
        {
            int count = 0;
            bool repeat = true;

            while (repeat)
            {
                repeat = false;

                foreach (var i in blueprintEvent.customEvent[eventIndex].arugments)
                {
                    if (count == 0)
                    {
                        if (i.label == input)
                        {
                            count++;
                            repeat = true;
                        }
                    }
                    else
                    {
                        if (i.label == input + (count - 1).ToString())
                        {
                            count++;
                            repeat = true;
                        }
                    }
                }
            }

            if (count == 0) return input;
            else return input + (count - 1).ToString();
        }


        /// <summary>
        /// Check if the label is repeat in event argument list, if repeat then given a unique label
        /// </summary>
        /// <param name="input">Event Name</param>
        /// <param name="index">Event Index</param>
        /// <returns></returns>
        public string Editor_CustomEventArgument_GetUniqueName_Field(string input, int index, int eventIndex)
        {
            for (int i = 0; i < blueprintEvent.customEvent[eventIndex].arugments.Count; i++)
            {
                if (i != index)
                {
                    if (blueprintEvent.customEvent[eventIndex].arugments[i].label == input) return Editor_CustomEventArgument_GetUniqueName(input, eventIndex);
                }
            }
            return input;
        }
        #endregion


        #region Custom Variable
        /// <summary>
        /// When user is clicking add a new custom variable
        /// </summary>
        public void Editor_CustomVariable_AddCustomVariable()
        {
            BlueprintVariable buffer = new BlueprintVariable();
            buffer.label = Editor_CustomVariable_GetUniqueName("New Variable");
            buffer.type = FieldType.Int;
            blueprintVariables.Add(buffer);

            for(int i = 0; i < blueprintVariables.Count; i++)
            {
                Editor_CustomVariable_ChangeCustomVariable(blueprintVariables[i].label);
            }
        }


        /// <summary>
        /// It will kill all the relationship with these variables
        /// </summary>
        public void Editor_CustomVariable_CleanCustomVariable()
        {
            int count = blueprintVariables.Count;
            for (int i = 0; i < count; i++)
            {
                Editor_CustomVariable_DeleteCustomVariable(i);
            }
        }

        /// <summary>
        /// This action will search all related var <br />
        /// Then trying to delete all the relationship
        /// </summary>
        /// <param name="index">Custom Var Index</param>
        /// <param name="varName">Var Name</param>
        public void Editor_CustomVariable_DeleteCustomVariable(string varName)
        {
            /* Loop all blueprint, well including this one */
            foreach (var i in GetAllBlueprint)
            {
                List<NodeBase> nb = new List<NodeBase>();
                /* Loop all nodes */
                foreach (var j in i.nodes)
                {
                    /* Set type */
                    if (j.NodeType == typeof(SetVariable).FullName && j.targetEventOrVar == name + "." + varName)
                    {
                        nb.Add(j);
                    }

                    /* Get type */
                    if (j.NodeType == typeof(GetVariable).FullName && j.targetEventOrVar == name + "." + varName)
                    {
                        nb.Add(j);
                    }

                    /* Set type */
                    if (j.NodeType == typeof(SetOutterVariable).FullName && j.targetEventOrVar == name + "." + varName)
                    {
                        nb.Add(j);
                    }

                    /* Get type */
                    if (j.NodeType == typeof(GetOutterVariable).FullName && j.targetEventOrVar == name + "." + varName)
                    {
                        nb.Add(j);
                    }
                }

                for(int j = 0; j < nb.Count; j++)
                {
                    i.Node_RemoveNode(nb[j]);
                }
            }
        }


        /// <summary>
        /// Kill the custom variable according to variable label 
        /// </summary>
        /// <param name="index"></param>
        public void Editor_CustomVariable_DeleteCustomVariable(int index)
        {
            Editor_CustomVariable_DeleteCustomVariable(blueprintVariables[index].label);
            blueprintVariables.RemoveAt(index);
        }


        /// <summary>
        /// This action will search all related var <br />
        /// Then trying to delete all the relationship
        /// <param name="index">Custom Var Index</param>
        /// <param name="varName">Var Name</param>
        public void Editor_CustomVariable_ChangeCustomVariable(string varName)
        {
            /* Loop all blueprint, well including this one */
            foreach (var i in GetAllBlueprint)
            {
                /* Loop all nodes */
                foreach (var j in i.nodes)
                {
                    /* Set type */
                    if (j.NodeType == typeof(SetVariable).FullName && j.targetEventOrVar.Split('.')[0] == name && j.targetEventOrVar.Split('.')[1] == varName)
                    {
                        (j as SetVariable).SetOptions(GetInheritVariable());
                    }

                    /* Get type */
                    if (j.NodeType == typeof(GetVariable).FullName && j.targetEventOrVar.Split('.')[0] == name && j.targetEventOrVar.Split('.')[1] == varName)
                    {
                        (j as GetVariable).SetOptions(GetInheritVariable());
                    }

                    /* Set type */
                    if (j.NodeType == typeof(SetOutterVariable).FullName && j.targetEventOrVar.Split('.')[0] == name && j.targetEventOrVar.Split('.')[1] == varName)
                    {
                        (j as SetOutterVariable).SetOptions(GetInheritVariable());
                    }

                    /* Get type */
                    if (j.NodeType == typeof(GetOutterVariable).FullName && j.targetEventOrVar.Split('.')[0] == name && j.targetEventOrVar.Split('.')[1] == varName)
                    {
                        (j as GetOutterVariable).SetOptions(GetInheritVariable());
                    }
                }
            }
        }


        public void Editor_CustomVariable_ChangeCustomVariable_All()
        {
            List<NodeBase> nb = new List<NodeBase>();

            foreach (var j in nodes)
            {
                /* Set type */
                if (j.NodeType == typeof(SetVariable).FullName)
                {
                    bool exist = false;

                    foreach (var k in GetInheritVariable())
                    {
                        if (k.Item2.name == j.targetEventOrVar.Split('.')[0] && k.Item1.label == j.targetEventOrVar.Split('.')[1])
                        {
                            exist = true;
                        }
                    }

                    if (!exist)
                    {
                        nb.Add(j);
                    }
                    else
                    {
                        (j as SetVariable).SetOptions(GetInheritVariable());
                    }
                }

                /* Get type */
                if (j.NodeType == typeof(GetVariable).FullName)
                {
                    (j as GetVariable).SetOptions(GetInheritVariable());
                }
            }

            for (int j = 0; j < nb.Count; j++)
            {
                Node_RemoveNode(nb[j]);
            }

            /* Loop all blueprint, well including this one */
            foreach (var i in GetAllBlueprint)
            {

                /* Loop all nodes */
                foreach (var j in i.nodes)
                {
                    

                    /* Set type */
                    if (j.NodeType == typeof(SetOutterVariable).FullName && j.targetEventOrVar.Split('.')[0] == name)
                    {
                        (j as SetOutterVariable).SetOptions(GetInheritVariable());
                    }

                    /* Get type */
                    if (j.NodeType == typeof(GetOutterVariable).FullName && j.targetEventOrVar.Split('.')[0] == name)
                    {
                        (j as GetOutterVariable).SetOptions(GetInheritVariable());
                    }
                }
            }
        }

        public void Editor_CustomVariable_ChangeCustomVariableName(string oldName, string newName)
        {
            /* Loop all blueprint, well including this one */
            foreach (var i in GetAllBlueprint)
            {
                /* Loop all nodes */
                foreach (var j in i.nodes)
                {
                    /* Set type */
                    if (j.NodeType == typeof(SetVariable).FullName && j.targetEventOrVar.Split('.')[0] == name)
                    {
                        (j as SetVariable).RenameContent(oldName, newName);
                    }

                    /* Get type */
                    if (j.NodeType == typeof(GetVariable).FullName && j.targetEventOrVar.Split('.')[0] == name)
                    {
                        (j as GetVariable).RenameContent(oldName, newName);
                    }

                    /* Get type */
                    if (j.NodeType == typeof(SetOutterVariable).FullName && j.targetEventOrVar.Split('.')[0] == name)
                    {
                        (j as SetOutterVariable).RenameContent(oldName, newName);
                    }

                    /* Get type */
                    if (j.NodeType == typeof(GetOutterVariable).FullName && j.targetEventOrVar.Split('.')[0] == name)
                    {
                        (j as GetOutterVariable).RenameContent(oldName, newName);
                    }
                }
            }
        }


        /// <summary>
        /// Get the unique name of variable label
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Editor_CustomVariable_GetUniqueName(string input)
        {
            int count = 0;
            bool repeat = true;

            while (repeat)
            {
                repeat = false;

                foreach (var i in blueprintVariables)
                {
                    if (count == 0)
                    {
                        if (i.label == input)
                        {
                            count++;
                            repeat = true;
                        }
                    }
                    else
                    {
                        if (i.label == input + (count - 1).ToString())
                        {
                            count++;
                            repeat = true;
                        }
                    }
                }
            }

            if (count == 0) return input;
            else return input + (count - 1).ToString();
        }


        /// <summary>
        /// Check if the label is repeat in variable list, if repeat then given a unique name
        /// </summary>
        /// <param name="input">Event Name</param>
        /// <param name="index">Event Index</param>
        /// <returns></returns>
        public string Editor_CustomVariable_GetUniqueName_Field(string input, int index)
        {
            for (int i = 0; i < blueprintVariables.Count; i++)
            {
                if (i != index)
                {
                    if (blueprintVariables[i].label == input) return Editor_CustomVariable_GetUniqueName(input);
                }
            }
            return input;
        }
        #endregion


        #region Inherit
        /// <summary>
        /// We have to update all the variables and event relationship <br />
        /// If inherit is null then kill all the inherit relationship <br />
        /// If inherit have something update the relationship dropdown or anything
        /// </summary>
        /// <param name="last"></param>
        public void Editor_InheritUpdate()
        {
            Editor_CustomEvent_ChangeEvent_All();
            Editor_CustomVariable_ChangeCustomVariable_All();
        }
        #endregion
    }
}
