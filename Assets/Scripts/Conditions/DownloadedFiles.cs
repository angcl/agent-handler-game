using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DownloadedFiles : ICondition
{
    private ResourceManager resourceManager;
    private BuildingClickable buildingClickable;

    public bool Randomize()
    {
        if (resourceManager == null)
            resourceManager = GameObject.FindObjectOfType<ResourceManager>();

        List<BuildingClickable> availableBuildings = new List<BuildingClickable>();
        foreach (var b in resourceManager.buildings)
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
        return buildingClickable.timeForFileDownload * 1.6f;
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