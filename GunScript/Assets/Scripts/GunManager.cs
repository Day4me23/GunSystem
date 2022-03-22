using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunManager : MonoBehaviour
{
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public GameObject muzzleFlash, bulletHoleGraphic;
    public CameraShake cameraShake;
    public float cameraShakeMagnitude, cameraShakeDuration;
    public TextMeshProUGUI text;
    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    public void MyInput()
    {
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) 
        { 
            Reload(); 
        }
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletPerTap;
            Shoot();
        }

    }

    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction while moving
        Vector3 direction = fpsCam.transform.forward + new Vector3(x,y,0);

        //RayCast shot
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<ShootingEnemy>().TakeDamage(damage);
            }
        }

        //ShakeCamera
        StartCoroutine(cameraShake.Shake(cameraShakeDuration, cameraShakeMagnitude));

        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0,180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShots);

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
