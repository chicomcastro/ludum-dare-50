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
    public float[] bulletSpeeds;

    void Start()
    {
        followBehaviour = GetComponent<FollowBehaviour>();
        StartCoroutine("SpawnBullet");
    }

    private IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            if (!followBehaviour.isAway() && CanShoot())
            {
                Shoot();
            }
            yield return new WaitForSeconds(shootPeriod);
        }
    }

    private bool CanShoot()
    {
        return !LevelManager.instance.IsInInterval() && !PauseController.instance.isPaused;
    }

    private void Shoot()
    {
        GameObject gamo = Instantiate(bulletPrefab, spawnSpot.position, spawnSpot.rotation);

        int currentLevel = LevelManager.instance.currentLevel;
        if (bulletSpeeds.Length > 0)
        {
            float currentLevelBulletSpeed = currentLevel < bulletSpeeds.Length ? bulletSpeeds[currentLevel] : bulletSpeeds[bulletSpeeds.Length - 1];
            gamo.GetComponent<Mover>().speed = currentLevelBulletSpeed;
        }

        Destroy(gamo, 30f);
    }
}
