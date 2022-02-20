public sealed class EndGameMenuModel
{
    public const string WIN_GAME = "<color=#17D133>YOU WIN :)</color>";
    public const string LOSE_GAME = "<color=#D13817>YOU LOSE :(</color>";

    private int _firstPlayerScore;
    private int _secondPlayerScore;

    public int FirstPlayerScore
    {
        get => _firstPlayerScore;
        set => _firstPlayerScore = value;
    }

    public int SecondPlayerScore
    {
        get => _secondPlayerScore;
        set => _secondPlayerScore = value;
    }
}