using UnityEngine.UI;

public sealed class PlayerHUDView : HUDView
{
    private bool _isDead;

    public Text ScoreValue;

    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    public void SetScreenActive(bool flag)
    {
        gameObject.SetActive(flag);
    } 

    public int GetScore()
    {
        string stringScoreValue = ScoreValue.text;
        int intScoreValue;
        bool tryGetValue = int.TryParse(stringScoreValue, out intScoreValue);

        return tryGetValue ? intScoreValue : -1;
    }

    public void SetScore(int? value)
    {
        if (value != null && value > 0)
        {
            var oldScoreValue = GetScore();
            ScoreValue.text = (oldScoreValue + value).ToString();
        }
    }
}
