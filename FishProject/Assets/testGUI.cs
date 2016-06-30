using UnityEngine;
using System.Collections;

public class testGUI : MonoBehaviour
{
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 50, 20), "add fish")) { print("making"); }
    }
}