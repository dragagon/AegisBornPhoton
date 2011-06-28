public class Disconnected : GameStateController
{
    public Disconnected(GameView view) : base(view)
    {
    }

    public override GameState State
    {
        get { return GameState.Disconnected; }
    }
}
