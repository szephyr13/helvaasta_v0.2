using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolSO : ScriptableObject
{
    [SerializeField] private bool introTrigger;
    [SerializeField] private bool tutorialTrigger;
    [SerializeField] private bool didNemoInform;
    [SerializeField] private bool log0300PM;
    [SerializeField] private bool log0320PM;

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
    public bool Log0300PM
    {
        get { return log0300PM; }
        set { log0300PM = value; }
    }
    public bool Log0320PM
    {
        get { return log0320PM; }
        set { log0320PM = value; }
    }
}
