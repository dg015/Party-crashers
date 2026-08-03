using UnityEngine;

[CreateAssetMenu(fileName ="TaskScriptableObject", menuName ="ScriptableObjects")]
public class TaskScriptableObject : ScriptableObject
{
    public string taskName;
    [TextArea(2, 5)]
    public string description;
    public TaskType taskType;

    public int scoreValue;
    [Range(1,5)] public int difficulty;
    public int timeDuration;

    public Sprite taskIcon;
}
