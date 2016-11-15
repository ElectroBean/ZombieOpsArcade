using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour
{
    public static int currentPoints;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 100, 10, 150, 50), "Points: " + currentPoints);
    }
}