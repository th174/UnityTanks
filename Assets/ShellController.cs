using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float duration;
    public float muzzleForce;
    public ParticleSystem smokeEffect;
    public ParticleSystem explosionEffect;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward * muzzleForce);
        explosionEffect.Pause();
        Destroy(this.gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        smokeEffect.transform.parent = null;
        var emissions = smokeEffect.emission;
        emissions.enabled = false;
        explosionEffect.transform.parent = null;
        explosionEffect.Play();
        Destroy(this.gameObject);
    }
}
