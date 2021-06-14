using System;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.DataStore
{
    public interface ITimecodeDecoderDataStore
    {
        IObservable<TimecodeData> OnTimecodeUpdatedAsObservable { get; }
    }
}