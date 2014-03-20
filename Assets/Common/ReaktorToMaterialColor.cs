using UnityEngine;
using System.Collections;

public class ReaktorToMaterialColor : MonoBehaviour
{
    public Gradient gradient;

    Reaktor reaktor;

    void Awake ()
    {
        reaktor = Reaktor.SearchAvailableFrom (gameObject);
    }
	
    void Update ()
    {
        renderer.material.color = gradient.Evaluate(reaktor.Output);
    }
}