using UnityEngine;
using UnityEngine.UI;


namespace UI.Dialogs
{
    public class SettingsDialog : Dialog
    {
        [SerializeField] private Toggle _soundOn;
        [SerializeField] private Button _resetPlayerPrefsButton;
        [SerializeField] private Button _backButton;

        protected override void Awake()
        {
            base.Awake();

            _soundOn.onValueChanged.AddListener(OnSoundOnChanged);
            _resetPlayerPrefsButton.onClick.AddListener(OnResetBtnClicked);
            _backButton.onClick.AddListener(Hide);
        }

        private void OnSoundOnChanged(bool val)
        {
            //
        }

        private void OnResetBtnClicked()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}