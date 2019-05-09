using System.Collections.Generic;
using TinyFox.WebSocket;

namespace test
{
    /// <summary>
    /// 自定义的WebSocket工作类
    /// </summary>
    public sealed class MyWebSocketWorker : WebSocketWorkerBase
    {

        #region <构造函数>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="reqEnv">OWIN字典</param>
        public MyWebSocketWorker(IDictionary<string, object> reqEnv) : base(reqEnv) { }

        #endregion




        #region <对父类事件的具体实现>


        /// <summary>
        /// 有新用户连接过来并且成功完成了握手等操作
        /// </summary>
        /// <param name="content">与新连接绑定的"会话对象"</param>
        protected override void OnAccept(WSContext content)
        {
            // 可以做一些初始化工作，比如登记客户IP地址之类的事情
            // var rip = content.RemoteIpAddress;
            // var url = content.RequestPath;
            // content.Tag = "xxx";
        }


        /// <summary>
        /// 接到到了数据的事件
        /// </summary>
        /// <param name="content">会话对象</param>
        /// <param name="message">消息内容</param>
        protected override void OnMessage(WSContext content, string message)
        {

            //约定：如果客户端发来 "exit" "close" 字串
            //服务器就关闭这个连接
            if (message == "exit" || message == "close")
            {
                //服务器关闭会话
                content.Close();
                return;
            }

            //回应客户端发送过来的内容
            content.SendMessage(message);
        }


        /// <summary>
        /// 数据发送完成的事件
        /// </summary>
        /// <param name="content">会话对象</param>
        protected override void OnSendComplete(WSContext content)
        {

            // your code ...

            /// ..... ////
        }



        /// <summary>
        /// 客户端关闭连接事件
        /// </summary>
        /// <param name="content">会话对象</param>
        protected override void OnClose(WSContext content)
        {

            // your code;
        }

        #endregion


    }
}
