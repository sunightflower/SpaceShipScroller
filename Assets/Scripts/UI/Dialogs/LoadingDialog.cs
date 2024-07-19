using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;
using CustomEventBus.Signals;


namespace UI.Dialogs
{
    public class LoadingDialog : Dialog
    {
        [SerializeField] private TextMeshProUGUI _progressText;
        private EventBus _eventBus;

        private void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<LoadProgressChangedSignal>(LoadProgressChanged);
            _eventBus.Subscribe<AllDataLoadedSignal>(OnAllResourcesLoaded);

            _progressText.text = "LoadingProgress is " + 0 + "%";
        }

        private void LoadProgressChanged(LoadProgressChangedSignal signal)
        {
            RedrawProgress(signal.Progress);
        }

        private void OnAllResourcesLoaded(AllDataLoadedSignal signal)
        {
            Hide();
        }

        private void RedrawProgress(float progress)
        {
            progress *= 100;
            _progressText.text = "LoadingProgress is " + progress + "%";
        }
    }
}