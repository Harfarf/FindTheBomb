using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Data / SaveData / BoolValue", fileName = "NewBoolVariable")]
public class BoolValue : ScriptableObject
{
    public bool InitialValue = false;

    public bool runTimeValue;

    public void ForceFillCondition()
    {
        runTimeValue = true;
    }
    public void SetRunTimeValue(bool newValue)
    {
        runTimeValue = newValue;
    }
}
