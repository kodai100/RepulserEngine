using System;
using System.Linq;
using System.Threading;
using kodai100.TimeCodeCalculation;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class SignalTriggerController : IDisposable, ISignalTriggerController
    {
        public IObservable<CommandSetting> OnTriggerAsObservable { get; }

        private ITimecodeSettingUseCase timecodeSettingUseCase;
        private IGlobalFrameOffsetSettingUseCase globalFrameOffsetSettingUseCase;

        private IEndPointSettingUseCase endPointSettingUseCase;
        private ISendToEndpointUseCase sendToEndpointUseCase;
        private ICommandSettingUseCase commandSettingUseCase;
        private IOnAirSettingUseCase onAirSettingUseCase;

        private CompositeDisposable disposable = new CompositeDisposable();

        private IOverlayUseCase overlayUseCase;

        public SignalTriggerController(
            ITimecodeSettingUseCase timecodeSettingUseCase,
            ITimecodeDecodeUseCase timecodeDecodeUseCase,
            IEndPointSettingUseCase endPointSettingUseCase,
            ISendToEndpointUseCase sendToEndpointUseCase,
            ICommandSettingUseCase commandSettingUseCase,
            IOnAirSettingUseCase onAirSettingUseCase,
            ICommandTriggerUseCase commandTriggerUseCase,
            IGlobalFrameOffsetSettingUseCase globalFrameOffsetSettingUseCase,
            IOverlayUseCase overlayUseCase)
        {
            this.timecodeSettingUseCase = timecodeSettingUseCase;
            this.globalFrameOffsetSettingUseCase = globalFrameOffsetSettingUseCase;

            this.endPointSettingUseCase = endPointSettingUseCase;
            this.sendToEndpointUseCase = sendToEndpointUseCase;
            this.commandSettingUseCase = commandSettingUseCase;
            this.onAirSettingUseCase = onAirSettingUseCase;

            this.overlayUseCase = overlayUseCase;

            timecodeDecodeUseCase.OnTimecodeUpdatedAsObservable.Subscribe(OnTimecodeUpdated).AddTo(disposable);

            commandTriggerUseCase.OnCommandTriggeredAsObservable.Subscribe(Send).AddTo(disposable);
        }

        public void Send(string command)
        {
            if (!onAirSettingUseCase.OnAirSettingViewModel.IsOnAir)
            {
                Debug.Log($"Current not on air. but this command triggered : {command}");
                return;
            }

            // TODO: CommandSettingUseCaseにGetCurrentを実装する
            var commandData = commandSettingUseCase.Load()
                .FirstOrDefault(element => element.CommandName == command);

            var endPoints = endPointSettingUseCase.GetCurrent();
            foreach (var endPoint in endPoints)
            {
                // 設定で有効じゃない場合はスキップ
                if (!endPoint.ConnectionEnabled) continue;

                var ipEndPoint = endPoint.EndPoint;

                if (commandData != null)
                {
                    sendToEndpointUseCase.Send(ipEndPoint, commandData.CommandName, commandData.CommandArguments,
                        commandData.CommandType);

                    overlayUseCase.Trigger(Color.red);
                }
            }
        }

        private void OnTimecodeUpdated(TimecodeData timecode)
        {
            var offsetTimecode = OffsetFilter(timecode);
            Debug.Log($"{timecode} : {offsetTimecode}");

            foreach (var timecodeSetting in timecodeSettingUseCase.Load())
            {
                if (timecodeSetting == null) continue;

                var state = timecodeSetting.Evaluate(offsetTimecode);

                if (state == PulseState.Pulse)
                {
                    Send(timecodeSetting.ConnectedCommandName);
                }
            }
        }

        // TODO: combine to decode use case (same code in TimecodeDisplayController)
        private TimecodeData OffsetFilter(TimecodeData inputTimecode)
        {
            var setting = globalFrameOffsetSettingUseCase.GetCurrent();
            var info = new FrameRateInfo((FrameRateType) Enum.ToObject(typeof(FrameRateType), setting.FrameRateType));
            var timecodeForCalculator = new TimeCode
            {
                DropFrame = info.DropFrame, Hour = inputTimecode.hour, Minute = inputTimecode.minute,
                Second = inputTimecode.second,
                Frame = inputTimecode.frame
            };
            var num = TimeCodeCalculator.TimeCodeToNumber(timecodeForCalculator, info);
            num += setting.Offset;

            // if smaller than zero by minus offset, return raw input value
            if (num < 0)
            {
                var failed = new TimecodeData(inputTimecode.hour, inputTimecode.minute, inputTimecode.second,
                    inputTimecode.frame,
                    inputTimecode.dropFrame);
                return failed;
            }

            var filteredTc = TimeCodeCalculator.FrameNumberToTimeCode(num, info);

            // avoid reference data override
            var result = new TimecodeData(filteredTc.Hour, filteredTc.Minute, filteredTc.Second, filteredTc.Frame,
                info.DropFrame);

            return result;
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}