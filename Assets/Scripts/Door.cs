using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    private void Awake()
    {
       
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay(Collision Col)
    {

        if (Col.gameObject.tag == "Player")
        {
            if (Points.currentPoints >= 100)
            {
                if (Input.GetButtonUp("e"))
                {
                    Points.currentPoints -= 100;
                    gameObject.SetActive(false);
                }

            }
        }
    }
}
    