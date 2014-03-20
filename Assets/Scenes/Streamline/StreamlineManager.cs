using UnityEngine;
using System.Collections;

public class StreamlineManager : MonoBehaviour
{
    public bool showProperties;

    // Public properties
    public float gravity = 0.3f;
    public Vector3 spawnOffset;

    // Prefabs
    public GameObject prefabLine;
    public GameObject prefabChain;

    // External references
    public Transform target;
    public TransformShaker targetShaker;
    public StreamlineKicker kicker;
    public ParticleSystem particles;
    public Transform cameraPivot;

    // Controllers
    public KnobValue knobTurbulence;
    public KnobValue knobDivergence;
    public KnobValue knobOffset;
    public KnobValue knobForward;
    public KnobValue knobParticle;
    public KnobValue knobKicker;
    public KnobValue knobYaw;
    public KnobValue knobColor;
    public Launchpad launchpad;

    void Awake ()
    {
        Physics.gravity = Vector3.right * gravity;
    }

    void Update ()
    {
        launchpad.Update (this);

        // Turbulence
        targetShaker.position.amount = knobTurbulence.Current;

        // Forward
        target.localPosition = Vector3.right * -knobForward.Current;

        // Particle
        particles.emissionRate = knobParticle.Current;

        // Kicker
        kicker.impulse = knobKicker.Current;

        // Yaw
        cameraPivot.localRotation = Quaternion.AngleAxis (knobYaw.Current, Vector3.forward);

        // Color
        var mixer = GetComponent<PolishableProbeMixer> ();
        mixer.mix = knobColor.Current;
        mixer.intensity = 1.0f + knobColor.Current;
    }

    void OnPadPressed (string id)
    {
        if (id == "Line") Spawn (prefabLine);
        if (id == "Chain") Spawn (prefabChain);
        if (id == "Kill") Kill ();
    }

    void Spawn (GameObject prefab)
    {
        var random = new Vector3 (Random.Range (0.9f, 1.1f), Random.value < 0.5f ? 1 : -1, Random.Range (0.9f, 1.1f));
        Instantiate (prefab, target.position + Vector3.Scale (spawnOffset, random), prefabLine.transform.rotation);
    }

    void Kill ()
    {
        var candidates = FindObjectsOfType (typeof(StreamlineThruster));
        if (candidates.Length > 0) Destroy (candidates [Random.Range (0, candidates.Length)]);
    }
}