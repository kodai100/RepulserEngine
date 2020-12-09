namespace ProjectBlue.RepulserEngine
{
    public class EndpointListPresenter : ListPresenter<EndpointSettingPresenter, EndpointSettingView>, IEndPointListPresenter
    {

        private OscSender sender = new OscSender();
        
        protected override string SaveHash => "Endpoint";
        
        public void Send(string oscAddress, string oscData)
        {
            
            ComponentList.ForEach(endpointSetting =>
            {
                sender.Send(endpointSetting.EndPoint, oscAddress, oscData);
            });
            
            Logger.Instance.Log($"{oscAddress} : {oscData}");
            
            Overlay.Instance.Trigger();
        }

        
    }
}