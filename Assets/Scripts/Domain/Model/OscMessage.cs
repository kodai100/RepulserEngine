
namespace ProjectBlue.RepulserEngine.Domain.Model
{
    public class OscMessage
    {
        public string OverrideIp = "";
        public string OscAddress;
        public string OscData;

        public OscMessage(string overrideIp, string oscAddress, string oscData)
        {
            this.OverrideIp = overrideIp;
            this.OscAddress = oscAddress;
            this.OscData = oscData;
        }

        public OscMessage(string oscAddress, string oscData)
        {
            this.OscAddress = oscAddress;
            this.OscData = oscData;
        }
    }

}