using UnityEngine;
using Newtonsoft.Json;


namespace Interactables
{
    [System.Serializable]
    public class InteractableSpawnData
    {
        [JsonProperty, SerializeField] private InteractableType _interactableType;
        [JsonProperty, SerializeField] private int _interactableGrade;
        [JsonProperty, SerializeField] private float _startCooldown;
        [JsonProperty, SerializeField] private float _endCooldown;
        [JsonProperty, SerializeField] private float _prewarmTime;

        public InteractableType InteractableType => _interactableType;
        public int InteractableGrade => _interactableGrade;     
        public float StartCooldown => _startCooldown;
        public float EndCooldown => _endCooldown;
        public float PrewarmTime => _prewarmTime;
    }
}