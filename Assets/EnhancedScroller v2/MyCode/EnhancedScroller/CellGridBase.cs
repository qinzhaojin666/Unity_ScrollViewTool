using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGridBase : MonoBehaviour
{
    private GameObject __mGameObject;
    private Transform __mTransform;
    public GameObject mGameObject { get { if (__mGameObject == null) __mGameObject = this.gameObject; return __mGameObject; } }
    public Transform mTransform { get { if (__mTransform == null) __mTransform = this.transform; return __mTransform; } }

    [HideInInspector]
    public ScrollerCtrlBase scrollerCtrl;
    [HideInInspector]
    public CellDataBase mData;
    [HideInInspector]
    public int mDataIndex;
    [HideInInspector]
    public bool active = false;

    public void setScrollerCtrl(ScrollerCtrlBase ctrl)
    {
        scrollerCtrl = ctrl;
    }

    public virtual void setData(CellDataBase data, int dataIndex)
    {
        active = data != null;
        mGameObject.SetActive(active);

        mData = data;
        mDataIndex = dataIndex;
    }

    public virtual void RefreshCellView(bool isReacquireData)
    {
        if (isReacquireData == true)
        {
            setData(scrollerCtrl.GetDataByID(mDataIndex), mDataIndex);
        }
    }
}
