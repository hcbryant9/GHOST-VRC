﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GameManager : UdonSharpBehaviour
{
    //if is english is true - english , if it is false - korean
    public bool isEnglish;
    public bool shouldFollow = false;

    //for introduction to gma
    public bool firstInteraction = false;

    //for interaction with general
    public bool generalInteraction = false;

    public bool loverInteraction = false;

    public int trashCount = 0;

    public int startCount = 0;
    void Start()
    {
        
    }
}
