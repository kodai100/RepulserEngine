using System;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class TimecodeDecoderRepository : ITimecodeDecoderRepository
    {
        public IObservable<TimecodeData> OnTimecodeUpdatedAsObservable
            => timecodeDecoderDataStore.OnTimecodeUpdatedAsObservable;

        private ITimecodeDecoderDataStore timecodeDecoderDataStore;

        public TimecodeDecoderRepository(ITimecodeDecoderDataStore timecodeDecoderDataStore)
        {
            this.timecodeDecoderDataStore = timecodeDecoderDataStore;
        }
    }
}