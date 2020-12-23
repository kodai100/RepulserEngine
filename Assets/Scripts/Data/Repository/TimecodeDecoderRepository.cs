using System;
using Ltc;
using ProjectBlue.RepulserEngine.DataStore;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class TimecodeDecoderRepository : ITimecodeDecoderRepository
    {

        public IObservable<Timecode> OnTimecodeUpdatedAsObservable 
            => timecodeDecoderDataStore.OnTimecodeUpdatedAsObserbable;

        private ITimecodeDecoderDataStore timecodeDecoderDataStore;
        
        public TimecodeDecoderRepository(ITimecodeDecoderDataStore timecodeDecoderDataStore)
        {
            this.timecodeDecoderDataStore = timecodeDecoderDataStore;
        }

        
    }

}

