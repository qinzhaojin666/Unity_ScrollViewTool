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
public class EveryImg : MonoBehaviour
{
    public RawImage titleImage;
    public Texture2D defaultTexture;
    private NetImageRequestObj requestObj;
    private int dataIndex;

    public void SetData(CellDataBase data, int dataIndex)
    {
        requestObj = null;

        this.gameObject.SetActive(data != null);

        if (data == null)
            return;

        string path = data.toOtherType<ImgCellData>().imgPath;


        titleImage.texture = defaultTexture;
        this.dataIndex = dataIndex;

        requestObj = NetImageManager.GetInstance().StartGetOne(path, titleImage, 1);
    }

    public void OnDisable()
    {
        if (requestObj != null && requestObj.ieSetImgReqObj != null)
        {
            IESetImageMgr.getInstance().RemoveImageInQueue("everyAsset", requestObj.ieSetImgReqObj);
        }
    }

}

