# Timecode4net
`Timecode4net` is a C# class for operations with [SMPTE timecodes](https://en.wikipedia.org/wiki/SMPTE_timecode).

[![Build status](https://ci.appveyor.com/api/projects/status/dwb6uv5cjp8tjuod?svg=true)](https://ci.appveyor.com/project/ailen0ada/timecode4net)
[![NuGet version](https://badge.fury.io/nu/TimeCode4net.svg)](https://badge.fury.io/nu/TimeCode4net)

## Features

- supports 23.98, 24, 25, 29.97, 30, 50, 59.94, 60 fps and msec
- supports drop-frame and non-drop-frame codes
- instantiate timecodes from frame count, string time code
- (WIP)timecode arithmetics: adding frame counts and other timecodes

## Usage

```cs
var fromFrames = Timecode.FromFrames(frames: 1800, frameRate: FrameRate.fps29_97, isDropFrame: true);
Console.WriteLine(fromFrames.ToString()); // 00:01:00;02

var fromString = Timecode.FromString(input: "01:00:03;35", frameRate: FrameRate.fps59_94, isDropFrame: true);
Console.WriteLine(fromString.TotalFrames); // 215999
```

## Credits
[smpte-timecode](https://github.com/CrystalComputerCorp/smpte-timecode) by Crystal Computer Corp.
