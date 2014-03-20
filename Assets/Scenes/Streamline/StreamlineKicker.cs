using UnityEngine;
using System.Collections;

public class StreamlineKicker : MonoBehaviour
{
    public float impulse = 20.0f;
    public float randomize = 0.1f;

    int counter;

    GameObject PickUpTarget ()
    {
        var candidates = GameObject.FindGameObjectsWithTag ("WantsOrder");
        if (candidates.Length > 0)
            return candidates [counter++ % candidates.Length];
        else
            return null;
    }

    Vector3 MakeVector ()
    {
        return Vector3.Lerp (transform.forward, Random.onUnitSphere, randomize) * impulse;
    }

    void OnReaktorTrigger ()
    {
        var target = PickUpTarget ();
        if (target && target.rigidbody)
            target.rigidbody.AddForce (MakeVector (), ForceMode.Impulse);
    }
}
