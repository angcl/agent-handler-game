using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera camera;

    public float moveRegion = 100f;
    public float speed = 0.1f;

    public Vector3 minPoint = new Vector3(-10, -10, 0);
    public Vector3 maxPoint = new Vector3(10, 10, 0);

    public delegate void CameraMoved();
    public event CameraMoved OnCameraMoved;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 cameraPos = camera.transform.position;
        bool willMove = false;

        if (mousePos.x <= moveRegion 
            || Input.GetKey(KeyCode.A) 
            || Input.GetKey(KeyCode.LeftArrow))
        {
            if (cameraPos.x - CalcSpeed() >= minPoint.x)
            {
                willMove = true;
                cameraPos.x -= CalcSpeed();
            }
        } 
        else if (mousePos.x >= (Screen.width - moveRegion) 
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.RightArrow))
        {
            if (cameraPos.x + CalcSpeed() <= maxPoint.x)
            {
                willMove = true;
                cameraPos.x += CalcSpeed();
            }
        }

        if (mousePos.y <= moveRegion 
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.DownArrow)) 
        {
            if (cameraPos.z - CalcSpeed() >= minPoint.y)
            {
                willMove = true;                
                cameraPos.z -= CalcSpeed();
            }
        } 
        else if (mousePos.y >= (Screen.height - moveRegion) 
            || Input.GetKey(KeyCode.W) 
            || Input.GetKey(KeyCode.UpArrow))
        {
            if (cameraPos.z + CalcSpeed() <= maxPoint.y)
            {
                willMove = true;
                cameraPos.z += CalcSpeed();
            }
        }

        if(willMove)
        {
            camera.transform.position = cameraPos;
            if (OnCameraMoved != null)
                OnCameraMoved();
        }
    }

    private float CalcSpeed()
    {
        return speed * Time.deltaTime;
    }

}
