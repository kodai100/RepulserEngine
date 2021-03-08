using System;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public interface ITimecodeDisplayPresenter
    {
        IObservable<TimecodeData> OnUpdateTimecodeAsObservable { get; }
    }
    
}
