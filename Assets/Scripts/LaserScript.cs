using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    LineRenderer line;
    private AudioSource aud;

	void Start ()
    {
        aud = GameObject.FindGameObjectWithTag("laserSound").GetComponent<AudioSource>();
        //gets the linerenderer component on the weapon
        line = gameObject.GetComponent<LineRenderer>();
        //turns the renderer off
        line.enabled = false;
        //turns the light component off
        gameObject.GetComponent<Light>().enabled = false;
    }
	

	void Update ()
    {
        if(Time.timeScale != 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                aud.Play();
                StopCoroutine("FireLaser");
                StartCoroutine("FireLaser");
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            aud.Stop();
        }
	}

    IEnumerator FireLaser()
    {
        line.enabled = true;
        gameObject.GetComponent<Light>().enabled = true;

        while (Input.GetButton("Fire1"))
        {
            //makes the texture spin while the button is being held
            line.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time);

            Ray ray = new Ray(transform.position, transform.forward);
            //sets a hit variable for where the raycast ends
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100))
                //stops the line at whatever it hits
                line.SetPosition(1, hit.point);
            if (hit.rigidbody)
            {
                if(hit.rigidbody.gameObject.tag == "Enemy")
                {
                    //if "hit" has the tag "Enemy", finds the EnemyHealth script and makes it take damage
                    hit.rigidbody.gameObject.GetComponent<ZombieHealth>().TakeDamage(0.8f);
                }
                
            }
            else
                //if the line doesn't hit anything it automatically stops 100 points forward
                line.SetPosition(1, ray.GetPoint(100));

            yield return null;
        }

        //turns the line and light off when Fire1 is released
        line.enabled = false;
        gameObject.GetComponent<Light>().enabled = false;
    }
}
