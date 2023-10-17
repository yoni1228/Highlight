using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : SokobanManager
{
    bool isLeftPressed, isRightPressed, isUpPressed, isDownPressed;
    string _nowDirection;
    MapManager _mapManager;
    public GameObject _boxPrefab;
    public GameObject _goalPrefab;
    public GameObject _wallPrefab;
    int _heartCount = 0;
    int count = 0;
    List<TreasureBox> _boxList = new List<TreasureBox>();
   // Queue _playerLocation = new Queue();
    TreasureBox _box;
    MonsterAnimation _ani;
    MainSceneManager _main;

    int a;

    string _currentDir;
    Vector2 _initPos;
    int _stepCount = 0;
    [SerializeField]GameObject _GoalBoxAni1;
    [SerializeField] GameObject _GoalBoxAni2;
    [SerializeField] Text _stepText;
    Animator _BoxSuccess;


    StageData _stageData;

    public override void Start()
    {
        base.Start();
        _ani = FindObjectOfType<MonsterAnimation>();
        _mapManager = _sokobanManager.GetComponent<MapManager>();
        _main = _sokobanManager.GetComponent<MainSceneManager>();      
         isLeftPressed = false; 
        isRightPressed = false; 
        isUpPressed = false; 
        isDownPressed = false;
        InitSetData();
        InitBox();

        //_Player.posx = _mapManager.posx;
        //Debug.Log(_mapManager.posx);
        //_Player.posy = _mapManager.posy;

        _GoalBoxAni1.SetActive(false);
        _GoalBoxAni2.SetActive(false);

    }
    public void Update()
    {
        
    }

    public void InitSetData()
    {

        _Player.posx = MapManager._Instance.initposx; //맵 매니저에 있는 플레이어 pos를 데려옴 
        _Player.posy = MapManager._Instance.initposy;

        _Player.GetComponent<SpriteRenderer>().sortingOrder = 20;

       // _Player.transform.localScale = new Vector3(MapManager._Instance.initSizeX, MapManager._Instance.initSizeY);

        //UpdatePlayerPos();

        //박스 초기화 


        Debug.Log($"x : {_Player.posx}, y : {_Player.posy}");

    }

    //private void UpdatePlayerPos()
    //{
    //    MapLayer _layer = _mapManager.GetLayerByCoord((int)_Player.posx, (int)_Player.posy);
    //    if (_layer == null)
    //        return;

        
    //    //Debug.Log(_layer._tileType);

    //    //if (_layer._tileType == 1)
    //    //  {
    //    _Player.targetPosition = _layer.transform.localPosition;// new Vector3(_Player.posx - 1, _Player.posy);
    //    //_playerLocation.Enqueue(_Player.targetPosition);
    //    Debug.Log($"{_Player.targetPosition.x},{_Player.targetPosition.y} {_layer.name}");
    //    //}
    //}

    /*
    * 가기전에 갈 위치의 맵 레이어를 가져온다.
    * 가져온 맵 레이어가 이동가능하면 플레이어도 이동
    * 이동 불가능이면 플레이어는 아무것도 안한다.
    * 
    */

    public int CheckMove(int x, int y)
    {

        int _firstMovingX = (int)_Player.posx + x;
        int _firstMovingY = (int)_Player.posy + y;

        

        MapLayer _layer = _mapManager.GetLayerByCoord(_firstMovingX, _firstMovingY);
        if (_layer == null)
            return 0;
        Debug.Log($" type : {_layer.TileType}  name : {_layer.name}");


        if (_layer.TileType == 0)
        {
            return 0;
        }

        if(getBox(_firstMovingX, _firstMovingY) != null)
        {
            int _secondMovingX = (int)_Player.posx + x + x;
            int _secondMovingY = (int)_Player.posy + y + y;

            MapLayer _secondlayer = _mapManager.GetLayerByCoord(_secondMovingX, _secondMovingY);

            if(_secondlayer.TileType == 0 )
            {
                return 0;
            }

            if(getBox(_firstMovingX, _firstMovingY) == null)
            {
                return 0;
            }

            return 2; //박스와 같이 이동
        }
 

        return 1; //고양이만 이동 


    }

    bool CheckGoal()
    {
        for(int i = 0; i < _boxList.Count; i++)
        {
            TreasureBox tr = _boxList[i];
            MapLayer _glayer = _mapManager.GetLayerByCoord((int)tr.posx, (int)tr.posy);
            if (_glayer.PropType != EnumManager.eTilePropType.Goal)
            {
                return false;
                
            }
                   

        }

        return true;
    }
    public bool Updatesss(int x, int y)
    {  
        Debug.Log(_Player.posx);
       
        MapLayer _layer = _mapManager.GetLayerByCoord((int)_Player.posx + x, (int)_Player.posy + y);
        MapLayer _layer2 = _mapManager.GetLayerByCoord((int)_Player.posx + x + x, (int)_Player.posy + y + y);
        if (_layer == null)
            return false;
        Debug.Log($" type : {_layer.TileType}  name : {_layer.name}");


        //if (_layer.TileType == 0)
        //{
        //    return false;
        //}

        //int checkMovingNum = CheckMove(x, y);

        //if (checkMovingNum == 1)
        //{
        //    _Player.posx += x;
        //    _Player.posy += y;

        //    return true;
        //}
        //if(checkMovingNum == 2)
        //{
        //    _Player.posx += x;
        //    _Player.posy += y;


        //    int _secondMovingX = (int)_Player.posx + x + x;
        //    int _secondMovingY = (int)_Player.posy + y + y;

        //    TreasureBox box2 = FindBoxByPosition(_layer._x, _layer._y);
        //    MapLayer _layer3 = _mapManager.GetLayerByCoord(box2.posx + x, box2.posy + y);
        //    if (_layer3 == null)
        //    {

        //        return false;
        //    }
        //    box2.posx += x;
        //    box2.posy += y;


        //}

        //return true;


        //UpdatePlayerPos();
        if (_layer.TileType == 0)
        {
            //_Player.posx -= x;
            //_Player.posy -= y;
            //Debug.Log("큼큼");

            return false;
        }

        _Player.posx += x;
        _Player.posy += y;


        TreasureBox box = FindBoxByPosition(_layer._x, _layer._y);
        TreasureBox box2 = FindBoxByPosition(_layer2._x , _layer2._y);
        if (box == null)
            return false;
    
            

      

        //if(box.posy == box2.posy && box.posy == box2.posy)
        //{
        //    return false;
        //}
        //if (_layer2 == null)
        //{

        //    return false;
        //}
     
        if (_layer2.TileType == 0)
        {
            _Player.posx -= x;
            _Player.posy -= y;
            //UpdatePlayerPos();
            Debug.Log("흠흠");
            return false;
        }


        if (box != null)
        {
            if (box2 != null)
            {
                _Player.posx -= x;
                _Player.posy -= y;
                UpdateBoxPosition();
                return false;
            }

     

            box.posx += x;
            box.posy += y;
            UpdateBoxPosition();
           

        


            if (_layer2.PropType == EnumManager.eTilePropType.Goal)
            {
            
                Debug.Log("골인!");
              
                UpdateBoxPosition();
                //해당 레이어 
                Vector3 pos = _layer2.transform.position;
                GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/FX001_01"), pos, Quaternion.identity);
                Destroy(obj, 0.5f);
                //_GoalBoxAni1.SetActive(true);
                //_GoalBoxAni2.SetActive(true);

                if (CheckGoal() == true)
                {
                    Debug.Log("게임 종료");
                    TreasureBox._Instance.OpenBox();
                    SoundManager._instance.PlaySFXSound(EnumManager.eSFXClipType.Clear);
                    GameObject go = Instantiate(ResourcePoolManager._instance.GetUIPrefabFromType(EnumManager.eUIWindowType.ClearWnd));
                    GameClear();
                  



                }
            }
        }
        else
        {
          
            return false;

        }

        return true;

    }
    public void GameClear()
    {
       
        GameObject go = Instantiate(ResourcePoolManager._instance.GetUIPrefabFromType(EnumManager.eUIWindowType.ClearWnd));

        //클리어 시 돈 보상
        Managers.Game.GetStageCoin(10);

        //클리어 시 스테이지 업
       // Managers.Game.CheckHighestStage();

        Managers.Game.SaveGame();
        
    }
    public void isBoxPossibleToMove(int x, int y)
    {
        /*
         * 박스가 존재하면 박스 움직임검사
         *  박스가 움직일수있으면 플레이어도 움직임가능
         *  박스가 못움직이면 플레이어도 못움직임
         */

      

    }
    //박스 초기화 

    public void SaveInitBoxPos()
    {
        for (int o = 0; o < _mapManager.sizeY; o++)
        {
            for (int k = 0; k < _mapManager.sizeX; k++)
            {
                MapLayer _layer = _mapManager.GetLayerByCoord(k, o);
                if (_layer.PropType == EnumManager.eTilePropType.Box)
                {

                  

                    _box.transform.parent = _mapManager.tr;

                    _box.initBox(_layer._x, _layer._y);

                    _boxList.Add(_box);



                }




            }

        }
        UpdateBoxPosition();
    }
    public void InitBox()
    {
        int x = _mapManager.sizeX;
        int y = _mapManager.sizeY;

        for (int o = 0; o < _mapManager.sizeY; o++)
        {
            for (int k = 0; k < _mapManager.sizeX; k++)
            {
                MapLayer _layer = _mapManager.GetLayerByCoord(k, o);
                if (_layer.PropType == EnumManager.eTilePropType.Box)
                {

                    _box = Instantiate(_boxPrefab, _layer.transform.position, Quaternion.identity).GetComponent<TreasureBox>();

                    
                    
                    _box.transform.parent = _mapManager.tr;

                    _box.GetComponent<SpriteRenderer>().sortingOrder = 19;

                    _box.initBox(_layer._x, _layer._y);

                   
                    _boxList.Add(_box);

               

                }

                if (_layer.PropType == EnumManager.eTilePropType.Goal)
                {
                    Goal _goal = Instantiate(_goalPrefab, _layer.transform.position, Quaternion.identity).GetComponent<Goal>();
                    _goal.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    _goal.transform.parent = _mapManager.tr;
                }


                if(_layer.PropType == EnumManager.eTilePropType.Wall)
                {
                    GameObject go = Instantiate(_wallPrefab, _layer.transform.position, Quaternion.identity);
                    go.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                }

            }

        }

        //좌표 상의 위치를 실질적 transform 위치로 변환
        UpdateBoxPosition();

    }

    public void InitReBox()
    {
        int x = _mapManager.sizeX;
        int y = _mapManager.sizeY;

        for (int o = 0; o < _mapManager.sizeY; o++)
        {
            for (int k = 0; k < _mapManager.sizeX; k++)
            {
                MapLayer _layer = _mapManager.GetLayerByCoord(k, o);
                if (_layer.PropType == EnumManager.eTilePropType.Box)
                {

                    _box = Instantiate(_boxPrefab, _layer.transform.position, Quaternion.identity).GetComponent<TreasureBox>();



                    _box.transform.parent = _mapManager.tr;

                    _box.GetComponent<SpriteRenderer>().sortingOrder = 20;

                    _box.initBox(_layer._x, _layer._y);


                    _boxList.Add(_box);



                }



            }

        }

        //좌표 상의 위치를 실질적 transform 위치로 변환
        UpdateBoxPosition();

    }

    TreasureBox getBox(int x , int y)
    {
        for (int i = 0; i < _boxList.Count; i++)
        {
            TreasureBox tr = _boxList[i];
            if (tr.posx == x && tr.posy == y)
                return tr;

        }

        return null;
    }

    bool UpdateBoxPosition()
    {

        for (int i = 0; i < _boxList.Count; i++)
        {
            TreasureBox tr = _boxList[i];
            MapLayer _layer = _mapManager.GetLayerByCoord(tr.posx, tr.posy);
            tr.transform.position = _layer.transform.position;

        }

        //_box.GetComponent<SpriteRenderer>().sortingOrder  = a;
        return false;
    }

    
    TreasureBox FindBoxByPosition(int x, int y)
    {
        for(int i = 0; i < _boxList.Count; i++)
        {
            TreasureBox tr = _boxList[i];
            if(tr.posx == x && tr.posy == y)
            {
                return tr;
            }

        }
        return null;
    }

    void DeleteBox()
    {
        for (int i = 0; i < _boxList.Count; i++)
        {
            Destroy(_boxList[i].gameObject);

            Debug.Log("BoxCount" + _boxList.Count);

 
        }

        _boxList.Clear();
    }


    public void isMoved()
    {
        float x = _Player.posx + moveX; 
        float y = _Player.posy + moveY;

         
    }

    #region [버튼 입력]
    public void ClickReButton()
    {
        //_stepCount = 0;
        //_stepText.GetComponent<Text>().text = _stepCount.ToString();
        InitSetData();
        DeleteBox();
        InitReBox();
        _mapManager._boxCount = 0;
        //_heartCount++;
    }

    public void ClickExitButton()
    {
        SceneManager.LoadScene("MainScene");
        SoundManager._instance.PlayBGMSound(EnumManager.eBGMClipType.MainHomeScene);
    }
    public void onLeftClickDown()
    {
        SoundManager._instance.PlaySFXSound(EnumManager.eSFXClipType.Move);
        Updatesss(-1, 0);
        _currentDir = "left";
        
    }

    public void onLeftClickUp()
    {
        isLeftPressed = false;
        _stepCount++;
        //_stepText.GetComponent<Text>().text = _stepCount.ToString();
    }


    public void onRightClickDown()
    {
        SoundManager._instance.PlaySFXSound(EnumManager.eSFXClipType.Move);
        Updatesss(1, 0);
        _currentDir = "right";
    }

    public void onRightClickUp()
    {
        isRightPressed = false;
        _stepCount++;
       // _stepText.GetComponent<Text>().text = _stepCount.ToString();
    }


    public void onUpClickDown()
    {
        SoundManager._instance.PlaySFXSound(EnumManager.eSFXClipType.Move);
        Updatesss(0, -1);
        if (_Player.GetComponent<SpriteRenderer>().sortingOrder < -5)
        {
            _Player.GetComponent<SpriteRenderer>().sortingOrder -= 5;
            a = _Player.GetComponent<SpriteRenderer>().sortingOrder;
        }
       
        _currentDir = "up";
    }

    public void onUpClickUp()
    {
        isUpPressed = false;
        _stepCount++;
        //_stepText.GetComponent<Text>().text = _stepCount.ToString();
    }


    public void onDownClickDown()
    {
        SoundManager._instance.PlaySFXSound(EnumManager.eSFXClipType.Move);
        Updatesss(0, 1);
        if(_Player.GetComponent<SpriteRenderer>().sortingOrder < 20)
        {
            _Player.GetComponent<SpriteRenderer>().sortingOrder += 5;
            a = _Player.GetComponent<SpriteRenderer>().sortingOrder;
        }
      
        _currentDir = "down";
    }

    public void onDownClickUp()
    {
        isDownPressed = false;
        _stepCount++;
        //stepText.GetComponent<Text>().text = _stepCount.ToString();
    }

    #endregion

}
