using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Translators
{

    public class OnAirSettingTranslator
    {

        public static OnAirSettingDataModel Translate(OnAirSettingViewModel viewModel)
        {
            var dataModel = new OnAirSettingDataModel(viewModel.IsOnAir);
            return dataModel;
        }
        
        public static OnAirSettingViewModel Translate(OnAirSettingDataModel dataModel)
        {
            var viewModel = new OnAirSettingViewModel(dataModel.IsOnAir);
            return viewModel;
        }
        
    }

}