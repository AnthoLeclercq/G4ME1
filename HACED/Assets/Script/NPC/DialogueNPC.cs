using UnityEngine;

[System.Serializable]
public class DialogueNPC
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
