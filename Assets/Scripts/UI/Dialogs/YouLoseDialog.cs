using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine.SceneManagement;


namespace UI.Dialogs
{
    public class YouLoseDialog : Dialog
    {
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Button _goToMenuButton;
        private EventBus _eventBus;

        private void Start()
        {
            _tryAgainButton.onClick.AddListener(TryAgain);
            _goToMenuButton.onClick.AddListener(GoToMenu);

            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        private void TryAgain()
        {
            _eventBus.Invoke(new RestartLevelSignal());
            Hide();
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene(StringConstants.MENU_SCENE_NAME);
        }
    }
}