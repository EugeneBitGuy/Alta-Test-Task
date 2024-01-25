using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float startSize;
    [SerializeField] private float criticalSize;
    [SerializeField] private float minimalBulletSize;
    [SerializeField] private float bulletSizePerFrame;
    
    [SerializeField] private Transform playerSphere;
    [SerializeField] private Transform bullet;

    private float proportionPlayerBullet;
    private float maxBulletSize;
    private bool isSpawningShoot = false;
    private bool canSpawn = true;
    void Start()
    {
        playerSphere.localScale = Vector3.one * startSize;
        proportionPlayerBullet = criticalSize / minimalBulletSize;
        maxBulletSize = startSize / proportionPlayerBullet;
        bullet.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canSpawn)
        {
            if (!isSpawningShoot)
            {
                isSpawningShoot = true;
                
                bullet.gameObject.SetActive(true);
                bullet.localScale = Vector3.one * minimalBulletSize;
                playerSphere.localScale -= Vector3.one * proportionPlayerBullet * bulletSizePerFrame;
                bullet.position = Vector3.forward * (playerSphere.localScale.x + bullet.localScale.x) / 2;
            }
            else
            {
                bullet.localScale += Vector3.one * bulletSizePerFrame;
                playerSphere.localScale -= Vector3.one * proportionPlayerBullet * bulletSizePerFrame;
                bullet.position = Vector3.forward * (playerSphere.localScale.x + bullet.localScale.x) / 2;
            }
        }
        else if (isSpawningShoot)
        {
            isSpawningShoot = false;
            canSpawn = false;
            bullet.DOLocalMove(Vector3.forward * 50, 5f).OnComplete(() =>
            {
                bullet.gameObject.SetActive(false);
                canSpawn = true;
            });

            startSize = playerSphere.localScale.x;
            
            playerSphere.localScale = Vector3.one * startSize;
            proportionPlayerBullet = criticalSize / minimalBulletSize;
            maxBulletSize = startSize / proportionPlayerBullet;
        }
        
        if(playerSphere.localScale.x < criticalSize)
        {
            Destroy(playerSphere.gameObject);
            Destroy(bullet.gameObject);
        }
    }
}
