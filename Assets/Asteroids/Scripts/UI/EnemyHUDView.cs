public sealed class EnemyHUDView : HUDView
{
    private bool _isDead;

    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    private void Awake()
    {
        ArmorSlider.onValueChanged.AddListener(RemoveIfNullValue);
    }   

    private void RemoveIfNullValue(float value)
    {
        if (value <= 0.01f)
        {
            ArmorSlider.gameObject.SetActive(false);
            ArmorSlider.onValueChanged.RemoveAllListeners();
        }
    }
}
