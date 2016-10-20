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
    public bool isInitDone = false;
    //----------------------------------------------------------------//

    ///////////////////////////MainFunctions////////////////////////////
    private void Awake()
    {
        instance = this;
        //当有图集进行更新时，通过这里首先去加载
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
        /*
        QiFunUITools.SetActive(gb, true);
        m_uiRootTrans = gb.transform;
        m_uiRootTrans.parent = transform;
        m_windowRootTrans = QiFunUITools.GetTrans(m_uiRootTrans, "window_root");
        m_cameraRootTrans = QiFunUITools.GetTrans(m_uiRootTrans, "camera_root");
        
        */
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