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
public class ImgCellView : CellViewBase
{
    public EveryImg[] lisChild;

    public override void setData(ref List<CellDataBase> lisData, int startingIndex)
    {
        for (var i = 0; i < lisChild.Length; i++)
        {
            lisChild[i].SetData(startingIndex + i < lisData.Count ? lisData[startingIndex + i] : null, startingIndex + i);
        }
    }

}

