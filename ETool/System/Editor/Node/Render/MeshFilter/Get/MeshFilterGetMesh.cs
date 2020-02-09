using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/MeshFilter/Get/GetMesh")]
    public class MeshFilterGetMesh : NodeBase
    {
        public MeshFilterGetMesh(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Mesh";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.MeshFilter, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Mesh, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Mesh), 1)]
        public Mesh GetMesh(BlueprintInput data)
        {
            return GetFieldOrLastInputField<MeshFilter>(0, data).mesh;
        }
    }
}
