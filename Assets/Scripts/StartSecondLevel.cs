using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * в начале уровня каждые 3секунды спавнятся из двух точек 10 движущихся целей 
 * 
 **/
public class StartSecondLevel : MonoBehaviour {

    public GameObject[] UIelements;

    [Header("Walking Target")]
    
    private WaitForSeconds untillStart;
    private float spawnPerSeconds = 3.0f;
    private Transform walkingTargetResp;
    private int spawnWithStart;

    
    public Transform[] walkingTargetWaypoints;
    public GameObject walkingTargetPrefab;
    public Transform[] RespawnPoint;
    public Text infoBoard;
    public static int counter ;
    public static int counterAllTarget;


    private void Start()
    {
        Weapon.damageWeapon = 1;
        counter = 0;
        counterAllTarget = 0;
        spawnWithStart = 8;
        infoBoard.text = "score: " + counter;
        untillStart = new WaitForSeconds(spawnPerSeconds);
        InstantiateWalkingTarget();
        StartCoroutine(LetsSpawn());

    }

    private IEnumerator LetsSpawn()
    {
        yield return untillStart;
        InstantiateWalkingTarget();
        if (spawnWithStart > 0)
        {
            spawnWithStart--;
            StartCoroutine(LetsSpawn());
        }
    }
    public void InstantiateWalkingTarget()
    {
        counterAllTarget++;
        walkingTargetResp = RespawnPoint[Random.Range(0, RespawnPoint.Length)];
        GameObject walkingTarget = Instantiate(walkingTargetPrefab, walkingTargetResp.position, walkingTargetResp.rotation);
        walkingTarget.GetComponent<WalkingTarget>().Init(walkingTargetWaypoints);
       
    }
    private void Update()
    {
        infoBoard.text = "score: " + counter;
        if (counterAllTarget == counter)infoBoard.text = "Game Over";
    }
}
