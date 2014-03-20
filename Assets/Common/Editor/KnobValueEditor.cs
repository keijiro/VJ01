// Knob-assignable value class.
// By Keijiro Takahashi, 2014

using UnityEngine;
using UnityEditor;
using System.Collections;

public class KnobValueEditor
{
    static public void Slider (KnobValue instance)
    {
        instance.realValue = EditorGUILayout.Slider (instance.label, instance.realValue, instance.minValue, instance.maxValue);
    }
}
