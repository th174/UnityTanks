using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float particleDuration;

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, particleDuration);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
