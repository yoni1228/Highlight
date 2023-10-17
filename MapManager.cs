using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager : MonoBehaviour
{


    public float posx, posy;
    static MapManager _uniqueInstance;
    public Transform tr;
    public int sizeX = 11;
    public int sizeY = 10;
    public float initLocateX = 0;
    public float initLocateY = 0;
    public int initposx = 0;
    public int initposy = 0;
    public float initSizeX = 0.3f;
    public float initSizeY = 0.3f;
    public int _boxCount = 0;
    public int _stageNum;
    [SerializeField] Sprite[] _tileSp;

    [SerializeField] GameObject _box;

   

  
    List<MapLayer> _tileList = new List<MapLayer>();
    public static MapManager _Instance
    {
        get { return _uniqueInstance; }
    }

    private void Awake()
    {
       
        _uniqueInstance = this;


        InitTile(LoadStage());
      

    }

    public baseStage LoadStage()
    {
       

        Debug.Log(Managers.Game.SaveData.Stage);
        return new stage1();
    }
    
    public void ReStart()
    {
        InitTile(LoadStage());
    }
    
    public baseStage LoadReStage()
    {
        return new Restage();
    }

    public void LoadRe()
    {
        InitTile(LoadReStage());
    }
    //public void  readyforStage(int stage)
    //{
    //    if (stage == 1)
    //    {
    //        InitTile(new stage1());
    //        _stageNum = 1;
    //    }

    //    else if (stage == 2)
    //    {
    //        InitTile(new stage2());
    //        _stageNum = 2;
    //    }

    //}
    public MapLayer GetLayerByCoord(int x, int y)
    {
        if (x < 0 || x >= sizeX)
            return null;
        if (y < 0 || y >= sizeY)
            return null;
       
        return _tileList[y * sizeX + x]; 

    }


    

    public bool InitTile(baseStage stage)
    {
        List<List<int>> _DATA = stage.GetData(); 
//        int[,] data = stage.GetData();
        initposx = stage.GetPosX();
        initposy = stage.GetPosY();
        initSizeX = stage.GetSizeX();
        initSizeY = stage.GetSizeY();
        _boxCount = stage.GetBoxCount();

        sizeX = _DATA[0].Count;// .GetLength(1); //-: 열의크기 
        sizeY = _DATA.Count;// GetLength(0);

        Debug.Log("sizex" + sizeX);
        Debug.Log(sizeY);
        int h = _DATA[0][1];//data[0, 1];
        //initLocateX = (sizeX - 1) * -5;
        //initLocateY = (sizeY - 2) * 5;
        //tr.localPosition = new Vector3((sizeX - 1) * -50, (sizeY - 1) * 50, 0);
        tr.localPosition = new Vector3((sizeX) * -0.27f, (sizeY) * 0.55f, 0); // 부모 오브젝트 초기 위치 
        int makeCount = sizeX * sizeY;
        //for (int i = 0; i < makeCount; i++)
        //{ 
        //    GameObject obj = new GameObject("layer");
        //   // obj.gameObject.
        //    obj.transform.parent = tr;
        //    //MapLayer _la = obj.AddComponent<MapLayer>();
        //    //_tileList.Add(_la);


        //}

        for (int o = 0; o < sizeY; o++)
        {
            for (int k = 0; k < sizeX; k++)
            {
                {
                    GameObject obj = new GameObject("layer");
                    
                    // obj.gameObject.
                    obj.transform.parent = tr;

                    MapLayer _la = obj.AddComponent<MapLayer>();
                    _la.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f); // 1에서 0.333f만큼 줄이기 
                    _tileList.Add(_la);
                    //                MapLayer _la = GetLayerByCoord(k, o);
                    //                _la.name = $"{k} {o}";

                    // _la.x = k;
                    // _la.y = o;
                    //_la._tileType = (EnumManager.eTileType)data[k, o];
                    try
                    {

                        _la.Init(_DATA[o][k], k, o);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"error : {k}, {o}");

                    }

                    //_la.Init(data[k, o], k, o);
                    //_la.Init
                    _la.ChangeLocal();
                    _la.SelectTileType();

                    for (int i = 0; i <= sizeY; i++)
                    {
                        if (_DATA[o][k] == 1 || _DATA[o][k] == 3 ) 
                        {
                            _la.transform.GetComponent<SpriteRenderer>().sortingOrder = -1;
                        }
                        else if (_DATA[o][k] == 0 && o == i)// 타일 특성이 0이고 y 값이 i
                        {
                            _la.transform.GetComponent<SpriteRenderer>().sortingOrder = i * 5;
                        }
                   


                    }
             



                }
            }

        }

        return true;

    }

    public bool isMoved(int x, int y)
    {
        return true;
    }
    private void Start()
    {
        ////_layer = GameObject.Find("MapManager").GetComponent<MapLayer>();
        //// MoveableTileLocate();


        
        //tr.localPosition = new Vector3((sizeX - 1) * -50, (sizeY - 1) * 50, 0);



        //// for (int i = 0; i < 4; i++)
        //{

        //    for (int o = 0; o < sizeY; o++)
        //    {
        //        for (int k = 0; k < sizeX; k++)
        //        {
                   
        //            GameObject obj = new GameObject("layer");
        //            obj.transform.parent = tr;
        //            MapLayer _la = obj.AddComponent<MapLayer>();

        //            _la.x = k;
        //            _la.y = o;
        //            _la._tileType = k;
                        
        //        }

        //    }

        //   // obj.transform.position = new Vector3(_la.x, _la.y, 0);
        //   // transform.localScale = new Vector3(sizeX, sizeY, 1);

        //}


        //GameObject obj1 = new GameObject("1");
        //obj1.transform.parent = transform;
        //MapLayer ll = obj1.AddComponent<MapLayer>();

        ////  obj1.transform.position = new Vector3(0, 0, 0);

        //GameObject obj2 = new GameObject("2");
        //obj2.transform.parent = transform;
        //obj2.AddComponent<MapLayer>();
        ////obj2.transform.position = new Vector3(1, 0, 0);

        //GameObject obj3 = new GameObject("3");
        //obj3.transform.parent = transform;
        //obj3.AddComponent<MapLayer>();
        //// obj3.transform.position = new Vector3(0, 1, 0);

        //GameObject obj4 = new GameObject("4");
        //obj4.transform.parent = transform;
        //obj4.AddComponent<MapLayer>();
        //// obj4.transform.position = new Vector3(1, 1, 0);
    }

  
    public Sprite GetSpriteTrans(EnumManager.eTileType type)
    {
        return _tileSp[(int)type];
    }
    


}
/*
 * 인터페이스는 상속 받아 이용하며 인터페이스 내에 모든 자식들을 구현해야 함 
 * 구조, 형태를 만들고 싶을 때 사용
 * 다중 상속이 가능하다.
 */
