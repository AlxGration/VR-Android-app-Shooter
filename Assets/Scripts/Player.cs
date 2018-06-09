
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Transform playerHead;
    public float rayLength = 10;
    public Weapon weapon;
    private float shootTimer = 0.0f;
    private WaitForSeconds lineRedererVisibilityTime;
    private ImageProgressBar imgProgressBar;

    private void Start()
    {
        weapon.Init();
        lineRedererVisibilityTime = new WaitForSeconds(weapon.fireRate * 0.4f);
        
    }

    private void Update()
    {
        Raycast();
        shootTimer += Time.deltaTime;
    }

    private void Raycast()
    {
        Ray ray = new Ray(playerHead.position, playerHead.forward * rayLength);
        RaycastHit hit;

        Debug.DrawRay(playerHead.position, playerHead.forward);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Target") && shootTimer >= weapon.fireRate)
            {
                MakeShot(hit.collider.GetComponent<Rigidbody>(), hit);
                return;
            }
            if(hit.collider.gameObject.CompareTag("WalkingTarget") && shootTimer >= weapon.fireRate)
            {
                MakeWalkingShot(hit.collider.gameObject, hit);
                return;
            }
            if (hit.collider.gameObject.CompareTag("VR_UI"))
            {
                imgProgressBar = hit.collider.gameObject.GetComponent<ImageProgressBar>();
                imgProgressBar.GazeOver = true;
                imgProgressBar.StartFillingProgressBar();
                return;
            } else if(imgProgressBar != null){
                imgProgressBar.GazeOver = false;
                imgProgressBar.StopFillingProgressBar();
                imgProgressBar = null;
                return;
            }
        }
    }

    private void MakeShot(Rigidbody targetRb, RaycastHit hit)
    {
        weapon.Shoot(hit.point, -hit.normal, targetRb);
        shootTimer = 0.0f;
        StartCoroutine(HandleLineRenderer());
    }

    private IEnumerator HandleLineRenderer()
    {
        yield return lineRedererVisibilityTime;
        weapon.ClearShotTrace();
    }

    private void MakeWalkingShot(GameObject targetGo, RaycastHit hit)
    {
        weapon.ShootWalkingTarget(hit.point, -hit.normal, targetGo);
        shootTimer = 0.0f;
        StartCoroutine(HandleLineRenderer());
    }
    public void plsChangeMyGun()
    {
        weapon.ChangeWeapon();
    }
}
