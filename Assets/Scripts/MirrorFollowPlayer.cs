using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorFollowPlayer : MonoBehaviour
{
    public Transform playerCamera;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 auxTransform = playerCamera.position;
        auxTransform.z = transform.position.z;
        transform.position = auxTransform;
    }
}
