using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStoryManager : MonoBehaviour
{
    public static GameStoryManager Instance;
    
    // reset setting
    public bool allowReset = true; // disable in scenes where auto reset is not allowed like the lake scene
    public bool wasDead = false;
    
    // visited level
    public bool visitedCity1 = false;
    public bool visitedControlRoom = false;
    public bool visitedReactor = false;

    // people talked to in city 1
    private int peopleTalkedCount = 0;
    private List<string> spokenPeopleIDs;
    
    // evidence in bad guy house
    private bool playerHasAxe = false;
    private bool newspaperFound = false;
    private bool powerPlanFound = false;

    // counter clock
    private static int[] startTime = new int[] { 8, 29, 45 }; // hh, mm, ss
    private int[] timeTillBang;


    public void SubtractFromClockTime(int hours, int minutes, int seconds)
    {
        float timeLeft = ClockCounter.IntsToTime(timeTillBang);
        float substractTime = ClockCounter.IntsToTime(new[] { hours, minutes, seconds });
        timeLeft -= substractTime;
        int[] timeArray = ClockCounter.TimeToIntArray(timeLeft);
        timeTillBang = timeArray;
    }
    
    public void SetClockTime(int hours, int minutes, int seconds)
    {
        timeTillBang[0] = hours;
        timeTillBang[1] = minutes;
        timeTillBang[2] = seconds;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found instance of GameStoryManager");
        }
        else
        {
            Debug.LogWarning("Will create instance of GameStoryManager");
            Instance = this;
            timeTillBang = startTime;
            DontDestroyOnLoad(gameObject);
        }
        spokenPeopleIDs = new List<string>();
    }

    public int[] GetTimeTillBang()
    {
        return timeTillBang; // hh, mm, ss
    }

    public void SoftResetToCity1Values() // player died in level and was reset to city 1
    {
        Debug.Log("soft reset to city 1 values");
        
        wasDead = true;
        visitedCity1 = true;
        
        peopleTalkedCount = 0;
        spokenPeopleIDs = new List<string>();
        
        playerHasAxe = false;
        newspaperFound = false;
        powerPlanFound = false;
        
        timeTillBang = startTime;
    }
    
    public void SoftResetToCity2Values() // player died in level and was reset to city 2
    {
        Debug.Log("soft reset to city 2 values");

        wasDead = true;
        visitedCity1 = true;
        
        peopleTalkedCount = 0;
        spokenPeopleIDs = new List<string>();
        
        playerHasAxe = false;
        newspaperFound = false;
        powerPlanFound = false;
        
        timeTillBang = startTime;
    }
    
    public void SetManagerValuesToAgainCity1() // back from control room
    {
        Debug.Log("reset to again city 1 values");

        visitedCity1 = true;
        
        peopleTalkedCount = 0;
        spokenPeopleIDs = new List<string>();
        
        playerHasAxe = false;
        newspaperFound = false;
        powerPlanFound = false;
    }
    
    public void HardResetValues() // restart the game
    {
        Debug.Log("hard reset all values");

        wasDead = false;
        
        visitedCity1 = false;
        visitedReactor = false;
        visitedControlRoom = false;
        
        peopleTalkedCount = 0;
        spokenPeopleIDs = new List<string>();
        
        playerHasAxe = false;
        newspaperFound = false;
        powerPlanFound = false;
        
        timeTillBang = startTime;
    }

    public void HardResetToStartLevel()
    {
        HardResetValues();
        LevelManager.Instance.LoadScene("02_Wasteland"); // change here the start level for the reset
    }

    public bool CheckAllEvidenceFound()
    {
        if (playerHasAxe && newspaperFound && powerPlanFound)
        {
            Debug.Log("all evidence found");
            return true;
        }

        return false;
    }

    public void AxeWasFound()
    {
        playerHasAxe = true;
        Debug.Log("playerHasAxe true");
    } 
    
    public void PaperWereFound()
    {
        newspaperFound = true;
        Debug.Log("newspaperFound true");
    } 
    
    public void PowerPlanWasFound()
    {
        powerPlanFound = true;
        Debug.Log("powerPlanFound true");
    } 

    public void CountUpPeopleTalkedTo(string objectID)
    {
        Debug.Log("people spoken to count: " + peopleTalkedCount);
        if (!spokenPeopleIDs.Contains(objectID))
        {
            spokenPeopleIDs.Add(objectID);
            peopleTalkedCount += 1;
            Debug.Log("talked to count: " + peopleTalkedCount);
            if (peopleTalkedCount == 4)
            {
                Debug.Log("talked to 4 People");
                peopleTalkedCount = 0;
                StartDialogueWithDelay(2, DialogueType.CityChoices);
            }
        }
    }
    
    public void StartDialogueWithDelay(int delay, DialogueType dialogueType)
    {
        StartCoroutine(DisplayDialogue(delay, dialogueType));
    }
    
    private IEnumerator DisplayDialogue(int delay, DialogueType dialogueType)
    {
        yield return new WaitForSeconds(delay);
        DialogueManager.Instance.setDialogueType(dialogueType);
        DialogueManager.Instance.StartNewDialogue();
    }

}
