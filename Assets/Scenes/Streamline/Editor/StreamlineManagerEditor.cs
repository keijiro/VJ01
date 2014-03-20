using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(StreamlineManager))]
public class StreamlineManagerEditor : Editor
{
    override public void OnInspectorGUI ()
    {
        var manager = target as StreamlineManager;
        
        manager.showProperties = EditorGUILayout.Foldout (manager.showProperties, "All Properties");
        if (manager.showProperties) DrawDefaultInspector ();
        
        EditorGUILayout.Space ();

        KnobValueEditor.Slider (manager.knobTurbulence);
        KnobValueEditor.Slider (manager.knobDivergence);

        EditorGUILayout.Space ();

        KnobValueEditor.Slider (manager.knobOffset);
        KnobValueEditor.Slider (manager.knobForward);
        
        EditorGUILayout.Space ();

        KnobValueEditor.Slider (manager.knobParticle);
        KnobValueEditor.Slider (manager.knobKicker);

        EditorGUILayout.Space ();

        KnobValueEditor.Slider (manager.knobYaw);
        KnobValueEditor.Slider (manager.knobColor);

        EditorGUILayout.Space ();
        
        LaunchpadEditor.Buttons (manager, manager.launchpad);
    }
}