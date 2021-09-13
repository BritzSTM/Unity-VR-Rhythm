using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneLoadType))]
public class SceneLoadTypeEditor : PropertyDrawer
{
    private readonly Color _warningColor = Color.red;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 빌드된 씬 이름들을 가져온다
        // 만약 씬 이름들 중에 현재 선택된 씬이 없으면 추가하고 경고한다.
        var sceneNames = GetEnableSceneNamesInBuildList();
        var sceneProp = property.FindPropertyRelative("Scene");

        var selectedName = sceneProp.stringValue;
        bool notIncludeName = (sceneNames.Find(x => x == selectedName) == null);
        if(notIncludeName)
        {
            Debug.LogWarning($"Not include selected scene : {selectedName}");
            sceneNames.Add(selectedName);
        }
        
        int popupIndex = sceneNames.IndexOf(selectedName);
        EditorGUI.BeginProperty(position, label, property);

        if (notIncludeName)
            EditorGUI.DrawRect(position, _warningColor);

        var propPos = position;
        propPos.height = EditorGUIUtility.singleLineHeight;

        popupIndex = EditorGUI.Popup(propPos, "Scene", popupIndex, sceneNames.ToArray());
        sceneProp.stringValue = sceneNames[popupIndex];

        propPos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        var selectedLoadMode = property.FindPropertyRelative("LoadMode");
        EditorGUI.PropertyField(propPos, selectedLoadMode);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = EditorGUIUtility.singleLineHeight * 2;
        height += EditorGUIUtility.standardVerticalSpacing * 3;

        return height;
    }

    public static List<string> GetEnableSceneNamesInBuildList()
    {
        var enableScenes = EditorBuildSettings.scenes.Where(x => x.enabled == true);

        var sceneNames = new List<string>();
        foreach(var enableScene in enableScenes)
        {
            var loaded = AssetDatabase.LoadAssetAtPath<SceneAsset>(enableScene.path);
            if (loaded != null)
                sceneNames.Add(loaded.name);
        }

        return sceneNames;
    }
}