using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public Animation anim;
    public bool m_Dead;
    public GameObject healthBar;
    public AudioSource aud;
    public float hasPlayed = 0f;

	// Use this for initialization
	void Start () {
        healthBar = GameObject.FindGameObjectWithTag("health");
        CurrentHealth = MaxHealth;
        anim = GetComponent<Animation>();
        m_Dead = false;
        aud = GameObject.FindGameObjectWithTag("playerHit").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        healthBar = GameObject.FindGameObjectWithTag("health");
        if (CurrentHealth <= 0)
        {
            OnDeath();
        }
         if(CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
        hasPlayed -= Time.deltaTime;
	}

    public void TakeDamage(float amount)
    {
        //reduce current health by the amount of damage done.
        CurrentHealth -= amount;
        float calcHealth = CurrentHealth / MaxHealth; //if health is 80 we get 0.8f
        SetHealthBar(calcHealth);
        if(hasPlayed <= 0)
        {
            aud.Play();
            hasPlayed = 1;
        }
    }

    private void OnDeath()
    {
        m_Dead = true;
        if(anim != null)
        {
            anim.Play("soldierDieFront");
        }
    }

    public void SetHealthBar(float myHealth)
    {
        if(healthBar != null)
        {
            healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
    }
}
