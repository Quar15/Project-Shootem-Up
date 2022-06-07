using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesetLine : MonoBehaviour
{
    public EndlessScrollingManager endlessScrollingManager;
    [SerializeField] private Transform[] _tilesPrefabs;
    private List<Tile> _spawnedTiles;
    [SerializeField] private Transform[] _tilesTransforms;

    private void Awake()
    {
        _spawnedTiles = new List<Tile>();
    }

    public TileData[] UpdateTiles(TileData[] tilesData)
    {
        Tile tempTile;
        for(int i=0; i < _tilesTransforms.Length - 1; i++)
        {
            tempTile = GetTile(tilesData[i]);
            SetTilePosition(tempTile, i);
            tilesData[i+1].nx = tempTile.tileData.x;
            tilesData[i].nz = tempTile.tileData.z;
        }

        int lastTileIndex = _tilesTransforms.Length - 1;
        tempTile = GetTile(tilesData[lastTileIndex]);
        SetTilePosition(tempTile, lastTileIndex);
        tilesData[lastTileIndex].nz = tempTile.tileData.z;

        return tilesData;
    }

    private Tile GetTile(TileData tileData)
    {
        List<Tile> avTiles = new List<Tile>();
        foreach(Tile t in _spawnedTiles)
        {
            if(!t.positionSet && t.CanBeSet(tileData))
                return t;
        }

        List<Tile> avTilesToSpawn = new List<Tile>();
        foreach (var tPrefab in _tilesPrefabs)
        {
            Tile t = tPrefab.GetComponent<Tile>();
            if(t.CanBeSet(tileData))
                return _spawnedTiles[SpawnTile(tPrefab)];
        }

        SpawnTile(_tilesPrefabs[0]);
        return _spawnedTiles[0];
    }

    public void SetTilesetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    private void SetTilePosition(Tile tile, int tileIndex)
    {
        // Set position
        Transform tempTileTransform = tile.GetTransform();
        tempTileTransform.parent = _tilesTransforms[tileIndex];
        tempTileTransform.localPosition = new Vector3(0f, 0f, 0f);
        // Set positionSet
        Tile tempTile = tempTileTransform.GetComponent<Tile>();
        tempTile.positionSet = true;
    }

    private int SpawnTile(Transform tilePrefab)
    {
        // Spawn tilePrefab
        Transform tempTileTransform = Instantiate(tilePrefab, new Vector3(0f, 0f, 0f), transform.rotation) as Transform;
        //Add new tile to spawnedTiles list
        Tile tempTile = tempTileTransform.GetComponent<Tile>();
        _spawnedTiles.Add(tempTile);
        return (_spawnedTiles.Count - 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayZoneEnd")
        {
            endlessScrollingManager.UpdateTilesetLine(this);
        }
    }
}
