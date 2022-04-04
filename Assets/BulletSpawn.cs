using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnSpot;

    public float initialDelay = 1f;
    public float shootPeriod = 0.5f;

    private FollowBehaviour followBehaviour;

    void Start()
    {
        followBehaviour = GetComponent<FollowBehaviour>();
        InvokeRepeating("SpawnBullet", initialDelay, shootPeriod);
    }

    private void SpawnBullet()
    {
        if (!followBehaviour.isAway() && CanShoot())
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        return !LevelManager.instance.IsInInterval();
    }

    private void Shoot()
    {
        GameObject gamo = Instantiate(bulletPrefab, spawnSpot.position, spawnSpot.rotation);
        Destroy(gamo, 30f);
    }
}
