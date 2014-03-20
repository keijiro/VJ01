using UnityEngine;
using System.Collections;

public class StreamlineThruster : MonoBehaviour
{
    public float force = 2.0f;
    public Shaker noise;

    StreamlineManager manager;
    Transform target;
    Vector3 offsetVector;

    void Start ()
    {
        manager = FindObjectOfType<StreamlineManager> ();
        target = GameObject.Find ("Target").transform;
        offsetVector = Random.insideUnitSphere;
        offsetVector.x = 0;
    }

    void FixedUpdate ()
    {
        noise.amount = manager.knobDivergence.Current;
        noise.Update (Time.fixedDeltaTime);

        var axis = -Physics.gravity.normalized;
        var targetPosition = target.position + offsetVector * manager.knobOffset.Current;
        var myPosition = transform.position;

        if (Vector3.Dot (myPosition, axis) < Vector3.Dot (targetPosition, axis))
        {
            var vector = targetPosition - myPosition + noise.Position;
            rigidbody.AddForce (vector * force, ForceMode.Force);
        }
    }
}
