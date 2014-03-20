using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GlobeManager))]
public class GlobeManagerEditor : Editor
{
    override public void OnInspectorGUI ()
    {
        var manager = target as GlobeManager;
        
        manager.showProperties = EditorGUILayout.Foldout (manager.showProperties, "All Properties");
        if (manager.showProperties)
            DrawDefaultInspector ();
        
        EditorGUILayout.Space ();

        KnobValueEditor.Slider (manager.knobYaw);
        KnobValueEditor.Slider (manager.knobPitch);

        EditorGUILayout.Space ();

        EditorGUILayout.HelpBox ("Knob 5 - Audio Reaction Level", MessageType.None);

        KnobValueEditor.Slider (manager.knobTurbulence);
        KnobValueEditor.Slider (manager.knobAlignment);
        KnobValueEditor.Slider (manager.knobTwist);

        EditorGUILayout.Space ();

        LaunchpadEditor.Buttons (manager, manager.launchpad);
    }
}
