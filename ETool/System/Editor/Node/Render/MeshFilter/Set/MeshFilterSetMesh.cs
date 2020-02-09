using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/MeshFilter/Set/SetMesh")]
    public class MeshFilterSetMesh : NodeBase
    {
        public MeshFilterSetMesh(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Mesh";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.MeshFilter, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Mesh, "Mesh", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            MeshFilter mf = GetFieldOrLastInputField<MeshFilter>(1, data);
            Mesh ms = GetFieldOrLastInputField<Mesh>(2, data);

            if (mf != null)
                mf.mesh = ms;

            ActiveNextEvent(0, data);
        }

        [NodePropertyGet(typeof(MeshFilter), 1)]
        public MeshFilter GetMesh(BlueprintInput data)
        {
            return GetFieldOrLastInputField<MeshFilter>(1, data);
        }
    }
}
