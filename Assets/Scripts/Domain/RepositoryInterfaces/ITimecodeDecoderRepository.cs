using System;
using Ltc;

namespace ProjectBlue.RepulserEngine.Repository
{

    public interface ITimecodeDecoderRepository
    {
        IObservable<Timecode> OnTimecodeUpdatedAsObservable { get; }
    }
    
}