using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
    [SerializeField] private float floatValue;

    public float Value
    {
        get { return floatValue; }
        set { floatValue = value; }
    }
}
