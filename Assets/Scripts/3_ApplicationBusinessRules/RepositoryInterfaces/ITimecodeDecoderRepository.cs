using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{

    public interface ITimecodeDecoderRepository
    {
        IObservable<TimecodeData> OnTimecodeUpdatedAsObservable { get; }
    }
    
}