using UnityEngine;
using System.Collections;

public class ForceUseLightProbes : MonoBehaviour
{
    void Start ()
    {
        foreach (var r in GetComponents<Renderer>())
            r.useLightProbes = true;
    }
}