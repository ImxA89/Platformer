using System;
using UnityEngine;

public class GameObjectOfTypeAttribute : PropertyAttribute
{
    public GameObjectOfTypeAttribute(Type requiredType, bool allowSceneObject = true)
    {
        Type = requiredType;
        AllowSceneObject = allowSceneObject;
    }

    public bool AllowSceneObject { get; }
    public Type Type { get; }
}
