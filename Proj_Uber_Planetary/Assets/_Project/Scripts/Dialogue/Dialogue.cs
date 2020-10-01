﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject nextText;

    [SerializeField]
    private int _lineIndex;

    public int LineIndex
    {
        get { return _lineIndex; }

        set { _lineIndex = value; }
    }

    public void QueueNextLine()
    {
        LineIndex ++;
        nextText.SetActive(true);
    }
}
