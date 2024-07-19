using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace UI.Dialogs
{
    public class PurchaseItemDialog : Dialog
    {
        [SerializeField] private TextMeshProUGUI _textLabel;
        [SerializeField] private Button _yesBtn;
        [SerializeField] private Button _noBtn;

        protected override void Awake()
        {
            base.Awake();

            _yesBtn.onClick.AddListener(() => { Hide(); });
            _noBtn.onClick.AddListener(() => { Hide(); });
        }

        public void Init(string textValue, UnityAction onYesBtnClicked)
        {
            _textLabel.text = textValue;
            _yesBtn.onClick.AddListener(onYesBtnClicked);
        }
    }
}