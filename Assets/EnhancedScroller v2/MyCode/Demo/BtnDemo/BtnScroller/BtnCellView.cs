using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-08-17 11:03:21
/// </summary>
public class BtnCellView : CellViewBase
{
    public Text titleText;

    public BtnCellData mBtnCellData
    {
        get
        {
            return mDataBase.toOtherType<BtnCellData>();
        }
    }


    public override void RefreshCellView(bool isReacquireData)
    {
        Debug.Log("RefreshCellView:"+ mBtnCellData.pathRoot);
        titleText.text = mBtnCellData.pathRoot;
    }

}

