using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MakeBallObject
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Ball Object")]
    public static void Create()
    {
        BallObject asset = ScriptableObject.CreateInstance<BallObject>();
        AssetDatabase.CreateAsset(asset, "Assets/NewBallObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif
}
