using System.Collections.Generic;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class TimecodeSettingUseCase : ITimecodeSettingUseCase
    {
        private ITimecodeSettingRepository timecodeSettingRepository;

        public TimecodeSettingUseCase(ITimecodeSettingRepository timecodeSettingRepository)
        {
            this.timecodeSettingRepository = timecodeSettingRepository;
        }

        public IEnumerable<TimecodeSetting> Load()
        {
            return timecodeSettingRepository.Load();
        }

        public void Save(IEnumerable<TimecodeSetting> timecodeSettings)
        {
            timecodeSettingRepository.Save(timecodeSettings);
        }
    }
}