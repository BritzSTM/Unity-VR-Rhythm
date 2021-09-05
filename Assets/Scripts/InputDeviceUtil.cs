using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.XR.Haptics;

public interface IInputDeviceRumble
{
    void Rumble(float amplitude, float duration);
}

namespace _internal
{
    public class XRCommonRumble : IInputDeviceRumble
    {
        private static readonly Version _chIssueVersion;
        private static readonly int _chBase;

        public readonly InputDevice Device;

        private SendHapticImpulseCommand _lastCmd;
        private float _lastAT;
        private float _lastDT;

        static XRCommonRumble()
        {
            _chIssueVersion = new Version(1, 1, 1);

            /*
             * input system 버전이슈 1.1.1 미만이라면 진동신호 기준이 1채널부터 시작
             * 1.1.1부터는 0부터 시작
             */
            _chBase = (InputSystem.version < _chIssueVersion) ? 1 : 0;
        }

        public XRCommonRumble(InputDevice device)
        {
            Device = device;
        }

        public void Rumble(float amplitude, float duration)
        {
            UpdateCommand(amplitude, duration);

            Device.ExecuteCommand(ref _lastCmd);
        }

        private void UpdateCommand(float amplitude, float duration)
        {
            if (_lastAT == amplitude && _lastDT == duration)
                return;

            _lastAT = amplitude;
            _lastDT = duration;

            _lastCmd = SendHapticImpulseCommand.Create(_chBase, amplitude, duration);
        }
    }
}

public static class InputDeviceUtil
{
    public static IInputDeviceRumble CreateDeviceRumbleOrNull<T>(InternedString usage)
        where T : InputDevice
    {
        // 현재는 단순히 XRController 구성만 구현되어 있으므로 단순하게 구현

        if (typeof(T) != typeof(XRController))
        {
            Debug.LogWarning($"[{nameof(InputDeviceUtil)}] Not impl {typeof(T)}");
            return null;
        }

        var device = InputSystem.GetDevice<T>(usage);
        if (device == null)
        {
            Debug.LogWarning($"[{nameof(InputDeviceUtil)}] Failed GetDevice for {typeof(T)} : {usage}");
            return null;
        }

        IInputDeviceRumble deviceRumble = new _internal.XRCommonRumble(device);
        return deviceRumble;
    }
}
