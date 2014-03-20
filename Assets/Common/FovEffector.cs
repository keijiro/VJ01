using UnityEngine;
using System.Collections;

public class FovEffector : MonoBehaviour
{
    public Shaker movement;

    [System.NonSerialized]
    public float baseValue;

    void Start ()
    {
        baseValue = camera.fieldOfView;
    }

    void Update ()
    {
        movement.Update (Time.deltaTime);
        camera.fieldOfView = baseValue + movement.Scalar;
    }
}
