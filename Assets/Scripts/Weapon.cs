using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Weapon {

    public float fireRate = 0.1f;
    public float shootForce = 1000.0f;
    public Transform gunEnd;
    public AudioSource shotAudio;
    public LineRenderer projectileLineRenderer;
    public GameObject[] WeaponsPack;
    private bool forChangeWeapon = true;
    public Transform[] gunsEnds;
    public AudioSource[] gunsAudios;
    public static int damageWeapon = 1;


    public void Init()
    {
        projectileLineRenderer.positionCount = 2;
    }
    // обработка выстрела по статичному объекту
    public void Shoot(Vector3 shootPoint, Vector3 force, Rigidbody targetRb)
    {
        //laser
        projectileLineRenderer.enabled = true;
        projectileLineRenderer.SetPosition(0, gunEnd.position);
        projectileLineRenderer.SetPosition(1, shootPoint);
        
        targetRb.AddForceAtPosition(force * shootForce, shootPoint);
        shotAudio.Play();
    }
    public void ClearShotTrace()
    {
        projectileLineRenderer.enabled = false;
    }
    // обработка выстрела по движущисчя объектам
    public void ShootWalkingTarget(Vector3 shootPoint, Vector3 force, GameObject targetGo)
    {
        projectileLineRenderer.enabled = true;
        projectileLineRenderer.SetPosition(0, gunEnd.position);
        projectileLineRenderer.SetPosition(1, shootPoint);

        targetGo.GetComponent<WalkingTarget>().TakeDamage();
        shotAudio.Play();
    }
    public void ChangeWeapon()
    {
        if (forChangeWeapon) // Sniper gun
        {
            forChangeWeapon = false;
            WeaponsPack[0].SetActive(false);
            WeaponsPack[1].SetActive(true);
            fireRate = 2.5f;
            gunEnd = gunsEnds[1];
            shotAudio = gunsAudios[1];
            damageWeapon = 3;
            shootForce = 3000.0f;
        }
        else //Pistolet
        {
            forChangeWeapon = true;
            WeaponsPack[1].SetActive(false);
            WeaponsPack[0].SetActive(true);
            fireRate = 0.5f;
            gunEnd.position = new Vector3(-0.014f, 0.017f, 0.24f);
            gunEnd = gunsEnds[0];
            shotAudio = gunsAudios[0];
            damageWeapon = 1;
            shootForce = 1000.0f;
        }
    }
}
