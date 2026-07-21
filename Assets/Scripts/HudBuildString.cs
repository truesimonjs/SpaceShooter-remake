using System;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
//using UnityEditor.SceneManagement;
[ExecuteInEditMode]
public class HudBuildString : MonoBehaviour
{

    private void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying) return;
        DateTime dt = DateTime.Now;
        string dateString = $"build date: {dt.Day}-{dt.Month}-{dt.Year}-{dt.Hour}:{dt.Minute}";
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (text.text != dateString && SceneManager.GetActiveScene().isDirty)
        {
            text.text = dateString;
            PrefabUtility.RecordPrefabInstancePropertyModifications(text);
            //EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

        }
#endif
    }



}
