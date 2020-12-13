using System.Net;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    
    public class EndpointSettingPresenter : ListComponentPresenter<EndpointSettingView, EndpointSetting>
    {
        
        private void Start()
        {
            Observable.Merge(
                listComponentView.OnIPValueChangedAsObservable,
                listComponentView.OnPortValueChangedAsObservable
            ).Subscribe(value =>
            {
                // listComponentView.SetEdited();

                UpdateData();
                
            }).AddTo(this);
        }
        
        
        public override void UpdateData()
        {
            if (Data == null)
            {
                Data = new EndpointSetting(
                    new IPEndPoint(
                        ValidatedIPAddress(listComponentView.ipTextField.text),
                        ValidatedPort(listComponentView.portTextField.text)),
                    "",
                    0
                );
            }
            else
            {
                Data.UpdateData(
                    new IPEndPoint(
                        ValidatedIPAddress(listComponentView.ipTextField.text),
                        ValidatedPort(listComponentView.portTextField.text)),
                    "",
                    0
                );
            }
        }

        private IPAddress ValidatedIPAddress(string address)
        {
            return IPAddress.TryParse(address, out var result) ? result : ValidatedIPAddress("127.0.0.1");
        }

        private int ValidatedPort(string port)
        {
            return int.TryParse(port, out var result) ? result : 10100;
        }
        
        // TODO validate
    }

}
