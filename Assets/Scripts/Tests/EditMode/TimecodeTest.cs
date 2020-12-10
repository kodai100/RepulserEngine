using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Ltc;
using UnityEditor;

namespace Tests
{
    public class TimecodeTest
    {
        
        private Timecode FramesToTimecode(int frames, int framerate)
        {

            var h = frames / (60 * 60 * framerate);
            var m = (frames / (60 * framerate)) % 60;
            var s = frames % (60 * framerate) / framerate;
            var f = frames % (60 * framerate) % framerate;

            return new Timecode { dropFrame = false, hour = h, minute = m, second = s, frame = f };
        }

        [UnityTest]
        public IEnumerator LargerThanTest()
        {

            var fps = 30;

            var totalFarameNum = 23 * 60 * 60 * fps + 59 * 60 * fps + 59 * fps + (fps-1);

            for(var i = 0; i < totalFarameNum; i++)
            {

                var baseTimecode = FramesToTimecode(i, fps);

                for (var j = 0; j < totalFarameNum; j++)
                {

                    var targetTimecode = FramesToTimecode(j, fps);

                    var result = baseTimecode > targetTimecode;

                    if(baseTimecode.ToFrame(fps) > targetTimecode.ToFrame(fps) != result)
                    {
                        Debug.LogError($"{baseTimecode} > {targetTimecode} = {result}");
                        AsyncProgressBar.Clear();
                    }

                    Assert.AreEqual(baseTimecode.ToFrame(fps) > targetTimecode.ToFrame(fps), result);

                }

                yield return null;
                AsyncProgressBar.Display($"Progress {i} / {totalFarameNum}", i / (float)totalFarameNum);
            }

            // 非表示
            AsyncProgressBar.Clear();

        }

    }
}
