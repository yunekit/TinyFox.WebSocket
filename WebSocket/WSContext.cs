namespace TinyFox.WebSocket
{

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

        ~WSContext()
        {
            if (_ws == null) return;
            _ws.Close();
            _ws = null;
        }

        #endregion



        #region <公用方法>


        /// <summary>
        /// 向客户端发送数据
        /// </summary>
        /// <param name="msg"></param>
        public void SendMessage(string msg) { _ws.SendMessage(msg); }


        /// <summary>
        /// 关闭与客户端的连接
        /// </summary>
        public void Close() { _ws.Close(); }

        #endregion

    }
}
