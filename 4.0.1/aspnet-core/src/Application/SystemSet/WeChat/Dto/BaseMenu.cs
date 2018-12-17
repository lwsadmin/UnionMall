using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.SystemSet
{
    public class BaseMenu
    {

        /// <summary>
        /// 菜单的响应动作类型
        /// </summary>
        public MenuType type { get; set; }
        /// <summary>
        /// 菜单标题，不超过16个字节；子菜单不超过40个字节
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// click等点击类型必须  菜单key值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// miniprogram类型必须,小程序的页面路径
        /// </summary>
        public string pagepath { get; set; }

        //miniprogram类型必须,小程序的appid
        public string appid { get; set; }
        /// <summary>
        /// view,miniprogram类型必须  网页链接，用户点击菜单可打开链接，不超过256字节
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<BaseMenu> sub_button { get; set; }
        public BaseMenu()
        {
            this.sub_button = new List<BaseMenu>();
        }
    }
    public enum MenuType
    {
        /// <summary>
        /// 点击推事件
        /// </summary>
        click,
        /// <summary>
        /// 跳转URL
        /// </summary>
        view,
        /// <summary>
        /// 扫码推事件
        /// </summary>
        scancode_push,
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        scancode_waitmsg,
        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        pic_sysphoto,
        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        pic_photo_or_album,
        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        pic_weixin,
        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        location_select,
        /// <summary>
        /// 下发消息（除文本消息）
        /// </summary>
        media_id,
        /// <summary>
        /// 跳转图文消息URL
        /// </summary>
        view_limited,

        /// <summary>
        /// 小程序
        /// </summary>
        miniprogram
    }
}
