using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-06-08 14:50:33
/// </summary>
public class NetImageProcess
{
    private bool isProcess = false;
    private NetImageMonoScript netImageMono;
    private WWW www;
    private NetImageRequestObj netImageRequestObj;

    #region 定义的私有变量
    /// <summary>
    /// 必选
    /// </summary>
    public NetImageProcess SetMono(NetImageMonoScript mono)
    {
        netImageMono = mono;
        return this;
    }
    public bool GetIsProcess()
    {
        return isProcess;
    }
    #endregion


    public void StartLoadImage(NetImageRequestObj netImageRequestObj)
    {
        isProcess = true;
        netImageRequestObj.netImageProcess = this;
        this.netImageRequestObj = netImageRequestObj;
        netImageMono.StartCoroutine(LoadImage(netImageRequestObj));
    }

    private IEnumerator LoadImage(NetImageRequestObj netImageRequestObj)
    {
        yield return this.HttpGet(netImageRequestObj, OnLoadNetImageSucceed);

        ProcessNextOne();
    }

    private IEnumerator HttpGet(NetImageRequestObj netImageRequestObj, Action<WWW, NetImageRequestObj> callbackSucceed)
    {
        bool isGetData = false;
        int count = 0;
        do
        {
            www = new WWW(netImageRequestObj.netImageData.url);
            DateTime date1 = DateTime.Now;
            while ((DateTime.Now - date1).TotalSeconds <= 10)
            {
                yield return new WaitForSeconds(0.1f);

                if (www==null)
                {
                    count = 2;
                    break;
                }

                if (www.isDone)
                {
                    //Debug.Log("获取网络图片成功!    " + netImageRequestObj.netImageData.url);

                    if (www.error == null && callbackSucceed != null)
                        callbackSucceed(www, netImageRequestObj);

                    isGetData = true;
                    break;
                }
            }
            www.Dispose();

            if (isGetData == false)
            {
                Debug.LogError("请求图片超过10秒钟:要销毁该www了,并重新请求网络图片!");
                count++;
            }

        } while (isGetData == false && count < 2);
    }

    public void Abort()
    {
        if (www != null)
        {
            www.Dispose();
            ProcessNextOne();
        }
    }

    private void ProcessNextOne()
    {
        isProcess = false;
        netImageRequestObj.netImageProcess = null;
        NetImageManager.GetInstance().ProcessNextOne();
    }

    private void OnLoadNetImageSucceed(WWW www, NetImageRequestObj netImageRequestObj)
    {
        Texture2D tex = www.texture;
        netImageRequestObj.netImageData.texture2D = tex;
        NetImageManager.GetInstance().ProcessSetImage(netImageRequestObj);
        //netImageRequestObj.netImageData.texture2D = null;
        NetImageManager.GetInstance().AddNetImageData(netImageRequestObj.netImageData);
    }


}
