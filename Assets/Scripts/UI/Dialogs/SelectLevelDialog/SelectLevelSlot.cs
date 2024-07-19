using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace UI.Dialogs
{
    public class SelectLevelSlot : MonoBehaviour
    {
        [SerializeField] private Button _levelClickedButton;
        [SerializeField] private TextMeshProUGUI _maxScoreText;
        [SerializeField] private TextMeshProUGUI _levelText;

        public void Init(LevelData levelData)
        {
            _levelClickedButton.onClick.AddListener(() =>
            {
                PlayerPrefs.SetInt(StringConstants.CURRENT_LEVEL, (levelData.ID));
                SceneManager.LoadScene(StringConstants.MAIN_SCENE_NAME);
            });
            _levelText.text = (levelData.ID + 1).ToString();

            var scoreController = ServiceLocator.Current.Get<ScoreController>();
            _maxScoreText.text = "Max score:" + scoreController.GetMaxScore(levelData.ID);
        }
    }
}