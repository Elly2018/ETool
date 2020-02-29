using ETool.ANode;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ETool
{
    /// <summary>
    /// Define who can use the properties <br />
    /// Public : Everyone, including outside blueprint <br />
    /// Protected : Only inherit blueprint <br />
    /// Private : Only self
    /// </summary>
    public enum AccessAbility
    {
        Public, Protected, Private
    }

    /// <summary>
    /// Define the field connection data <br />
    /// input or output <br />
    /// Event or data
    /// </summary>
    public enum ConnectionType
    {
        None,
        DataInput,
        DataOutput,
        DataBoth, 
        EventInput, 
        EventOutput,
        EventBoth,
    }

    /// <summary>
    /// Define what a field container is
    /// </summary>
    public enum FieldContainer
    {
        Object,
        Array
    }


    /// <summary>
    /// When drawing enum on node editor <br />
    /// Developer need a container contain enum index and enum string
    /// </summary>
    public struct EnumUseStruct
    {
        public string fieldName;
        public int fieldIndex;
    }

    /// <summary>
    /// Field class define how field behave <br />
    /// Also contain most static casting method in it
    /// </summary>
    [System.Serializable]
    public partial class Field
    {
        #region Variable
        /// <summary>
        /// Define field gray background left and right padding
        /// </summary>
        private const float leftPadding = -3;
        private const float rightPadding = -3;

        /// <summary>
        /// Field drawing rect
        /// </summary>
        public Rect rect;

        /// <summary>
        /// Field title string
        /// </summary>
        public string title;

        /// <summary>
        /// Define the field contain data
        /// </summary>
        public GenericObject target;

        /// <summary>
        /// Define the field contain data
        /// </summary>
        public GenericObject[] target_array = new GenericObject[0];

        /// <summary>
        /// Define how input output and it's event or data
        /// </summary>
        public ConnectionType connectionType;

        /// <summary>
        /// Define what field type this field is
        /// </summary>
        public FieldType fieldType;

        /// <summary>
        /// Define the style <br />
        /// </summary>
        public INodeStyle _iNodeStyle;

        /// <summary>
        /// Define the data container type <br />
        /// object or array or other
        /// </summary>
        public FieldContainer fieldContainer;

        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;

        public bool onConnection = false;
        public bool hideField = false;
        #endregion

        /// <summary>
        /// This contructor use for default field <br />
        /// Support basic data type <br />
        /// </summary>
        /// <param name="fieldType">Field Data Type</param>
        /// <param name="title">Field Title</param>
        /// <param name="connectionType">Connection IO</param>
        /// <param name="iNode">Node Interface</param>
        public Field(FieldType fieldType, string title, ConnectionType connectionType, INodeStyle iNode, FieldContainer fieldContainer)
        {
            target = new GenericObject();
            this.fieldType = fieldType;
            this.title = title;
            this.connectionType = connectionType;
            _iNodeStyle = iNode;

            if (fieldContainer == FieldContainer.Object)
            {
                inPoint = new ConnectionPoint(ConnectionPointType.In, StyleUtility.GetStyle(_iNodeStyle.GetInPointStyle()));
                outPoint = new ConnectionPoint(ConnectionPointType.Out, StyleUtility.GetStyle(_iNodeStyle.GetOutPointStyle()));
            }
            if (fieldContainer == FieldContainer.Array)
            {
                inPoint = new ConnectionPoint(ConnectionPointType.In, StyleUtility.GetStyle(_iNodeStyle.GetInPointArrayStyle()));
                outPoint = new ConnectionPoint(ConnectionPointType.Out, StyleUtility.GetStyle(_iNodeStyle.GetOutPointArrayStyle()));
            }

            inPoint.fieldType = fieldType;
            outPoint.fieldType = fieldType;
            this.fieldContainer = fieldContainer;
        }

        /// <summary>
        /// This contructor use for default field <br />
        /// Support basic data type <br />
        /// </summary>
        /// <param name="fieldType">Field Data Type</param>
        /// <param name="title">Field Title</param>
        /// <param name="connectionType">Connection IO</param>
        /// <param name="hide">Hide Inputfield</param>
        /// <param name="iNode">Node Interface</param>
        public Field(FieldType fieldType, string title, ConnectionType connectionType, bool hide, INodeStyle iNode, FieldContainer fieldContainer)
        {
            target = new GenericObject();
            this.fieldType = fieldType;
            this.title = title;
            this.connectionType = connectionType;
            _iNodeStyle = iNode;

            if (fieldContainer == FieldContainer.Object)
            {
                inPoint = new ConnectionPoint(ConnectionPointType.In, StyleUtility.GetStyle(_iNodeStyle.GetInPointStyle()));
                outPoint = new ConnectionPoint(ConnectionPointType.Out, StyleUtility.GetStyle(_iNodeStyle.GetOutPointStyle()));
            }
            if (fieldContainer == FieldContainer.Array)
            {
                inPoint = new ConnectionPoint(ConnectionPointType.In, StyleUtility.GetStyle(_iNodeStyle.GetInPointArrayStyle()));
                outPoint = new ConnectionPoint(ConnectionPointType.Out, StyleUtility.GetStyle(_iNodeStyle.GetOutPointArrayStyle()));
            }

            inPoint.fieldType = fieldType;
            outPoint.fieldType = fieldType;
            hideField = hide;
            this.fieldContainer = fieldContainer;
        }

        /// <summary>
        /// The constructor use reference to create instance
        /// </summary>
        /// <param name="reference">Reference</param>
        public Field(Field reference)
        {
            rect = new Rect(reference.rect);
            title = new string(reference.title.ToCharArray());
            target = new GenericObject(reference.target);
            target_array = (GenericObject[])reference.target_array.Clone();
            connectionType = reference.connectionType;
            fieldType = reference.fieldType;
            fieldContainer = reference.fieldContainer;

            inPoint = new ConnectionPoint(reference.inPoint);
            outPoint = new ConnectionPoint(reference.outPoint);

            if (fieldContainer == FieldContainer.Object)
            {
                inPoint.style = StyleUtility.GetStyle(StyleType.In_Point);
                outPoint.style = StyleUtility.GetStyle(StyleType.Out_Point);
            }
            if (fieldContainer == FieldContainer.Array)
            {
                inPoint.style = StyleUtility.GetStyle(StyleType.In_Point_Array);
                outPoint.style = StyleUtility.GetStyle(StyleType.Out_Point_Array);
            }

            onConnection = reference.onConnection;
            hideField = reference.hideField;
        }

#if UNITY_EDITOR
        public virtual void DrawBG()
        {
            Rect Full = new Rect(rect);
            Full.x -= leftPadding;
            Full.width += rightPadding + leftPadding;

            GUI.Label(rect, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
        }

        /// <summary>
        /// Drawing method
        /// </summary>
        public virtual void Draw()
        {
            /* Create the instance of top part and bottom part */
            Rect Full = new Rect(rect);
            Rect Top = new Rect(rect);
            Rect Bottom = new Rect(rect);

            /* Half top and half bottom */
            Top.height -= Top.height / 2;
            Bottom.height -= Bottom.height / 2;
            Bottom.y += Bottom.height;

            Top.x -= leftPadding;
            Bottom.x -= leftPadding;
            Full.x -= leftPadding;
            Top.width += rightPadding + leftPadding;
            Bottom.width += rightPadding + leftPadding;
            Full.width += rightPadding + leftPadding;

            /* Check field update */
            /* When update, it will trigger node method: FieldUpdate() */
            EditorGUI.BeginChangeCheck();

            /* Enum type */
            if((int)fieldType >= 2000 && (int)fieldType != 2001)
            {
                int bufferIndex = EnumField(target.genericBasicType.target_Int, Top, Bottom, FormExistEnumStruct(FieldTypeStruct.GetStruct(fieldType).Item2));
                if(bufferIndex != target.genericBasicType.target_Int)
                {
                    target.genericBasicType.target_Int = bufferIndex;
                    FieldUpdate();
                }
                return;
            }

            if((int)fieldType == 2001)
            {
                GUI.Label(Top, ((FieldType)target.genericBasicType.target_Int).ToString(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                if(GUI.Button(Bottom, "Type Select"))
                {
                    NodeBasedEditor.Editor_Instance.OnUseTypeList(this);
                }
            }

            switch (fieldType)
            {
                /* 0 - 10 */
                #region Event
                case FieldType.Event: // 0
                    {
                        GUI.Label(Full, title, StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Button: // 1
                    break;

                case FieldType.Object: // 2
                    {
                        GUI.Label(Full, title, StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Dropdown: // 3
                    {
                        EditorGUI.LabelField(Top, EToolString.GetString_Node(title, "Dropdown"), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (target_array.Length != 0)
                        {
                            string[] e = new string[target_array.Length];
                            for (int i = 0; i < e.Length; i++)
                            {
                                e[i] = target_array[i].genericBasicType.target_String;
                            }
                            if (target.genericBasicType.target_Int > e.Length) target.genericBasicType.target_Int = 0;
                            if (PrintField())
                                target.genericBasicType.target_Int = EditorGUI.Popup(Bottom, target.genericBasicType.target_Int, e);
                            else
                                EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        }
                        break;
                    }

                case FieldType.Component: // 4
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, GetComponentEnumUseStruct());
                        break;
                    }

                case FieldType.Variable: // 5
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericBasicType.target_Int = EditorGUI.Popup(Bottom, target.genericBasicType.target_Int, GetVariableStringArray());
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                #endregion

                /* 10 - 49 */
                #region Basic Type Draw
                case FieldType.Int: // 10
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericBasicType.target_Int = EditorGUI.IntField(Bottom, target.genericBasicType.target_Int);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Long: // 11
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericBasicType.target_Long = EditorGUI.LongField(Bottom, target.genericBasicType.target_Long);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Float: // 12
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericBasicType.target_Float = EditorGUI.FloatField(Bottom, target.genericBasicType.target_Float);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Double: // 13
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericBasicType.target_Double = EditorGUI.DoubleField(Bottom, target.genericBasicType.target_Double);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.String: // 14
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericBasicType.target_String = EditorGUI.TextField(Bottom, target.genericBasicType.target_String);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Boolean: // 15
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericBasicType.target_Boolean = EditorGUI.Toggle(Bottom, target.genericBasicType.target_Boolean);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Number: // 16
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, GetNumberEnumUseStruct());
                        break;
                    }

                case FieldType.Vector: //17
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, GetVectorEnumUseStruct());
                        break;
                    }

                case FieldType.Char: // 18
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                        {
                            target.genericBasicType.target_Char = EditorGUI.TextField(Bottom, target.genericBasicType.target_Char.ToString())[0];
                        }

                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                #endregion

                /* 50 - 199 */
                #region Unity Type Draw
                case FieldType.GameObject: // 50
                    {
                        target.genericUnityType.target_GameObject = ObjectField<GameObject>(target.genericUnityType.target_GameObject, Top, Bottom);
                        break;
                    }

                case FieldType.Transform: // 51
                    {
                        target.genericUnityType.target_Transform = ObjectField<Transform>(target.genericUnityType.target_Transform, Top, Bottom);
                        break;
                    }

                case FieldType.Vector2: // 52
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericUnityType.target_Vector2 = EditorGUI.Vector2Field(Bottom, "", target.genericUnityType.target_Vector2);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Vector3: // 53
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericUnityType.target_Vector3 = EditorGUI.Vector3Field(Bottom, "", target.genericUnityType.target_Vector3);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Vector4: // 54
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericUnityType.target_Vector4 = EditorGUI.Vector4Field(Bottom, "", target.genericUnityType.target_Vector4);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Rect: // 55
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericUnityType.target_Rect = EditorGUI.RectField(Bottom, target.genericUnityType.target_Rect);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Color: // 56
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericUnityType.target_Color = EditorGUI.ColorField(Bottom, target.genericUnityType.target_Color);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Texture: // 57
                    {
                        target.genericUnityType.target_Texture = ObjectField<Texture>(target.genericUnityType.target_Texture, Top, Bottom);
                        break;
                    }

                case FieldType.Texture2D: // 58
                    {
                        target.genericUnityType.target_Texture2D = ObjectField<Texture2D>(target.genericUnityType.target_Texture2D, Top, Bottom);
                        break;
                    }

                case FieldType.Texture3D: // 59
                    {
                        target.genericUnityType.target_Texture3D = ObjectField<Texture3D>(target.genericUnityType.target_Texture3D, Top, Bottom);
                        break;
                    }

                case FieldType.Material: // 60
                    {
                        target.genericUnityType.target_Material = ObjectField<Material>(target.genericUnityType.target_Material, Top, Bottom);
                        break;
                    }

                case FieldType.Quaternion: // 61
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                        {
                            Vector4 q = new Vector4(
                                target.genericUnityType.target_Quaternion.x,
                                target.genericUnityType.target_Quaternion.y,
                                target.genericUnityType.target_Quaternion.z,
                                target.genericUnityType.target_Quaternion.w);
                            Vector4 resultQ = EditorGUI.Vector4Field(Bottom, "", q);
                            target.genericUnityType.target_Quaternion = new Quaternion(resultQ.x, resultQ.y, resultQ.z, resultQ.w);
                        }
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.AudioClip: // 62
                    {
                        target.genericUnityType.target_AudioClip = ObjectField<AudioClip>(target.genericUnityType.target_AudioClip, Top, Bottom);
                        break;
                    }

                case FieldType.AnimatorStateInfo: // 63
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.AnimatorClipInfo: // 64
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Blueprint: // 65
                    {
                        target.genericUnityType.target_Blueprint = ObjectField<EBlueprint>(target.genericUnityType.target_Blueprint, Top, Bottom);
                        break;
                    }

                case FieldType.GameData: // 66
                    {
                        target.genericUnityType.target_GameData = ObjectField<EGameData>(target.genericUnityType.target_GameData, Top, Bottom);
                        break;
                    }

                case FieldType.AudioMixer: // 67
                    {
                        target.genericUnityType.target_AudioMixer = ObjectField<AudioMixer>(target.genericUnityType.target_AudioMixer, Top, Bottom);
                        break;
                    }

                case FieldType.Touch: // 68
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Ray: // 69
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Mesh: // 70
                    {
                        target.genericUnityType.target_Mesh = ObjectField<Mesh>(target.genericUnityType.target_Mesh, Top, Bottom);
                        break;
                    }

                case FieldType.Flare: // 71
                    {
                        target.genericUnityType.target_Flare = ObjectField<Flare>(target.genericUnityType.target_Flare, Top, Bottom);
                        break;
                    }

                case FieldType.Matrix4x4: // 72
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Plane: // 73
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Bounds: // 74
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Clip: // 75
                    {
                        target.genericUnityType.target_VideoClip = ObjectField<VideoClip>(target.genericUnityType.target_Mesh, Top, Bottom);
                        break;
                    }

                case FieldType.Renderer:
                    break;
                case FieldType.RenderTexture:
                    break;
                #endregion

                /* 200 - 1999 */
                #region Component Type Draw
                case FieldType.Rigidbody: // 200
                    {
                        target.target_Component.rigidbody = ObjectField<Rigidbody>(target.target_Component.rigidbody, Top, Bottom);
                        break;
                    }

                case FieldType.Rigidbody2D: // 201
                    {
                        target.target_Component.rigidbody2D = ObjectField<Rigidbody2D>(target.target_Component.rigidbody2D, Top, Bottom);
                        break;
                    }

                case FieldType.Collision: // 202
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Collision2D: // 203
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Collider: // 204
                    {
                        target.target_Component.collider = ObjectField<Collider>(target.target_Component.collider, Top, Bottom);
                        break;
                    }

                case FieldType.Collider2D: // 205
                    {
                        target.target_Component.collider2D = ObjectField<Collider2D>(target.target_Component.collider2D, Top, Bottom);
                        break;
                    }

                case FieldType.MeshFilter: // 206
                    {
                        target.target_Component.meshFilter = ObjectField<MeshFilter>(target.target_Component.meshFilter, Top, Bottom);
                        break;
                    }

                case FieldType.MeshRenderer: // 207
                    {
                        target.target_Component.meshRenderer = ObjectField<MeshRenderer>(target.target_Component.meshRenderer, Top, Bottom);
                        break;
                    }

                case FieldType.Animator: // 208
                    {
                        target.target_Component.animator = ObjectField<Animator>(target.target_Component.animator, Top, Bottom);
                        break;
                    }

                case FieldType.NodeComponent: // 209
                    {
                        target.target_Component.nodeComponent = ObjectField<ENodeComponent>(target.target_Component.nodeComponent, Top, Bottom);
                        break;
                    }

                case FieldType.Light: // 210
                    {
                        target.target_Component.light = ObjectField<Light>(target.target_Component.light, Top, Bottom);
                        break;
                    }

                case FieldType.AudioSource: // 211
                    {
                        target.target_Component.audioSource = ObjectField<AudioSource>(target.target_Component.audioSource, Top, Bottom);
                        break;
                    }
                case FieldType.Camera: // 212
                    {
                        target.target_Component.camera = ObjectField<Camera>(target.target_Component.camera, Top, Bottom);
                        break;
                    }

                case FieldType.Character:
                    {
                        target.target_Component.characterController = ObjectField<CharacterController>(target.target_Component.camera, Top, Bottom);
                        break;
                    }

                case FieldType.VideoPlayer:
                    {
                        target.target_Component.videoPlayer = ObjectField<VideoPlayer>(target.target_Component.camera, Top, Bottom);
                        break;
                    }
                #endregion
            }

            /* If user change gui value */
            /* Trigger field update event */
            bool changed = EditorGUI.EndChangeCheck();
            if (changed)
            {
                FieldUpdate();
            }

            /* Drawing connection point */
            DrawPoint();
        }


        public void FieldUpdate()
        {
            NodeBasedEditor.Instance.Node_GetNodeByField(this).FieldUpdate();
            EditorUtility.SetDirty(NodeBasedEditor.Instance);
        }


        /// <summary>
        /// The method drawing unity object type
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="target">Object Instance</param>
        /// <param name="top">Top Rect</param>
        /// <param name="bottom">Bottom Rect</param>
        /// <returns></returns>
        private T ObjectField<T>(UnityEngine.Object target, Rect top, Rect bottom) where T : UnityEngine.Object
        {
            EditorGUI.LabelField(top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
            if (PrintField())
            {
                return (T)EditorGUI.ObjectField(bottom, target, typeof(T), true);
            }
            else
            {
                EditorGUI.LabelField(bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                return target as T;
            }
        }

        /// <summary>
        /// The method drawing enum object type <br />
        /// Use int to store enum id
        /// </summary>
        /// <param name="target">Target Int32</param>
        /// <param name="top">Top Rect</param>
        /// <param name="bottom">Bottom Rect</param>
        /// <param name="useStructs">Enum Struct</param>
        /// <returns>Enum ID</returns>
        private int EnumField(int target, Rect top, Rect bottom, EnumUseStruct[] useStructs)
        {
            EditorGUI.LabelField(top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
            if (PrintField())
            {
                bool targetInRange = false;
                int Current_Index = 0;
                for (int i = 0; i < useStructs.Length; i++)
                {
                    if (useStructs[i].fieldIndex == target)
                    {
                        targetInRange = true;
                        Current_Index = i;
                    }
                }
                if (!targetInRange) target = useStructs[0].fieldIndex;

                List<string> stringArray = new List<string>();
                for(int i = 0; i < useStructs.Length; i++)
                {
                    stringArray.Add(useStructs[i].fieldName);
                }

                int index = EditorGUI.Popup(bottom, Current_Index, stringArray.ToArray());
                return useStructs[index].fieldIndex;
            }
            else
                EditorGUI.LabelField(bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
            return target;
        }

        private static int GUIEnumField(int target, EnumUseStruct[] useStructs)
        {
            bool targetInRange = false;
            int Current_Index = 0;
            for (int i = 0; i < useStructs.Length; i++)
            {
                if (useStructs[i].fieldIndex == target)
                {
                    targetInRange = true;
                    Current_Index = i;
                }
            }
            if (!targetInRange) target = useStructs[0].fieldIndex;

            List<string> stringArray = new List<string>();
            for (int i = 0; i < useStructs.Length; i++)
            {
                stringArray.Add(useStructs[i].fieldName);
            }

            int index = EditorGUILayout.Popup(Current_Index, stringArray.ToArray());
            return useStructs[index].fieldIndex;
        }
#endif

        /// <summary>
        /// Get field object instance by given field type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public object GetValue(FieldType t)
        {
            return Field.GetObjectByFieldType(t, target);
        }

        /// <summary>
        /// Get field object instance by given field type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public object GetArrayValue(int index, FieldType t)
        {
            return Field.GetObjectByFieldType(t, target_array[index]);
        }

        /// <summary>
        /// Adding ([type name]) before the real title string
        /// </summary>
        /// <returns></returns>
        private string GetTypePrefixTitle()
        {
            return "(" + fieldType.ToString() + ")" + title;
        }

        /// <summary>
        /// Get custom variable array (only select blueprint)
        /// </summary>
        /// <returns></returns>
        private string[] GetVariableStringArray()
        {
            NodeBase nb = EBlueprint.GetNodeByField(this);
            EBlueprint b = EBlueprint.GetBlueprintByNode(nb);
            List<BlueprintVariable> bv = b.blueprintVariables;
            string[] result = new string[bv.Count];
            for(int i = 0; i < bv.Count; i++)
            {
                result[i] = bv[i].label;
            }
            return result;
        }

        /// <summary>
        /// Define if field should be print
        /// </summary>
        /// <returns></returns>
        private bool PrintField()
        {
            /* When it's not on connection use curve */
            /* And hidefield state is none */
            return !onConnection && !hideField;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Drawing connection point
        /// </summary>
        protected void DrawPoint()
        {
            switch (connectionType)
            {
                case ConnectionType.DataInput:
                    inPoint.Draw();
                    break;
                case ConnectionType.DataOutput:
                    outPoint.Draw();
                    break;
                case ConnectionType.DataBoth:
                    inPoint.Draw();
                    outPoint.Draw();
                    break;
                case ConnectionType.EventInput:
                    inPoint.Draw();
                    break;
                case ConnectionType.EventOutput:
                    outPoint.Draw();
                    break;
                case ConnectionType.EventBoth:
                    inPoint.Draw();
                    outPoint.Draw();
                    break;
            }
        }

        public void ProcessEvent(Event e)
        {
            switch (connectionType)
            {
                case ConnectionType.DataInput:
                    inPoint.ProcessEvents(e);
                    break;
                case ConnectionType.DataOutput:
                    outPoint.ProcessEvents(e);
                    break;
                case ConnectionType.DataBoth:
                    inPoint.ProcessEvents(e);
                    outPoint.ProcessEvents(e);
                    break;
                case ConnectionType.EventInput:
                    inPoint.ProcessEvents(e);
                    break;
                case ConnectionType.EventOutput:
                    outPoint.ProcessEvents(e);
                    break;
                case ConnectionType.EventBoth:
                    inPoint.ProcessEvents(e);
                    outPoint.ProcessEvents(e);
                    break;
            }
        }

        private static T EditorObjectField<T>(UnityEngine.Object o) where T : UnityEngine.Object
        {
            o = (T)EditorGUILayout.ObjectField(o, typeof(T), true);
            return o as T;
        }

#endif

        public static FieldType GetFieldTypeByString(string str)
        {
            return FieldType.Event;
        }

        public static EnumUseStruct[] GetComponentEnumUseStruct()
        {
            return GetFieldTypeEnumUseStruct(200, 1999);
        }

        public static EnumUseStruct[] GetNumberEnumUseStruct()
        {
            return GetFieldTypeEnumUseStruct(10, 13);
        }

        public static EnumUseStruct[] GetVectorEnumUseStruct()
        {
            return GetFieldTypeEnumUseStruct(52, 54);
        }

        public static EnumUseStruct[] GetTypeEnumUseStruct()
        {
            return GetFieldTypeEnumUseStruct(10, 9999);
        }

        public static EnumUseStruct[] FormExistEnumStruct<T>() where T : Enum
        {
            List<EnumUseStruct> result = new List<EnumUseStruct>();
            string[] FieldTypeString = Enum.GetNames(typeof(T));
            foreach (var i in FieldTypeString)
            {
                object type = (T)Enum.Parse(typeof(T), i);
                result.Add(new EnumUseStruct() { fieldIndex = (int)type, fieldName = i });
            }
            return result.ToArray();
        }

        public static EnumUseStruct[] FormExistEnumStruct(Type t)
        {
            List<EnumUseStruct> result = new List<EnumUseStruct>();
            string[] FieldTypeString = Enum.GetNames(t);
            foreach (var i in FieldTypeString)
            {
                object type = Enum.Parse(t, i);
                result.Add(new EnumUseStruct() { fieldIndex = (int)type, fieldName = i });
            }
            return result.ToArray();
        }

        public static EnumUseStruct[] GetFieldTypeEnumUseStruct(int min, int max)
        {
            List<EnumUseStruct> result = new List<EnumUseStruct>();
            string[] FieldTypeString = Enum.GetNames(typeof(FieldType));
            foreach (var i in FieldTypeString)
            {
                FieldType type = (FieldType)Enum.Parse(typeof(FieldType), i);
                if ((int)type >= min && (int)type <= max)
                {
                    result.Add(new EnumUseStruct() { fieldIndex = (int)type, fieldName = i });
                }
            }
            return result.ToArray();
        }
    }
}
