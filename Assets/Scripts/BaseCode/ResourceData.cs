using UnityEngine;

[CreateAssetMenu(fileName = "ResourceData", menuName = "ScriptableObjects/ResourceData")]
public class ResourceData : ScriptableObject
{
    public string resourceName;
    public int resourceAmount;
}
