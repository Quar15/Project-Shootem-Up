using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public int x;
    public int y;
    public int z;
    public int nx;
    public int ny;
    public int nz;

    public TileData()
    {
        x = 1;
        y = -1;
        z = 1;
        nx = 1;
        ny = -1;
        nz = 1;
    }

    public TileData(int _x = 0, int _y = -1, int _z = 0, int _nx = 0, int _ny = -1, int _nz = 0)
    {
        x = _x;
        y = _y;
        z = _z;
        nx = _nx;
        ny = _ny;
        nz = _nz;
    }
}

public class Tile : MonoBehaviour
{
    public TileData tileData;
    public bool positionSet;

    public Transform GetTransform(){ return transform; }

    public bool CanBeSet(TileData tileDataToCheck)
    {
        if(tileDataToCheck.nz != 0 && tileDataToCheck.nz != tileData.nz)
            return false;
        if(tileDataToCheck.nx != 0 && tileDataToCheck.nx != tileData.nx)
            return false;

        return true;
    }
}
