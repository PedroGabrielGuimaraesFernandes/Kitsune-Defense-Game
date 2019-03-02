using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour {

    public Camera cam;
    public float maximumLenght;

    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotaton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(cam != null)
        {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            rayMouse = cam.ScreenPointToRay(mousePos);
            if(Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maximumLenght))
            {
                RotateToMouseDirection(gameObject, hit.point);
            }
            else
            {
                var pos = rayMouse.GetPoint(maximumLenght);
                RotateToMouseDirection(gameObject, pos);
            }
        }
        else
        {
            Debug.Log("Sem Camera");
        }
	}

    void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotaton = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotaton, 1);
    }

    public Quaternion GetRotation()
    {
        return rotaton;
    }
}
