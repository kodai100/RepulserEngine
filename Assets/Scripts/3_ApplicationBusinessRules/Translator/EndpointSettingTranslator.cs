using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Translators
{

    public class EndpointSettingTranslator
    {

        public static EndpointSettingDataModel Translate(EndpointSettingViewModel viewModel)
        {
            var dataModel = new EndpointSettingDataModel(viewModel.EndPoint, viewModel.EndPointName, viewModel.OffsetFrame);
            return dataModel;
        }
        
        public static EndpointSettingViewModel Translate(EndpointSettingDataModel dataModel)
        {
            var viewModel = new EndpointSettingViewModel(dataModel.EndPoint, dataModel.EndPointName, dataModel.OffsetFrame);
            return viewModel;
        }
        
    }

}