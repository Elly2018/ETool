#if UNITY_EDITOR
using ETool.ANode;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    [InitializeOnLoad]
    public class EBlueprint_WindowChanged : UnityEditor.AssetModificationProcessor
    {
        static AssetDeleteResult OnWillDeleteAsset(string assetName, RemoveAssetOptions options)
        {
            EBlueprint target = AssetDatabase.LoadAssetAtPath<EBlueprint>(assetName);
            if(target != null)
            {
                BlueprintGotDelete(target);
            }

            return AssetDeleteResult.DidNotDelete;
        }

        static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
        {
            EBlueprint target = AssetDatabase.LoadAssetAtPath<EBlueprint>(sourcePath);
            if (target != null)
            {
                string Oldname = Path.GetFileName(sourcePath).Replace(".asset", "");
                string Newname = Path.GetFileName(destinationPath).Replace(".asset", "");

                if(Oldname != Newname)
                    BlueprintChangeName(Oldname, Newname);
            }

            return AssetMoveResult.DidNotMove;
        }


        /// <summary>
        /// It will change all blueprint of nodes that have relationship with this blueprint
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        static void BlueprintChangeName(string oldname, string newname)
        {
            /* Loop all blueprint */
            foreach (var i in EBlueprint.GetAllBlueprint)
            {
                foreach(var j in i.nodes)
                {
                    if(j.targetEventOrVar.Split('.')[0] == oldname)
                    {
                        j.targetEventOrVar = newname + "." + j.targetEventOrVar.Split('.')[1];
                    }
                }
            }
        }


        /// <summary>
        /// It will remove all the relationship of this blueprint
        /// </summary>
        /// <param name="target"></param>
        static void BlueprintGotDelete(EBlueprint target)
        {
            /* Loop all blueprint */
            foreach(var i in EBlueprint.GetAllBlueprint)
            {
                /* All the blueprint inherit should cut down the relationship */
                if(i.Inherit == target)
                {
                    i.Inherit = null;
                    i.Editor_InheritUpdate();
                }

                List<NodeBase> nb = new List<NodeBase>();

                /* Loop all nodes */
                foreach(var j in i.nodes)
                {
                    if(j.targetEventOrVar.Split('.')[0] == target.name)
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
    }
}
#endif