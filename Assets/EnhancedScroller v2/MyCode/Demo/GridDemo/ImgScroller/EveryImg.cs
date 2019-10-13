using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-08-17 11:03:21
/// </summary>
public class EveryImg : CellGridBase
{
    public RawImage titleImage;
    public Texture2D defaultTexture;
    private NetImageRequestObj requestObj;

    public override void RefreshCellView()
    {
        requestObj = null;
        string path = mData.toOtherType<ImgCellData>().imgPath;
        titleImage.texture = defaultTexture;
        requestObj = NetImageManager.GetInstance().StartGetOne(path, titleImage, 1, judgePath);
    }

    private bool judgePath()
    {
        if (requestObj != null && mData != null)
        {
            //Debug.Log(requestObj.netImageData.url == mData.toOtherType<ImgCellData>().imgPath);
            return requestObj.netImageData.url == mData.toOtherType<ImgCellData>().imgPath;
        }
        else
        {
            return false;
        }
    }

    public void OnDisable()
    {
        //Debug.LogError("OnDisable------"+ requestObj);
        if (requestObj != null)
        {
            if (requestObj.netImageProcess != null)
            {
                Debug.LogError("OnDisable------Abort");
                requestObj.netImageProcess.Abort();
            }
            if (requestObj.ieSetImgReqObj != null)
            {
                IESetImageMgr.getInstance().RemoveImageInQueue("everyAsset", requestObj.ieSetImgReqObj);
            }
        }
    }

}

