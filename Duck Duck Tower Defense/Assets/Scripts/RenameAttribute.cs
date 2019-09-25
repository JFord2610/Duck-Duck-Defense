//Credit where credit is due
//https://answers.unity.com/questions/1487864/change-a-variable-name-only-on-the-inspector.html


using UnityEngine;

public class RenameAttribute : PropertyAttribute
{
    public string NewName { get; private set; }
    public RenameAttribute(string name)
    {
        NewName = name;
    }
}