using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    public float speed = 5f;
    public int damage = 50;
    public GameObject impactEffect;
    public float explosionRadius = 0f;
    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distancethisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distancethisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distancethisFrame, Space.World);
    }

    void HitTarget(){
        if(impactEffect)
        {
        GameObject effectIns = (GameObject)Instantiate(impactEffect,transform.position,transform.rotation);
        Destroy(effectIns,5f);
        }


        if(explosionRadius > 0)
        {
            Explode();
        }else{
            Damage(target);
        }
        
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Enemy")){
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();  
        if(e != null)
        {
            e.TakeDamage(damage);
        }
    }


    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
