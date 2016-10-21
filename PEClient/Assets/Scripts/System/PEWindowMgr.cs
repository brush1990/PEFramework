/************************************************************
    File      : PEWindowMgr
	Author    : Plane
    Version   : 1.0
    Function  : Nothing
    Date      : 2016/10/19 14:54:35
    Copyright : 2016 CodeVFX Inc.
*************************************************************/
using System.Collections;
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

    //----------------------------------------------------------------//

    ///////////////////////////ToolMethonds/////////////////////////////
    //----------------------------------------------------------------//
}

public enum AssetType
{
    CommonUI,
}