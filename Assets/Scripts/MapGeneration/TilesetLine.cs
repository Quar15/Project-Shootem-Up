using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesetLine : MonoBehaviour
{
    public EndlessScrollingManager endlessScrollingManager;
    private Transform[] _tilesPrefabs;
    private List<Tile> _spawnedTiles;
    [SerializeField] private Transform[] _tilesTransforms;

    public void Init(TilePrefabsManager tilePrefabsManager)
    {
        _tilesPrefabs = tilePrefabsManager.tilePrefabs.ToArray();
        _spawnedTiles = new List<Tile>();
        foreach (Transform tilePrefab in _tilesPrefabs)
        {
            for(int i=0; i<29; i++)
            {
                SpawnTile(tilePrefab, i, false);
            }
        }
        
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
        foreach (Transform tTransform in _tilesPrefabs)
        {
            Tile t = tTransform.GetComponent<Tile>();
            if(t.CanBeSet(tileData))
                avTiles.Add(t);
        }

        int rng = Random.Range(0, avTiles.Count);
        if(avTiles.Count == 0)
        {
            Debug.Log("rng: " + rng + ", avTiles.Count: " + avTiles.Count);
            tileData.LogTileData();
            return _spawnedTiles[0];
        }
        
        Tile tileToReturn = avTiles[rng];

        foreach (Tile t in _spawnedTiles)
        {
            if(!t.positionSet && t.id == tileToReturn.id)
                return t;
        }

        return _spawnedTiles[SpawnTile(tileToReturn.GetTransform())];
    }

    public Vector3 GetTilesetPosition() { return transform.position; }
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

        tempTile.SetActive(true);
    }

    private int SpawnTile(Transform tilePrefab)
    {
        // Spawn tilePrefab
        Transform tempTileTransform = Instantiate(tilePrefab, new Vector3(0, -10f, -50f), transform.rotation) as Transform;
        //Add new tile to spawnedTiles list
        Tile tempTile = tempTileTransform.GetComponent<Tile>();
        tempTile.SetActive(false);
        _spawnedTiles.Add(tempTile);
        return (_spawnedTiles.Count - 1);
    }

    private int SpawnTile(Transform tilePrefab, int tileIndex, bool isVisible = true)
    {
        // Spawn tilePrefab
        Transform tempTileTransform = Instantiate(tilePrefab, new Vector3(0, -10f, -50f), transform.rotation) as Transform;
        // Set position
        tempTileTransform.parent = _tilesTransforms[tileIndex];
        if(isVisible)
            tempTileTransform.localPosition = new Vector3(0f, 0f, 0f);
        else
            tempTileTransform.localPosition = new Vector3(0, -10f, -50f);
        
        //Add new tile to spawnedTiles list
        Tile tempTile = tempTileTransform.GetComponent<Tile>();
        _spawnedTiles.Add(tempTile);
        return (_spawnedTiles.Count - 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayZoneEnd")
        {
            foreach (Tile t in _spawnedTiles)
            {
                t.positionSet = false;
                t.GetTransform().localPosition = new Vector3(0, -10f, -50f);
                t.SetActive(false);
            }
            
            endlessScrollingManager.UpdateTilesetLine(this);
        }
    }
}
