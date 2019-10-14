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
        netImageRequestObj.netImageProcessType = ProcessType.Processing;
        netImageMono.StartCoroutine(LoadImage(netImageRequestObj));
    }

    private IEnumerator LoadImage(NetImageRequestObj netImageRequestObj)
    {
        yield return this.HttpGet(netImageRequestObj, OnLoadNetImageSucceed);

        isProcess = false;
        NetImageMgr.getInstance().ProcessNextOne();
    }

    private IEnumerator HttpGet(NetImageRequestObj netImageRequestObj, Action<WWW, NetImageRequestObj> callbackSucceed)
    {
        bool isGetData = false;
        int count = 0;
        do
        {
            WWW www = new WWW(netImageRequestObj.netImageData.url);
            DateTime date1 = DateTime.Now;
            while ((DateTime.Now - date1).TotalSeconds <= 10)
            {
                yield return new WaitForSeconds(0.1f);

                if (www.isDone)
                {
                    Debug.Log("获取网络图片成功!    " + netImageRequestObj.netImageData.url);
                    if (string.IsNullOrEmpty(www.error) && callbackSucceed != null)
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

    private void OnLoadNetImageSucceed(WWW www, NetImageRequestObj netImageRequestObj)
    {
        Texture2D tex = www.texture;
        netImageRequestObj.netImageData.texture2D = tex;
        NetImageMgr.getInstance().AddNetImageData(netImageRequestObj.netImageData);

        if (netImageRequestObj.netImageProcessType != ProcessType.Abort)
        {
            netImageRequestObj.netImageProcessType = ProcessType.Processed;
            NetImageMgr.getInstance().ProcessSetImage(netImageRequestObj);
        }
    }


}
