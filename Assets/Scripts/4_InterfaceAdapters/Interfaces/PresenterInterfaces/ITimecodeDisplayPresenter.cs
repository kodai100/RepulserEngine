using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public interface ITimecodeDisplayPresenter
    {
        IObservable<TimecodeData> OnUpdateTimecodeAsObservable { get; }
    }
    
}
