using UnityEngine;
using System.Collections;

public class ChainBuilder : MonoBehaviour
{
    #region Public properties

    public int nodeNum = 10;
    public float interval = 1.0f;
    public float mass = 1.0f;
    public float drag = 0.1f;
    public float angularDrag = 0.1f;
    public float colliderRadius;
    public GameObject nodePrefab;

    #endregion

    #region Physics setup

    void AddRigidbody (GameObject node, bool isFixed)
    {
        var rb = node.AddComponent<Rigidbody> ();

        rb.mass = mass;
        rb.drag = drag;
        rb.angularDrag = angularDrag;

        if (isFixed)
        {
            rb.isKinematic = true;
        }
        else if (colliderRadius > 0.0f)
        {
            var col = node.AddComponent<SphereCollider> ();
            col.radius = colliderRadius;
        }
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

    void BuildChain (GameObject root)
    {
        var node = root;

        // Make the chain of nodes.
        for (var i = 0; i < nodeNum; i++)
        {
            GameObject newNode;

            if (nodePrefab == null)
                newNode = new GameObject ();
            else
                newNode = Instantiate (nodePrefab) as GameObject;

            newNode.name = "node " + i;
            newNode.transform.parent = root.transform;
            newNode.transform.localPosition = Vector3.forward * interval * (i + 1);
            newNode.layer = root.layer;

            AddRigidbody (newNode, false);
            AddJoint (node, newNode);

            node = newNode;
        }
    }

    #endregion

    #region MonoBehaviour
    
    void Awake ()
    {
        BuildChain (gameObject);
    }

    #endregion
}
