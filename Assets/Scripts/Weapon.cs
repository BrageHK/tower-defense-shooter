using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float speed = 10f;
    public AudioManager audioManager;
    
    private float timeBetweenBullets = 0.15f;
    private float bulletTimer = 0f;
    
    private void Update()
    {
        bulletTimer += Time.deltaTime;
    }

    public void Fire() {
        if (bulletTimer <= timeBetweenBullets) return;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.AddComponent<BulletController>();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bullet.transform.right * speed;
        FindObjectOfType<AudioManager>().Play("GunShot");
        bulletTimer = 0f;
    }

}
