using AegisBornCommon.Models;
using UnityEngine;
using System.Collections;

public class CharacterSelect : GameView
{
    private CharacterSelectController _characterSelectController;
    bool _receivedCharacters = false;

    protected override void Awake()
    {
        base.Awake();

        _characterSelectController = new CharacterSelectController(this);

        if (_engine.StateController != null && _engine.State == GameState.Connected)
        {
            _engine.StateController = _characterSelectController;
            LoginOperations.GetCharacters(_engine);
        }
        else
        {
            Application.LoadLevel("Login");
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 225, 400, 100), _engine.State.ToString());

        if (_receivedCharacters)
        {
            GUI.Box(new Rect(300, 10, 100, 300), "Characters");

            int yPos = 50;

            foreach (AegisBornCharacter character in _characterSelectController.Characters)
            {

                if (GUI.Button(new Rect(310, yPos, 80, 50), character.Name))
                {
                    //new SelectCharacterMessage(smartFox, false, character.ID).Send();
                }

                yPos += 60;
            }

            if (GUI.Button(new Rect(100, 165, 100, 25), "New Character") || (Event.current.type == EventType.keyDown && Event.current.character == '\n'))
            {
                //Application.LoadLevel("CharacterCreate");
            }
        }
        if (GUI.Button(new Rect(100, 195, 100, 25), "Back"))
        {
            _engine.Disconnect();
            //LoginOperations.ExitWorld(_engine);
            Application.LoadLevel("Login");
        }
    }

    public void AfterCharacterList()
    {
        _receivedCharacters = true;
    }

    public void AfterCharacterSelected()
    {
        Debug.Log("going to main game");
        //Application.LoadLevel("Game");
    }

}
