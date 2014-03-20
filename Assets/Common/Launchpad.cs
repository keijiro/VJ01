// Key-assignable button class.
// By Keijiro Takahashi, 2014
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Launchpad
{
    [System.Serializable]
    public class Pad
    {
        public string id = "-";
        public int noteNumber = 36;
        public string label;

        public string DisplayName {
            get {
                return (label != null && label.Length > 0) ? label : id;
            }
        }
    }

    public Pad[] pads;
    public int padsPerLine = 4;

    public void Update (MonoBehaviour owner)
    {
        if (pads != null)
            foreach (var pad in pads)
                if (MidiJack.GetKeyDown (pad.noteNumber))
                    Launch (owner, pad);
    }

    public void Launch (MonoBehaviour owner, Pad pad)
    {
        owner.SendMessage ("OnPadPressed", pad.id);
    }
}
