using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private GameController gameController;
    private Camera mainCamera;

    public float moveRegion = 100f;
    public float speed = 0.1f;

    public Vector3 minPoint = new Vector3(-10, -10, 0);
    public Vector3 maxPoint = new Vector3(10, 10, 0);

    public delegate void CameraMoved();
    public event CameraMoved OnCameraMoved;
    public GameObject objectToFocus;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (gameController.gameState != EGameState.PLAYING)
            return;
            
        if (objectToFocus != null) {
            // Todo: Make this smooth scrollable
            var newPosition = new Vector3(objectToFocus.transform.position.x, transform.position.y, objectToFocus.transform.position.z - 18.0f);
            if (Vector3.Distance(newPosition, transform.position) < 1f) {
                objectToFocus = null;
                return;
            }
            transform.position = Vector3.Lerp(transform.position, newPosition, speed * 0.25f * Time.deltaTime);
            return;
        }

        Vector3 mousePos = Input.mousePosition;
        Vector3 cameraPos = mainCamera.transform.position;
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
            mainCamera.transform.position = cameraPos;
            if (OnCameraMoved != null)
                OnCameraMoved();
        }
    }

    private float CalcSpeed()
    {
        return speed * Time.deltaTime;
    }

}
