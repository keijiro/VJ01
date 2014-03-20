using UnityEngine;
using System.Collections;

public class ReaktorToKvantNormalDeformer : MonoBehaviour
{
    public float minSpeed = 0.2f;
    public float maxSpeed = 3.0f;
    public float minNoise = 0.01f;
    public float maxNoise = -0.1f;

    Reaktor reaktor;
    KvantNormalDeformer deformer;

    void Awake ()
    {
        reaktor = Reaktor.SearchAvailableFrom (gameObject);
        deformer = GetComponent<KvantNormalDeformer> ();
    }
	
    void Update ()
    {
        deformer.speed = Mathf.Lerp (minSpeed, maxSpeed, reaktor.Output);
        deformer.noise = Mathf.Lerp (minNoise, maxNoise, reaktor.Output);
    }
}
