/************************************************************
    File      : GameRoot
	Author    : Plane
    Version   : 1.0
    Function  : Nothing
    Date      : 2016/10/19 15:56:9
    Copyright : 2016 CodeVFX Inc.
*************************************************************/
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class GameRoot : MonoBehaviour 
{
    ///////////////////////////Data Define//////////////////////////////    
    private static GameRoot instance = null;
    public static GameRoot Instance
    {
        get
        {
            return instance;
        }
    }
    GameObject StateGB = null;
    GameObject SystemGB = null;
    private List<ISystem> systemList = new List<ISystem>();
    private Dictionary<Type, ISystem> systemMap = new Dictionary<Type, ISystem>();

    private List<IGameState> gameStateList = new List<IGameState>();//存储所有的gameState
    public static bool isStateGBCreateDone = false;

    //----------------------------------------------------------------//

    ///////////////////////////MainFunctions////////////////////////////
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        //Create GameState Objs
        StateGB = new GameObject();
        StateGB.name = "StateRoot";
        StateGB.transform.parent = this.transform;
        StateGB.transform.localPosition = Vector3.zero;
        //Create GameSystem Objs
        SystemGB = new GameObject();
        SystemGB.name = "SystemRoot";
        SystemGB.transform.parent = this.transform;
        SystemGB.transform.localPosition = Vector3.zero;

        StartCoroutine(SwitchToLoginState());
    }

    private IEnumerator SwitchToLoginState()
    {
        while(!PEWindowMgr.Instance.isInitDone)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        InitGameCoreSystems();
        ChangeGameStateToTarget(GameStateType.LoginState);
    }

    GameStateType curGameState = GameStateType.None;
    GameStateType dstGameState = GameStateType.None;
    IGameState levGameState;
    public void ChangeGameStateToTarget(GameStateType inputState)
    {
        if (curGameState == inputState) return;

        if (curGameState == GameStateType.LoadingState)
        {
            //如果当前正处于loading状态
            curGameState = dstGameState;
        }
        else
        {
            //如果当前处于非loading的状态
            dstGameState = inputState;
            LoadingState.mLoadSceneDone = false;
            //把当前状态跳转到loading状态，加载启动加载新场景
            curGameState = GameStateType.LoadingState;

            //当启动游戏进入登录状态时特殊处理，不弹出loading界面，其它的状态转换都弹出Loading界面
            if (dstGameState != GameStateType.LoginState)
            {
                //打开Loading界面
                //TODO
                //PEWindowMgr.Instance.InitWindowCache(....)
            }
        }

        IGameState dstState = GetGameState(curGameState);
        if (dstState != null)
        {
            dstState.Enter();
        }
    }
    //----------------------------------------------------------------//

    private void InitGameCoreSystems()
    {
        //AddGameSys<ResourceSys>(systemGB.transform);
        //AddGameSys<EventSys>(SystemGB.transform);
        InitAllGameSyss();
    }

    ///////////////////////////ToolMethonds/////////////////////////////
    void AddGameSys<T>(Transform transform) where T : ISystem, new()
    {
        GameObject go = new GameObject();
        go.transform.parent = transform;
        ISystem sys = go.AddComponent<T>();
        go.name = sys.GetName();
        systemList.Add(sys);
        systemMap.Add(sys.GetType(), sys);
    }
    private void InitAllGameSyss()
    {
        for (int i = 0; i < systemList.Count; i++)
        {
            systemList[i].Init();
        }
    }
    public IGameState GetGameState(GameStateType state)
    {
        for (int i = 0; i < gameStateList.Count; i++)
        {
            if (gameStateList[i].GetStateType() == state)
            {
                return gameStateList[i];
            }
        }
        return null;
    }
    public GameStateType GetDstGameState()
    {
        return dstGameState;
    }
    public void ClearLastLeaveState()
    {
        if (levGameState != null)
            levGameState.Leave();
    }
    //----------------------------------------------------------------//
}

public class ISystem : MonoBehaviour
{
    public virtual void Init() { }
    public string GetName()
    {
        return GetType().Name;
    }
}