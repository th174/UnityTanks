using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float duration;
    public float muzzleForce;
    public float baseDamage;

    public ParticleSystem smokeEffect;
    public ParticleSystem explosionEffect;
    public ParticleSystem muzzleFlash;

	public AudioSource explosionSource;

    private float initialSpeed = -1;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward * muzzleForce);
        explosionEffect.Stop(withChildren:true, stopBehavior:ParticleSystemStopBehavior.StopEmittingAndClear);
        muzzleFlash.transform.parent = null;
        Destroy(this.gameObject, duration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (initialSpeed < 0)
        {
            initialSpeed = this.GetComponent<Rigidbody>().velocity.magnitude;
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        var health = hit.gameObject.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(GetDamage());
        }
        smokeEffect.transform.parent = null;
        var emissions = smokeEffect.emission;
        emissions.enabled = false;
        explosionEffect.transform.parent = null;
        explosionEffect.Play(withChildren:true);
        explosionEffect.Play();
        Destroy(this.gameObject);
    }

    float GetDamage()
    {
        return baseDamage * Mathf.Pow(this.GetComponent<Rigidbody>().velocity.magnitude / initialSpeed, 2);
    }
}
