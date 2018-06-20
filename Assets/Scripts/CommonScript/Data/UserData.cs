using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public static UserData instance = new UserData();
    public Dictionary<string, int> variableDict;//消さない

    private UserData()
    {
        variableDict = SaveManager.LoadVariableDict();
    }
}