public interface baseStage
{


    //int[,] GetData();
    List<List<int>> GetData();
    int GetPosX();
    int GetPosY();
    float GetSizeX();
    float GetSizeY();
    int GetBoxCount();


}



public class stage1 : baseStage
{
    //public int[,] GetData()
    //{
    //    return tb;
    //}

    public int _stageNum;
    public string empty;
    public int Posx;
    public int Posy;
    public int BoxCount;
    public float SizeX;
    public float SizeY;
    public List<Dictionary< string, object>> _stageInfo;

    public stage1() //생성자 : 변수에 값을 삽입할 때 사용
    {
        _stageNum = Managers.Game.HighestStage;
        empty = _stageNum.ToString();
        _stageInfo= CSVReaderForStage.Read("stage" + empty);

        Posx = (int)_stageInfo[0]["PosX"];// Int32.Parse(_PosX);
        Posy = (int)_stageInfo[0]["PosY"];
        BoxCount = (int)_stageInfo[0]["BoxCount"];

        Debug.Log("BOX=" + BoxCount);
        SizeX = (float)_stageInfo[0]["SizeX"];
        SizeY = (float)_stageInfo[0]["SizeY"]; //y Board Size


    }

    public List<List<int>> GetData()
    {
        List<List<int>> data = CSVReader.Read(empty);
        
        return data;
    }
    public int GetPosX()
    {
        return Posx;
    }
    public int GetPosY()
    {
        return Posy;
    }

    public int GetBoxCount()
    {
        return BoxCount;
    }

    public float GetSizeX()
    {
        return SizeX;
    }
    public float GetSizeY()
    {
        return SizeY;
    }
    //public List<Dictionary<string, object>> _stageInfo = CSVReaderForStage.Read("stage"+empty);
    
    //public int Posx = (int)_stageInfo[0]["PosX"];// Int32.Parse(_PosX);
    //public int Posy = (int)_stageInfo[0]["PosY"];
    //public int BoxCount = (int)_stageInfo[0]["BoxCount"];
    //public float SizeX = (float)_stageInfo[0]["SizeX"];
    //public float SizeY = (float)_stageInfo[0]["SizeY"]; //y Board Size

    //public int Posx = 2;// Int32.Parse(_PosX);
    //public int Posy = 2;
    //public int BoxCount = 2;
    //public float SizeX = 0.5f;
    //public float SizeY = 0.5f; //y Board Size


    //List<int[,]> _list = new List<int[,]>(); //파일에 있는 맵 정보를 불러옴 파일 이름 01, 02, 03... 01을 읽어서 리스트에 넣기
    //



    //private int[,] tb = new int[,]
    //{

    //    { 4, 4, 4, 4, 4, 4 , 4},
    //    { 4, 0, 0, 0, 0, 0, 4 },
    //    { 0, 0, 1, 1, 0, 0, 0 },
    //    { 0, 2, 3, 1, 3, 2, 0 },
    //    { 0, 0, 1, 1, 1, 0, 0 },
    //    { 4, 0, 0, 0, 0, 0, 4 },
    //    { 4, 4, 4, 4, 4, 4 ,4 },


    //};
}

public class Restage : baseStage
{
    public int _stageNum;
    public string empty;
    public int Posx;
    public int Posy;
    public int BoxCount;
    public float SizeX;
    public float SizeY;
    public List<Dictionary<string, object>> _stageInfo;

    public Restage() //생성자 : 변수에 값을 삽입할 때 사용
    {

        _stageNum = Managers.Game.HighestStage -1;
        empty = _stageNum.ToString();
        _stageInfo = CSVReaderForStage.Read("stage" + empty);

        Posx = (int)_stageInfo[0]["PosX"];// Int32.Parse(_PosX);
        Posy = (int)_stageInfo[0]["PosY"];
        BoxCount = (int)_stageInfo[0]["BoxCount"];

        Debug.Log("BOX=" + BoxCount);
        SizeX = (float)_stageInfo[0]["SizeX"];
        SizeY = (float)_stageInfo[0]["SizeY"]; //y Board Size


    }

    public List<List<int>> GetData()
    {
        List<List<int>> data = CSVReader.Read(empty);

        return data;
    }
    public int GetPosX()
    {
        return Posx;
    }
    public int GetPosY()
    {
        return Posy;
    }

    public int GetBoxCount()
    {
        return BoxCount;
    }

    public float GetSizeX()
    {
        return SizeX;
    }
    public float GetSizeY()
    {
        return SizeY;
    }
}

//}


