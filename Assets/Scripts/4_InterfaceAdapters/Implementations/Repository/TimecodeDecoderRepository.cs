using System;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class TimecodeDecoderRepository : ITimecodeDecoderRepository
    {

        public IObservable<TimecodeData> OnTimecodeUpdatedAsObservable 
            => timecodeDecoderDataStore.OnTimecodeUpdatedAsObserbable;

        private ITimecodeDecoderDataStore timecodeDecoderDataStore;
        
        public TimecodeDecoderRepository(ITimecodeDecoderDataStore timecodeDecoderDataStore)
        {
            this.timecodeDecoderDataStore = timecodeDecoderDataStore;
        }

        
    }

}

