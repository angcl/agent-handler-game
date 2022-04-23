using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;

    private GameController gameController;

    [SerializeField]
    private UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameState != EGameState.PLAYING)
            return;

        if(Input.GetMouseButtonUp(0))
        {
            if (uiController.IsContextMenuActive()) {
                uiController.HideContextMenu();
                return;
            }

            var mousePos = Input.mousePosition;
            if ( uiController.IsPointInTaskArea(mousePos) ) {
                return;
            }
            var ray = mainCamera.ScreenPointToRay(mousePos);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            if (hits.Length == 0) {
                uiController.HideContextMenu();
                return;
            }

            RaycastHit nearestHit = hits[0];
            foreach (RaycastHit hit in hits) {
                if (hit.distance < nearestHit.distance) {
                    nearestHit = hit;
                }
            }

            var clickable = nearestHit.collider.GetComponent<IClickable>();
            if (clickable == null) {
                uiController.HideContextMenu();
            } else {
                uiController.ShowContextMenu(mousePos, clickable);
            }
        }
    }
}
