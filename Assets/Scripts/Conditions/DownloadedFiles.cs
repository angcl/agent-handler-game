using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DownloadedFiles : ICondition
{
    private BuildingClickable buildingClickable;

    public bool Randomize()
    {
        BuildingClickable[] availableBuildings = GameObject.FindObjectsOfType<BuildingClickable>();

        availableBuildings = availableBuildings.AsQueryable().Where(b => !b.downloadFiles && !b.isDownloaded && !b.uploadVirus).ToArray();
        if(availableBuildings.Length == 0){
            return false;
        }

        buildingClickable = availableBuildings[Random.Range(0, availableBuildings.Length)];
        return true;
    }

    public bool Check()
    {
        return buildingClickable.isDownloaded;
    }

    public float TimeToSolve() 
    {
        return buildingClickable.timeForFileDownload * 1.5f;
    }

    public float ReputationLoss()
    {
        return 0.25f;
    }

    public ICondition Clone() 
    {
        return (ICondition) this.MemberwiseClone();
    }
    
    public GameObject GetObjectToFocus() 
    {
        return buildingClickable.gameObject;
    }

    public ETaskIcon GetTaskIcon()
    {
        return ETaskIcon.DOWNLOAD;
    }
}