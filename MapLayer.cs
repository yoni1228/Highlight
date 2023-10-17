using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLayer : MonoBehaviour
{
    public int _x = 0, _y = 0;

    public int _isBoxInit = 0;
    public SpriteRenderer _tileImage;
  

    private EnumManager.eTileType _tileType;
    private EnumManager.eTilePropType _propType;
    public Sprite[] _inputSprite;


    public EnumManager.eTileType TileType
    {
        get
        {
            return _tileType;
        }
    }

    public EnumManager.eTilePropType PropType
    {
        get
        {
            return _propType;
        }
    }

    private void Awake()
    {
        _tileImage = gameObject.AddComponent<SpriteRenderer>();
        _tileImage.transform.localScale = new Vector3(1, 1, 1);
        _tileImage.transform.localPosition = new Vector3(0, 0, 0);
    }

    public bool Init(int tiletype, int x, int y)
    {
        _x = x;
        _y = y;

        if (tiletype == 0)
            _tileType = EnumManager.eTileType._Moveable;
        else if (tiletype == 4)
            _tileType = EnumManager.eTileType.None;
        else 
           _tileType = EnumManager.eTileType._Moveunable;
        //==========================================================
        if (tiletype == 2)
            _propType = EnumManager.eTilePropType.Goal;
        else if (tiletype == 3)
            _propType = EnumManager.eTilePropType.Box;
        else if (tiletype == 0)
            _propType = EnumManager.eTilePropType.Wall;

         
       

        
      


        return true;
    }
    public void ChangeLocal()
    {
        _tileImage.transform.localPosition = new Vector3(_x * _tileImage.size.x, - (_y * _tileImage.size.y) * 0.8f);

     

    

    }
    public void SelectTileType()
    {
    
        TileTypeMenu(TileType);
    }
    public void TileTypeMenu(EnumManager.eTileType type)
    {
        switch(type)
        {
            //case EnumManager.eTileType._GoalTile:
            //    _tileImage.sprite = MapManager._Instance.GetSpriteTrans(type);

                //break;
            case EnumManager.eTileType._Moveable:
                _tileImage.sprite = MapManager._Instance.GetSpriteTrans(type);
                break;
            case EnumManager.eTileType._Moveunable:
                _tileImage.sprite = MapManager._Instance.GetSpriteTrans(type);
                break;
            case EnumManager.eTileType.None:
                _tileImage.sprite = MapManager._Instance.GetSpriteTrans(type);
                break;

            default:
                Debug.LogError("정의되지 않았습니다");
                break;
        }
    }



}

