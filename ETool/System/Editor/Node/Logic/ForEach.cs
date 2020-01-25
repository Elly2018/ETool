using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/ForEach")]
    public class ForEach : NodeBase
    {
        public ForEach(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "For Each";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            int count = 0;

            if (CheckIfConnectionExist(1, data, true))
                count = (int)GetFieldInputValue(1, data);
            else
                count = fields[1].target.target_Int;

            for(int i = 0; i < count; i++)
            {
                ActiveNextEvent(0, data);
            }
            ActiveNextEvent(2, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Count", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Event, "Finish", ConnectionType.EventOutput, this, FieldContainer.Object));
        }
    }
}
