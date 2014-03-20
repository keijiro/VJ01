using UnityEngine;
using System.Collections;

public class GlobeManager : MonoBehaviour
{
    public bool showProperties;

    // External objects
    public Transform cameraPivot;
    public Transform twistPivot;
    public GlobeLineMaker[] lines;
    public Transform[] linePivots;
    public TransformShaker[] lineShakers;
    public KvantGenericDeformer globeDeformer;
    public Bloom bloomFx;
    public ParticleSystem ballEmitter;
    public Light mainLight;

    // Public variables
    public float gravity = 10;

    // Controllers
    public KnobValue knobYaw;
    public KnobValue knobPitch;
    public KnobValue knobAlignment;
    public KnobValue knobTurbulence;
    public KnobValue knobTwist;
    public Launchpad launchpad;

    // Private variables
    bool stageSwitch;
    float stageParam;
    bool ballSwitch;
    float initialFieldOfView;
    float initialLightIntensity;

    void Start ()
    {
        // Global settings.
        Physics.gravity = Vector3.right * gravity;

        // Retrieve the initial values.
        initialFieldOfView = Camera.main.fieldOfView;
        initialLightIntensity = mainLight.intensity;
    }

    void Update ()
    {
        launchpad.Update (this);

        // Yaw, Pitch
        cameraPivot.localRotation =
            Quaternion.AngleAxis (knobYaw.Current, Vector3.up) *
            Quaternion.AngleAxis (knobPitch.Current, Vector3.right);

        // Turbulence
        foreach (var shaker in lineShakers)
            shaker.position.amount = knobTurbulence.Current;

        twistPivot.localPosition = Vector3.up * knobTurbulence.Current * 0.4f;

        // Alignment
        for (var i = 0; i < linePivots.Length; i++)
        {
            var x = i - linePivots.Length / 2 + 0.5f;
            linePivots [i].localPosition = Vector3.forward * knobAlignment.Current * x;
        }

        // Twist
        twistPivot.localRotation = Quaternion.AngleAxis (knobTwist.Current, Vector3.right);

        // Impulse animation (camera and light)
        Camera.main.fieldOfView = ExpEase.Out (Camera.main.fieldOfView, initialFieldOfView, -6.0f);
        mainLight.intensity = ExpEase.Out (mainLight.intensity, initialLightIntensity, -4.0f);
        
        // Stage switching
        stageParam = ExpEase.Out (stageParam, stageSwitch ? 1.0f : 0.0f, -4.0f);
        GetComponent<PolishableProbeMixer> ().mix = stageParam;

        if (stageParam > 0.001f)
        {
            globeDeformer.enabled = true;
            globeDeformer.deformAmount = Vector3.one * (stageParam * 0.1f);
            bloomFx.enabled = true;
            bloomFx.bloomIntensity = stageParam;
        }
        else
        {
            globeDeformer.enabled = false;
            bloomFx.enabled = false;
        }

        globeDeformer.transform.localScale = Vector3.one * (50.0f - stageParam * 0.5f);

        // Balls
        ballEmitter.emissionRate = ballSwitch ? 140 : 0;
    }

    void OnPadPressed (string id)
    {
        if (id == "Stage") stageSwitch = !stageSwitch;
        if (id == "Ball") ballSwitch = !ballSwitch;
        if (id == "Cut") StartCoroutine (CutLineEffects ());
    }

    IEnumerator CutLineEffects ()
    {
        // Cut the line.
        foreach (var l in lines) l.CutLine ();

        // Zoom and flash.
        Camera.main.fieldOfView = 30;
        mainLight.intensity *= 1.75f;

        // Stop the world.
        Time.timeScale = 0;

        // Shake the camera and wait for one frame.
        cameraPivot.transform.localPosition = Random.onUnitSphere;
        yield return null;

        // Shake the camera and wait for one frame again.
        cameraPivot.transform.localPosition = Random.onUnitSphere;
        yield return null;

        // Return to the normal state.
        cameraPivot.transform.localPosition = Vector3.zero;
        Time.timeScale = 1;
    }
}