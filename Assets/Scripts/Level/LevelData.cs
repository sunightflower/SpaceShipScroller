using UnityEngine;
using Interactables;
using Newtonsoft.Json;
using System.Collections.Generic;


[System.Serializable]
public class LevelData
{
    [JsonProperty, SerializeField] private int _id;
    [JsonProperty, SerializeField] private float _levelLength;
    [JsonProperty, SerializeField] private float _startSpeed;
    [JsonProperty, SerializeField] private float _endSpeed;
    [JsonProperty, SerializeField] private int _goldForPass;
    [JsonProperty, SerializeField] private List<InteractableSpawnData> _interactableSpawnData;

    public int ID => _id;
    public float LevelLength => _levelLength;
    public float StartSpeed => _startSpeed;
    public float EndSpeed => _endSpeed;
    public int GoldForPass => _goldForPass;
    public List<InteractableSpawnData> InteractableData => _interactableSpawnData;
}