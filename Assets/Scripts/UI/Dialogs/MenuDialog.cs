using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace UI.Dialogs
{
    public class MenuDialog : Dialog
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _selectLevelButton;
        [SerializeField] private Button _customizeShipButton;
        [SerializeField] private Button _settingsButton;

        protected void Awake()
        {
            base.Awake();

            _playButton.onClick.AddListener(OnPlayButtonClick);
            _selectLevelButton.onClick.AddListener(OnSelectLevelButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
            _customizeShipButton.onClick.AddListener(OnCustomizeShipButtonClick);
        }

        private void OnPlayButtonClick()
        {
            SceneManager.LoadScene(StringConstants.MAIN_SCENE_NAME);
        }

        private void OnSelectLevelButtonClick()
        {
            DialogManager.ShowDialog<SelectLevelDialog>();
        }

        private void OnCustomizeShipButtonClick()
        {
            DialogManager.ShowDialog<CustomizeShipDialog>();
        }

        private void OnSettingsButtonClick()
        {
            DialogManager.ShowDialog<SettingsDialog>();
        }
    }
}