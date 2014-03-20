using UnityEngine;
using System.Collections;

public class HasteRoadTile : MonoBehaviour
{
    public float destructionDistance = 10.0f;
    public float swing = 110.0f;
    public float noiseFrequency = 0.19f;
    public Vector3 noiseVelocity;

    Transform manager;
    Vector3 offset;
    Quaternion initialRotation;

    void Start ()
    {
        manager = GameObject.FindWithTag ("Manager").transform;
        initialRotation = transform.localRotation;
    }

    void Update ()
    {
        // Swinging
        var wave = Kvant.Noise ((transform.position + noiseVelocity * Time.time) * noiseFrequency);
        transform.rotation = initialRotation * Quaternion.AngleAxis (swing * wave, Vector3.right);

        // Destruction by distance
        float distance = manager.position.z - transform.position.z;
        if (distance > destructionDistance) Destroy (gameObject);
    }
}