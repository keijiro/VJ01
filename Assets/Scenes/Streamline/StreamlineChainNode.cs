using UnityEngine;
using System.Collections;

public class StreamlineChainNode : MonoBehaviour
{
    public float initialScale = 1.0f;
    public float scaleAmount = 0.2f;
    public float minSpeed = 1.0f;
    public float maxSpeed = 15.0f;
    public float interval = 1.2f;

    static int counter;

    Reaktor reaktor;
    float time;

    void Start ()
    {
        reaktor = Reaktor.SearchAvailableFrom (gameObject);
        time = counter++ * -interval;
    }

    void Update ()
    {
        time += Time.deltaTime * Mathf.Lerp (minSpeed, maxSpeed, reaktor.Output);
        var scale = initialScale + Mathf.Sin (time) * scaleAmount * reaktor.Output;
        transform.localScale = Vector3.one * scale;
    }
}
