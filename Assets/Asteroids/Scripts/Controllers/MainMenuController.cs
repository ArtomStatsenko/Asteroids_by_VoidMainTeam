public sealed class MainMenuController
{
    private MainMenuView _view;

    public MainMenuController(MainMenuView view)
    {
        _view = view;
    }

    public void OnEnable()
    {
        _view.OnExitButtonClickEvent += QuitGame;
    }

    public void OnDisable()
    {
        _view.OnExitButtonClickEvent -= QuitGame;
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
