using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class DialougeSystem : ScriptableObject
{
    [TextArea(10, 14)] public string speechText;
    public int speechNum;

    [SerializeField] DialougeSystem[] nextStates;
    // Start is called before the first frame update

    public string GetStateStory()
    {
        return speechText;
    }

    public DialougeSystem[] GetNextStates()
    {
        return nextStates;
    }
}
