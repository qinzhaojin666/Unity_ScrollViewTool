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
public class ImgScrollerCtrl : ScrollerCtrlBase
{
    public Action<BtnCellView> OnCellViewClick;
    public List<string> lisImgPath;
    protected override void Start()
    {
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_1f69f9ec04654e3aa23fd1546e907c5d~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_1f69f9ec04654e3aa23fd1546e907c5d~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_7636649a1ff9458aa8e3da3894c21c30~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_7636649a1ff9458aa8e3da3894c21c30~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_cebe144a25af4074a4c9c4e117f5151f~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_cebe144a25af4074a4c9c4e117f5151f~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_942a74dcc9904220ad8a083175525065~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_942a74dcc9904220ad8a083175525065~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_a850de011c6d466d846e11bb2b17c76c~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_a850de011c6d466d846e11bb2b17c76c~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_26e3de48092d43109dbce7cbd416e55d~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_26e3de48092d43109dbce7cbd416e55d~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_cdee6730fab84e57812cf8fc35b5691e~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_cdee6730fab84e57812cf8fc35b5691e~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_d01a681b6d474221b1896fefd6f4a15c~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_d01a681b6d474221b1896fefd6f4a15c~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_29761607d78d46de8815c7bca0e12905~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_29761607d78d46de8815c7bca0e12905~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_3dd5926473b9464889f3a4a0e88b79e2~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_3dd5926473b9464889f3a4a0e88b79e2~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_81a0d4d103294848863f0d2e6e955c2a~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_81a0d4d103294848863f0d2e6e955c2a~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_223f9ee4eb994faba6bdab9ee705c4c5~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_223f9ee4eb994faba6bdab9ee705c4c5~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_7221aa8567a1492b9d06d0f46cb6421c~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_7221aa8567a1492b9d06d0f46cb6421c~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_1f69f9ec04654e3aa23fd1546e907c5d~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_1f69f9ec04654e3aa23fd1546e907c5d~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_7636649a1ff9458aa8e3da3894c21c30~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_7636649a1ff9458aa8e3da3894c21c30~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_cebe144a25af4074a4c9c4e117f5151f~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_cebe144a25af4074a4c9c4e117f5151f~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_942a74dcc9904220ad8a083175525065~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_942a74dcc9904220ad8a083175525065~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_a850de011c6d466d846e11bb2b17c76c~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_a850de011c6d466d846e11bb2b17c76c~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_26e3de48092d43109dbce7cbd416e55d~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_26e3de48092d43109dbce7cbd416e55d~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_cdee6730fab84e57812cf8fc35b5691e~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_cdee6730fab84e57812cf8fc35b5691e~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_d01a681b6d474221b1896fefd6f4a15c~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_d01a681b6d474221b1896fefd6f4a15c~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_29761607d78d46de8815c7bca0e12905~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_29761607d78d46de8815c7bca0e12905~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_3dd5926473b9464889f3a4a0e88b79e2~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_3dd5926473b9464889f3a4a0e88b79e2~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_81a0d4d103294848863f0d2e6e955c2a~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_81a0d4d103294848863f0d2e6e955c2a~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_223f9ee4eb994faba6bdab9ee705c4c5~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_223f9ee4eb994faba6bdab9ee705c4c5~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_7221aa8567a1492b9d06d0f46cb6421c~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_7221aa8567a1492b9d06d0f46cb6421c~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_1f69f9ec04654e3aa23fd1546e907c5d~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_1f69f9ec04654e3aa23fd1546e907c5d~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_7636649a1ff9458aa8e3da3894c21c30~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_7636649a1ff9458aa8e3da3894c21c30~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_cebe144a25af4074a4c9c4e117f5151f~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_cebe144a25af4074a4c9c4e117f5151f~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_942a74dcc9904220ad8a083175525065~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_942a74dcc9904220ad8a083175525065~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_a850de011c6d466d846e11bb2b17c76c~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_a850de011c6d466d846e11bb2b17c76c~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_26e3de48092d43109dbce7cbd416e55d~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_26e3de48092d43109dbce7cbd416e55d~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_cdee6730fab84e57812cf8fc35b5691e~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_cdee6730fab84e57812cf8fc35b5691e~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_d01a681b6d474221b1896fefd6f4a15c~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_d01a681b6d474221b1896fefd6f4a15c~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_29761607d78d46de8815c7bca0e12905~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_29761607d78d46de8815c7bca0e12905~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_3dd5926473b9464889f3a4a0e88b79e2~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_3dd5926473b9464889f3a4a0e88b79e2~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_81a0d4d103294848863f0d2e6e955c2a~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_81a0d4d103294848863f0d2e6e955c2a~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_223f9ee4eb994faba6bdab9ee705c4c5~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_223f9ee4eb994faba6bdab9ee705c4c5~mv2.jpg");
        lisImgPath.Add("https://static.wixstatic.com/media/4a0a97_7221aa8567a1492b9d06d0f46cb6421c~mv2.jpg/v1/fill/w_200,h_200,al_c,q_80/4a0a97_7221aa8567a1492b9d06d0f46cb6421c~mv2.jpg");


        base.Start();
        Invoke("SetImgDataList", 1);
    }

    public void SetImgDataList()
    {
        List<CellDataBase> lisImgCellData = new List<CellDataBase>();

        foreach (var item in lisImgPath)
        {
            lisImgCellData.Add(new ImgCellData() { imgPath = item });
        }

        setDataList(lisImgCellData).ReloadData();//启动Scroller
    }

    public void OnRefershBtnClick()
    {
        RefershData();
    }

}
