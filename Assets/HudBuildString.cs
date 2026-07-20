using System;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
[ExecuteInEditMode]
public class HudBuildString : MonoBehaviour
{
    private void Update()
    {
        if (Application.isPlaying) return;
        DateTime dt = DateTime.Now;
        string dateString = $"build date: {dt.Day}-{dt.Month}-{dt.Year}-{dt.Hour}:{dt.Minute}";
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (text.text != dateString && SceneManager.GetActiveScene().isDirty)
        {
            
            text.text = dateString;
            EditorUtility.SetDirty(gameObject);
        }
    }


}
