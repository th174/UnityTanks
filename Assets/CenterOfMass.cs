using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour {

    public Transform com;
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = com.position - transform.position;
    }
}
