using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    // The time in seconds before the shell is removed
    private float m_MaxLifeTime = 2f;
    public float m_MaxDamage = 10f;

    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        // find the rigidbody of the collision object
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
        //
        // only enemies will have rigidbody scripts
        if (targetRigidbody != null)
        {
            // find the enemyHealth script associated with the rigidbody
            EnemyHealth targetHealth = targetRigidbody.GetComponent<EnemyHealth>();
            if (targetHealth != null)
            {
                float damage = m_MaxDamage;
                
                targetHealth.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
