using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Vector3SO : ScriptableObject
{
    [SerializeField] private Vector3 vector;

    public Vector3 Value
    {
        get { return vector; }
        set { vector = value; }
    }
}

