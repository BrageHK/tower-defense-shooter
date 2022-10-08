using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    public float range = 1f;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;
    public float damage = 60f;
    public int cost = 100;


    private float fireCountdown = 0f;
    private GameObject bulletPrefab;
    private Transform bulletSpawn;

    private void Start() {
        bulletSpawn = transform.Find("FirePoint");
        bulletPrefab = Resources.Load("Prefabs/CannonBullet", typeof(GameObject)) as GameObject;
    }

    private void Update()
    {
        fireCountdown += Time.deltaTime;
        if (GetFirstEnemyPosition() != Vector2.zero)
        {
            Vector2 relativePos = GetFirstEnemyPosition() - (Vector2)transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, relativePos);
            rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z - 90f);
            transform.rotation = rotation;
            if(fireCountdown >= fireRate)
            {
                Fire();
                fireCountdown = 0f;
            }
        }
    }
    private Vector2 GetFirstEnemyPosition()
    {
        Vector2 closestEnemyPosition = new Vector2(0, 0);
        int highestIndex = -1;
        float highestDistance = 0f;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            SlimeMovement script = enemy.GetComponent<SlimeMovement>();
            float enemyDistance = Vector3.Distance(enemy.transform.position, transform.position);
                        
            if (enemyDistance < range)
            {   
                if (script.movePositionIndex >= highestIndex && script.distanceFromLastCheckpoint > highestDistance)
                {
                    highestIndex = script.movePositionIndex;
                    highestDistance = script.distanceFromLastCheckpoint;
                    closestEnemyPosition = (Vector2) enemy.transform.position;
                    closestEnemyPosition += script.GetDirectionVector(0.2f) * enemyDistance;
                }
                
            }            
        }
        return closestEnemyPosition;
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.AddComponent<BulletController>();
        bullet.GetComponent<BulletController>().damage = damage;
        FindObjectOfType<AudioManager>().Play("GunShot");
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = -bullet.transform.right * bulletSpeed;
    }

}
