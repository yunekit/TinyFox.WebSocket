namespace TinyFox.WebSocket
{
    using System;
    using System.Threading.Tasks;
    using System.Text;


    /// <summary>
    /// WebSocket会话上下文对象
    /// <para>一个会话对象代表一个具体的连接</para>
    /// </summary>
    public class WSContext
    {


        #region <私有变量>

        /// <summary>
        /// 与本上下文关联的websocket对象
        /// </summary>
        private WebSocket _ws = null;

        #endregion



        #region <公共字段>

        /// <summary>
        ///  开发者附加在该会话的对象
        /// </summary>
        public object Tag = null;


        #endregion



        #region <公共属性>

        /// <summary>
        /// 会话的本地IP地址
        /// </summary>
        public string LocalIpAddress { get; private set; }

        /// <summary>
        /// 会话的本地端口号
        /// </summary>
        public int LocalPort { get; private set; }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string RemoteIpAddress { get; private set; }

        /// <summary>
        /// 客户端端口号
        /// </summary>
        public int RemotePort { get; private set; }

        /// <summary>
        /// 访问的URL路径
        /// </summary>
        public string RequestPath { get; private set; }

        /// <summary>
        /// 访问的查询字串
        /// </summary>
        public string QueryString { get; private set; }


        #endregion



        #region <构造与析构>

        /// <summary>
        /// 实例化一个WSContext上下文对象
        /// </summary>
        /// <param name="ws"></param>
        public WSContext(WebSocket ws)
        {
            _ws = ws;

            LocalIpAddress = ws.LocalIpAddress;
            LocalPort = ws.LocalPort;
            RemoteIpAddress = ws.RemoteIpAddress;
            RemotePort = ws.RemotePort;
            RequestPath = ws.RequestPath;
            QueryString = ws.Query;
        }


        /// <summary>
        /// 析构函数
        /// </summary>
        ~WSContext()
        {
            if (_ws == null) return;
            _ws.Close();
            _ws = null;
        }

        #endregion



        #region <公用方法>


        /// <summary>
        /// 向客户端非阻塞发送文本数据(utf8字符集)
        /// </summary>
        /// <param name="msg"></param>
        public void SendMessage(string msg) { _ws.SendMessage(msg); }

        /// <summary>
        /// 向客户端非阻塞发送文本数据
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="encoding">字符集</param>
        public void SendMessage(string text, Encoding encoding) { _ws.SendMessage(text, encoding); }

        /// <summary>
        /// 异步发送文本数据
        /// </summary>
        /// <param name="msg">文本</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public async Task SendMessageAsync(string msg, Encoding encoding) { await _ws.SendMessageAsync(msg, encoding); }

        /// <summary>
        /// 向客户端发送二进制数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <param name="isEndOfMessage">是否是一个继续发送任务的最后一个片断</param>
        public void SendBytes(ArraySegment<byte> buffer, bool isEndOfMessage = true) { _ws.SendBytes(buffer, isEndOfMessage); }


        /// <summary>
        /// 异步发送二进制数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <param name="isEndOfMsg">是否是最后一块数据</param>
        /// <returns></returns>
        public async Task SendBytesAsync(ArraySegment<byte> buffer, bool isEndOfMsg = true) { await _ws.SendBytesAsync(buffer, isEndOfMsg); }

        /// <summary>
        /// 关闭与客户端的连接
        /// </summary>
        public void Close(int code = 1000, string reason = null) { _ws.Close(code, reason); }


        #endregion

    }
}
