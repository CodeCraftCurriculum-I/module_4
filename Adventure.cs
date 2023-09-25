
using System.Runtime.CompilerServices;
using Adventure;
using Utils;
using static Utils.IO;


Debug.enabled = true;
Debug.level = Debug.DebugLevel.Verbose;

const int NOT_FOUND = -1;

bool isPlaying = true;

Location currentLocation = Room.Initialize();
Player player = new() { HitPoints = 10, Inventory = new() };

string currentDescription = currentLocation.Description;
ActionSubject currentSubject = null;

// These are actions that are not part of the game, but the application
var basicActions = new Dictionary<string, Action>
{
    ["quit"] = () => isPlaying = false,
    ["exit"] = () => isPlaying = false,
    ["clear"] = () => Clear(),
    ["look"] = () => Print(currentDescription),
    ["restart"] = () => Print("This should restart the game, but it doesn't yet."),
    ["help"] = () => Print("This should print a helpfull message, maybe a list of commands? But it doesn't yet.")
};

while (isPlaying)
{
    // Print the room description 
    // Read the player's input
    // Handle the player's input
    // Update the game state
    // Check for the end of the game

    if (string.IsNullOrEmpty(currentDescription) == false)
    {
        Print(currentDescription);
        currentDescription = "";
    }

    string playerInput = ReadInput();

    Debug.Log($"Player input: {playerInput}", Debug.DebugLevel.Verbose);

    if (basicActions.ContainsKey(playerInput))
    {
        basicActions[playerInput]();
    }
    else
    {
        // find action and subject from players input.
        string[] possibleSubjects = currentLocation.Subjects.Keys.ToArray();
        currentSubject = null;

        foreach (string s in possibleSubjects)
        {
            if (playerInput.IndexOf(s) != NOT_FOUND)
            {
                currentSubject = currentLocation.Subjects[s];
                break;
            }
        }

        if (currentSubject != null)
        {

            Debug.Log($"Subject: {currentSubject.Id}", Debug.DebugLevel.Verbose);

            string action = null;

            foreach (string a in GameActions.AllActions)
            {
                if (playerInput.IndexOf(a) != NOT_FOUND)
                {
                    action = a;
                    break;
                }
            }

            if (action != null)
            {
                Debug.Log($"Action : {action}", Debug.DebugLevel.Verbose);

                // NB Husk at action må kombineres med state for å få riktig effect.
                Effect actionEffect = null;

                if (currentSubject.Actions.ContainsKey(action))
                {
                    actionEffect = currentSubject.Actions[action];
                }

                if (actionEffect != null)
                {
                    actionEffect.Action(currentSubject, playerInput, currentLocation, player, (newLocation) =>
                    {
                        Print($"Moving to {newLocation}");
                    });
                    currentDescription = actionEffect.Description;
                }
                else
                {
                    Print($"Doing {action} with {currentSubject.Id} does nothing");
                }

                // Does the current subject suport this action?
                // If the cirrent subject does not suport this action does it have a default respons?
                // else this does nothing. 
                //currentSubject?.Actions[action].Action(currentSubject, playerInput, currentDescription, player, (newDescription) => currentDescription = newDescription);
            }
            else
            {
                Print("That does nothing");
            }
        }




    }

}


