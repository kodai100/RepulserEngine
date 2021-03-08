using System;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface ITimecodeDisplayUseCase
    {
        IObservable<TimecodeData> OnTimecodeUpdatedAsObservable { get; }
    }

}