/***************************************************************
 *        WebSocket 应用示例  之二
 * =============================================================   
 * 本DEMO的目的意义：
 *  演示封装一个 WebSocket 对象
 *  
 *  使用方法：将编译得到的dll放到网站的bin文件夹中。
 * *************************************************************/


#region <USING>

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TinyFox.WebSocket;

#endregion


namespace test
{

    /// <summary>
    /// 为Jexus/Tinyfox提示的适配器
    /// </summary>
    public class Startup
    {



        /// <summary>
        /// OWIN适配器的主函数
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public Task OwinMain(IDictionary<string, object> env)
        {
            
            //检查是否是WebSocket会话请求
            if (WebSocket.IsWebSocket(env))
            {
               return new MyWebSocketWorker(env).Open();
                //注：Open方法可以带参数
                //如：Open(new Dictionary<string, object> { {"websocket.SubProtocol","yourxxx"} });
            }


            //如果不是websocket请求，就接普通OWIN处理
            return ProcessOtherRequest(env);
        }



        /// <summary>
        /// 普通OWIN请求的处理函数
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        private Task ProcessOtherRequest(IDictionary<string, object> env)
        {

            // 从字典中获取向客户（浏览器）发送数据的“流”对象
            /////////////////////////////////////////////////////////
            var responseStream = env["owin.ResponseBody"] as Stream;

            // 你准备发送的数据
            const string outString = "<html><head><title>Jexus Owin Server</title></head><body>Jexus Owin Server!<br /><h3>Jexus Owin Server，放飞您灵感的翅膀...</h3>\r\n</body></html>";
            var outBytes = Encoding.UTF8.GetBytes(outString);

            // 从参数字典中获取Response HTTP头的字典对象
            var responseHeaders = env["owin.ResponseHeaders"] as IDictionary<string, string[]>;

            // 设置必要的http响应头
            ////////////////////////////////////////////////////////////////

            // 设置 Content-Type头
            responseHeaders.Add("Content-Type", new[] { "text/html; charset=utf-8" });


            // 把正文写入流中，发送给浏览器
            responseStream.Write(outBytes, 0, outBytes.Length);


            return Task.FromResult(1);

        }


    }


}
