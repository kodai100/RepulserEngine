using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.ViewModel
{
    public class OnAirSettingViewModel
    {

        public ReactiveProperty<bool> isOnAir = new ReactiveProperty<bool>();

        public bool IsOnAir => isOnAir.Value;
        
        public OnAirSettingViewModel(bool isOnAir)
        {
            this.isOnAir.Value = isOnAir;
        }

        public OnAirSettingViewModel() : this(false)
        {
            
        }

    }

}