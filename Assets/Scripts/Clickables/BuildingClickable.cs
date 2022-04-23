using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClickable : MonoBehaviour, IClickable
{
    public bool isEnergized { get; private set; } = true;
    public bool isHacked { get; private set; } = false;
    public bool isDownloaded { get; private set; } = false;

    private bool uploadVirus = false;

    [SerializeField]
    public float timeForVirusUpload { get; private set; } = 10.0f;

    public bool downloadFiles { get; private set; } = false;

    [SerializeField]
    public float timeForFileDownload { get; private set; } = 10.0f;
    
    private float timePassedSinceDeactivation = 0.0f;
    private float timePassedSinceHacked = 0.0f;
    private float timePassedSinceFileTransaction = 0.0f;
    private float timePassedSinceDownloaded = 0.0f;

    [SerializeField]
    private float timeForReactivation = 10.0f;

    private WorldSpaceCanvasManager worldSpaceCanvasManager;

    private InfoElement infoElement;
    [SerializeField]
    private float offsetY = 4.0f;

    void Awake()
    {
        worldSpaceCanvasManager = GameObject.FindObjectOfType<WorldSpaceCanvasManager>();
        var collider = gameObject.GetComponent<BoxCollider>();
        var position = new Vector3(transform.position.x, collider.bounds.max.y + offsetY, transform.position.z);
        infoElement = worldSpaceCanvasManager.CreateElement(position).GetComponent<InfoElement>();
    }

    void Update()
    {
        if(!isEnergized)
        {
            timePassedSinceDeactivation += Time.deltaTime;
            if(timePassedSinceDeactivation >= timeForReactivation)
            {
                SetEnergyState(true);
                timePassedSinceDeactivation = 0.0f;
            }

            return;
        }

        if (isHacked)
        {
            timePassedSinceHacked += Time.deltaTime;
            if (timePassedSinceHacked >= timeForReactivation)
            {
                SetHackedState(false);
                timePassedSinceHacked = 0.0f;
            }
        }

        if (isDownloaded)
        {
            timePassedSinceDownloaded += Time.deltaTime;
            if (timePassedSinceDownloaded >= timeForReactivation / 2) {
                SetDownloadedState(false);
            }
        }

        if (uploadVirus || downloadFiles) {
            timePassedSinceFileTransaction += Time.deltaTime;
            
            if (uploadVirus) {
                infoElement.UpdateProgress(timePassedSinceFileTransaction / timeForVirusUpload);
                if (timePassedSinceFileTransaction >= timeForVirusUpload) {
                    SetHackedState(true);
                }
            } else if (downloadFiles) {
                infoElement.UpdateProgress(timePassedSinceFileTransaction / timeForFileDownload);
                if (timePassedSinceFileTransaction >= timeForFileDownload) 
                {
                    SetDownloadedState(true);
                    timePassedSinceFileTransaction = 0.0f;
                    infoElement.SetBarState(false, uploadVirus ? Color.red : Color.green);
                }
            }
        }
    }

    private void SetEnergyState(bool state)
    {
        isEnergized = state;

        if (isEnergized) {
            infoElement.HideIcon(EInfoIcon.DEACTIVATED_ENERGY);
            gameObject.GetComponent<Renderer>().material.color = Color.grey;
            return;
        }

        infoElement.ShowIcon(EInfoIcon.DEACTIVATED_ENERGY);
        gameObject.GetComponent<Renderer>().material.color = Color.black;

        timePassedSinceDeactivation = 0.0f;
    }

    private void SetHackedState(bool state) 
    {
        if (!isEnergized) {
            return;
        }
        
        isHacked = state;
        if (isHacked) {
            infoElement.ShowIcon(EInfoIcon.HACKED);
            infoElement.SetBarState(false, uploadVirus ? Color.red : Color.green);

            timePassedSinceFileTransaction = 0.0f;
            uploadVirus = false;
            return;
        }

        infoElement.HideIcon(EInfoIcon.HACKED);
        timePassedSinceHacked = 0.0f;
    }

    private void SetDownloadedState(bool state) {
        if (!isEnergized) {
            return;
        }

        isDownloaded = state;
        if (isDownloaded) {
            infoElement.ShowIcon(EInfoIcon.LEAKED);
            infoElement.SetBarState(false, downloadFiles ? Color.green : Color.red);

            timePassedSinceDownloaded = 0.0f;
            downloadFiles = false;
            return;
        }

        infoElement.HideIcon(EInfoIcon.LEAKED);
        timePassedSinceDownloaded = 0.0f;
    }

    public void Run(EContextButton contextButton)
    {
        if (contextButton == EContextButton.ACTIVATE) {
            SetEnergyState(true);
        }

        if (!isEnergized) {
            return;
        }

        if (contextButton == EContextButton.DEACTIVATE) {
            SetEnergyState(false);
        }

        if (uploadVirus || downloadFiles) {
            return;
        }

        if (contextButton == EContextButton.UPLOAD) {
            uploadVirus = true;
        }

        if (contextButton == EContextButton.DOWNLOAD) {
            downloadFiles = true;
        }
        
        infoElement.SetBarState(uploadVirus || downloadFiles, uploadVirus ? Color.red : Color.green);
    }

    public EContextButton[] GetContextButtons()
    {
        if(isEnergized)
        {
            return new EContextButton[] {
                EContextButton.DEACTIVATE,
                EContextButton.DOWNLOAD,
                EContextButton.UPLOAD
            };
        }
        else
        {
            return new EContextButton[] {
                EContextButton.ACTIVATE
            };
        }
    }
}
