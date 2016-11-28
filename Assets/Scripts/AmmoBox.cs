using UnityEngine;
using System.Collections;

public class AmmoBox : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<SwapWeapons>().weaponNumber == 1)
        {
            col.gameObject.GetComponentInChildren<ShootingRifle>().MaxAmmo += 20;
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<SwapWeapons>().weaponNumber == 2)
        {
            col.gameObject.GetComponentInChildren<ShootShotgun>().MaxAmmo += 3;
            Destroy(gameObject);
        }
    }
}
