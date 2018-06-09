using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WalkingTarget : MonoBehaviour {

    private NavMeshAgent navMesh;
    private Transform curDestination;
    private Transform[] wayPoints;
    private int health;
    public GameObject[] healthImages;
    public GameObject destroyedTargetPrefab;
    

    public void Init(Transform[] _wayPoints)
    {
        wayPoints = _wayPoints;
    }


    private void Start()
    {
        navMesh = gameObject.GetComponent<NavMeshAgent>();
        health = healthImages.Length;
        SetRandomNavMeshAgintDestination();// рандомное движение ходячих объектов
    }

    private void Update()
    {
        
        float dist = navMesh.remainingDistance;
        if(dist != Mathf.Infinity && navMesh.pathStatus == NavMeshPathStatus.PathComplete && navMesh.remainingDistance < 0.1f)
        {
            SetRandomNavMeshAgintDestination();
        }
    }

    private void SetRandomNavMeshAgintDestination()
    {
        // выбор нового вектора движения
        int waypointIndex = Random.Range(0, wayPoints.Length);
        curDestination = wayPoints[waypointIndex];
        navMesh.SetDestination(curDestination.position);
    }
    
    public void TakeDamage()
    {
        for (int i = health - Weapon.damageWeapon; i < health; i++){
            if (i < 0) continue;
            healthImages[i].SetActive(false);
        }
        health -= Weapon.damageWeapon ;
        if (health <= 0) // если не осталось жизней, вызываем анимацию разрушения
        {
            Transform curTransform = transform;
            GameObject go = Instantiate(destroyedTargetPrefab, curTransform.position, curTransform.rotation);
            Destroy(gameObject);
            Destroy(go, 5.0f);
            StartSecondLevel.counter++; // +1 труп))
         }
    }
}
