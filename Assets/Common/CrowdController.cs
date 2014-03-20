using UnityEngine;
using System.Collections;

public class CrowdController : MonoBehaviour
{
    #region Public properties

    public GameObject[] prefabs;
    public int numberOfPeople = 10;
    public Vector2 range = Vector2.one;

    #endregion

    #region Private variables

    GameObject[] instances;

    #endregion

    #region Monobehaviour functions

    void Start ()
    {
        instances = new GameObject[numberOfPeople];

        for (var i = 0; i < numberOfPeople; i++)
        {
            var prefab = prefabs [Random.Range (0, prefabs.Length)];
            var position = SamplePositionOnNavMesh ();
            var rotation = RandomYawRotation ();
            instances [i] = Instantiate (prefab, position, rotation) as GameObject;
        }
    }

    void Update ()
    {
        foreach (var i in instances)
        {
            if (i.CompareTag ("WantsOrder"))
            {
                i.SendMessage ("SetDestination", SamplePositionOnNavMesh ());
            }
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube (transform.position, new Vector3(range.x, 1, range.y));
    }

    #endregion

    #region Private functions

    Vector3 SamplePositionOnNavMesh ()
    {
        var wx = range.x * 0.5f;
        var wz = range.y * 0.5f;
        for (var i = 0; i < 30; i++)
        {
            var p = new Vector3(Random.Range (-wx, wx), 0, Random.Range (-wz, wz));
            NavMeshHit hit;
            if (NavMesh.SamplePosition (transform.position + p, out hit, 1.0f, 1))
            {
                if (!Physics.CheckSphere(hit.position + Vector3.up, 1.0f))
                    return hit.position;
            }
        }
        Debug.LogError ("Can't sample a point on the nav mesh.");
        return transform.position;
    }

    Quaternion RandomYawRotation ()
    {
        return Quaternion.AngleAxis (Random.Range (-180.0f, 180.0f), Vector3.up);
    }

    #endregion
}
