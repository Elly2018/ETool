using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ETool
{
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
        Component = 4,
        Variable = 5,

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
        Clip = 75,
        Renderer = 76,
        RenderTexture = 77,
        Collision = 78,
        Collision2D = 79,
        Cubemap = 80,

        // Component
        // Range: 200 - 1999
        Rigidbody = 200,
        Rigidbody2D = 201,
        Light = 202,
        Camera = 203,
        Collider = 204,
        Collider2D = 205,
        MeshFilter = 206,
        MeshRenderer = 207,
        Animator = 208,
        NodeComponent = 209,
        AudioSource = 210,
        Character = 211,
        VideoPlayer = 212,

        // Useful enum type
        // Range: 2000 - NaN
        ForceMode = 2000,
        Type = 2001,
        Key = 2002,
        Interpolation = 2003,
        DetectionMode = 2004,
        CursorMode = 2005,
        CursorLockMode = 2006,
        EnvironemntPath = 2007,
        AvatarIKGoal = 2008,
        LightType = 2009,
        ShadowType = 2010,
        InstallMode = 2011,
        Platform = 2012,
        FullScreenMode = 2013,
        ScreenOrientation = 2014,
        CollisionFlag = 2015,
        VideoAspectRatio = 2016,
        VideoRenderMode = 2017,
        VideoSource = 2018,
        VideoAudioOutputMode = 2019,
        CameraType = 2020,
        CameraClearFlags = 2021,
    }

    public class FieldTypeStruct : Tuple<FieldType, Type, Color>
    {
        public FieldTypeStruct(FieldType fieldType, Type classType, Color guiColor) : base(fieldType, classType, guiColor)
        {
            
        }

        public static FieldTypeStruct GetStruct(FieldType type)
        {
            foreach (var i in GetRegisterList)
            {
                if (type == i.Item1) return i;
            }
            return null;
        }

        private static List<FieldTypeStruct> GetRegisterList
        {
            get
            {
                List<FieldTypeStruct> buffer = new List<FieldTypeStruct>()
                {
                    new FieldTypeStruct(FieldType.Event, typeof(Nullable), new Color(1, 1, 1)), // 0
                    new FieldTypeStruct(FieldType.Button, typeof(Nullable), new Color(1, 1, 1)), // 1
                    new FieldTypeStruct(FieldType.Object, typeof(Nullable), new Color(1, 1, 1)), // 2
                    new FieldTypeStruct(FieldType.Dropdown, typeof(Nullable), new Color(1, 1, 1)), // 3
                    new FieldTypeStruct(FieldType.Component, typeof(Nullable), new Color(0, 0, 1)), // 4
                    new FieldTypeStruct(FieldType.Variable, typeof(Nullable), new Color(0, 0, 1)), // 5

                    new FieldTypeStruct(FieldType.Int, typeof(int), new Color(1, 0, 0)), // 10
                    new FieldTypeStruct(FieldType.Long, typeof(long), new Color(1, 0, 0)), // 11
                    new FieldTypeStruct(FieldType.Float, typeof(float), new Color(1, 0, 0)), // 12
                    new FieldTypeStruct(FieldType.Double, typeof(double), new Color(1, 0, 0)), // 13
                    new FieldTypeStruct(FieldType.String, typeof(string), new Color(1, 0, 0)), // 14
                    new FieldTypeStruct(FieldType.Boolean, typeof(bool), new Color(1, 0, 0)), // 15
                    new FieldTypeStruct(FieldType.Number, typeof(Nullable), new Color(0, 0, 0)), // 16
                    new FieldTypeStruct(FieldType.Vector, typeof(Nullable), new Color(0, 0, 0)), // 17
                    new FieldTypeStruct(FieldType.Char, typeof(char), new Color(1, 0, 0)), // 18

                    new FieldTypeStruct(FieldType.GameObject, typeof(GameObject), new Color(0, 1, 0)), // 50
                    new FieldTypeStruct(FieldType.Transform, typeof(Transform), new Color(0, 1, 0)), // 51
                    new FieldTypeStruct(FieldType.Vector2, typeof(Vector2), new Color(0, 1, 0)), // 52
                    new FieldTypeStruct(FieldType.Vector3, typeof(Vector3), new Color(0, 1, 0)), // 53
                    new FieldTypeStruct(FieldType.Vector4, typeof(Vector4), new Color(0, 1, 0)), // 54
                    new FieldTypeStruct(FieldType.Rect, typeof(Rect), new Color(0, 1, 0)), // 55
                    new FieldTypeStruct(FieldType.Color, typeof(Color), new Color(0, 1, 0)), // 56
                    new FieldTypeStruct(FieldType.Texture, typeof(Texture), new Color(0, 1, 0)), // 57
                    new FieldTypeStruct(FieldType.Texture2D, typeof(Texture2D), new Color(0, 1, 0)), // 58
                    new FieldTypeStruct(FieldType.Texture3D, typeof(Texture3D), new Color(0, 1, 0)), // 59
                    new FieldTypeStruct(FieldType.Material, typeof(Material), new Color(0, 1, 0)), // 60
                    new FieldTypeStruct(FieldType.Quaternion, typeof(Quaternion), new Color(0, 1, 0)), // 61
                    new FieldTypeStruct(FieldType.AudioClip, typeof(AudioClip), new Color(0, 1, 0)), // 62
                    new FieldTypeStruct(FieldType.AnimatorStateInfo, typeof(AnimatorStateInfo), new Color(0, 1, 0)), // 63
                    new FieldTypeStruct(FieldType.AnimatorClipInfo, typeof(AnimatorClipInfo), new Color(0, 1, 0)), // 64
                    new FieldTypeStruct(FieldType.Blueprint, typeof(EBlueprint), new Color(0, 1, 0)), // 65
                    new FieldTypeStruct(FieldType.GameData, typeof(EGameData), new Color(0, 1, 0)), // 66
                    new FieldTypeStruct(FieldType.AudioMixer, typeof(AudioMixer), new Color(0, 1, 0)), // 67
                    new FieldTypeStruct(FieldType.Touch, typeof(Touch), new Color(0, 1, 0)), // 68
                    new FieldTypeStruct(FieldType.Ray, typeof(Ray), new Color(0, 1, 0)), // 69
                    new FieldTypeStruct(FieldType.Mesh, typeof(Mesh), new Color(0, 1, 0)), // 70
                    new FieldTypeStruct(FieldType.Flare, typeof(Flare), new Color(0, 1, 0)), // 71
                    new FieldTypeStruct(FieldType.Matrix4x4, typeof(Matrix4x4), new Color(0, 1, 0)), // 72
                    new FieldTypeStruct(FieldType.Plane, typeof(Plane), new Color(0, 1, 0)), // 73
                    new FieldTypeStruct(FieldType.Bounds, typeof(Bounds), new Color(0, 1, 0)), // 74
                    new FieldTypeStruct(FieldType.Clip, typeof(VideoClip), new Color(0, 1, 0)), // 75
                    new FieldTypeStruct(FieldType.Renderer, typeof(Renderer), new Color(0, 1, 0)), // 76
                    new FieldTypeStruct(FieldType.RenderTexture, typeof(RenderTexture), new Color(0, 1, 0)), // 77
                    new FieldTypeStruct(FieldType.Collision, typeof(Collision), new Color(0, 1, 0)), // 78
                    new FieldTypeStruct(FieldType.Collision2D, typeof(Collision2D), new Color(0, 1, 0)), // 79

                    new FieldTypeStruct(FieldType.Rigidbody, typeof(Rigidbody), new Color(0, 0, 1)), // 200
                    new FieldTypeStruct(FieldType.Rigidbody2D, typeof(Rigidbody2D), new Color(0, 0, 1)), // 201
                    new FieldTypeStruct(FieldType.Light, typeof(Light), new Color(0, 0, 1)), // 202
                    new FieldTypeStruct(FieldType.Camera, typeof(Camera), new Color(0, 0, 1)), // 203
                    new FieldTypeStruct(FieldType.Collider, typeof(Collider), new Color(0, 0, 1)), // 204
                    new FieldTypeStruct(FieldType.Collider2D, typeof(Collider2D), new Color(0, 0, 1)), // 205
                    new FieldTypeStruct(FieldType.MeshFilter, typeof(MeshFilter), new Color(0, 0, 1)), // 206
                    new FieldTypeStruct(FieldType.MeshRenderer, typeof(MeshRenderer), new Color(0, 0, 1)), // 207
                    new FieldTypeStruct(FieldType.Animator, typeof(Animator), new Color(0, 0, 1)), // 208
                    new FieldTypeStruct(FieldType.NodeComponent, typeof(ENodeComponent), new Color(0, 0, 1)), // 209
                    new FieldTypeStruct(FieldType.AudioSource, typeof(AudioSource), new Color(0, 0, 1)), // 210
                    new FieldTypeStruct(FieldType.Character, typeof(CharacterController), new Color(0, 0, 1)), // 211
                    new FieldTypeStruct(FieldType.VideoPlayer, typeof(VideoPlayer), new Color(0, 0, 1)), // 212

                    new FieldTypeStruct(FieldType.ForceMode, typeof(ForceMode), new Color(0, 0, 1)), // 2000
                    new FieldTypeStruct(FieldType.Type, typeof(FieldType), new Color(0, 0, 1)), // 2001
                    new FieldTypeStruct(FieldType.Key, typeof(KeyCode), new Color(0, 0, 1)), // 2002
                    new FieldTypeStruct(FieldType.Interpolation, typeof(RigidbodyInterpolation), new Color(0, 0, 1)), // 2003
                    new FieldTypeStruct(FieldType.DetectionMode, typeof(CollisionDetectionMode), new Color(0, 0, 1)), // 2004
                    new FieldTypeStruct(FieldType.CursorMode, typeof(CursorMode), new Color(0, 0, 1)), // 2005
                    new FieldTypeStruct(FieldType.CursorLockMode, typeof(CursorLockMode), new Color(0, 0, 1)), // 2006
                    new FieldTypeStruct(FieldType.EnvironemntPath, typeof(Environment.SpecialFolder), new Color(0, 0, 1)), // 2007
                    new FieldTypeStruct(FieldType.AvatarIKGoal, typeof(AvatarIKGoal), new Color(0, 0, 1)), // 2008
                    new FieldTypeStruct(FieldType.LightType, typeof(LightType), new Color(0, 0, 1)), // 2009
                    new FieldTypeStruct(FieldType.ShadowType, typeof(LightShadows), new Color(0, 0, 1)), // 2010
                    new FieldTypeStruct(FieldType.InstallMode, typeof(ApplicationInstallMode), new Color(0, 0, 1)), // 2011
                    new FieldTypeStruct(FieldType.Platform, typeof(RuntimePlatform), new Color(0, 0, 1)), // 2012
                    new FieldTypeStruct(FieldType.FullScreenMode, typeof(FullScreenMode), new Color(0, 0, 1)), // 2013
                    new FieldTypeStruct(FieldType.ScreenOrientation, typeof(ScreenOrientation), new Color(0, 0, 1)), // 2014
                    new FieldTypeStruct(FieldType.CollisionFlag, typeof(CollisionFlags), new Color(0, 0, 1)), // 2015
                    new FieldTypeStruct(FieldType.VideoAspectRatio, typeof(VideoAspectRatio), new Color(0, 0, 1)), // 2016
                    new FieldTypeStruct(FieldType.VideoRenderMode, typeof(VideoRenderMode), new Color(0, 0, 1)), // 2017
                    new FieldTypeStruct(FieldType.VideoSource, typeof(VideoSource), new Color(0, 0, 1)), // 2018
                    new FieldTypeStruct(FieldType.VideoAudioOutputMode, typeof(VideoAudioOutputMode), new Color(0, 0, 1)), // 2019
                };
                return buffer;
            }
        }

        public static object GetGO(GenericObject go, FieldType type)
        {
            switch (type)
            {
                case FieldType.Event: return null; // 0
                case FieldType.Button: return null; // 1
                case FieldType.Object: return null; // 2
                case FieldType.Dropdown: return null; // 3
                case FieldType.Component: return go.genericBasicType.target_Int; // 4
                case FieldType.Variable: return go.genericBasicType.target_Int; // 5

                case FieldType.Int: return go.genericBasicType.target_Int; // 10
                case FieldType.Long: return go.genericBasicType.target_Long; // 11
                case FieldType.Float: return go.genericBasicType.target_Float; // 12
                case FieldType.Double: return go.genericBasicType.target_Double; // 13
                case FieldType.String: return go.genericBasicType.target_String; // 14
                case FieldType.Boolean: return go.genericBasicType.target_Boolean; // 15
                case FieldType.Number: return go.genericBasicType.target_Int; // 16
                case FieldType.Vector: return go.genericBasicType.target_Int; // 17
                case FieldType.Char: return go.genericBasicType.target_Char; // 18

                case FieldType.GameObject: return go.genericUnityType.target_GameObject; // 50
                case FieldType.Transform: return go.genericUnityType.target_Transform; // 51
                case FieldType.Vector2: return go.genericUnityType.target_Vector2; // 52
                case FieldType.Vector3: return go.genericUnityType.target_Vector3; // 53
                case FieldType.Vector4: return go.genericUnityType.target_Vector4; // 54
                case FieldType.Rect: return go.genericUnityType.target_Rect; // 55
                case FieldType.Color: return go.genericUnityType.target_Color; // 56
                case FieldType.Texture: return go.genericUnityType.target_Texture; // 57
                case FieldType.Texture2D: return go.genericUnityType.target_Texture2D; // 58
                case FieldType.Texture3D: return go.genericUnityType.target_Texture3D; // 59
                case FieldType.Material: return go.genericUnityType.target_Material; // 60
                case FieldType.Quaternion: return go.genericUnityType.target_Quaternion; // 61
                case FieldType.AudioClip: return go.genericUnityType.target_AudioClip; // 62
                case FieldType.AnimatorStateInfo: return go.genericUnityType.target_AnimatorStateInfo; // 63
                case FieldType.AnimatorClipInfo: return go.genericUnityType.target_AnimatorClipInfo; // 64
                case FieldType.Blueprint: return go.genericUnityType.target_Blueprint; // 65
                case FieldType.GameData: return go.genericUnityType.target_GameData; // 66
                case FieldType.AudioMixer: return go.genericUnityType.target_AudioMixer; // 67
                case FieldType.Touch: return go.genericUnityType.target_Touch; // 68
                case FieldType.Ray: return go.genericUnityType.target_Ray; // 69
                case FieldType.Mesh: return go.genericUnityType.target_Mesh; // 70
                case FieldType.Flare: return go.genericUnityType.target_Flare; // 71
                case FieldType.Matrix4x4: return go.genericUnityType.target_Matrix4X4; // 72
                case FieldType.Plane: return go.genericUnityType.target_Plane; // 73
                case FieldType.Bounds: return go.genericUnityType.target_Bounts; // 74
                case FieldType.Clip: return go.genericUnityType.target_VideoClip; // 75
                case FieldType.Renderer: return go.genericUnityType.target_Renderer; // 76
                case FieldType.RenderTexture: return go.genericUnityType.target_RenderTexture; // 77
                case FieldType.Collision: return go.genericUnityType.target_Collision; // 78
                case FieldType.Collision2D: return go.genericUnityType.target_Collision2D; // 79

                case FieldType.Rigidbody: return go.target_Component.rigidbody; // 200
                case FieldType.Rigidbody2D: return go.target_Component.rigidbody2D; // 201
                case FieldType.Light: return go.target_Component.light; // 202
                case FieldType.Camera: return go.target_Component.camera; //203
                case FieldType.Collider: return go.target_Component.collider; // 204
                case FieldType.Collider2D: return go.target_Component.collider2D; // 205
                case FieldType.MeshFilter: return go.target_Component.meshFilter; // 206
                case FieldType.MeshRenderer: return go.target_Component.meshRenderer; // 207
                case FieldType.Animator: return go.target_Component.animator; // 208
                case FieldType.NodeComponent: return go.target_Component.nodeComponent; // 209
                case FieldType.AudioSource: return go.target_Component.audioSource; // 210
                case FieldType.Character: return go.target_Component.characterController; // 211
                case FieldType.VideoPlayer: return go.target_Component.videoPlayer; // 212

                case FieldType.ForceMode: return (ForceMode)go.genericBasicType.target_Int; // 2000
                case FieldType.Type: return (FieldType)go.genericBasicType.target_Int; // 2001
                case FieldType.Key: return (KeyCode)go.genericBasicType.target_Int; // 2004
                case FieldType.Interpolation: return (RigidbodyInterpolation)go.genericBasicType.target_Int; // 2005
                case FieldType.DetectionMode: return (CollisionDetectionMode)go.genericBasicType.target_Int; // 2006
                case FieldType.CursorMode: return (CursorMode)go.genericBasicType.target_Int; // 2007
                case FieldType.CursorLockMode: return (CursorLockMode)go.genericBasicType.target_Int; // 2008
                case FieldType.EnvironemntPath: return (Environment.SpecialFolder)go.genericBasicType.target_Int; // 2009
                case FieldType.AvatarIKGoal: return (AvatarIKGoal)go.genericBasicType.target_Int; // 2010
                case FieldType.LightType: return (LightType)go.genericBasicType.target_Int; // 2011
                case FieldType.ShadowType: return (LightShadows)go.genericBasicType.target_Int; // 2012
                case FieldType.InstallMode: return (ApplicationInstallMode)go.genericBasicType.target_Int; // 2013
                case FieldType.Platform: return (RuntimePlatform)go.genericBasicType.target_Int; // 2014
                case FieldType.FullScreenMode: return (FullScreenMode)go.genericBasicType.target_Int; // 2015
                case FieldType.ScreenOrientation: return (ScreenOrientation)go.genericBasicType.target_Int; // 2016
                case FieldType.CollisionFlag: return (CollisionFlags)go.genericBasicType.target_Int; // 2017
                case FieldType.VideoAspectRatio: return (VideoAspectRatio)go.genericBasicType.target_Int; // 2018
                case FieldType.VideoRenderMode: return (VideoRenderMode)go.genericBasicType.target_Int; // 2019
                case FieldType.VideoSource: return (VideoSource)go.genericBasicType.target_Int; // 2020
                case FieldType.VideoAudioOutputMode: return (VideoAudioOutputMode)go.genericBasicType.target_Int; // 2021
            }
            return null;
        }

        public static void SetGO(GenericObject go, FieldType type, object o)
        {
            switch (type)
            {
                case FieldType.Event: break; // 0
                case FieldType.Button: break; // 1
                case FieldType.Object: break; // 2
                case FieldType.Dropdown: break; // 3
                case FieldType.Component: go.genericBasicType.target_Int = (int)o; break; // 4
                case FieldType.Variable: go.genericBasicType.target_Int = (int)o; break; // 5

                case FieldType.Int: go.genericBasicType.target_Int = (int)o; break; // 10
                case FieldType.Long: go.genericBasicType.target_Long = (long)o; break; // 11
                case FieldType.Float: go.genericBasicType.target_Float = (float)o; break; // 12
                case FieldType.Double: go.genericBasicType.target_Double = (double)o; break; // 13
                case FieldType.String: go.genericBasicType.target_String = (string)o; break; // 14
                case FieldType.Boolean: go.genericBasicType.target_Boolean = (bool)o; break; // 15
                case FieldType.Number: go.genericBasicType.target_Int = (int)o; break; // 16
                case FieldType.Vector: go.genericBasicType.target_Int = (int)o; break; // 17
                case FieldType.Char: go.genericBasicType.target_Char = (char)o; break; // 18

                case FieldType.GameObject: go.genericUnityType.target_GameObject = (GameObject)o; break; // 50
                case FieldType.Transform: go.genericUnityType.target_Transform = (Transform)o; break; // 51
                case FieldType.Vector2: go.genericUnityType.target_Vector2 = (Vector2)o; break; // 52
                case FieldType.Vector3: go.genericUnityType.target_Vector3 = (Vector3)o; break; // 53
                case FieldType.Vector4: go.genericUnityType.target_Vector4 = (Vector4)o; break; // 54
                case FieldType.Rect: go.genericUnityType.target_Rect = (Rect)o; break; // 55
                case FieldType.Color: go.genericUnityType.target_Color = (Color)o; break; // 56
                case FieldType.Texture: go.genericUnityType.target_Texture = (Texture)o; break; // 57
                case FieldType.Texture2D: go.genericUnityType.target_Texture2D = (Texture2D)o; break; // 58
                case FieldType.Texture3D: go.genericUnityType.target_Texture3D = (Texture3D)o; break; // 59
                case FieldType.Material: go.genericUnityType.target_Material = (Material)o; break; // 60
                case FieldType.Quaternion: go.genericUnityType.target_Quaternion = (Quaternion)o; break; // 61
                case FieldType.AudioClip: go.genericUnityType.target_AudioClip = (AudioClip)o; break; // 62
                case FieldType.AnimatorStateInfo: go.genericUnityType.target_AnimatorStateInfo = (AnimatorStateInfo)o; break; // 63
                case FieldType.AnimatorClipInfo: go.genericUnityType.target_AnimatorClipInfo = (AnimatorClipInfo)o; break; // 64
                case FieldType.Blueprint: go.genericUnityType.target_Blueprint = (EBlueprint)o; break; // 65
                case FieldType.GameData: go.genericUnityType.target_GameData = (EGameData)o; break; // 66
                case FieldType.AudioMixer: go.genericUnityType.target_AudioMixer = (AudioMixer)o; break; // 67
                case FieldType.Touch: go.genericUnityType.target_Touch = (Touch)o; break; // 68
                case FieldType.Ray: go.genericUnityType.target_Ray = (Ray)o; break; // 69
                case FieldType.Mesh: go.genericUnityType.target_Mesh = (Mesh)o; break; // 70
                case FieldType.Flare: go.genericUnityType.target_Flare = (Flare)o; break; // 71
                case FieldType.Matrix4x4: go.genericUnityType.target_Matrix4X4 = (Matrix4x4)o; break; // 72
                case FieldType.Plane: go.genericUnityType.target_Plane = (Plane)o; break; // 73
                case FieldType.Bounds: go.genericUnityType.target_Bounts = (Bounds)o; break; // 74
                case FieldType.Clip: go.genericUnityType.target_VideoClip = (VideoClip)o; break; // 75
                case FieldType.Renderer: go.genericUnityType.target_Renderer = (Renderer)o; break; // 76
                case FieldType.RenderTexture: go.genericUnityType.target_RenderTexture = (RenderTexture)o; break; // 77
                case FieldType.Collision: go.genericUnityType.target_Collision = (Collision)o; break; // 78
                case FieldType.Collision2D: go.genericUnityType.target_Collision2D = (Collision2D)o; break; // 79

                case FieldType.Rigidbody: go.target_Component.rigidbody = (Rigidbody)o; break; // 200
                case FieldType.Rigidbody2D: go.target_Component.rigidbody2D = (Rigidbody2D)o; break; // 201
                case FieldType.Light: go.target_Component.light = (Light)o; break; // 202
                case FieldType.Camera: go.target_Component.camera = (Camera)o; break; //203
                case FieldType.Collider: go.target_Component.collider = (Collider)o; break; // 204
                case FieldType.Collider2D: go.target_Component.collider2D = (Collider2D)o; break; // 205
                case FieldType.MeshFilter: go.target_Component.meshFilter = (MeshFilter)o; break; // 206
                case FieldType.MeshRenderer: go.target_Component.meshRenderer = (MeshRenderer)o; break; // 207
                case FieldType.Animator: go.target_Component.animator = (Animator)o; break; // 208
                case FieldType.NodeComponent: go.target_Component.nodeComponent = (ENodeComponent)o; break; // 209
                case FieldType.AudioSource: go.target_Component.audioSource = (AudioSource)o; break; // 210
                case FieldType.Character: go.target_Component.characterController = (CharacterController)o; break; // 211
                case FieldType.VideoPlayer: go.target_Component.videoPlayer = (VideoPlayer)o; break; // 212

                case FieldType.ForceMode: go.genericBasicType.target_Int = (int)(ForceMode)o; break; // 2000
                case FieldType.Type: go.genericBasicType.target_Int = (int)(FieldType)o; break; // 2001
                case FieldType.Key: go.genericBasicType.target_Int = (int)(KeyCode)o; break; // 2004
                case FieldType.Interpolation: go.genericBasicType.target_Int = (int)(RigidbodyInterpolation)o; break; // 2005
                case FieldType.DetectionMode: go.genericBasicType.target_Int = (int)(CollisionDetectionMode)o; break; // 2006
                case FieldType.CursorMode: go.genericBasicType.target_Int = (int)(CursorMode)o; break; // 2007
                case FieldType.CursorLockMode: go.genericBasicType.target_Int = (int)(CursorLockMode)o; break; // 2008
                case FieldType.EnvironemntPath: go.genericBasicType.target_Int = (int)(Environment.SpecialFolder)o; break; // 2009
                case FieldType.AvatarIKGoal: go.genericBasicType.target_Int = (int)(AvatarIKGoal)o; break; // 2010
                case FieldType.LightType: go.genericBasicType.target_Int = (int)(LightType)o; break; // 2011
                case FieldType.ShadowType: go.genericBasicType.target_Int = (int)(LightShadows)o; break; // 2012
                case FieldType.InstallMode: go.genericBasicType.target_Int = (int)(ApplicationInstallMode)o; break; // 2013
                case FieldType.Platform: go.genericBasicType.target_Int = (int)(RuntimePlatform)o; break; // 2014
                case FieldType.FullScreenMode: go.genericBasicType.target_Int = (int)(FullScreenMode)o; break; // 2015
                case FieldType.ScreenOrientation: go.genericBasicType.target_Int = (int)(ScreenOrientation)o; break; // 2016
                case FieldType.CollisionFlag: go.genericBasicType.target_Int = (int)(CollisionFlags)o; break; // 2017
                case FieldType.VideoAspectRatio: go.genericBasicType.target_Int = (int)(VideoAspectRatio)o; break; // 2018
                case FieldType.VideoRenderMode: go.genericBasicType.target_Int = (int)(VideoRenderMode)o; break; // 2019
                case FieldType.VideoSource: go.genericBasicType.target_Int = (int)(VideoSource)o; break; // 2020
                case FieldType.VideoAudioOutputMode: go.genericBasicType.target_Int = (int)(VideoAudioOutputMode)o; break; // 2021
            }
        }
    }

    public partial class Field
    {
#if UNITY_EDITOR
        public static GenericObject DrawFieldHelper(GenericObject o, FieldType type)
        {
            if ((int)type >= 2000)
            {
                o.genericBasicType.target_Int = GUIEnumField(o.genericBasicType.target_Int, FormExistEnumStruct(FieldTypeStruct.GetStruct(type).Item2));
                return o;
            }

            switch (type)
            {
                case FieldType.Event: break; // 0
                case FieldType.Button: break; // 1
                case FieldType.Object: break; // 2
                case FieldType.Dropdown: break; // 3
                case FieldType.Component: break; // 4
                case FieldType.Variable: break; // 5

                case FieldType.Int: o.genericBasicType.target_Int = EditorGUILayout.IntField(o.genericBasicType.target_Int); break; // 10
                case FieldType.Long: o.genericBasicType.target_Long = EditorGUILayout.LongField(o.genericBasicType.target_Long); break; // 11
                case FieldType.Float: o.genericBasicType.target_Float = EditorGUILayout.FloatField(o.genericBasicType.target_Float); break; // 12
                case FieldType.Double: o.genericBasicType.target_Double = EditorGUILayout.DoubleField(o.genericBasicType.target_Double); break; // 13
                case FieldType.String: o.genericBasicType.target_String = EditorGUILayout.TextField(o.genericBasicType.target_String); break; // 14
                case FieldType.Boolean: o.genericBasicType.target_Boolean = EditorGUILayout.Toggle(o.genericBasicType.target_Boolean); break; // 15
                case FieldType.Number: break; // 16
                case FieldType.Vector: break; // 17
                case FieldType.Char: break; // 18

                case FieldType.GameObject: o.genericUnityType.target_GameObject = EditorObjectField<GameObject>(o.genericUnityType.target_GameObject); break; // 50
                case FieldType.Transform: o.genericUnityType.target_Transform = EditorObjectField<Transform>(o.genericUnityType.target_Transform); break; // 51
                case FieldType.Vector2: o.genericUnityType.target_Vector2 = EditorGUILayout.Vector2Field("", o.genericUnityType.target_Vector2); break; // 52
                case FieldType.Vector3: o.genericUnityType.target_Vector3 = EditorGUILayout.Vector3Field("", o.genericUnityType.target_Vector3); break; // 53
                case FieldType.Vector4: o.genericUnityType.target_Vector4 = EditorGUILayout.Vector4Field("", o.genericUnityType.target_Vector4); break; // 54
                case FieldType.Rect:
                    {
                        Vector4 r = new Vector4(o.genericUnityType.target_Rect.x, o.genericUnityType.target_Rect.y, o.genericUnityType.target_Rect.width, o.genericUnityType.target_Rect.height);
                        r = EditorGUILayout.Vector4Field("", r);
                        o.genericUnityType.target_Rect = new Rect(r.x, r.y, r.z, r.w);
                        break;
                    } // 55
                case FieldType.Color: o.genericUnityType.target_Color = EditorGUILayout.ColorField(o.genericUnityType.target_Color); break; // 56
                case FieldType.Texture: o.genericUnityType.target_Texture = EditorObjectField<Texture>(o.genericUnityType.target_Texture); break; // 57
                case FieldType.Texture2D: o.genericUnityType.target_Texture2D = EditorObjectField<Texture2D>(o.genericUnityType.target_Texture2D); break; // 58
                case FieldType.Texture3D: o.genericUnityType.target_Texture3D = EditorObjectField<Texture3D>(o.genericUnityType.target_Texture3D); break; // 59
                case FieldType.Material: o.genericUnityType.target_Material = EditorObjectField<Material>(o.genericUnityType.target_Material); break; // 60
                case FieldType.Quaternion:
                    {
                        Vector4 r = new Vector4(o.genericUnityType.target_Quaternion.x, o.genericUnityType.target_Quaternion.y, o.genericUnityType.target_Quaternion.z, o.genericUnityType.target_Quaternion.w);
                        r = EditorGUILayout.Vector4Field("", r);
                        o.genericUnityType.target_Quaternion = new Quaternion(r.x, r.y, r.z, r.w);
                        break;
                    } // 61
                case FieldType.AudioClip: o.genericUnityType.target_AudioClip = EditorObjectField<AudioClip>(o.genericUnityType.target_AudioClip); break; // 60
                case FieldType.AnimatorStateInfo: break; // 61
                case FieldType.AnimatorClipInfo: break; // 62
                case FieldType.Blueprint: o.genericUnityType.target_Blueprint = EditorObjectField<EBlueprint>(o.genericUnityType.target_Blueprint); break; // 65
                case FieldType.GameData: o.genericUnityType.target_GameData = EditorObjectField<EGameData>(o.genericUnityType.target_GameData); break; // 66
                case FieldType.AudioMixer: o.genericUnityType.target_AudioMixer = EditorObjectField<AudioMixer>(o.genericUnityType.target_AudioMixer); break; // 67
                case FieldType.Touch: break; // 68
                case FieldType.Ray: break; // 69
                case FieldType.Mesh: o.genericUnityType.target_Mesh = EditorObjectField<Mesh>(o.genericUnityType.target_Mesh); break; // 70
                case FieldType.Flare: o.genericUnityType.target_Flare = EditorObjectField<Flare>(o.genericUnityType.target_Flare); break; // 71
                case FieldType.Matrix4x4: break; // 72
                case FieldType.Plane: break; // 73
                case FieldType.Bounds: break; // 74
                case FieldType.Clip: o.genericUnityType.target_VideoClip = EditorObjectField<VideoClip>(o.genericUnityType.target_VideoClip); break; // 75
                case FieldType.Renderer: o.genericUnityType.target_Renderer = EditorObjectField<Renderer>(o.genericUnityType.target_Renderer); break; // 76
                case FieldType.RenderTexture: o.genericUnityType.target_RenderTexture = EditorObjectField<RenderTexture>(o.genericUnityType.target_RenderTexture); break; // 77
                case FieldType.Collision: EditorGUILayout.LabelField(type.ToString()); break; // 78
                case FieldType.Collision2D: EditorGUILayout.LabelField(type.ToString()); break; // 79
                case FieldType.Cubemap: o.genericUnityType.target_Cubemap = EditorObjectField<Cubemap>(o.genericUnityType.target_Cubemap); break; // 80

                case FieldType.Rigidbody: o.target_Component.rigidbody = EditorObjectField<Rigidbody>(o.target_Component.rigidbody); break; // 200
                case FieldType.Rigidbody2D: o.target_Component.rigidbody2D = EditorObjectField<Rigidbody2D>(o.target_Component.rigidbody2D); break; // 201
                
                case FieldType.Collider: o.target_Component.collider = EditorObjectField<Collider>(o.target_Component.collider); break; // 204
                case FieldType.Collider2D: o.target_Component.collider2D = EditorObjectField<Collider2D>(o.target_Component.collider2D); break; // 205
                case FieldType.MeshFilter: o.target_Component.meshFilter = EditorObjectField<MeshFilter>(o.target_Component.meshFilter); break; // 206
                case FieldType.MeshRenderer: o.target_Component.meshRenderer = EditorObjectField<MeshRenderer>(o.target_Component.meshRenderer); break; // 207
                
                case FieldType.Light:
                    break;
                case FieldType.Camera:
                    break;
                case FieldType.Animator:
                    break;
                case FieldType.NodeComponent:
                    break;
                case FieldType.AudioSource:
                    break;
                case FieldType.Character:
                    break;
                case FieldType.VideoPlayer:
                    break;
                case FieldType.ForceMode:
                    break;
                case FieldType.Type:
                    break;
                case FieldType.Key:
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
                case FieldType.InstallMode:
                    break;
                case FieldType.Platform:
                    break;
                case FieldType.FullScreenMode:
                    break;
                case FieldType.ScreenOrientation:
                    break;
                case FieldType.CollisionFlag:
                    break;
                case FieldType.VideoAspectRatio:
                    break;
                case FieldType.VideoRenderMode:
                    break;
                case FieldType.VideoSource:
                    break;
                case FieldType.VideoAudioOutputMode:
                    break;
                case FieldType.CameraType:
                    break;
                case FieldType.CameraClearFlags:
                    break;
            }
            return o;
        }
#endif
        /// <summary>
        /// You can define connection color by field type
        /// </summary>
        /// <param name="fieldType">Type</param>
        /// <param name="alpha">Color Alpha</param>
        /// <returns>Color Match The Field Type</returns>
        public static Color GetColorByFieldType(FieldType fieldType, float alpha)
        {
            FieldTypeStruct target = FieldTypeStruct.GetStruct(fieldType);
            if (target == null)
            {
                Debug.LogWarning("You didn't implement " + fieldType.ToString() + " to field type struct");
                Color c = Color.white;
                return new Color(c.r, c.g, c.b, alpha);
            }
            else
            {
                Color c = target.Item3;
                return new Color(c.r, c.g, c.b, alpha);
            }
            
        }

        /// <summary>
        /// Get type by field type
        /// </summary>
        /// <param name="tf">Type</param>
        /// <returns></returns>
        public static Type GetTypeByFieldType(FieldType tf)
        {
            return FieldTypeStruct.GetStruct(tf).Item2;
        }

        /// <summary>
        /// Get object by field type <br />
        /// </summary>
        /// <param name="tf">Field Type</param>
        /// <param name="go">Field Data</param>
        /// <returns></returns>
        public static object GetObjectByFieldType(FieldType tf, GenericObject go)
        {
            return FieldTypeStruct.GetGO(go, tf);
        }

        public static object[] GetObjectArrayByFieldType(FieldType tf, GenericObject[] go)
        {
            object[] result = new object[go.Length];
            for (int i = 0; i < go.Length; i++)
            {
                result[i] = GetObjectByFieldType(tf, go[i]);
            }
            return result;
        }

        public static object SetObjectByFieldType(FieldType tf, GenericObject go, object o)
        {
            FieldTypeStruct.SetGO(go, tf, o);
            return o;
        }

        public static GenericObject[] SetObjectArrayByField(FieldType tf, GenericObject[] go, object[] o)
        {
            if (go.Length == o.Length)
            {
                /* Just apply the value */
                for (int i = 0; i < go.Length; i++)
                {
                    SetObjectByFieldType(tf, go[i], o[i]);
                }
            }
            else if (go.Length > o.Length)
            {
                /* Go remove element */
                int diff = go.Length - o.Length;
                List<GenericObject> buffer = go.ToList();

                for (int i = 0; i < diff; i++)
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

                for (int i = 0; i < diff; i++)
                {
                    GenericObject bufferGO = new GenericObject();
                    SetObjectByFieldType(tf, bufferGO, o[go.Length + i]);
                    buffer.Add(bufferGO);
                }
                go = buffer.ToArray();
            }
            return go;
        }
    }
}
