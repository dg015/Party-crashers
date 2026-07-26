using UnityEngine;

[CreateAssetMenu(fileName ="TaskScriptableObject", menuName ="ScriptableObjects")]
public class Task : ScriptableObject
{


    public string taskName;
    [TextArea(2, 5)]
    public string description;
    public TaskType taskType;

    public int scoreValue;
    [Range(1,5)] public int difficulty;
    private int timeDuration;

    public Sprite taskIcon;
}
