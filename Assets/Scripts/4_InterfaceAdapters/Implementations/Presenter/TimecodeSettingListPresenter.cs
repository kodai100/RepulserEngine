using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class TimecodeSettingListPresenter : ITimecodeSettingListPresenter
    {

        private ITimecodeSettingUseCase timecodeSettingUseCase;

        public TimecodeSettingListPresenter(ITimecodeSettingUseCase timecodeSettingUseCase)
        {
            this.timecodeSettingUseCase = timecodeSettingUseCase;
        }

        public IEnumerable<TimecodeSetting> Load()
        {
            return timecodeSettingUseCase.Load();
        }
        
        public void Save(IEnumerable<TimecodeSetting> settingList)
        {
            timecodeSettingUseCase.Save(settingList);
        }
    }
}