using System;
using Ltc;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public interface ITimecodeDecodePresenter
    {
        IObservable<Timecode> OnTimecodeChangedAsObservable { get; }
    }
    
}
