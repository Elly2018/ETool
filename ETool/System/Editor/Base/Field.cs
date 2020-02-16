using ETool.ANode;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

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
    /// Field type, it mean support object type
    /// </summary>
    public enum FieldType
    {
        // Event type
        // Range: 0 - 9
        Event = 0,
        Button = 1,
        Object = 2,
        Dropdown = 3,

        // Basic data type
        // Range: 10 - 49
        Int = 10,
        Long = 11,
        Float = 12,
        Double = 13,
        String = 14,
        Boolean = 15,
        Number = 16,
        Vector = 17,
        Char = 18,

        // Unity basic data type
        // Range: 50 - 199
        GameObject = 50,
        Transform = 51,
        Vector2 = 52,
        Vector3 = 53,
        Vector4 = 54,
        Rect = 55,
        Color = 56,
        Texture = 57,
        Texture2D = 58,
        Texture3D = 59,
        Material = 60,
        Quaternion = 61,
        AudioClip = 62,
        AnimatorStateInfo = 63,
        AnimatorClipInfo = 64,
        Blueprint = 65,
        GameData = 66,
        AudioMixer = 67,
        Touch = 68,
        Ray = 69,
        Mesh = 70,
        Flare = 71,
        Matrix4x4 = 72,
        Plane = 73,
        Bounds = 74,

        // Component
        // Range: 200 - 1999
        Rigidbody = 200,
        Rigidbody2D = 201,
        Collision = 202,
        Collision2D = 203,
        Collider = 204,
        Collider2D = 205,
        MeshFilter = 206,
        MeshRenderer = 207,
        Animator = 208,
        NodeComponent = 209,
        Light = 210,
        AudioSource = 211,
        Camera = 212,
        Character = 213,

        // Useful enum type
        // Range: 2000 - NaN
        ForceMode = 2000,
        Type = 2001,
        Component = 2002,
        Variable = 2003,
        Key = 2004,
        Interpolation = 2005,
        DetectionMode = 2006,
        CursorMode = 2007,
        CursorLockMode = 2008,
        EnvironemntPath = 2009,
        AvatarIKGoal = 2010,
        LightType = 2011,
        ShadowType = 2012,
        InstallMode = 2013,
        Platform = 2014,
        FullScreenMode = 2015,
        ScreenOrientation = 2016,
        CollisionFlag = 2017,
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
    public class Field
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
            target = reference.target;
            target_array = reference.target_array;
            connectionType = reference.connectionType;
            fieldType = reference.fieldType;
            fieldContainer = reference.fieldContainer;

            inPoint = reference.inPoint;
            outPoint = reference.outPoint;

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

                case FieldType.Object: // 1
                    {
                        GUI.Label(Full, title, StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Dropdown:
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
                        target.genericUnityType.blueprint = ObjectField<EBlueprint>(target.genericUnityType.blueprint, Top, Bottom);
                        break;
                    }

                case FieldType.GameData: // 66
                    {
                        target.genericUnityType.gameData = ObjectField<EGameData>(target.genericUnityType.gameData, Top, Bottom);
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

                case FieldType.Matrix4x4:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
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
                        target.target_Component.nodeComponent = ObjectField<NodeComponent>(target.target_Component.nodeComponent, Top, Bottom);
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
                #endregion

                /* 2000 - Nan */
                #region Enum Type
                case FieldType.ForceMode:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericBasicType.target_Int = (int)(ForceMode)EditorGUI.EnumPopup(Bottom, (ForceMode)target.genericBasicType.target_Int);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Component:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, GetComponentEnumUseStruct());
                        break;
                    }

                case FieldType.Type:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, GetTypeEnumUseStruct());
                        break;
                    }

                case FieldType.Variable:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.genericBasicType.target_Int = EditorGUI.Popup(Bottom, target.genericBasicType.target_Int, GetVariableStringArray());
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Key:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, FormExistEnumStruct<KeyCode>());
                        break;
                    }

                case FieldType.Interpolation:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, FormExistEnumStruct<RigidbodyInterpolation>());
                        break;
                    }

                case FieldType.DetectionMode:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, FormExistEnumStruct<CollisionDetectionMode>());
                        break;
                    }

                case FieldType.CursorMode:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, FormExistEnumStruct<CursorMode>());
                        break;
                    }

                case FieldType.CursorLockMode:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, FormExistEnumStruct<CursorLockMode>());
                        break;
                    }

                case FieldType.EnvironemntPath:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, FormExistEnumStruct<Environment.SpecialFolder>());
                        break;
                    }

                case FieldType.AvatarIKGoal:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, FormExistEnumStruct<AvatarIKGoal>());
                        break;
                    }

                case FieldType.LightType:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, FormExistEnumStruct<LightType>());
                        break;
                    }

                case FieldType.ShadowType:
                    {
                        target.genericBasicType.target_Int = EnumField(target.genericBasicType.target_Int, Top, Bottom, FormExistEnumStruct<LightShadows>());
                        break;
                    }
                    #endregion

            }

            /* If user change gui value */
            /* Trigger field update event */
            bool changed = EditorGUI.EndChangeCheck();
            if (changed)
            {
                NodeBasedEditor.Instance.Node_GetNodeByField(this).FieldUpdate();
                EditorUtility.SetDirty(NodeBasedEditor.Instance);
            }

            /* Drawing connection point */
            DrawPoint();
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
            List<BlueprintVariable> bv = NodeBasedEditor.Instance.blueprintVariables;
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

        /// <summary>
        /// You can define connection color by field type
        /// </summary>
        /// <param name="fieldType">Type</param>
        /// <param name="alpha">Color Alpha</param>
        /// <returns>Color Match The Field Type</returns>
        public static Color GetColorByFieldType(FieldType fieldType, float alpha)
        {
            Color c = Color.white;
            switch (fieldType)
            {
                case FieldType.Event:
                    break;
                case FieldType.Button:
                    break;

                case FieldType.Int:
                    c = Color.red;
                    break;
                case FieldType.Long:
                    c = Color.yellow;
                    break;
                case FieldType.Float:
                    c = Color.green;
                    break;
                case FieldType.String:
                    c = Color.cyan;
                    break;
                case FieldType.Boolean:
                    c = Color.blue;
                    break;
                case FieldType.Double:
                    c = Color.yellow;
                    break;
                case FieldType.GameObject:
                    c = new Color(0.6f, 0.6f, 0.9f);
                    break;
                case FieldType.Transform:
                    break;
                case FieldType.Vector2:
                    c = Color.green;
                    break;
                case FieldType.Vector3:
                    c = Color.green;
                    break;
                case FieldType.Vector4:
                    c = Color.green;
                    break;
                case FieldType.Rect:
                    break;
                case FieldType.Color:
                    break;
                case FieldType.Texture:
                    break;
                case FieldType.Texture2D:
                    break;
                case FieldType.Texture3D:
                    break;
                case FieldType.Material:
                    break;
                case FieldType.Quaternion:
                    break;
                case FieldType.Rigidbody:
                    break;
                case FieldType.Rigidbody2D:
                    break;
                case FieldType.Collision:
                    break;
                case FieldType.Collision2D:
                    break;
                case FieldType.Collider:
                    break;
                case FieldType.Collider2D:
                    break;
                case FieldType.MeshFilter:
                    break;
                case FieldType.MeshRenderer:
                    break;
                case FieldType.ForceMode:
                    break;
                case FieldType.Type:
                    break;
                case FieldType.Component:
                    break;
                case FieldType.Variable:
                    break;
                case FieldType.Key:
                    break;
                case FieldType.Object:
                    break;
                case FieldType.Dropdown:
                    break;
                case FieldType.Number:
                    break;
                case FieldType.Vector:
                    break;
                case FieldType.Char:
                    break;
                case FieldType.AudioClip:
                    break;
                case FieldType.AnimatorStateInfo:
                    break;
                case FieldType.AnimatorClipInfo:
                    break;
                case FieldType.Blueprint:
                    break;
                case FieldType.GameData:
                    break;
                case FieldType.AudioMixer:
                    break;
                case FieldType.Touch:
                    break;
                case FieldType.Ray:
                    break;
                case FieldType.Mesh:
                    break;
                case FieldType.Flare:
                    break;
                case FieldType.Animator:
                    break;
                case FieldType.NodeComponent:
                    break;
                case FieldType.Light:
                    break;
                case FieldType.AudioSource:
                    break;
                case FieldType.Interpolation:
                    break;
                case FieldType.DetectionMode:
                    break;
                case FieldType.CursorMode:
                    break;
                case FieldType.CursorLockMode:
                    break;
                case FieldType.EnvironemntPath:
                    break;
                case FieldType.AvatarIKGoal:
                    break;
                case FieldType.LightType:
                    break;
                case FieldType.ShadowType:
                    break;
            }
            return new Color(c.r, c.g, c.b, alpha);
        }

        /// <summary>
        /// Get type by field type
        /// </summary>
        /// <param name="tf">Type</param>
        /// <returns></returns>
        public static Type GetTypeByFieldType(FieldType tf)
        {
            switch (tf)
            {
                #region Event
                case FieldType.Event:
                    break;
                case FieldType.Button:
                    break;
                case FieldType.Object:
                    return typeof(object);
                case FieldType.Dropdown:
                    break;
                #endregion

                #region Basic Type
                case FieldType.Int:
                    return typeof(Int32); // 10
                case FieldType.Long:
                    return typeof(Int64); // 11
                case FieldType.Float:
                    return typeof(Single); // 12
                case FieldType.String:
                    return typeof(String); // 13
                case FieldType.Boolean:
                    return typeof(Boolean); // 14
                case FieldType.Double:
                    return typeof(Double); // 15
                case FieldType.Number:
                    break; // 16
                case FieldType.Vector:
                    break; // 17
                case FieldType.Char:
                    return typeof(char);
                #endregion

                #region Unity Type
                case FieldType.GameObject:
                    return typeof(GameObject); // 50
                case FieldType.Transform:
                    return typeof(Transform); // 51
                case FieldType.Vector2:
                    return typeof(Vector2); // 52
                case FieldType.Vector3:
                    return typeof(Vector3); // 53
                case FieldType.Vector4:
                    return typeof(Vector4); // 54
                case FieldType.Rect:
                    return typeof(Rect); // 55
                case FieldType.Color:
                    return typeof(Color); // 56
                case FieldType.Texture:
                    return typeof(Texture); // 57
                case FieldType.Texture2D:
                    return typeof(Texture2D); // 58
                case FieldType.Texture3D:
                    return typeof(Texture3D); // 59
                case FieldType.Material:
                    return typeof(Material); // 60
                case FieldType.Quaternion:
                    return typeof(Quaternion); // 61
                case FieldType.AudioClip:
                    return typeof(AudioClip); // 62
                case FieldType.AnimatorStateInfo:
                    return typeof(AnimatorStateInfo); // 63
                case FieldType.AnimatorClipInfo:
                    return typeof(AnimatorClipInfo); // 64
                case FieldType.Blueprint:
                    return typeof(EBlueprint); // 65
                case FieldType.GameData:
                    return typeof(EGameData); // 66
                case FieldType.AudioMixer:
                    return typeof(AudioMixer); // 67
                case FieldType.Touch:
                    return typeof(Touch); // 68
                case FieldType.Ray:
                    return typeof(Ray); // 69
                case FieldType.Mesh:
                    return typeof(Mesh); // 70
                case FieldType.Flare:
                    return typeof(Flare); // 71
                case FieldType.Matrix4x4:
                    return typeof(Matrix4x4); // 72
                #endregion

                #region Component Type
                case FieldType.Rigidbody:
                    return typeof(Rigidbody); // 200
                case FieldType.Rigidbody2D:
                    return typeof(Rigidbody2D); // 201
                case FieldType.Collision:
                    return typeof(Collision); // 202
                case FieldType.Collision2D:
                    return typeof(Collision2D); // 203
                case FieldType.Collider:
                    return typeof(Collider); // 204
                case FieldType.Collider2D:
                    return typeof(Collider2D); // 205
                case FieldType.MeshFilter:
                    return typeof(MeshFilter); // 206
                case FieldType.MeshRenderer:
                    return typeof(MeshRenderer); // 207
                case FieldType.Animator:
                    return typeof(Animator); // 208
                case FieldType.NodeComponent:
                    return typeof(NodeComponent); // 209
                case FieldType.Light:
                    return typeof(Light); // 210
                case FieldType.AudioSource:
                    return typeof(AudioSource); // 211
                case FieldType.Camera:
                    return typeof(Camera); // 212
                #endregion

                #region Enum type
                case FieldType.ForceMode:
                    return typeof(ForceMode); // 2000
                case FieldType.Type:
                    return typeof(FieldType); // 2001
                case FieldType.Component:
                    return typeof(Component); // 2002
                case FieldType.Variable:
                    return typeof(GenericObject); // 2003
                case FieldType.Key:
                    return typeof(KeyCode); // 2004
                case FieldType.Interpolation:
                    return typeof(RigidbodyInterpolation); // 2005
                case FieldType.DetectionMode:
                    return typeof(CollisionDetectionMode); // 2006
                case FieldType.CursorMode:
                    return typeof(CursorMode); // 2007
                case FieldType.CursorLockMode:
                    return typeof(CursorLockMode); // 2008
                case FieldType.EnvironemntPath:
                    return typeof(Environment.SpecialFolder); // 2009
                case FieldType.AvatarIKGoal:
                    return typeof(AvatarIKGoal); // 2010
                case FieldType.LightType:
                    return typeof(LightType); // 2011
                case FieldType.ShadowType:
                    return typeof(LightShadows); // 2012
                    #endregion
            }
            return typeof(Nullable);
        }

        /// <summary>
        /// Get object by field type <br />
        /// </summary>
        /// <param name="tf">Field Type</param>
        /// <param name="go">Field Data</param>
        /// <returns></returns>
        public static object GetObjectByFieldType(FieldType tf, GenericObject go)
        {
            switch (tf)
            {
                case FieldType.Event:
                    break;
                case FieldType.Button:
                    break;

                #region Basic type
                case FieldType.Int:
                    return go.genericBasicType.target_Int; // 10
                case FieldType.Long:
                    return go.genericBasicType.target_Long; // 11
                case FieldType.Float:
                    return go.genericBasicType.target_Float; // 12
                case FieldType.Double:
                    return go.genericBasicType.target_Double; // 13
                case FieldType.String:
                    return go.genericBasicType.target_String; // 14
                case FieldType.Boolean:
                    return go.genericBasicType.target_Boolean; // 15
                case FieldType.Number:
                    return go.genericBasicType.target_Int; // 16
                case FieldType.Vector:
                    return go.genericBasicType.target_Int; // 17
                case FieldType.Char:
                    return go.genericBasicType.target_Char; // 18
                #endregion

                #region Unity Type
                case FieldType.GameObject:
                    return go.genericUnityType.target_GameObject; // 50
                case FieldType.Transform:
                    return go.genericUnityType.target_Transform; // 51
                case FieldType.Vector2:
                    return go.genericUnityType.target_Vector2; // 52
                case FieldType.Vector3:
                    return go.genericUnityType.target_Vector3; // 53
                case FieldType.Vector4:
                    return go.genericUnityType.target_Vector4; // 54
                case FieldType.Rect:
                    return go.genericUnityType.target_Rect; // 55
                case FieldType.Color:
                    return go.genericUnityType.target_Color; // 56
                case FieldType.Texture:
                    return go.genericUnityType.target_Texture; // 57
                case FieldType.Texture2D:
                    return go.genericUnityType.target_Texture2D; // 58
                case FieldType.Texture3D:
                    return go.genericUnityType.target_Texture3D; // 59
                case FieldType.Material:
                    return go.genericUnityType.target_Material; // 60
                case FieldType.Quaternion:
                    return go.genericUnityType.target_Quaternion; // 61
                case FieldType.AudioClip:
                    return go.genericUnityType.target_AudioClip; // 62
                case FieldType.Blueprint:
                    return go.genericUnityType.blueprint; // 65
                case FieldType.GameData:
                    return go.genericUnityType.gameData; // 66
                case FieldType.AnimatorStateInfo:
                    return go.genericUnityType.target_AnimatorStateInfo; // 67
                case FieldType.AnimatorClipInfo:
                    return go.genericUnityType.target_AnimatorClipInfo; // 68
                case FieldType.AudioMixer:
                    return go.genericUnityType.target_AudioMixer; // 69
                case FieldType.Touch:
                    return go.genericUnityType.target_Touch; // 70
                case FieldType.Ray:
                    return go.genericUnityType.target_Ray; // 71
                case FieldType.Mesh:
                    return go.genericUnityType.target_Mesh; // 72
                case FieldType.Flare:
                    return go.genericUnityType.target_Flare; // 73
                case FieldType.Matrix4x4:
                    return go.genericUnityType.target_matrix4X4; // 74
                #endregion

                #region Component Type
                case FieldType.Rigidbody:
                    return go.target_Component.rigidbody; // 200
                case FieldType.Rigidbody2D:
                    return go.target_Component.rigidbody2D; // 201
                case FieldType.Collision:
                    return go.target_Component.collision; // 202
                case FieldType.Collision2D:
                    return go.target_Component.collision2D; // 203
                case FieldType.Collider:
                    return go.target_Component.collider; // 204
                case FieldType.Collider2D:
                    return go.target_Component.collider2D; // 205
                case FieldType.MeshFilter:
                    return go.target_Component.meshFilter; // 206
                case FieldType.MeshRenderer:
                    return go.target_Component.meshRenderer; // 207
                case FieldType.Animator:
                    return go.target_Component.animator; // 208
                case FieldType.NodeComponent:
                    return go.target_Component.nodeComponent; // 209
                case FieldType.Light:
                    return go.target_Component.light; // 210
                case FieldType.AudioSource:
                    return go.target_Component.audioSource; // 211
                case FieldType.Camera:
                    return go.target_Component.camera; // 212
                #endregion

                #region Enum Type
                case FieldType.ForceMode:
                    return (ForceMode)go.genericBasicType.target_Int;
                case FieldType.Type:
                    return (FieldType)go.genericBasicType.target_Int;
                case FieldType.Component:
                    return go.genericBasicType.target_Int;
                case FieldType.Variable:
                    return go.genericBasicType.target_Int;
                case FieldType.Key:
                    return (KeyCode)go.genericBasicType.target_Int;
                case FieldType.Interpolation:
                    return (RigidbodyInterpolation)go.genericBasicType.target_Int;
                case FieldType.DetectionMode:
                    return (CollisionDetectionMode)go.genericBasicType.target_Int;
                case FieldType.Object:
                    return typeof(object);
                case FieldType.CursorMode:
                    return (CursorMode)go.genericBasicType.target_Int;
                case FieldType.CursorLockMode:
                    return (CursorLockMode)go.genericBasicType.target_Int;
                case FieldType.Dropdown:
                    return (CursorLockMode)go.genericBasicType.target_Int;
                case FieldType.EnvironemntPath:
                    return (Environment.SpecialFolder)go.genericBasicType.target_Int;
                case FieldType.AvatarIKGoal:
                    return (AvatarIKGoal)go.genericBasicType.target_Int;
                case FieldType.LightType:
                    return (LightType)go.genericBasicType.target_Int;
                case FieldType.ShadowType:
                    return (LightShadows)go.genericBasicType.target_Int;
                    #endregion
            }
            return typeof(Nullable);
        }

        public static object[] GetObjectArrayByFieldType(FieldType tf, GenericObject[] go)
        {
            object[] result = new object[go.Length];
            for(int i = 0; i < go.Length; i++)
            {
                result[i] = GetObjectByFieldType(tf, go[i]);
            }
            return result;
        }

        public static object SetObjectByFieldType(FieldType tf, GenericObject go, object o)
        {
            switch (tf)
            {
                case FieldType.Event:
                    break;
                case FieldType.Button:
                    break;

                #region Basic Type
                case FieldType.Int:
                    go.genericBasicType.target_Int = (Int32)o; // 10
                    return go.genericBasicType.target_Int;
                case FieldType.Long:
                    go.genericBasicType.target_Long = (Int64)o; // 11
                    return go.genericBasicType.target_Long;
                case FieldType.Float:
                    go.genericBasicType.target_Float = (Single)o; // 12
                    return go.genericBasicType.target_Float;
                case FieldType.String:
                    go.genericBasicType.target_String = (String)o; // 13
                    return go.genericBasicType.target_String;
                case FieldType.Boolean:
                    go.genericBasicType.target_Boolean = (Boolean)o; // 14
                    return go.genericBasicType.target_Boolean;
                case FieldType.Double:
                    go.genericBasicType.target_Double = (Double)o; // 15
                    return go.genericBasicType.target_Double;
                #endregion

                #region Unity Type
                case FieldType.GameObject:
                    go.genericUnityType.target_GameObject = (GameObject)o; // 50
                    return go.genericUnityType.target_GameObject;
                case FieldType.Transform:
                    go.genericUnityType.target_Transform = (Transform)o; // 51
                    return go.genericUnityType.target_Transform;
                case FieldType.Vector2:
                    go.genericUnityType.target_Vector2 = (Vector2)o; // 52
                    return go.genericUnityType.target_Vector2;
                case FieldType.Vector3:
                    go.genericUnityType.target_Vector3 = (Vector3)o; // 53
                    return go.genericUnityType.target_Vector3;
                case FieldType.Vector4:
                    go.genericUnityType.target_Vector4 = (Vector4)o; // 54
                    return go.genericUnityType.target_Vector4;
                case FieldType.Rect:
                    go.genericUnityType.target_Rect = (Rect)o; // 55
                    return go.genericUnityType.target_Rect;
                case FieldType.Color:
                    go.genericUnityType.target_Color = (Color)o; // 56
                    return go.genericUnityType.target_Color;
                case FieldType.Texture:
                    go.genericUnityType.target_Texture = (Texture)o; // 57
                    return go.genericUnityType.target_Texture;
                case FieldType.Texture2D:
                    go.genericUnityType.target_Texture2D = (Texture2D)o; // 58
                    return go.genericUnityType.target_Texture2D;
                case FieldType.Texture3D:
                    go.genericUnityType.target_Texture3D = (Texture3D)o; // 59
                    return go.genericUnityType.target_Texture3D;
                case FieldType.Material:
                    go.genericUnityType.target_Material = (Material)o; // 60
                    return go.genericUnityType.target_Material;
                case FieldType.Quaternion:
                    go.genericUnityType.target_Quaternion = (Quaternion)o; // 61
                    return go.genericUnityType.target_Quaternion;
                case FieldType.Blueprint:
                    go.genericUnityType.blueprint = (EBlueprint)o; // 65
                    return go.genericUnityType.blueprint;
                case FieldType.GameData:
                    go.genericUnityType.gameData = (EGameData)o; // 66
                    return go.genericUnityType.gameData;
                #endregion

                #region Component
                case FieldType.Rigidbody:
                    go.target_Component.rigidbody = (Rigidbody)o; // 200
                    return go.target_Component.rigidbody;
                case FieldType.Rigidbody2D:
                    go.target_Component.rigidbody2D = (Rigidbody2D)o; // 201
                    return go.target_Component.rigidbody2D;
                case FieldType.Collision:
                    go.target_Component.collision = (Collision)o; // 202
                    return go.target_Component.collision;
                case FieldType.Collision2D:
                    go.target_Component.collision2D = (Collision2D)o; // 203
                    return go.target_Component.collision2D;
                case FieldType.Collider:
                    go.target_Component.collider = (Collider)o; // 204
                    return go.target_Component.collider;
                case FieldType.Collider2D:
                    go.target_Component.collider2D = (Collider2D)o; // 205
                    return go.target_Component.collider2D;
                case FieldType.MeshFilter:
                    go.target_Component.meshFilter = (MeshFilter)o; // 206
                    return go.target_Component.meshFilter;
                case FieldType.MeshRenderer:
                    go.target_Component.meshRenderer = (MeshRenderer)o; // 207
                    return go.target_Component.meshRenderer;
                case FieldType.Animator:
                    go.target_Component.animator = (Animator)o; // 208
                    return go.target_Component.animator;
                case FieldType.AudioSource:
                    go.target_Component.audioSource = (AudioSource)o; // 211
                    return go.target_Component.audioSource;
                #endregion

                #region Enum Type
                case FieldType.ForceMode:
                    go.genericBasicType.target_Int = (Int32)o;
                    return go.genericBasicType.target_Int;
                case FieldType.Type:
                    go.genericBasicType.target_Int = (Int32)o;
                    return go.genericBasicType.target_Int;
                case FieldType.Component:
                    go.genericBasicType.target_Int = (Int32)o;
                    return go.genericBasicType.target_Int;
                case FieldType.Variable:
                    go.genericBasicType.target_Int = (Int32)o;
                    return go.genericBasicType.target_Int;
                case FieldType.Key:
                    go.genericBasicType.target_Int = (Int32)o;
                    return go.genericBasicType.target_Int;
                case FieldType.Interpolation:
                    go.genericBasicType.target_Int = (Int32)o;
                    return go.genericBasicType.target_Int;
                case FieldType.DetectionMode:
                    go.genericBasicType.target_Int = (Int32)o;
                    return go.genericBasicType.target_Int;
                case FieldType.Object:
                    break;
                case FieldType.Dropdown:
                    break;
                case FieldType.CursorMode:
                    break;
                case FieldType.CursorLockMode:
                    break;
                case FieldType.EnvironemntPath:
                    break;
                    #endregion
            }
            return typeof(Nullable);
        }

        public static GenericObject[] SetObjectArrayByField(FieldType tf, GenericObject[] go, object[] o)
        {
            if(go.Length == o.Length)
            {
                /* Just apply the value */
                for(int i = 0; i < go.Length; i++)
                {
                    SetObjectByFieldType(tf, go[i], o[i]);
                }
            }
            else if (go.Length > o.Length)
            {
                /* Go remove element */
                int diff = go.Length - o.Length;
                List<GenericObject> buffer = go.ToList();

                for(int i = 0; i < diff; i++)
                {
                    buffer.RemoveAt(buffer.Count - 1);
                }

                for (int i = 0; i < buffer.Count; i++)
                {
                    SetObjectByFieldType(tf, buffer[i], o[i]);
                }

                go = buffer.ToArray();
            }
            else if (go.Length < o.Length)
            {
                /* Go adding element */
                int diff = o.Length - go.Length;
                List<GenericObject> buffer = go.ToList();

                for (int i = 0; i < buffer.Count; i++)
                {
                    SetObjectByFieldType(tf, buffer[i], o[i]);
                }

                for(int i = 0; i < diff; i++)
                {
                    GenericObject bufferGO = new GenericObject();
                    SetObjectByFieldType(tf, bufferGO, o[go.Length + i]);
                    buffer.Add(bufferGO);
                }
                go = buffer.ToArray();
            }
            return go;
        }

        public static GenericObject DrawFieldHelper(GenericObject o, FieldType type)
        {
            switch (type)
            {
                case FieldType.Event:
                    break;
                case FieldType.Button:
                    break;

                #region Basic Type
                case FieldType.Int:
                    o.genericBasicType.target_Int = EditorGUILayout.IntField(o.genericBasicType.target_Int); // 10
                    break;
                case FieldType.Long:
                    o.genericBasicType.target_Long = EditorGUILayout.LongField(o.genericBasicType.target_Long); // 11
                    break;
                case FieldType.Float:
                    o.genericBasicType.target_Float = EditorGUILayout.FloatField(o.genericBasicType.target_Float); // 12
                    break;
                case FieldType.String:
                    o.genericBasicType.target_String = EditorGUILayout.TextField(o.genericBasicType.target_String); // 13
                    break;
                case FieldType.Boolean:
                    o.genericBasicType.target_Boolean = EditorGUILayout.Toggle(o.genericBasicType.target_Boolean); // 14
                    break;
                case FieldType.Double:
                    o.genericBasicType.target_Double = EditorGUILayout.DoubleField(o.genericBasicType.target_Double); // 15
                    break;
                #endregion

                #region Unity Type
                case FieldType.GameObject:
                    o.genericUnityType.target_GameObject = EditorObjectField<GameObject>(o.genericUnityType.target_GameObject);
                    break;
                case FieldType.Transform:
                    o.genericUnityType.target_Transform = EditorObjectField<Transform>(o.genericUnityType.target_Transform);
                    break;
                case FieldType.Vector2:
                    o.genericUnityType.target_Vector2 = EditorGUILayout.Vector2Field("", o.genericUnityType.target_Vector2);
                    break;
                case FieldType.Vector3:
                    o.genericUnityType.target_Vector3 = EditorGUILayout.Vector3Field("", o.genericUnityType.target_Vector3);
                    break;
                case FieldType.Vector4:
                    o.genericUnityType.target_Vector4 = EditorGUILayout.Vector4Field("", o.genericUnityType.target_Vector4);
                    break;
                case FieldType.Rect:
                    Vector4 r = new Vector4(o.genericUnityType.target_Rect.x, o.genericUnityType.target_Rect.y, o.genericUnityType.target_Rect.width, o.genericUnityType.target_Rect.height);
                    r = EditorGUILayout.Vector4Field("", r);
                    o.genericUnityType.target_Rect = new Rect(r.x, r.y, r.z, r.w);
                    break;
                case FieldType.Color:
                    o.genericUnityType.target_Color = EditorGUILayout.ColorField(o.genericUnityType.target_Color);
                    break;
                case FieldType.Texture:
                    o.genericUnityType.target_Texture = EditorObjectField<Texture>(o.genericUnityType.target_Texture);
                    break;
                case FieldType.Texture2D:
                    o.genericUnityType.target_Texture2D = EditorObjectField<Texture2D>(o.genericUnityType.target_Texture2D);
                    break;
                case FieldType.Texture3D:
                    o.genericUnityType.target_Texture3D = EditorObjectField<Texture3D>(o.genericUnityType.target_Texture3D);
                    break;
                case FieldType.Material:
                    o.genericUnityType.target_Material = EditorObjectField<Material>(o.genericUnityType.target_Material);
                    break;
                case FieldType.Quaternion:
                    break;
                #endregion

                case FieldType.Rigidbody:
                    o.target_Component.rigidbody = EditorObjectField<Rigidbody>(o.target_Component.rigidbody);
                    break;
                case FieldType.Rigidbody2D:
                    o.target_Component.rigidbody2D = EditorObjectField<Rigidbody2D>(o.target_Component.rigidbody2D);
                    break;
                case FieldType.Collision:
                    break;
                case FieldType.Collision2D:
                    break;
                case FieldType.Collider:
                    o.target_Component.collider = EditorObjectField<Collider>(o.target_Component.collider);
                    break;
                case FieldType.Collider2D:
                    o.target_Component.collider2D = EditorObjectField<Collider2D>(o.target_Component.collider2D);
                    break;
                case FieldType.MeshFilter:
                    o.target_Component.meshFilter = EditorObjectField<MeshFilter>(o.target_Component.meshFilter);
                    break;
                case FieldType.MeshRenderer:
                    o.target_Component.meshRenderer = EditorObjectField<MeshRenderer>(o.target_Component.meshRenderer);
                    break;
                case FieldType.ForceMode:
                    break;
                case FieldType.Type:
                    break;
                case FieldType.Component:
                    break;
                case FieldType.Variable:
                    break;
                case FieldType.Key:
                    break;
                case FieldType.Object:
                    break;
                case FieldType.Dropdown:
                    break;
                case FieldType.Animator:
                    break;
                case FieldType.Blueprint:
                    break;
                case FieldType.GameData:
                    break;
                case FieldType.AudioSource:
                    break;
                case FieldType.Interpolation:
                    break;
                case FieldType.DetectionMode:
                    break;
                case FieldType.CursorMode:
                    break;
                case FieldType.CursorLockMode:
                    break;
                case FieldType.EnvironemntPath:
                    break;
            }
            return o;
        }

        public static FieldType GetFieldTypeByString(string str)
        {
            return FieldType.Event;
        }

        private static T EditorObjectField<T>(UnityEngine.Object o) where T : UnityEngine.Object
        {
            o = (T)EditorGUILayout.ObjectField(o, typeof(T), true);
            return o as T;
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

        private static EnumUseStruct[] GetFieldTypeEnumUseStruct(int min, int max)
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
