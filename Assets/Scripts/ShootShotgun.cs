using UnityEngine;
using System.Collections;

public class ShootShotgun : MonoBehaviour
{

    // Prefab of the Shell
    public Rigidbody m_Bullet;
    // A child of the tank where the shells are spawned
    public Transform[] Spawns;
    // The force given to the shell when firing
    public float m_LaunchForce = 30f;

    public float m_FireSpeed;
    private float m_FireRate = 0f;

    public float MaxAmmo = 6f;
    public float CurrentAmmo = 6f;

    public float MaxCurrent = 6f;

    private GameObject weapon;


    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentAmmo != 0)
        {

            if (Input.GetButton("Fire1") && m_FireRate <= 0)
            {
                //InvokeRepeating("Fire", 0f, 1f);
                CurrentAmmo -= 1;
                Fire();
                m_FireRate += m_FireSpeed;
            }
        }


        if (MaxAmmo > 0)
        {
            if (Input.GetKeyDown("r"))
            {
                // calculates the difference between maximum current ammo and the actual current ammo
                float difference = MaxCurrent - CurrentAmmo;

                // ensures you can only reload when the amount of ammo you have is not equal to that of the max you can have
                if (CurrentAmmo != MaxCurrent)
                {
                    //determines if ammo in stock is greater than or equal to the difference between maxcurrent and totalcurrent
                    if (MaxAmmo >= difference)
                    {
                        //makes the current ammo = the current ammo + the difference
                        CurrentAmmo = CurrentAmmo + difference;
                    }
                    //makes the current ammo = current ammo + ammo in stock
                    else CurrentAmmo = CurrentAmmo + MaxAmmo;

                    if (difference <= MaxAmmo)
                    {
                        MaxAmmo = MaxAmmo - difference;
                    }
                    else
                        MaxAmmo = 0;

                }
            }
        }


        if (Input.GetButtonUp("Fire1"))
        {
            CancelInvoke();
        }

        if (m_FireRate >= 0)
        {
            m_FireRate -= Time.deltaTime;
        }


    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 50), "Ammo: " + CurrentAmmo + " /  " + MaxAmmo);
    }

    private void Fire()
    {
        for (var i = 0; i < Spawns.Length; i++)
        {
            Rigidbody bulletInstance = Instantiate(m_Bullet, Spawns[i].position, Spawns[i].rotation) as Rigidbody;
            bulletInstance.velocity = m_LaunchForce * Spawns[i].forward;
        }
    }
}