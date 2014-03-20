using UnityEngine;
using System.Collections;

public class ReplicatePrefabAlongParticle : MonoBehaviour
{
    public GameObject prefab;
    public AnimationCurve sizeAnimation = AnimationCurve.Linear(0, 0, 1, 1);

    ParticleSystem.Particle[] particles;
    GameObject[] pool;

    void Start ()
    {
        particles = new ParticleSystem.Particle[particleSystem.maxParticles];
        pool = new GameObject[particleSystem.maxParticles];
        for (var i = 0; i < particleSystem.maxParticles; i++)
            pool[i] = Instantiate (prefab) as GameObject;
    }
    
    void Update ()
    {
        var count = particleSystem.GetParticles (particles);

        for (var i = 0; i < count; i++)
        {
            var p = particles [i];
            var instance = pool [i];
            instance.transform.position = p.position;
            instance.transform.localRotation = Quaternion.AngleAxis (p.rotation, p.axisOfRotation);
            instance.transform.localScale = Vector3.one * (p.size * sizeAnimation.Evaluate (p.lifetime / p.startLifetime));
            instance.renderer.enabled = true;
        }

        for (var i = count; i < pool.Length; i++)
            pool [i].renderer.enabled = false;
    }
}
