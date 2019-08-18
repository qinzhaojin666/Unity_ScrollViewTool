using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-08-17 10:37:01
/// </summary>
public class CellViewBase : EnhancedScrollerCellView
{
    private GameObject __mGameObject;
    private Transform __mTransform;
    public GameObject mGameObject { get { if (__mGameObject == null) __mGameObject = this.gameObject; return __mGameObject; } }
    public Transform mTransform { get { if (__mTransform == null) __mTransform = this.transform; return __mTransform; } }

    protected CellDataBase mDataBase;

    public delegate void OnCellViewClick(CellViewBase cellViewBase);
    public OnCellViewClick onCellViewClick;

    public CellViewBase setIdentifier(string type)
    {
        this.cellIdentifier = type;
        return this;
    }

    public virtual void setData(CellDataBase dataBase)
    {
        this.mDataBase = dataBase;
        this.RefreshCellView();
    }

    public virtual void setData(ref List<CellDataBase> lisData, int startingIndex)
    {
        //重写的示例 参照此处
        //for (var i = 0; i < lisChild.Length; i++)
        //{
        //    lisChild[i].SetData(startingIndex + i < lisData.Count ? lisData[startingIndex + i] : null, startingIndex + i);
        //}
    }


    public virtual void OnBtnClick()
    {
        if (this.onCellViewClick != null)
            onCellViewClick(this);
    }
}
