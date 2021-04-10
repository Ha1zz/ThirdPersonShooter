using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health_System;

public class BulletScript : MonoBehaviour
{
    private IDamagable DamagableObject;
    public int BulletDamage = 2;

    private int time = 10;
    public AudioSource bullet;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownOne());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 10.0f * Time.deltaTime);
        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bullet.Play(0);
            DamagableObject = other.GetComponent<IDamagable>();
            DamagableObject?.TakeDamage(BulletDamage);
            Destroy(this.gameObject);
        }
    }

    IEnumerator CountDownOne()
    {
        time--;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(CountDownOne());
    }

}
