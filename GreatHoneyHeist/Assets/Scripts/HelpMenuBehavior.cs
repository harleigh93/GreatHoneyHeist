﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenuBehavior : MonoBehaviour
{

    public GameObject helpWindow;
    public GameObject queenDialogue;

    public void helpRun()               //Function to activate the help menu, showing it on screen
    {
        helpWindow.SetActive(true);
    }
    public void closeHelp()             //Function to deactivate the help menu, hiding it
    {
        helpWindow.SetActive(false);
    }
    public void closeQueen()             //Function to deactivate the queen dialogue, hiding it
    {
        queenDialogue.SetActive(false);
    }
}
