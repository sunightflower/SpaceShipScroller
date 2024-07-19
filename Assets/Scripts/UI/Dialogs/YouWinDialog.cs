using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine.SceneManagement;


namespace UI.Dialogs
{
    public class YouWinDialog : Dialog
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _goToMenuButton;
        [SerializeField] private TextMeshProUGUI _currentScoreText;

        [SerializeField] private TextMeshProUGUI _maxScoreText;
        [SerializeField] private TextMeshProUGUI _youObtainGoldText;
        private EventBus _eventBus;

        private void Start()
        {
            _nextLevelButton.onClick.AddListener(NextLevel);
            _goToMenuButton.onClick.AddListener(GoToMenu);
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public void Init(int currentScore, int maxScore, int addGoldValue)
        {
            _currentScoreText.text = "Your score: " + currentScore;
            _maxScoreText.text = "Max score: " + maxScore;
            _youObtainGoldText.text = "You received " + addGoldValue + " gold!";
        }

        private void NextLevel()
        {
            _eventBus.Invoke(new NextLevelSignal());
            Hide();
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene(StringConstants.MENU_SCENE_NAME);
            Hide();
        }
    }
}