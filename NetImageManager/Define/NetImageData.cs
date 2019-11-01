using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-06-08 14:51:02
/// </summary>
public class NetImageMonoScript : MonoBehaviour
{

}

public class NetImageData
{
    public string url;

    public Texture2D texture2D;
    private Sprite _sprite;
    private Sprite _sprite_GridScale;


    private Texture2D _texture2D_GridScale;
    public Texture2D texture2D_GridScale {
        get {
            if (_texture2D_GridScale == null)
            {
                _texture2D_GridScale = CropScale.ScaleTexture(texture2D, 202, 147);
                _texture2D_GridScale.Compress(false);
            }
            return _texture2D_GridScale;
        }
    }

    public Sprite getSprite()
    {
        if (_sprite == null && texture2D != null)
        {
            _sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
        }
        return _sprite;
    }

    public Sprite getSprite_GridScale()
    {
        if (_sprite_GridScale == null && texture2D_GridScale != null)
        {
            _sprite_GridScale = Sprite.Create(texture2D_GridScale, new Rect(0, 0, texture2D_GridScale.width, texture2D_GridScale.height), new Vector2(0.5f, 0.5f));
        }
        return _sprite_GridScale;
    }

    public void Copy(NetImageData data)
    {
        this.url = data.url;
        this.texture2D = data.texture2D;
        this._sprite = data._sprite;
        this._texture2D_GridScale = data._texture2D_GridScale;
        this._sprite_GridScale = data._sprite_GridScale;
    }
}

public class NetImageRequestObj
{
    public int reqId;
    public NetImageData netImageData;
    public Image imageComponent;
    public RawImage rawImageComponent;
    /// <summary>
    /// 0原生大小,1GridScale大小 X202 Y147
    /// </summary>
    public int useScaleId = 0;
    public ProcessType _netImageProcessType = ProcessType.None;
    public ProcessType netImageProcessType {
        get {
            return _netImageProcessType;
        }
        set {
            if (value== ProcessType.Abort)
            {
                NetImageMgr.getInstance().AbortObj(this);
            }
            _netImageProcessType = value;
        }
    }

    public IESetImageRequestObj ieSetImgReqObj;

    public void SetComponentSprite()
    {
        if (netImageProcessType != ProcessType.Processed || ieSetImgReqObj.setImageProcessType != ProcessType.Processed)
        {
            Debug.Log("Processed有问题 返回 " + netImageProcessType + " " + ieSetImgReqObj.setImageProcessType);
            return;
        }

        if (this.imageComponent != null)
        {
            this.imageComponent.sprite = netImageData.getSprite();
        }
        if (this.rawImageComponent != null)
        {
            this.rawImageComponent.texture = netImageData.texture2D;
        }
    }

    public void SetComponentSprite_GridSprite()
    {
        if (netImageProcessType != ProcessType.Processed || ieSetImgReqObj.setImageProcessType != ProcessType.Processed)
        {
            Debug.Log("Processed有问题 返回 " + netImageProcessType + " " + ieSetImgReqObj.setImageProcessType);
            return;
        }

        if (this.imageComponent != null)
        {
            this.imageComponent.sprite = netImageData.getSprite_GridScale();
        }
        if (this.rawImageComponent != null)
        {
            this.rawImageComponent.texture = netImageData.texture2D_GridScale;
        }
    }

    public void Copy(NetImageRequestObj netImageRequestObj)
    {
        this.imageComponent = netImageRequestObj.imageComponent;
        this.rawImageComponent = netImageRequestObj.rawImageComponent;
        this.useScaleId = netImageRequestObj.useScaleId;
        this.netImageData = new NetImageData();
        this.netImageData.Copy(netImageRequestObj.netImageData);
        this.netImageProcessType = netImageRequestObj.netImageProcessType;
    }

    public void Abort()
    {
        if (this.netImageProcessType != ProcessType.Processed)
        {
            netImageProcessType = ProcessType.Abort;
            //Debug.Log("Abort-netImage");
        }

        if (ieSetImgReqObj != null && ieSetImgReqObj.setImageProcessType != ProcessType.Processed)
        {
            ieSetImgReqObj.Abort("everyAsset");
            //Debug.Log("Abort-ieSetImg");
        }
    }
}
