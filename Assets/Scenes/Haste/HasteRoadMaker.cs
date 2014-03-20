using UnityEngine;
using System.Collections;

public class HasteRoadMaker : MonoBehaviour
{
    public GameObject tilePrefab;
    public int columnCount = 15;
    public Shaker twistShaker;
    public float interval = 1;

    float previousPosition;
    float lastLinePosition;

    void Update ()
    {
        var position = transform.position.z;

        // Twist the pivot (depending on the movement of the node).
        twistShaker.Update (position - previousPosition);
        transform.parent.localRotation = Quaternion.AngleAxis (twistShaker.Scalar, Vector3.forward);
        previousPosition = position;

        // Create lines in a predefined interval (1 unit).
        while (position - lastLinePosition >= interval)
        {
            lastLinePosition += interval;
            CreateLine (lastLinePosition);
        }
    }

    void CreateLine (float offset)
    {
        var origin = transform.position + transform.forward * (offset - transform.position.z);
        var dx = transform.right * interval;
        for (var column = 0; column < columnCount; column++)
        {
            var position = origin + dx * (column - 0.5f * (columnCount - 1));
            Instantiate (tilePrefab, position, transform.rotation);
        }
    }
}