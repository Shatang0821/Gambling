using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData",menuName = "Data/Entity")]
public class EntityDataSo : ScriptableObject
{
    public EntityData EntityData;
}

[Serializable]
public class EntityData
{
    public float HP;
}