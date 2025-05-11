using UnityEngine;

public class MovableObjectState : MonoBehaviour, ISaveable
{
    [SerializeField] private string uniqueID = "MovableObject_1";

    public string GetID() => uniqueID;

    public SaveData Save()
    {
        return new SaveData
        {
            id = GetID(),
            position = transform.position
        };
    }

    public void Load(SaveData data)
    {
        if (data.id == GetID())
        {
            transform.position = data.position;
        }
    }
}
