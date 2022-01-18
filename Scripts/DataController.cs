using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    //싱글톤 선언
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if(!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    //게임 데이터 파일 이름 설정
    public string GameDataFileName = "InsomniaData.json";

    //원하는 이름(영문).json
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            //게임이 시작되면 자동으로 실행
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    public void Start()
    {
        LoadGameData();
        SaveGameData();
    }

    //저장된 게임 불러오기
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        //저장된 게임이 있다면
        if(File.Exists(filePath))
        {
            print("불러오기 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }

        //저장된 게임이 없다면
        else
        {
            print("새로운 파일 생성");
            _gameData = new GameData();
        }
    }

    //게임 저장하기
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;

        //이미 저장된 파일이 있다면 덮어쓰기
        File.WriteAllText(filePath, ToJsonData);

        print("저장완료");
        print("1는 " + gameData.isClear1);
        print("2는 " + gameData.isClear2);
        print("3는 " + gameData.isClear3);
        print("4는 " + gameData.isClear4);
    }

    //게임이 종료되면 자동저장되도록(임시)
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
