using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public Transform cameraPoints;

	TransformShaker shaker;

    void Awake ()
    {
        shaker = GetComponentInChildren<TransformShaker> ();
    }

    void Update ()
    {
        if (Input.anyKeyDown)
        {
            for (var i = 1; i < 10; i++)
            {
                if (Input.GetKeyDown (i.ToString()))
                {
                    var target = cameraPoints.FindChild(i.ToString());
                    transform.position = target.position;
                    transform.rotation = target.rotation;
                    shaker.Reshake ();
                    break;
                }
            }
        }
    }
}
