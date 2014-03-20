using UnityEngine;
using System.Collections;

public class AutoDestructor : MonoBehaviour
{
    public Bounds bounds = new Bounds (Vector3.zero, new Vector3 (100, 100, 100));

    void Update ()
    {
        if (!bounds.Contains (transform.position))
            Destroy (gameObject);
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube (bounds.center, bounds.size);
    }
}
