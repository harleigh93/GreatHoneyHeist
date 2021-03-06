﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* TEAM 5
* Harleigh Bass, Kimberly Brooks, Emma Kratt
* SCRIPT: HelpMenuBehavior.cs
*/

public class HelpMenuBehavior : MonoBehaviour
{

    public GameObject helpWindow;           //Create gameobjects to show/hide textbox elements
    public GameObject queenBee;
    public GameObject room2text;
    public GameObject room1text;
    public GameObject room3text;
    public GameObject room4text;
    public GameObject textbox;
    public GameObject continueButton;
    public GameObject queencopy;

    public void helpRun()               //Function to activate the help menu, showing it on screen
    {
        helpWindow.SetActive(true);
    }
    public void closeHelp()             //Function to deactivate the help menu, hiding it
    {
        helpWindow.SetActive(false);
    }
    public void closeText()                                     //On continue press (used to close Queen Bee Dialouge), set all possible displayed texts and the textbox to invisible
    {
        textbox.SetActive(false);
        room2text.SetActive(false);
        room1text.SetActive(false);
        room3text.SetActive(false);
        room4text.SetActive(false);
        continueButton.SetActive(false);                        // Hide Continue button as well
        queencopy = GameObject.Find("queenBee(Clone)");
        Destroy(queencopy);                                     //Destroy the displayed queen with text

        // How to end the game if Player is at the very last area
        if (playerBehavior.finishedGame == true)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // quit game in editor
            #else
            Application.Quit(); // if game is built, quit game in window
            #endif
        }
    }
}
