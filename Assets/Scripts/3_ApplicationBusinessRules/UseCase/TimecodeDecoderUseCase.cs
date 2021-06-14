using System;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Repository;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class TimecodeDecoderUseCase : ITimecodeDecodeUseCase
    {
        private ITimecodeDecoderRepository repository;

        public IObservable<TimecodeData> OnTimecodeUpdatedAsObservable => repository.OnTimecodeUpdatedAsObservable;

        public TimecodeDecoderUseCase(ITimecodeDecoderRepository repo)
        {
            this.repository = repo;
        }
    }
}