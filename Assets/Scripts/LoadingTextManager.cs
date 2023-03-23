using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTextManager : MonoBehaviour
{
    public Transform leader;
    public float followSharpness = 0.05f;
    void LateUpdate()
    {
        transform.position += (leader.position - transform.position) * followSharpness;
        transform.rotation = leader.rotation;
    }
}
