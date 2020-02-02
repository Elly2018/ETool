using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Material/Get By Mesh Renderer")]
    public class GetByMeshRender : NodeBase
    {
        public GetByMeshRender(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get By Mesh Renderer";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.MeshRenderer, "Source", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Shared", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Material, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Material), 2)]
        public Material GetMat(BlueprintInput data)
        {
            if((bool)GetFieldOrLastInputField(1, data))
            {
                return ((MeshRenderer)GetFieldOrLastInputField(0, data)).sharedMaterial;
            }
            else
            {
                return ((MeshRenderer)GetFieldOrLastInputField(0, data)).material;
            }
        }
    }
}
