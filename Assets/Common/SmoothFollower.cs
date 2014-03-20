using UnityEngine;
using System.Collections;

public class SmoothFollower : MonoBehaviour
{
    public Transform target;
    public bool killVerticalMove;
    [Range(0, 1)]
    public float sensitivity = 0.5f;

    void Update ()
    {
        var position = target.position;
        if (killVerticalMove)
            position.y = transform.position.y;

        var coeff = sensitivity * -9.9f - 0.1f;

        transform.position = ExpEase.Out (transform.position, position, coeff);
        transform.rotation = ExpEase.Out (transform.rotation, target.rotation, coeff);
    }
}
