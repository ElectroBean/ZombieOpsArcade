using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
   


	// Use this for initialization
	void Start () {
        CurrentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(float amount)
    {
        //reduce current health by the amount of damage done.
        CurrentHealth -= amount;

    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width/2 - 40, Screen.height - 100, 300, 100), "Health: " + CurrentHealth);
    }
}
