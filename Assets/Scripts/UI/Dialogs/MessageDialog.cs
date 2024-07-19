using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace UI.Dialogs
{
    public class MessageDialog : Dialog
    {
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _okButton;

        public void Init(string text)
        {
            _messageText.text = text;
            _okButton.onClick.AddListener(Hide);
        }
    }
}
