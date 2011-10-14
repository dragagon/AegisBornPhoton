using System;
using System.Collections.Generic;
using CJRGaming.MMO.Common;
using ExitGames.Client.Photon;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class Login : View
{
    public string _username;
    public string _password;

    void Awake()
    {
        Controller = new LoginController(this);
    }

    private LoginController _controller; 

    public override IViewController Controller
    {
        get { return _controller; }
        protected set { _controller = value as LoginController; }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 116, 100, 100), "Userame: ");
        _username = GUI.TextField(new Rect(100, 116, 200, 20), _username, 25);
        GUI.Label(new Rect(10, 141, 100, 100), "Password: ");
        _password = GUI.PasswordField(new Rect(100, 141, 200, 20), _password, '*', 25);
        if (Controller.IsConnected && GUI.Button(new Rect(100, 60, 100, 30), "Logout"))
        {
            Controller.ApplicationQuit();
        }
        if (GUI.Button(new Rect(210, 60, 100, 30), "Send login"))
        {
            _controller.SendLogin(_username, _password);
        }
        GUI.Label(new Rect(10, 180, 100, 100), "" +PhotonEngine.Instance.State);
    }
}
