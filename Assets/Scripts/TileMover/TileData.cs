using UnityEngine;
using System.Collections.Generic;

public class TileData : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tiles;
    [SerializeField] private float _tileLength;
    [SerializeField] private float _parallaxKoef = 1f;

    public void Move(float speedKoef)
    {
        speedKoef *= _parallaxKoef;
        foreach (var tile in _tiles)
        {
            tile.transform.Translate(Vector3.down * (Time.deltaTime * speedKoef));

            if (tile.transform.position.y <= -1 * _tileLength)           
                tile.transform.Translate(Vector3.up * (_tileLength * _tiles.Count));            
        }
    }
}
