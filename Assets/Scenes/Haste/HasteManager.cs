using UnityEngine;
using System.Collections;

public class HasteManager : MonoBehaviour
{
    public bool showProperties;

    // External objects
    public TransformShaker cameraShaker;
    public HasteRoadMaker roadMaker;
    public ParticleSystem particles;

    // Controllers
    public KnobValue knobSpeed;
    public KnobValue knobTwist;
    public KnobValue knobFov;
    public KnobValue knobParticles;
    public KnobValue knobBank;
    public KnobValue knobPitch;
    public KnobValue knobTime;

    // Private variables
    float positionShakeAmount;
    float rotationShakeAmount;

    void Start ()
    {
        positionShakeAmount = cameraShaker.position.amount;
        rotationShakeAmount = cameraShaker.rotation.amount;
    }

    void Update ()
    {
        // Advance the root node.
        transform.position += transform.forward * (knobSpeed.Current * Time.deltaTime);

        // Speed
        cameraShaker.position.amount = positionShakeAmount * knobSpeed.Current / knobSpeed.maxValue;
        cameraShaker.rotation.amount = rotationShakeAmount * knobSpeed.Current / knobSpeed.maxValue;

        // Twist
        roadMaker.twistShaker.amount = knobTwist.Current;

        // FOV
        Camera.main.fieldOfView = knobFov.Current;

        // Particles
        particles.emissionRate = knobParticles.Current;

        // Bank/Pitch
        Camera.main.transform.localRotation =
            Quaternion.AngleAxis (knobBank.Current, Vector3.forward) *
            Quaternion.AngleAxis (knobPitch.Current, Vector3.up);

        // Time
        Time.timeScale = knobTime.Current;
    }
}