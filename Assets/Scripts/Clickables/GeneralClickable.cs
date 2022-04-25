using UnityEngine;

public abstract class GeneralClickable : MonoBehaviour, IClickable
{
    
    protected Task task;
    protected InfoElement infoElement;

    private WorldSpaceCanvasManager worldSpaceCanvasManager;

    [SerializeField]
    private Vector3 offset = new Vector3(0.0f, 4.0f, 0.0f);

    void Awake()
    {
        worldSpaceCanvasManager = GameObject.FindObjectOfType<WorldSpaceCanvasManager>();
        var collider = gameObject.GetComponent<BoxCollider>();
        var position = new Vector3(transform.position.x + offset.x, collider.bounds.max.y + offset.y, transform.position.z + offset.z);
        infoElement = worldSpaceCanvasManager.CreateElement(position).GetComponent<InfoElement>();
    }

    public abstract void Run(EContextButton contextButton);

    public void SetTask(Task task)
    {
        this.task = task;
        infoElement.SetSelected(true);
        infoElement.SetTaskIcon(task.GetCurrentCondition().GetTaskIcon());
    }

    public bool HasTask()
    {
        return this.task != null;
    }

    public virtual void ResetTask()
    {
        this.task = null;
        infoElement.SetSelected(false);
    }

    public abstract void Reset();

    public abstract EContextButton[] GetContextButtons();

}