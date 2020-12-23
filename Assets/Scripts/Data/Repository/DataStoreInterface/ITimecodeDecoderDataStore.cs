using System;
using Ltc;

namespace ProjectBlue.RepulserEngine.DataStore
{
    public interface ITimecodeDecoderDataStore
    {
        IObservable<Timecode> OnTimecodeUpdatedAsObserbable { get; }
    }
}