using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyFox.WebSocket
{

    /// <summary>
    /// 自定义WebSocket工作器的基类
    /// </summary>
    public abstract class WebSocketWorkerBase
    {

        /***************************************************************
         * 注意：不建议开发者随意修改本类；
         * 即命名修改，也要小心，要尽量简洁，确保无错
         * *************************************************************/



        #region <变量定义>


        /// <summary>
        /// 与本自定义绑定的WebSocket实例（子类可读）
        /// </summary>
        private readonly WebSocket _webSocket;

        /// <summary>
        /// 与webSocket实例绑定的“会话对话”
        /// </summary>
        private WSContext _content;

        #endregion;



        #region <初始化与开始WebSocket交互的方法实现>

        /// <summary>
        /// MyWebSocket构造函数
        /// </summary>
        /// <param name="owinenv"></param>
        public WebSocketWorkerBase(IDictionary<string, object> owinenv)
        {

            _webSocket = new WebSocket(owinenv)
            {
                OnSend = (ws) => OnSendComplete(_content),
                OnClose = (ws) => OnClose(_content),
                OnRead = (ws, txt) => OnMessage(_content, txt)
            };
        }



        /// <summary>
        /// 完成与客户端握手并开启数据交流过程
        /// </summary>
        ///<param name="param">用于WebSocket握手响应用的参数</param>
        /// <returns></returns>
        public Task Open(IDictionary<string, object> param = null)
        {
            //尝试握手，同意请求
            //如果握手成功
            if (_webSocket.Accept(param))
            {
                _content = new WSContext(_webSocket);

                //激活OnAccept事件，通知应用层握手已经完成
                OnAccept(_content);


                //开始接受远端数据
                //本方法只需在连接成功后调用一次，然后就能不断继续。
                _webSocket.StartAsync();


                //返回WebSocket工作任务
                return _webSocket.WorkTask;
            }

            //如果握手失败
            Console.WriteLine("Error: 与客户端握手失败, 客户端 IP 地址是: {0}", _webSocket.RemoteIpAddress);

            //返回（失败的）完成任务
            return Task.FromResult(new Exception("WebSocket Accept Error."));
        }



        #endregion



        #region <定义需要子类具体实现的方法>



        /// <summary>
        /// 与客户端握手完成事件
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnAccept(WSContext content) { }

        /// <summary>
        /// 发送完成事件
        /// </summary>
        /// <param name="content">会话对象</param>
        protected abstract void OnSendComplete(WSContext content);


        /// <summary>
        /// 接收到客户端数据事件
        /// </summary>
        /// <param name="content">会话对象</param>
        /// <param name="message">内容</param>
        protected abstract void OnMessage(WSContext content, string message);


        /// <summary>
        /// 客户端关闭事件
        /// </summary>
        /// <param name="content">会话对象</param>
        protected abstract void OnClose(WSContext content);


        #endregion

    }
}
