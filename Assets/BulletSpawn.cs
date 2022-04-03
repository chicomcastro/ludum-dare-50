using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform spawnSpot;

    public float initialDelay = 1f;
    public float shootPeriod = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBullet", initialDelay, shootPeriod);
    }

    private void SpawnBullet()
    {
        GameObject gamo = Instantiate(bulletPrefab, spawnSpot.position, spawnSpot.rotation, transform);
        Destroy(gamo, 30f);
    }
}
