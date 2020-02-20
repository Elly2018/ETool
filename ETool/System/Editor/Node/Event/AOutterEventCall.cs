using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Event/External Call")]
    [CanNotCopy]
    public class AOutterEventCall : NodeBase
    {
        private EBlueprint ETarget;
        private BlueprintCustomEvent MyTarget = null;

        public AOutterEventCall(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "External Event Call";
        }

        /// <summary>
        /// Editor stage <br />
        /// It will force modify this outter event call target
        /// </summary>
        /// <param name="targetBP"></param>
        /// <param name="targetEvent"></param>
        public void SetCustomEvent(EBlueprint targetBP, BlueprintCustomEvent targetEvent)
        {
            ETarget = targetBP;
            MyTarget = targetEvent;
            UpdateContent();
        }


        /// <summary>
        /// Use the given reference and modify the fields
        /// </summary>
        private void UpdateContent()
        {
            fields[1].target.genericUnityType.target_Blueprint = ETarget;

            if(ETarget == null)
            {
                /* Set the target null */
                Zero();
                targetEventOrVar = ".";
                return;
                
            }
            else
            {
                /* The target is not null */

                /* Get the custom event struct from target blueprint */
                List<Tuple<BlueprintCustomEvent, EBlueprint>> buffer = ETarget.GetAllPublicEvent();

                int index = (int)fields[3].GetValue(FieldType.Int);
                if (index > -1 && index <= buffer.Count)
                {
                    MyTarget = buffer[index].Item1;
                    UpdateField();
                }
                targetEventOrVar = ETarget.name + "." + buffer[index].Item1.eventName;
            }
        }

        private void DropdownInitliaze(List<Tuple<BlueprintCustomEvent, EBlueprint>> buffer)
        {
            fields[3].target_array = new GenericObject[buffer.Count];
            for (int i = 0; i < buffer.Count; i++)
            {
                fields[3].target_array[i] = new GenericObject();
                fields[3].target_array[i].genericBasicType.target_String = buffer[i].Item1.eventName;
            }
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            List<object> _arg = new List<object>();
            for (int i = 4; i < fields.Count; i++)
            {
                if (CheckIfConnectionExist(i, data, true))
                    _arg.Add(GetFieldInputValue(i, data));
                else
                    _arg.Add(null);

            }
            EBlueprint target = (EBlueprint)GetFieldOrLastInputField(2, data);
            target.Custom_CallCustomEvent(fields[3].target_array[fields[3].target.genericBasicType.target_Int].genericBasicType.target_String, _arg.ToArray());
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "BP Sample", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "BP Instance", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Dropdown, "BP Function", ConnectionType.None, this, FieldContainer.Object));
        }

        public override void FieldUpdate()
        {
            FieldUpdateAndRunningTimeInitialize();
        }

        public override void FinalFieldInitialize(BlueprintInput data)
        {
            FieldUpdateAndRunningTimeInitialize();
        }

        private void FieldUpdateAndRunningTimeInitialize()
        {
            ETarget = fields[1].target.genericUnityType.target_Blueprint;

            if (ETarget != null)
            {
                List<Tuple<BlueprintCustomEvent, EBlueprint>> buffer = ETarget.GetAllPublicEvent();
                DropdownInitliaze(buffer);
            }
            else
            {
                Zero();
                return;
            }

            foreach (var i in ETarget.blueprintEvent.customEvent)
            {
                if (i.eventName == fields[3].target_array[fields[3].target.genericBasicType.target_Int].genericBasicType.target_String)
                {
                    MyTarget = i;
                    UpdateContent();
                }
            }
        }

        private void Zero()
        {
            fields[3].target_array = new GenericObject[0];
            fields[3].target.genericBasicType.target_Int = 0;

            while(fields.Count != 4)
            {
                ACustomEvent.RemoveVariableField(this, true);
            }
        }

        private void UpdateField()
        {
            bool change = true;
            while (change)
            {
                change = false;
                if (fields.Count > MyTarget.arugments.Count + 4)
                {
                    change = true;
                    ACustomEvent.RemoveVariableField(this, true);
                }
                if (fields.Count < MyTarget.arugments.Count + 4)
                {
                    change = true;
                    ACustomEvent.AddVariableField(MyTarget.arugments[fields.Count - 4], this, true);
                }
            }

            for (int i = 4; i < fields.Count; i++)
            {
                if (!ACustomEvent.CheckArugmentMatch(MyTarget.arugments[i - 4], fields[i]))
                    ACustomEvent.ChangeVariableField(MyTarget.arugments[i - 4], this, i, true);
            }
        }
    }
}
