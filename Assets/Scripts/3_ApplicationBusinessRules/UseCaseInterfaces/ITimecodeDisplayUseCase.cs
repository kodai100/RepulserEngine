using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface ITimecodeDisplayUseCase
    {
        IObservable<TimecodeData> OnTimecodeUpdatedAsObservable { get; }
    }

}