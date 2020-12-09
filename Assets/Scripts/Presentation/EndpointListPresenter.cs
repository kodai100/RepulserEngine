
namespace ProjectBlue.RepulserEngine
{
    public class EndpointListPresenter : ListPresenter<EndpointSettingPresenter, EndpointSettingView>
    {

        private readonly OscSender oscSender = new OscSender();
        
        protected override string SaveHash => "Endpoint";
        
        private void OnDestroy()
        {
            oscSender.Dispose();
        }
    
        public void Send(string oscAddress, string oscData)
        {
            
            ComponentList.ForEach(endpointSetting =>
            {
                oscSender.Send(endpointSetting.EndPoint, oscAddress, oscData);
            });
            
            Logger.Instance.Log($"{oscAddress} : {oscData}");
            
            Overlay.Instance.Trigger();
        }

        
    }
}