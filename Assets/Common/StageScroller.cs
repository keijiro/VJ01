using UnityEngine;
using System.Collections;

public class StageScroller : MonoBehaviour
{
    public Vector3 direction = Vector3.right;
    public float scrollSpeed = 0.1f;
    public float wrapPoint = 10.0f;

    void Update ()
    {
        transform.position += direction * (scrollSpeed * Time.deltaTime);
        if (Vector3.Dot(transform.position, direction) > wrapPoint)
            transform.position -= direction * (wrapPoint * 2);
    }
}
