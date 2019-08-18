using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-07-22 16:38:48
/// </summary>
public class IESetImageProcess
{
    public List<IESetImageRequestObj> lisReqObj = new List<IESetImageRequestObj>();

    private WaitForSeconds wait = new WaitForSeconds(0.1f);

    private WaitForSeconds wait2 = new WaitForSeconds(0.2f);

    public void StartProcess(IESetImageMono mono)
    {
        //Debug.Log(mono.name);
        mono.StopCoroutine(IEStartProcess());
        mono.StartCoroutine(IEStartProcess());
    }

    public void EnQueue(IESetImageRequestObj reqObj)
    {
        //Debug.Log("EnQueue--------------------");
        lisReqObj.Add(reqObj);
    }

    public void RemoveOne(IESetImageRequestObj reqObj)
    {
        if (reqObj == null)
            return;

        for (int i = 0; i < lisReqObj.Count; i++)
        {
            if (lisReqObj[i].reqId == reqObj.reqId)
            {
                lisReqObj[i].ieRequestObjType = IERequestObjType.None;
                lisReqObj.RemoveAt(i);
                Debug.Log("DeQueue--------------------" + reqObj.reqId);
                break;
            }
        }
    }


    private IEnumerator IEStartProcess()
    {
        while (true)
        {
            if (lisReqObj.Count <= 0)
            {
                yield return wait;
                continue;
            }

            IESetImageRequestObj reqObj = lisReqObj[0];
            if (reqObj != null && reqObj.action != null && reqObj.ieRequestObjType == IERequestObjType.NoProcess)
            {
                reqObj.action();
                reqObj.ieRequestObjType = IERequestObjType.Processed;
            }
            reqObj = null;
            lisReqObj.RemoveAt(0);

            yield return wait2;
        }
    }


}
