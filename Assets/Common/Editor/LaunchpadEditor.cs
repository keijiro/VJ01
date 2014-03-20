// Key-assignable button class.
// By Keijiro Takahashi, 2014

using UnityEngine;
using UnityEditor;
using System.Collections;

static public class LaunchpadEditor
{
    static public void Buttons (MonoBehaviour owner, Launchpad launchpad)
    {
        if (launchpad == null || launchpad.pads == null)
            return;

        var buttonOption = GUILayout.Width (64);

        for (var i = 0; i < launchpad.pads.Length; i++)
        {
            var pad = launchpad.pads [i];
            var column = i % launchpad.padsPerLine;

            if (column == 0)
                EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button (pad.DisplayName, buttonOption) && EditorApplication.isPlaying)
                launchpad.Launch (owner, pad);

            if (column == launchpad.padsPerLine - 1 || i == launchpad.pads.Length - 1)
                EditorGUILayout.EndHorizontal ();
        }
    }
}
