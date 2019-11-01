using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-08-17 11:37:21
/// </summary>
public static class EnhancedScrollerExtension
{
    public static T toOtherType<T>(this CellDataBase gridDataBase) where T : CellDataBase
    {
        return gridDataBase as T;
    }
    public static T toOtherType<T>(this CellViewBase cellViewBase) where T : CellViewBase
    {
        return cellViewBase as T;
    }
}
