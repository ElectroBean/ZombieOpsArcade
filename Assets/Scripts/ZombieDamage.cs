using UnityEngine;
using System.Collections;

public class ZombieDamage : MonoBehaviour {

    public Animator anim;
    public bool attacking;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //while the player and enemy stay 
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Player")
        {
            attacking = GetComponentInParent<ZombieMovement>().attacking;
            if(attacking == true)
            {
                col.gameObject.GetComponent<PlayerHealth>().TakeDamage(2f);
            }
        }
    }
}
