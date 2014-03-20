// Knob-assignable value class.
// By Keijiro Takahashi, 2014

using UnityEngine;
using System.Collections;

[System.Serializable]
public class KnobValue
{
    public string label = "Knob";
    public int knobNumber = 0;
    public float minValue = 0.0f;
    public float maxValue = 1.0f;
    public float sensitivity = 0.5f;
    public float realValue = 0.0f;

    float filteredValue;
    float lastKnobValue;
    int lastFrameCount = -1;

    public float Current {
        get {
            Update ();
            return filteredValue;
        }
    }

    public void Update ()
    {
        if (lastFrameCount < 0)
        {
            filteredValue = realValue;
        }

        if (Time.frameCount != lastFrameCount)
        {
            var fromKnob = MidiJack.GetKnob (knobNumber);

            if (fromKnob != lastKnobValue)
            {
                realValue = Mathf.Lerp (minValue, maxValue, fromKnob);
                lastKnobValue = fromKnob;
            }

            if (sensitivity >= 1.0f)
                filteredValue = realValue;
            else
                filteredValue = ExpEase.Out (filteredValue, realValue, sensitivity * -9.9f - 0.1f);

            lastFrameCount = Time.frameCount;
        }
    }
}
