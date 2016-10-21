/************************************************************
    File      : PEWindowMgr
	Author    : Plane
    Version   : 1.0
    Function  : ���ڹ���
    Date      : 2016/10/19 14:54:35
    Copyright : 2016 CodeVFX Inc.
*************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEWindowMgr : MonoBehaviour
{
    ///////////////////////////Data Define//////////////////////////////
    private static PEWindowMgr instance = null;
    public static PEWindowMgr Instance
    {
        get
        {
            return instance;
        }
    }
    private Transform uiRootTrans = null;
    private Transform windowRootTrans = null;
    private Transform cameraRootTrans = null;
    public bool isInitDone = false;
    class PWindow
    {
        public PEWindowEnum windowEnum;
        public string windowName = "";
        public PWindow(PEWindowEnum windowEnum, string luaName)
        {
            this.windowEnum = windowEnum;
            if (luaName == "")
            {
                windowName = windowEnum.ToString();
            }
            else
            {
                windowName = luaName;
            }            
        }
    }
    class PWindowType
    {
        public WindowBase windowBase;
        public ResType resType;
        public ResCacheType cacheType;
        public PWindowType(WindowBase windowBase, ResType resType, ResCacheType cacheType)
        {
            this.windowBase = windowBase;
            this.resType = resType;
            this.cacheType = cacheType;
        }
    }
    private Dictionary<PWindow, PWindowType> windowDic = new Dictionary<PWindow, PWindowType>();
    //----------------------------------------------------------------//

    ///////////////////////////MainFunctions////////////////////////////
    private void Awake()
    {
        instance = this;
        //����ͼ�����и���ʱ��ͨ����������ȥ����
        //TODO
        StartCoroutine(InitWindowMgr());
    }

    IEnumerator InitWindowMgr()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        var uiroot = GameObject.Find("UIRoot");
        if (uiroot != null)
        {
            Destroy(uiroot);
        }
        GameObject gb = null;        
       
        gb = (GameObject)ResourceMgr.GetInstantiateOB("UIRoot", ResType.UICommomType, ResCacheType.Always);
        gb.name = "UIRoot";
        uiRootTrans = gb.transform;
        uiRootTrans.parent = transform;
        windowRootTrans = PEUITools.GetTrans(uiRootTrans, "windowRoot");
        cameraRootTrans = PEUITools.GetTrans(uiRootTrans, "cameraRoot");        
        isInitDone = true;
    }

    //����UI���ڵ�״̬
    public void SetWindowState(PEWindowEnum windowEnum, ResType resType, ResCacheType cacheType = ResCacheType.Never)
    {

    }
    //Ԥ���ش��ڽ�����
    public void InitWindowCache(PEWindowEnum windowEnum, ResType resType, ResCacheType cacheType = ResCacheType.Never,string luaName = "")
    {        
        PWindow pwindow = new PWindow(windowEnum, luaName);
        string windowName = pwindow.windowName;
        GameObject gb = null;
        if (!windowDic.ContainsKey(pwindow))
        {
            gb = (GameObject)ResourceMgr.GetInstantiateOB(windowName, resType, cacheType);
        }
        gb.name = windowName;
        gb.transform.parent = windowRootTrans;
        gb.transform.localPosition = Vector3.zero;
        gb.transform.localScale = Vector3.one;
        NGUITools.SetActive(gb, false);
        WindowBase windowBase = gb.GetComponent<WindowBase>();
        if (windowBase == null)
        {
            windowBase = GetOrAddWindowHandle(gb, windowEnum, luaName);
        }



    }
    //----------------------------------------------------------------//

    ///////////////////////////ToolMethonds/////////////////////////////
    //��ȡ����Ӵ��ڵĸ����ƽű�
    private WindowBase GetOrAddWindowHandle(GameObject gb, PEWindowEnum windowEnum, string luaName)
    {
        WindowBase windowBase = null;
        switch (windowEnum)
        {
            case PEWindowEnum.Login_window:
                windowBase = PEUITools.GetOrAddWindowHandle<HandleLoginWindow>(gb);
                break;
            case PEWindowEnum.Logined_window:
                windowBase = PEUITools.GetOrAddWindowHandle<HandleLoginedWindow>(gb);
                break;
            default:
                Debug.LogError("Can not Get window Handle Scripts");
                break;
        }
        return windowBase;
    }
    //----------------------------------------------------------------//
}