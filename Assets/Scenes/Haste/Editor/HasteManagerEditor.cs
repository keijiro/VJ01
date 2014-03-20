using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(HasteManager))]
public class HasteManagerEditor : Editor
{
    override public void OnInspectorGUI ()
    {
        var manager = target as HasteManager;
        
        manager.showProperties = EditorGUILayout.Foldout (manager.showProperties, "All Properties");
        if (manager.showProperties) DrawDefaultInspector ();
        
        EditorGUILayout.Space ();
        
        KnobValueEditor.Slider (manager.knobSpeed);
        KnobValueEditor.Slider (manager.knobTwist);

        EditorGUILayout.Space ();

        KnobValueEditor.Slider (manager.knobFov);
        KnobValueEditor.Slider (manager.knobParticles);

        EditorGUILayout.Space ();

        KnobValueEditor.Slider (manager.knobBank);
        KnobValueEditor.Slider (manager.knobPitch);

        EditorGUILayout.Space ();

        KnobValueEditor.Slider (manager.knobTime);
    }
}