using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Aiming : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;

    private void Awake()
    {
        
    }
    // Update is called once per frame
    private void Update()
    {
        //top down shooter aiming using ray cast to ground
        if(SceneManager.GetActiveScene().name == "Prototype_1")
        {
            if(Camera.main != null)
                 {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Plane plane = new Plane(Vector3.up, Vector3.zero);
                    float distance;
                    if (plane.Raycast(ray, out distance))
                    {
                        Vector3 target = ray.GetPoint(distance);
                        Vector3 direction = target - transform.position;
                         float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(0, rotation, 0);
                    }
                  }
        }


        //if the scene name is Prototype_1 - fps, the aiming changes to fps aiming
        if(SceneManager.GetActiveScene().name == "Prototype_1 - FPS") {
            if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        } }
        
        



    }
}