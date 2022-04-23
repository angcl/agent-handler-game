using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UploadedVirus : ICondition
{
    private BuildingClickable buildingClickable;

    public bool Randomize()
    {
        BuildingClickable[] availableBuildings = GameObject.FindObjectsOfType<BuildingClickable>();

        availableBuildings = availableBuildings.AsQueryable().Where(b => !b.isHacked).ToArray();
        if(availableBuildings.Length == 0){
            return false;
        }

        buildingClickable = availableBuildings[Random.Range(0, availableBuildings.Length)];
        return true;
    }

    public bool Check()
    {
        return buildingClickable.isHacked;
    }

    public float TimeToSolve() {
        return buildingClickable.timeForVirusUpload * 1.5f;
    }

    public ICondition Clone() {
        return (ICondition) this.MemberwiseClone();
    }
    
    public GameObject GetObjectToFocus() {
        return buildingClickable.gameObject;
    }

    public ETaskIcon GetTaskIcon()
    {
        return ETaskIcon.UPLOAD;
    }

}