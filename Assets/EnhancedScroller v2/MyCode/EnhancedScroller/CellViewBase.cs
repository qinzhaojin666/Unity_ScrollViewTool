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

    [Header("格子模式勾选填写")]
    public bool isGridModel = false;
    public int gridCount = 4;
    public CellGridBase gridPrefab;
    private List<CellGridBase> lisCellGrid = new List<CellGridBase>();

    void Awake()
    {
        lisCellGrid.Add(gridPrefab);
        for (int i = 0; i < gridCount - 1; i++)
        {
            GameObject item = Instantiate(gridPrefab.mGameObject, mTransform);
            lisCellGrid.Add(item.GetComponent<CellGridBase>());
        }
    }

    public CellViewBase setIdentifier(string type)
    {
        this.cellIdentifier = type;
        return this;
    }

    public virtual void setData(CellDataBase dataBase)
    {
        this.mDataBase = dataBase;
    }

    public virtual void setData(ref List<CellDataBase> lisData, int startingIndex)
    {
        //继承的子类中 重写该方法参照此处代码
        for (int i = 0; i < lisCellGrid.Count; i++)
        {
            lisCellGrid[i].setData(startingIndex + i < lisData.Count ? lisData[startingIndex + i] : null, startingIndex + i);
        }
    }

    public override void RefreshCellView(bool isReacquireData = false)
    {
        if (isGridModel)
        {
            for (int i = 0; i < lisCellGrid.Count; i++)
            {
                if (lisCellGrid[i].active)
                    lisCellGrid[i].RefreshCellView(isReacquireData);
            }
        }
    }

    public virtual void OnBtnClick()
    {
        if (this.onCellViewClick != null)
            onCellViewClick(this);
    }
}
