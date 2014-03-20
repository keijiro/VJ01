using UnityEngine;
using System.Collections;

public class ReaktorToKvantGenericDeformer : MonoBehaviour
{
    public float minSpeed = 0.2f;
    public float maxSpeed = 3.0f;
    public float minAmount = 0.01f;
    public float maxAmount = 1.0f;

    Reaktor reaktor;
    KvantGenericDeformer deformer;
    Vector3 initialAmount;

    void Awake ()
    {
        reaktor = Reaktor.SearchAvailableFrom (gameObject);
        deformer = GetComponent<KvantGenericDeformer> ();
        initialAmount = deformer.deformAmount;
    }
	
    void Update ()
    {
        deformer.noiseSpeed = Mathf.Lerp (minSpeed, maxSpeed, reaktor.Output);
        deformer.deformAmount = initialAmount * Mathf.Lerp (minAmount, maxAmount, reaktor.Output);
    }
}
