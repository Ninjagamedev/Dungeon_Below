using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    public Vector3 Offset;
    public List<BuffCardBlueprint> PactsList;
    public GameObject[] Blessings;
    private float fireCountdown = 0f;
    public float slowPct = .7f;

    [Header("Unity Fields")]
    public Transform target;
    private Enemy targetEnemy;
    public float rotationSpeed = 1f;
    public GameObject bulletPrefab;
    public GameObject RangeIndicator;
    public Transform firePoint;
    public string enemyTag = "Enemy";


    void Start()
    {
        InvokeRepeating("UpdateTarget",0f, 0.5f);   
        transform.position = transform.position + Offset;
    }

    public void showRange()
    {
        RangeIndicator.SetActive(true);
        RangeIndicator.transform.localScale = new Vector3(range * 10, 1, range * 10);
    }
    public void hideRange()
    {
        RangeIndicator.SetActive(false);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach( GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }else
        {
            target = null;
        }

    }
    // Update is called once per frame
    void Update()
    {
        fireCountdown -= Time.deltaTime;
        if (target == null)
        {
            return;
        }
        LockOnTarget();
        if(fireCountdown <= 0f){

            Shoot();
            fireCountdown = 1f / fireRate;

        }


    }

    void LockOnTarget()
     {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f,rotation.y,0f);

    }

    void Shoot(){
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet!=null){
            bullet.Seek(target);
        }
    }

   public void ReceiveBlessing(BuffCardBlueprint Card,string Attribute, float value)
    {
        Debug.Log(Card + Attribute +  value);
        PactsList.Add(Card);
        //Procurar o atributo certo

        BuffStat(Attribute, value);
        Debug.Log("STATS AUMENTADAS");
    }

    void BuffStat(string Attribute, float value)
    {
        if (Attribute == "range")
        {
            range += value;
        }
        if (Attribute == "fireRate")
        {
            fireRate += value;
        }


    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);   
    }
}
