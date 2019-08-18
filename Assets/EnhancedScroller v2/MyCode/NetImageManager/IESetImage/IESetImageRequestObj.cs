using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-08-14 16:09:54
/// </summary>
public class IESetImageRequestObj
{
    public int reqId;
    public Action action;
    public IERequestObjType ieRequestObjType = IERequestObjType.None;
}
public enum IERequestObjType
{
    None,
    NoProcess,
    Processed
}
