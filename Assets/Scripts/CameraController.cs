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

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 cameraPos = camera.transform.position;

        if (mousePos.x <= moveRegion 
            || Input.GetKey(KeyCode.A) 
            || Input.GetKey(KeyCode.LeftArrow))
        {
            if (cameraPos.x - CalcSpeed() >= minPoint.x)
            {
                cameraPos.x -= CalcSpeed();
            }
        } 
        else if (mousePos.x >= (Screen.width - moveRegion) 
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.RightArrow))
        {
            if (cameraPos.x + CalcSpeed() <= maxPoint.x)
            {
                cameraPos.x += CalcSpeed();
            }
        }

        if (mousePos.y <= moveRegion 
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.DownArrow)) 
        {
            if (cameraPos.z - CalcSpeed() >= minPoint.y)
            {
                cameraPos.z -= CalcSpeed();
            }
        } 
        else if (mousePos.y >= (Screen.height - moveRegion) 
            || Input.GetKey(KeyCode.W) 
            || Input.GetKey(KeyCode.UpArrow))
        {
            if (cameraPos.z + CalcSpeed() <= maxPoint.y)
            {
                cameraPos.z += CalcSpeed();
            }
        }

        camera.transform.position = cameraPos;
    }

    private float CalcSpeed()
    {
        return speed * Time.deltaTime;
    }
}
