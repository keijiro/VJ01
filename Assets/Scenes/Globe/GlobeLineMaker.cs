using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobeLineMaker : MonoBehaviour
{
    #region Properties and variables

    // A class used for storing the frame history.
    class Frame
    {
        public float time;
        public Vector3 position;
        public Vector3 upVector;

        public Frame (float time, Vector3 position, Vector3 upVector)
        {
            this.time = time;
            this.position = position;
            this.upVector = upVector;
        }

        public Quaternion Rotation {
            get { return Quaternion.FromToRotation (Vector3.up, upVector); }
        }
    }

    // Public properties.
    public Transform target;
    public float velocity = 7.1f;
    public float historyLength = 1.4f;
    public float sampleInterval = 0.1f;
    public GameObject tailPrefab;

    // Private variables.
    Queue<Frame> frameQueue;
    float intervalTimer;

    #endregion

    #region MonoBehaviour functions

    void Awake ()
    {
        frameQueue = new Queue<Frame> ();
    }

    void Update ()
    {
        EnqueueFrameWithInterval ();

        // Move the frames right.
        var delta = Vector3.right * Time.deltaTime * velocity;
        foreach (var frame in frameQueue) frame.position += delta;

        // Update the sweep path.
        GetComponent<SweepMesh> ().path = MakePath();
    }

    #endregion

    #region Public functions

    public void CutLine ()
    {
        if (frameQueue.Count < 2) return;

        // Make a frame array from the frame queue.
        var frames = new Frame[frameQueue.Count - 1];
        for (var i = frames.Length - 1; i >= 0 ; i--)
            frames [i] = frameQueue.Dequeue ();

        // Spawn a tail along the frame array.
        SpawnTail (frames);
    }

    #endregion

    #region Private functions

    void EnqueueFrameWithInterval ()
    {
        // Count the interval.
        intervalTimer += Time.deltaTime;
        if (intervalTimer < sampleInterval) return;
        intervalTimer -= sampleInterval;

        var currentTime = Time.time;
        var oldestTime = currentTime - historyLength;

        // Dequeue old frames.
        while (frameQueue.Count > 0 && frameQueue.Peek ().time < oldestTime)
            frameQueue.Dequeue ();

        // Enqueue the current frame.
        frameQueue.Enqueue (new Frame (currentTime, target.position, target.forward));
    }

    AnimationCurve[] MakePath()
    {
        if (frameQueue.Count < 2) return null;

        var path = new AnimationCurve[] {
            new AnimationCurve (),  // position.x
            new AnimationCurve (),  // position.y
            new AnimationCurve (),  // position.z
            new AnimationCurve (),  // up.x
            new AnimationCurve (),  // up.y
            new AnimationCurve ()   // up.z
        };

        // The first frame.
        {
            var p = target.position;
            var u = target.forward;
            path [0].AddKey (0, p.x);
            path [1].AddKey (0, p.y);
            path [2].AddKey (0, p.z);
            path [3].AddKey (0, u.x);
            path [4].AddKey (0, u.y);
            path [5].AddKey (0, u.z);
        }

        var currentTime = Time.time;
        var timeScale = 1.0f / (currentTime - frameQueue.Peek ().time);

        // Plot the frames in the queue.
        foreach (var frame in frameQueue)
        {
            var t = (currentTime - frame.time) * timeScale;
            var p = frame.position;
            var u = frame.upVector;
            path [0].AddKey (t, p.x);
            path [1].AddKey (t, p.y);
            path [2].AddKey (t, p.z);
            path [3].AddKey (t, u.x);
            path [4].AddKey (t, u.y);
            path [5].AddKey (t, u.z);
        }

        return path;
    }

    void SpawnTail(Frame[] frames)
    {
        // Make a root node from the first frame.
        var root = Instantiate (tailPrefab, frames [0].position, frames [0].Rotation) as GameObject;

        // Make a chain of nodes.
        var prevNode = root;
        for (var i = 1; i < frames.Length; i++)
        {
            var frame = frames[i];

            // Make a game object.
            GameObject newNode = new GameObject ();
            newNode.name = "node " + i;
            newNode.transform.parent = root.transform;
            newNode.transform.position = frame.position;
            newNode.transform.rotation = frame.Rotation;

            // Add physics properties.
            AddRigidbody (newNode);
            AddJoint (prevNode, newNode);

            prevNode = newNode;
        }

        // Give upforce.
        root.rigidbody.AddForce ((Random.onUnitSphere + Vector3.up) * 4.0f, ForceMode.Impulse);
    }

    void AddRigidbody (GameObject node)
    {
        var rb = node.AddComponent<Rigidbody> ();
        rb.velocity = Vector3.right * 5.0f + Random.onUnitSphere * 3.0f;
    }

    void AddJoint (GameObject node, GameObject boundTo)
    {
        var joint = node.AddComponent<ConfigurableJoint> ();
        joint.connectedBody = boundTo.rigidbody;
        
        var limit = new SoftJointLimit ();
        limit.limit = 0.1f;
        limit.spring = 40.0f;
        joint.linearLimit = limit;
        
        limit.limit = 10.0f;
        joint.angularYLimit = limit;
        joint.angularZLimit = limit;
        joint.highAngularXLimit = limit;
        joint.lowAngularXLimit = limit;
        
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.angularXMotion = ConfigurableJointMotion.Limited;
        joint.angularYMotion = ConfigurableJointMotion.Limited;
        joint.angularZMotion = ConfigurableJointMotion.Limited;
    }

    #endregion
}