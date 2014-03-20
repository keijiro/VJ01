using UnityEngine;
using System.Collections;

public class WireSolidMeshBuilder : MonoBehaviour
{
    static Mesh sharedMesh;

    void Awake ()
    {
        if (sharedMesh == null)
            InitializeSharedMesh ();
        GetComponent<MeshFilter> ().sharedMesh = sharedMesh;
    }

    static void InitializeSharedMesh ()
    {
        sharedMesh = new Mesh ();
        sharedMesh.subMeshCount = 2;
        
        var vertices = new Vector3 [4 * 6];
        
        vertices [0] = new Vector3 (-1, +1, -1);
        vertices [1] = new Vector3 (+1, +1, -1);
        vertices [2] = new Vector3 (+1, -1, -1);
        vertices [3] = new Vector3 (-1, -1, -1);
        
        vertices [4] = new Vector3 (+1, +1, -1);
        vertices [5] = new Vector3 (+1, +1, +1);
        vertices [6] = new Vector3 (+1, -1, +1);
        vertices [7] = new Vector3 (+1, -1, -1);
        
        vertices [8] = new Vector3 (+1, +1, +1);
        vertices [9] = new Vector3 (-1, +1, +1);
        vertices [10] = new Vector3 (-1, -1, +1);
        vertices [11] = new Vector3 (+1, -1, +1);
        
        vertices [12] = new Vector3 (-1, +1, +1);
        vertices [13] = new Vector3 (-1, +1, -1);
        vertices [14] = new Vector3 (-1, -1, -1);
        vertices [15] = new Vector3 (-1, -1, +1);
        
        vertices [16] = new Vector3 (-1, +1, +1);
        vertices [17] = new Vector3 (+1, +1, +1);
        vertices [18] = new Vector3 (+1, +1, -1);
        vertices [19] = new Vector3 (-1, +1, -1);
        
        vertices [20] = new Vector3 (-1, -1, -1);
        vertices [21] = new Vector3 (+1, -1, -1);
        vertices [22] = new Vector3 (+1, -1, +1);
        vertices [23] = new Vector3 (-1, -1, +1);
        
        sharedMesh.vertices = vertices;
        
        var indices = new int[vertices.Length];
        for (var i = 0; i < indices.Length; i++) {
            indices [i] = i;
        }
        sharedMesh.SetIndices (indices, MeshTopology.Quads, 0);

        sharedMesh.Optimize ();
        sharedMesh.RecalculateNormals ();

        indices = new int[4 * 3 * 2];
        var offs = 0;
        for (var i1 = 0; i1 < 16; i1 += 4) {
            for (var i2 = i1; i2 < i1 + 3; i2++) {
                indices [offs++] = i2;
                indices [offs++] = i2 + 1;
            }
        }
        sharedMesh.SetIndices (indices, MeshTopology.Lines, 1);
    }
}
