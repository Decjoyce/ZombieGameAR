using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugTextDisplayer : MonoBehaviour
{
    public static DebugTextDisplayer instance;
    TextMeshProUGUI text;
    private void Awake()
    {
        instance = this;
        text = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(string stuff)
    {
        text.text = stuff;
    }
}
