using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-05-17 09:49:24
/// </summary>
public class NetImageMgr
{
    private static NetImageMgr _instance;

    public static NetImageMgr getInstance()
    {
        if (_instance == null)
        {
            _instance = new NetImageMgr();
        }
        return _instance;
    }

    private static NetImageMonoScript _netImageMono;
    private static NetImageMonoScript netImageMono {
        get {
            if (_netImageMono == null)
                _netImageMono = new GameObject("NetImageManager").AddComponent<NetImageMonoScript>();
            GameObject.DontDestroyOnLoad(_netImageMono.gameObject);
            return _netImageMono;
        }
    }

    private Dictionary<string, NetImageData> dicNetImage = new Dictionary<string, NetImageData>();//存储已网络请求过的Texture2D

    public void AddNetImageData(NetImageData netImageData)
    {
        if (dicNetImage.ContainsKey(netImageData.url) == false)
        {
            dicNetImage.Add(netImageData.url, netImageData);
            //超出 移除
            if (dicNetImage.Count > this.maxStoreImageDataCount)
            {
                var ie = dicNetImage.GetEnumerator();
                if (ie.MoveNext())
                {
                    dicNetImage[ie.Current.Key] = null;
                    dicNetImage.Remove(ie.Current.Key);
                }
            }
        }
        else
        {
            //Debug.LogError("已请求过该url，重复添加NetImageData！url：" + netImageData.url);
            netImageData = null;
        }
    }

    private int maxRequestCount = 3;//设置同时请求网络图片的最大个数
    private int maxStoreImageDataCount = 66;//存储的最多网络请求图片历史数目 超出依次清理
    private int curReqId = 0;//当前使用的索引
    private List<NetImageProcess> lisProcess = new List<NetImageProcess>();//存放处理器
    private List<NetImageRequestObj> lisObj = new List<NetImageRequestObj>();//存放请求


    /// <summary>
    /// 可选，默认为2 设置同时请求加载网络图片的最大个数
    /// </summary>
    public NetImageMgr SetMaxRequestCount(int maxCount)
    {
        this.maxRequestCount = maxCount;
        return this;
    }

    /// <summary>
    /// 可选，默认为66 设置同时请求加载网络图片的最大个数
    /// </summary>
    public NetImageMgr SetMaxStoreImageDataCount(int maxCount)
    {
        this.maxStoreImageDataCount = maxCount;
        return this;
    }


    private int GetOneReqId()
    {
        if (curReqId >= 10000)
            curReqId = 0;

        return curReqId++;
    }



    /// <summary>
    /// 开始获取一个网络图片 并自动设置图片 自动设置加载图片队列reqid
    /// </summary>
    public NetImageRequestObj StartGetOne(string url, RawImage rawImageComponent1, int useScaleID = 0)
    {
        NetImageRequestObj reqObj = new NetImageRequestObj() {
            reqId = GetOneReqId(),
            rawImageComponent = rawImageComponent1,
            netImageData = new NetImageData() { url = url },
            useScaleId = useScaleID,
            netImageProcessType = ProcessType.NoProcess
        };
        StartGetOne(reqObj);
        return reqObj;
    }

    /// <summary>
    /// 开始获取一个网络图片 并自动设置图片 自动分配加载图片队列reqid
    /// </summary>
    public NetImageRequestObj StartGetOne(string url, Image imageComponent1, int useScaleID = 0)
    {
        NetImageRequestObj reqObj = new NetImageRequestObj() {
            reqId = GetOneReqId(),
            imageComponent = imageComponent1,
            netImageData = new NetImageData() { url = url },
            useScaleId = useScaleID,
            netImageProcessType = ProcessType.NoProcess
        };
        StartGetOne(reqObj);
        return reqObj;
    }

    public void StartGetOne(NetImageRequestObj netImageRequestObj, int useScaleID = 0)
    {
        if (dicNetImage.ContainsKey(netImageRequestObj.netImageData.url))
        {
            netImageRequestObj.netImageData = dicNetImage[netImageRequestObj.netImageData.url];
            netImageRequestObj.netImageProcessType = ProcessType.Processed;
            ProcessSetImage(netImageRequestObj);
        }
        else
        {
            lisObj.Add(netImageRequestObj);
            ProcessNextOne();
        }
    }

    public void AbortObj(NetImageRequestObj reqObj)
    {
        if (reqObj == null)
            return;

        int index = GetObjIndex(reqObj);
        if (index >= 0)
        {
            lisObj[index].netImageProcessType = ProcessType.NoProcess;
            lisObj.RemoveAt(index);
            Debug.Log("Abort--------------------" + reqObj.reqId);
        }
    }


    public int GetObjIndex(NetImageRequestObj reqObj)
    {
        for (int i = 0; i < lisObj.Count; i++)
        {
            if (lisObj[i].reqId == reqObj.reqId)
            {
                return i;
            }
        }
        return -1;
    }

    public void ProcessSetImage(NetImageRequestObj netImageRequestObj)
    {
        NetImageRequestObj netImageRequestObj2 = new NetImageRequestObj();
        IESetImageRequestObj reqObj = new IESetImageRequestObj();

        if (netImageRequestObj.useScaleId == 0)
        {
            netImageRequestObj.netImageData.getSprite();
            netImageRequestObj2.Copy(netImageRequestObj);

            reqObj.action = netImageRequestObj2.SetComponentSprite;
        }
        else if (netImageRequestObj.useScaleId == 1)
        {
            netImageRequestObj.netImageData.getSprite_GridScale();
            netImageRequestObj2.Copy(netImageRequestObj);

            reqObj.action = netImageRequestObj2.SetComponentSprite_GridSprite;
        }

        netImageRequestObj.ieSetImgReqObj = reqObj;
        netImageRequestObj2.ieSetImgReqObj = reqObj;

        IESetImageMgr.getInstance().AddSetImageInQueue("everyAsset", reqObj);
    }


    private NetImageProcess GetProcess()
    {
        if (lisProcess.Count < this.maxRequestCount)
        {
            NetImageProcess netImageProcess = new NetImageProcess();
            netImageProcess.SetMono(netImageMono);
            lisProcess.Add(netImageProcess);
            return netImageProcess;
        }
        else
        {
            for (int i = 0; i < lisProcess.Count; i++)
            {
                if (lisProcess[i].GetIsProcess() == false)
                {
                    return lisProcess[i];
                }
            }
            return null;
        }
    }

    /// <summary>
    /// 开启一轮请求
    /// </summary>
    public void ProcessNextOne()
    {
        if (lisObj.Count <= 0)
            return;

        NetImageProcess process = this.GetProcess();
        if (process == null)
            return;

        process.StartLoadImage(lisObj[0]);
        lisObj.RemoveAt(0);
    }



}

