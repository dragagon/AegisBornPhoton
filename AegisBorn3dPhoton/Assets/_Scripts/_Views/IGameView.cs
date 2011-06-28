using System;
using ExitGames.Client.Photon;

public interface IGameView
{
    bool IsDebugLogEnabled { get; }

    void LogDebug(IGameStateController game, string message);

    void LogError(IGameStateController game, string message);

    void LogError(IGameStateController game, Exception exception);

    void LogInfo(IGameStateController game, string message);

    void OnConnect(IGameStateController game);

    void OnDisconnect(IGameStateController game, StatusCode returnCode);
}
