using UnityEngine;
using System.Collections;

public class DirectionFollower : MonoBehaviour
{
    public LayerMask triggerLayer;
    public string triggerName;
    public float dampTime = 1.0f;

    Animator animator;

    void Awake ()
    {
        animator = GetComponent<Animator> ();
    }

    void Update ()
    {
        var trigger = PickUpTrigger ();
        if (trigger)
        {
            var dot = Vector3.Dot (trigger.transform.forward, transform.right);
            animator.SetFloat ("Direction", dot, dampTime, Time.deltaTime);
        }
    }

    Collider PickUpTrigger()
    {
        foreach (var collider in Physics.OverlapSphere (transform.position, 1.0f, triggerLayer.value))
            if (collider.name == triggerName)
                return collider;
        return null;
    }
}
