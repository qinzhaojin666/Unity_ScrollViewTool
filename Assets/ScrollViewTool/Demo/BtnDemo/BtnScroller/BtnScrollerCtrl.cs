using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using System;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-08-17 10:34:11
/// </summary>
public class BtnScrollerCtrl : ScrollerCtrlBase
{
    public Transform role;//按钮被点击的变换图片
    private int curShowTuChe = -1;

    public Action<BtnCellView> OnCellViewClick;

    protected override void Start()
    {
        base.Start();
        this.SetBtnDataList();
    }

    public void SetBtnDataList()
    {
        List<CellDataBase> lisBtnCellData = new List<CellDataBase>();

        for (int i = 0; i < 20; i++)
        {
            lisBtnCellData.Add(new BtnCellData() { pathRoot = "path" + i });
        }

        this.setDataList(lisBtnCellData);
        this.ReloadData();

        if (curShowTuChe == -1)
            onCellViewClick((CellViewBase)this.scroller.GetCellViewAtDataIndex(0));
    }

    protected override void onCellViewClick(CellViewBase cellViewBase)
    {
        BtnCellView btnCellView = cellViewBase.toOtherType<BtnCellView>();

        if (curShowTuChe != btnCellView.dataIndex)
        {
            curShowTuChe = btnCellView.dataIndex;
            SetRole(btnCellView.mTransform);

            if (OnCellViewClick != null)
            {
                OnCellViewClick(btnCellView);
            }
        }
    }

    void SetRole(Transform transform)
    {
        role.gameObject.SetActive(true);
        role.SetParent(transform, false);
        role.SetAsFirstSibling();
        role.localPosition = Vector3.zero;
    }

    public override void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
    {
        base.CellViewVisibilityChanged(cellView);

        if (cellView.dataIndex == curShowTuChe)
        {
            role.gameObject.SetActive(cellView.active);
            if (cellView.active == true)
            {
                SetRole(cellView.transform);
            }
        }
    }
}
