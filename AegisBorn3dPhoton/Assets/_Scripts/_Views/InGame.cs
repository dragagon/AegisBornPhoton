using AegisBornCommon.Models;
using UnityEngine;

public class InGame : GameView
{
    private InGameController _inGameController;

    protected override void Awake()
    {
        base.Awake();

        _inGameController = new InGameController(this);

        if (_engine.StateController != null && _engine.State == GameState.CharacterSelect)
        {
            _engine.StateController = _inGameController;
        }
        else
        {
            Application.LoadLevel("Login");
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 225, 400, 100), _engine.State.ToString());
    }

}
