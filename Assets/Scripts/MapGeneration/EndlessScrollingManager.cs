using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessScrollingManager : MonoBehaviour
{
    [Header("Play Area")]
    [SerializeField] private float _playAreaSpeed;
    [SerializeField] private Transform _playArea;

    [Header("Tileset Lines")]
    [SerializeField] private TilePrefabsManager _tilePrefabsManager;
    [SerializeField] private TilesetLine[] _tilesetLines;
    [SerializeField] public Transform startingTransform;
    private Transform currLineTransform;
    private TileData[] currTileData;

    private void Start() 
    {
        _tilePrefabsManager.Init();
        currLineTransform = startingTransform;
        // Setup first line visuals
        currTileData = new TileData[29];
        for(int i=0; i < currTileData.Length; i++)
            currTileData[i] = new TileData();

        
        for (int i = 0; i < _tilesetLines.Length; i++)
        {
            TilesetLine tl = _tilesetLines[i];
            tl.Init(_tilePrefabsManager);
            tl.endlessScrollingManager = this;
            UpdateTilesetLine(tl);
        }
    }

    private void Update()
    {
        _playArea.Translate(Vector3.forward * _playAreaSpeed * Time.deltaTime);
    }

    public void UpdateTilesetLine(int tilesetIndex)
    {
        TilesetLine currTilesetLine = _tilesetLines[tilesetIndex];
        // Update line visuals
        currTileData = currTilesetLine.UpdateTiles(currTileData);
        // Update line position
        currTilesetLine.SetTilesetPosition(currLineTransform.position);
        currLineTransform.position += new Vector3(0f, 0f, 2f);
    }

    public void UpdateTilesetLine(TilesetLine tilesetLine)
    {
        // Update line visuals
        currTileData = tilesetLine.UpdateTiles(currTileData);
        // Update line position
        tilesetLine.SetTilesetPosition(currLineTransform.position);
        currLineTransform.position += new Vector3(0f, 0f, 2f);
    }
}
