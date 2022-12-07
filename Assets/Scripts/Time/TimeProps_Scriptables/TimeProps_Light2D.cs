using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "TimeProps_Light2D_File", menuName = "Scriptables/TimeProps/Light2D")]
public class TimeProps_Light2D : TimeProps
{
    [SerializeField] public List<TimeProperties_Light2D> list = new List<TimeProperties_Light2D>();
}


