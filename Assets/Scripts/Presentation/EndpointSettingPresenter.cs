using System.Net;
using UnityEngine;

namespace ProjectBlue.RepulserEngine
{
    
    public class EndpointSettingPresenter : ListComponentPresenter<EndpointSettingView>
    {

        private IPEndPoint endpoint;

        public IPEndPoint EndPoint => endpoint;

        public override void Save(int index)
        {
            PlayerPrefs.SetString($"IP_{index}", endpoint.Address.ToString());
            PlayerPrefs.SetInt($"IP_{index}", endpoint.Port);
        }

        public override void Load(int index)
        {
            var loadedIp = PlayerPrefs.GetString($"IP_{index}");
            var loadedPort = PlayerPrefs.GetInt($"Port_{index}");
            
            if (!string.IsNullOrEmpty(loadedIp))
            {
                listComponentView.SetIPText(loadedIp);
                endpoint = new IPEndPoint(IPAddress.Parse(loadedIp), loadedPort);
            }
        }
        
    }

}
