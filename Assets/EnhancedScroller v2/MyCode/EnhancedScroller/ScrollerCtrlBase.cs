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
public class ScrollerCtrlBase : MonoBehaviour, IEnhancedScrollerDelegate
{
    public EnhancedScroller scroller;
    public CellViewBase cellViewPrefab;

    private float _cellSize = -1;
    public float cellSize
    {
        get
        {
            if (_cellSize == -1)
            {
                RectTransform rect = cellViewPrefab.GetComponent<RectTransform>();
                _cellSize = scroller.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical ? rect.sizeDelta.y : rect.sizeDelta.x;
            }
            return _cellSize;
        }
    }

    private int _pageCount = -1;
    /// <summary>
    /// 一页包含几个格子 用于翻页
    /// </summary>
    public int pageCount
    {
        get
        {
            if (_pageCount == -1)
            {
                _pageCount = scroller.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical ? (int)scroller.ScrollRectSize / (int)cellSize : (int)scroller.ScrollRectSize / (int)cellSize;
            }
            return _pageCount;
        }
    }

    [Header("格子模式勾选")]
    public bool isGridModel = false;
    /// <summary>
    /// 如果是格子模式 该值有用
    /// </summary>
    private int _numberOfCellsPerRow = -1;
    private int numberOfCellsPerRow
    {
        get
        {
            if (_numberOfCellsPerRow == -1)
            {
                _numberOfCellsPerRow = cellViewPrefab.lisCellGrid.Count;
            }
            return _numberOfCellsPerRow;
        }
    }

    [Space(20)]
    public List<CellDataBase> lisData;



    private bool isInit = false;

    private void InitCtrl()
    {
        scroller.Delegate = this;
        scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
        isInit = true;
    }

    /// <summary>
    /// 提前把Prefab关闭
    /// </summary>
    public void StartDisablePrefab()
    {
        if (cellViewPrefab.gameObject != null)
            cellViewPrefab.mGameObject.SetActive(false);
    }

    public ScrollerCtrlBase setDataList(List<CellDataBase> _lisData)
    {
        this.lisData = _lisData;
        return this;
    }

    /// <summary>
    /// Scroller的启动方法  注意：调用之前先setDataList
    /// </summary>
    public virtual ScrollerCtrlBase ReloadData()
    {
        if (isInit == false)
            InitCtrl();

        scroller.ReloadData();
        return this;
    }

    public virtual ScrollerCtrlBase RefershData()
    {
        scroller.RefreshActiveCellViews();
        return this;
    }


    public virtual EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        CellViewBase cellView = scroller.GetCellView(cellViewPrefab) as CellViewBase;
        if (isGridModel == false)
        {
            cellView.name = "Cell " + dataIndex.ToString();
            cellView.setData(lisData[dataIndex]);
            cellView.onCellViewClick = this.onCellViewClick;
        }
        else
        {
            cellView.name = "Cell " + (dataIndex * numberOfCellsPerRow).ToString() + " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();
            cellView.setData(ref lisData, dataIndex * numberOfCellsPerRow);
        }
        return cellView;
    }

    public virtual float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return cellSize;
    }

    public virtual int GetNumberOfCells(EnhancedScroller scroller)
    {
        if (isGridModel == false)
        {
            return lisData.Count;
        }
        else
        {
            return Mathf.CeilToInt((float)lisData.Count / (float)numberOfCellsPerRow);
        }
    }

    public virtual void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
    {
        CellViewBase view = cellView as CellViewBase;

        if (cellView.active)
            view.RefreshCellView();
    }

    /// <summary>
    /// single模式 格子被点击 
    /// </summary>
    protected virtual void onCellViewClick(CellViewBase cellViewBase)
    {

    }

    public void OnUpButtonClick()
    {
        if (scroller.IsTweening == true)
            return;

        scroller.JumpToDataIndex(scroller.StartCellViewIndex - pageCount, 0, 0.05f, true, EnhancedScroller.TweenType.linear, 0.5f);
    }

    public void OnDownButtonClick()
    {
        if (scroller.IsTweening == true)
            return;

        scroller.JumpToDataIndex(scroller.StartCellViewIndex + pageCount, 0, 0.05f, true, EnhancedScroller.TweenType.linear, 0.5f);
    }
}
