using UnityEngine;

public class CharacterCreate : GameView
{
    private CharacterCreateController _characterCreateController;

    bool showErrorDialog = false;
    string characterName = "";
    string sex = "";
    string characterClass = "";

    protected override void Awake()
    {
        base.Awake();

        _characterCreateController = new CharacterCreateController(this);
        if (_engine.StateController != null && _engine.State == GameState.CharacterSelect)
        {
            _engine.StateController = _characterCreateController;
        }
        else
        {
            Application.LoadLevel("Login");
        }

    }

    void OnGUI()
    {

        if (showErrorDialog)
        {

        }

        GUI.Label(new Rect(120, 116, 100, 100), "Name: ");
        characterName = GUI.TextField(new Rect(200, 116, 200, 20), characterName, 25);

        GUI.Box(new Rect(10, 10, 100, 300), "Classes");

        if (GUI.Button(new Rect(20, 50, 80, 50), "Fighter"))
        {
            characterClass = "Fighter";
        }
        if (GUI.Button(new Rect(20, 110, 80, 50), "Mage"))
        {
            characterClass = "Mage";
        }
        if (GUI.Button(new Rect(20, 170, 80, 50), "Rogue"))
        {
            characterClass = "Rogue";
        }
        if (GUI.Button(new Rect(20, 230, 80, 50), "Cleric"))
        {
            characterClass = "Cleric";
        }

        if (GUI.Button(new Rect(150, 170, 80, 50), "Male"))
        {
            sex = "M";
        }
        if (GUI.Button(new Rect(240, 170, 80, 50), "Female"))
        {
            sex = "F";
        }

        if (GUI.Button(new Rect(200, 265, 100, 25), "Create") || (Event.current.type == EventType.keyDown && Event.current.character == '\n'))
        {
            if (!string.IsNullOrEmpty(characterName) && !string.IsNullOrEmpty(sex) && !string.IsNullOrEmpty(characterClass))
            {
                LoginOperations.CreateCharacter(_engine, characterName, sex, characterClass);
            }
        }
    }

    public void AfterCreateCharacter()
    {
        Application.LoadLevel("CharacterSelect");
    }
}
