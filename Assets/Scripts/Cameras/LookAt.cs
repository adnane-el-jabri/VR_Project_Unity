using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    public GameObject Target;
    public GameObject Origin;

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        Origin.transform.LookAt(Target.transform);
    }
}

