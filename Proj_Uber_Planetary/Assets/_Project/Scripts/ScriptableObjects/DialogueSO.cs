﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Create Dialogue", order = 1)]
public class DialogueSO : ScriptableObject
{
    private int _index;

    [TextArea(3,10)]
    public string[] lines;

    

}
