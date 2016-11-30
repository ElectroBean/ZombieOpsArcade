using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public Animation anim;
    public bool m_Dead;


	// Use this for initialization
	void Start () {
        CurrentHealth = MaxHealth;
        anim = GetComponent<Animation>();
        m_Dead = false;
	}
	
	// Update is called once per frame
	void Update () {
	     if(CurrentHealth <= 0)
        {
            OnDeath();
        }
	}

    public void TakeDamage(float amount)
    {
        //reduce current health by the amount of damage done.
        CurrentHealth -= amount;

    }

    private void OnDeath()
    {
        m_Dead = true;
        if(anim != null)
        {
            anim.Play("soldierDieFront");
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width/2 - 40, Screen.height - 100, 300, 100), "Health: " + CurrentHealth);
    }
}
