using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{   

    [SerializeField]
    private GameObject _bulletDecal;
    [SerializeField]
    private float _bulletSpeed = 50.0f;
    private float _bulletLifeTime = 3.0f;

    public Vector3 Target { get; set; }
    public bool Hit { get; set; }

    private void OnEnable() {
        Destroy(this.gameObject, _bulletLifeTime);
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, _bulletSpeed * Time.deltaTime);
        if(!Hit && Vector3.Distance(transform.position, Target) < 0.0001f){
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        ContactPoint contact = other.GetContact(0);
        GameObject.Instantiate(_bulletDecal, contact.point + contact.normal * 0.0001f, Quaternion.LookRotation(contact.normal));
        Destroy(this.gameObject);
    }
}
