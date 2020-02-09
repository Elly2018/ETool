using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Audio;

namespace ETool
{
    /// <summary>
    /// The object that usually store in blueprint <br />
    /// Because unity doesn't support much about generic data type <br />
    /// </summary>
    [System.Serializable]
    public class GenericObject
    {
        /* Basic data type */
        public GenericBasicType genericBasicType = null;

        /* Unity data type */
        public GenericUnityType genericUnityType = null;

        /* Component data type */
        public GenericComponent target_Component = null;

        public GenericObject()
        {
            genericBasicType = new GenericBasicType();
            genericUnityType = new GenericUnityType();
            target_Component = new GenericComponent();
        }

        public GenericObject(GenericObject reference)
        {
            genericBasicType = new GenericBasicType(reference.genericBasicType);
            genericUnityType = new GenericUnityType(reference.genericUnityType);
            target_Component = new GenericComponent(reference.target_Component);
        }

        public T GetUnityComponent<T>() where T : UnityEngine.Object
        {
            FieldInfo[] memberInfos = target_Component.GetType().GetFields();
            int memberIndexMatch = -1;
            for(int i = 0; i < memberInfos.Length; i++)
            {
                if(memberInfos[i].GetType() == typeof(T))
                {
                    memberIndexMatch = i;
                }
            }
            return target_Component.GetType().GetFields()[memberIndexMatch].GetValue(target_Component) as T;
        }
    }

    /// <summary>
    /// The basic system data type
    /// </summary>
    [System.Serializable]
    public class GenericBasicType
    {
        public int target_Int; // 10
        public long target_Long; // 11
        public float target_Float; // 12
        public string target_String; // 13
        public bool target_Boolean; // 14
        public double target_Double; // 15
        public char target_Char; // 16

        public GenericBasicType()
        {
            target_Int = 0;
            target_Long = 0;
            target_Float = 0.0f;
            target_String = string.Empty;
            target_Boolean = false;
            target_Double = 0;
            target_Char = ' ';
        }

        public GenericBasicType(GenericBasicType reference)
        {
            target_Int = reference.target_Int;
            target_Long = reference.target_Long;
            target_Float = reference.target_Float;
            target_String = reference.target_String;
            target_Boolean = reference.target_Boolean;
            target_Double = reference.target_Double;
        }
    }

    /// <summary>
    /// The basic unity data type
    /// </summary>
    [System.Serializable]
    public class GenericUnityType
    {
        public GameObject target_GameObject; // 50
        public Transform target_Transform; // 51
        public Vector2 target_Vector2; // 52
        public Vector3 target_Vector3; // 53
        public Vector4 target_Vector4; // 54
        public Rect target_Rect = Rect.zero; // 55
        public Color target_Color = Color.white; // 56
        public Texture target_Texture = null; // 57
        public Texture2D target_Texture2D = null; // 58
        public Texture3D target_Texture3D = null; // 59
        public Material target_Material = null; // 60
        public Quaternion target_Quaternion = Quaternion.identity; // 61
        public AudioClip target_AudioClip = null; // 62
        public AnimatorStateInfo target_AnimatorStateInfo; // 63
        public AnimatorClipInfo target_AnimatorClipInfo; // 64
        public EBlueprint blueprint = null; // 65
        public EGameData gameData = null; // 66
        public AudioMixer target_AudioMixer = null; // 67
        public Touch target_Touch; // 68
        public Ray target_Ray; // 69
        public Mesh target_Mesh = null; // 70
        public Flare target_Flare = null; // 71

        public GenericUnityType()
        {
            target_GameObject = null;
            target_Transform = null;
            target_Vector2 = Vector2.zero;
            target_Vector3 = Vector3.zero;
            target_Vector4 = Vector4.zero;
            target_Rect = Rect.zero;
            target_Color = Color.white;
            target_Texture = null;
            target_Texture2D = null;
            target_Texture3D = null;
            target_Material = null;
            target_Quaternion = Quaternion.identity;
            target_AudioClip = null;
            target_AnimatorStateInfo = new AnimatorStateInfo();
            target_AnimatorClipInfo = new AnimatorClipInfo();
            blueprint = null;
            gameData = null;
            target_AudioMixer = null;
            target_Touch = new Touch();
            target_Ray = new Ray();
            target_Mesh = null;
            target_Flare = null;
        }

        public GenericUnityType(GenericUnityType reference)
        {
            target_GameObject = reference.target_GameObject;
            target_Transform = reference.target_Transform;
            target_Vector2 = reference.target_Vector2;
            target_Vector3 = reference.target_Vector3;
            target_Vector4 = reference.target_Vector4;
            target_Rect = reference.target_Rect;
            target_Color = reference.target_Color;
            target_Texture = reference.target_Texture;
            target_Texture2D = reference.target_Texture2D;
            target_Texture3D = reference.target_Texture3D;
            target_Material = reference.target_Material;
            target_Quaternion = reference.target_Quaternion;
        }
    }

    /// <summary>
    /// The basic component often use
    /// </summary>
    [System.Serializable]
    public class GenericComponent
    {
        public Rigidbody rigidbody; // 200
        public Rigidbody2D rigidbody2D; // 201
        public Collision collision; // 202
        public Collision2D collision2D; // 203
        public Collider collider; // 204
        public Collider2D collider2D; // 205
        public MeshFilter meshFilter; // 206
        public MeshRenderer meshRenderer; // 207
        public Animator animator; // 208
        public NodeComponent nodeComponent; // 209
        public Light light; // 210
        public AudioSource audioSource; // 211

        public GenericComponent()
        {
        }

        public GenericComponent(GenericComponent reference)
        {
            rigidbody = reference.rigidbody; // 200
            rigidbody2D = reference.rigidbody2D; // 201
            collision = reference.collision; // 202
            collision2D = reference.collision2D; // 203
            collider = reference.collider; // 204
            collider2D = reference.collider2D; // 205
            meshFilter = reference.meshFilter; // 206
            meshRenderer = reference.meshRenderer; // 207
            animator = reference.animator; // 208
            audioSource = reference.audioSource; // 211
        }
    }

    public class TypeUtility
    {
        public static Type GetTypeByName(string name)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            foreach(var i in ass.GetTypes())
            {
                if (i.FullName == name) return i;
            }
            return typeof(Nullable);
        }
    }
}
