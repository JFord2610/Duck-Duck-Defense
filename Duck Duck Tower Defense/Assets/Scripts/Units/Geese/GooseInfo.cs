using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Geese/Goose Info")]
public class GooseInfo : ScriptableObject
{
    public string gooseName;
    public GooseStats stats;
    public Sprite sprite;

    public GooseInfo Createcopy()
    {
        GooseInfo newInfo = CreateInstance<GooseInfo>();
        newInfo.gooseName = gooseName;
        newInfo.stats = stats.CreateCopy();
        newInfo.sprite = sprite;
        return newInfo;
    }
}
