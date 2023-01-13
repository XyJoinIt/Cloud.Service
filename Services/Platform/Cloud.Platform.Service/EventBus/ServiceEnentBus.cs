namespace Cloud.Platform.Service.EventBus
{
    public class ServiceEnentBus : ICapSubscribe
    {
        [CapSubscribe("SendMsgTest")]
        public void SendMsgTest(string msg, [FromCap] CapHeader keyValues)
        {
            Console.WriteLine(msg);
        }

        [CapSubscribe("SendDelayMsgTest")]
        public void SendDelayMsgTest(string msg, [FromCap] CapHeader keyValues)
        {
            Console.WriteLine(msg);
        }
    }
}
