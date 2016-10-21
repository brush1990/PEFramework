/************************************************************
    File      : LoginState
	Author    : Plane
    Version   : 1.0
    Function  : ��Ϸ��¼״̬
    Date      : 2016/10/21 11:48:53
    Copyright : 2016 CodeVFX Inc.
*************************************************************/
using System.Collections;
using UnityEngine;

public class LoginState : IGameState 
{
    ///////////////////////////MainFunctions////////////////////////////
    
    public override void Enter()
    {
        Debug.Log("flag");
    }

    private IEnumerator LoginCoroutine()
    {
        //���ñ�־������ȷ��ABSys�е�ABӳ���ֵ��Ѿ�������ɣ����Կ�ʼ����UI����Դ�����ȥ�򿪵�¼����
        //TODO
        bool isDone = true;
        while (!isDone)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //���صȴ�������tips��ʾ��ͨ�ô��ڡ�
        //TODO

        mProgressValue = 0;
        //��ʾ��¼���棬Ԥ���ص�¼��ɽ���
        PEWindowMgr.Instance.SetWindowState(PEWindowEnum.Login_window, ResType.UICommomType);

    }
    //----------------------------------------------------------------//

    ///////////////////////////ToolMethonds/////////////////////////////    
    public override GameStateType GetStateType()
    {
        return GameStateType.LoginState;
    }
    //----------------------------------------------------------------//
}