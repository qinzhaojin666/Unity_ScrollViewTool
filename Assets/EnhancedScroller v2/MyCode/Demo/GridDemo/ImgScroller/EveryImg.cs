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
        if (requestObj != null)
        {
            if (requestObj.netImageProcessType != ProcessType.Processed)
            {
                requestObj.netImageProcessType = ProcessType.Abort;
            }

            if (requestObj.ieSetImgReqObj != null && requestObj.ieSetImgReqObj.setImageProcessType != ProcessType.Processed)
            {
                requestObj.ieSetImgReqObj.setImageProcessType = ProcessType.Abort;
                IESetImageMgr.getInstance().RemoveImageInQueue("everyAsset", requestObj.ieSetImgReqObj);
            }
        }


        requestObj = null;
        string path = mData.toOtherType<ImgCellData>().imgPath;
        titleImage.texture = defaultTexture;
        
        requestObj = NetImageManager.GetInstance().StartGetOne(path, titleImage, 1);
    }



}

