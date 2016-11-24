using UnityEngine;
using System.Collections;

public class Aiming : MonoBehaviour
{
    

    private void Awake()
    {
        
    }
    // Update is called once per frame
    private void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
        //{
        //    transform.LookAt(hit.point);
        //}
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
}