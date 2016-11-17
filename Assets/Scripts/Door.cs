using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public GameObject[] Spawn1;
    public GameObject[] Spawn2;
    public GameObject[] Spawn3;
    public GameObject[] Spawn4;

    private void Awake()
    {
        GameObject.FindGameObjectsWithTag("door");
        
    }
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Spawn1.Length; i++)
        {
            Spawn1[i].SetActive(true);
        }
        for (int i = 0; i < Spawn2.Length; i++)
        {
            Spawn2[i].SetActive(false);
        }
        for (int i = 0; i < Spawn3.Length; i++)
        {
            Spawn3[i].SetActive(false);
        }
        for (int i = 0; i < Spawn4.Length; i++)
        {
            Spawn4[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay(Collision Col)
    {
        if (gameObject.tag == "door1")
        {
            if (Col.gameObject.tag == "Player")
            {
                if (Points.currentPoints >= 100)
                {
                    if (Input.GetButtonUp("e"))
                    {
                        Points.currentPoints -= 100;
                        gameObject.SetActive(false);
                        for(int i = 0; i < Spawn2.Length; i++)
                        {
                            Spawn2[i].SetActive(true);
                        }
                    }
                }
            }
        }
        if (gameObject.tag == "door2")
        {
            if (Col.gameObject.tag == "Player")
            {
                if (Points.currentPoints >= 100)
                {
                    if (Input.GetButtonUp("e"))
                    {
                        Points.currentPoints -= 100;
                        gameObject.SetActive(false);
                        for (int i = 0; i < Spawn3.Length; i++)
                        {
                            Spawn3[i].SetActive(true);
                        }
                    }
                }
            }
        }
        if (gameObject.tag == "door3")
        {
            if (Col.gameObject.tag == "Player")
            {
                if (Points.currentPoints >= 100)
                {
                    if (Input.GetButtonUp("e"))
                    {
                        Points.currentPoints -= 100;
                        gameObject.SetActive(false);
                        for (int i = 0; i < Spawn4.Length; i++)
                        {
                            Spawn4[i].SetActive(true);
                        }
                    }
                }
            }
        }
    }
    
}
    