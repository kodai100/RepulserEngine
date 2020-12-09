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
            Debug.Log("Save");
            endpoint = new IPEndPoint(IPAddress.Parse(listComponentView.IP), int.Parse(listComponentView.Port));
            
            PlayerPrefs.SetString($"IP_{index}", endpoint.Address.ToString());
            PlayerPrefs.SetInt($"Port_{index}", endpoint.Port);
        }

        public override void Load(int index)
        {
            var loadedIp = PlayerPrefs.GetString($"IP_{index}");
            var loadedPort = PlayerPrefs.GetInt($"Port_{index}", 0);
            
            if (!string.IsNullOrEmpty(loadedIp) && loadedPort != 0)
            {
                listComponentView.SetIPText(loadedIp);
                listComponentView.SetPortText(loadedPort);
                endpoint = new IPEndPoint(IPAddress.Parse(loadedIp), loadedPort);
            }
        }
        
    }

}
