using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-07-22 15:42:44
/// </summary>
public class IESetImageMgr
{
    private static IESetImageMgr _instance;
    public static IESetImageMgr getInstance()
    {
        if (_instance == null)
        {
            _instance = new IESetImageMgr();
        }
        return _instance;
    }

    private IESetImageMono _mono;
    public IESetImageMono getMono()
    {
        if (_mono == null)
        {
            _mono = new GameObject("IESetImageMgr").AddComponent<IESetImageMono>();
            GameObject.DontDestroyOnLoad(_mono.gameObject);
        }
        return _mono;
    }

    private Dictionary<string, IESetImageProcess> dicAllReq = new Dictionary<string, IESetImageProcess>();
    private int curReqId = 0;

    private int GetOneReqId()
    {
        if (curReqId >= 10000)
            curReqId = 0;

        return curReqId++;
    }

    /// <summary>
    /// 添加进序列 自动分配加载图片队列reqid
    /// </summary>
    public void AddSetImageInQueue(string id, IESetImageRequestObj reqObj)
    {
        IESetImageProcess process;
        if (dicAllReq.ContainsKey(id))
        {
            process = dicAllReq[id];
        }
        else
        {
            process = new IESetImageProcess();
            dicAllReq.Add(id, process);
            process.StartProcess(getMono());
        }

        reqObj.setImageProcessType = ProcessType.NoProcess;
        reqObj.reqId = this.GetOneReqId();

        process.EnQueue(reqObj);
    }

    public void RemoveImageInQueue(string id, IESetImageRequestObj reqObj)
    {
        if (dicAllReq.ContainsKey(id))
        {
            dicAllReq[id].RemoveOne(reqObj);
        }
    }

}

