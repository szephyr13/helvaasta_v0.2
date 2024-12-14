using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolSO : ScriptableObject
{
    [SerializeField] private bool introTrigger;
    [SerializeField] private bool tutorialTrigger;
    [SerializeField] private bool didNemoInform;

    public bool IntroTrigger
    {
        get { return introTrigger; }
        set { introTrigger = value; }
    }
    public bool Telephone
    {
        get { return tutorialTrigger;  }
        set { tutorialTrigger = value; }
    }
    public bool NemoInform
    {
        get { return didNemoInform; }
        set { didNemoInform = value; }
    }
}
