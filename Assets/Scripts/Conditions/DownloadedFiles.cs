using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DownloadedFiles : ICondition
{
    private BuildingClickable buildingClickable;

    public bool Randomize()
    {
        BuildingClickable[] allBuildings = GameObject.FindObjectsOfType<BuildingClickable>();

        List<BuildingClickable> availableBuildings = new List<BuildingClickable>();

        foreach (var b in allBuildings)
        {
            if (!b.downloadFiles && !b.isDownloaded && !b.uploadVirus && !b.HasTask())
                availableBuildings.Add(b);
        }

        if(availableBuildings.Count == 0){
            return false;
        }
        buildingClickable = availableBuildings[Random.Range(0, availableBuildings.Count)];
        
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