![](https://github.com/ProjectBLUE-000/RepulserEngine/workflows/StandaloneWindows64/badge.svg)

# Repulser Engine

Timecode(LTC)信号を受信し、特定のタイムコードのタイミングでOSCの信号を発火するアプリケーションです。

![thumbnail](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Assets/Thumbnails/thumbnail.png)

# Audio Input Setting

オーディオ入力の設定を行います。

オーディオ入力設定は、アプリを再起動すると再度設定する必要がありますのでご注意ください。(修正予定)

![audio](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Assets/Thumbnails/audio.png)

- Device
    - PCに認識されているオーディオデバイスが列挙されます。
    - 使用したいデバイスを選択してください。

![device](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Assets/Thumbnails/device.png)

- Channel
    - オーディオデバイスにチャンネル情報がある場合はチャンネルが列挙されます。
    - 使用したいチャンネルを選択してください。
- Volume
    - 音声入力のボリュームを制御できます。
    - ピークに触れないように設定するのがベターです。
- Status
    - Sampling rate : 44,100Hz 固定です。
    - Software Latency : このアプリケーションに取り込むまでのレイテンシが表示されます。
    - Amplifier : 上記ボリュームの大きさが表示されます。

# Timecode / Status Indicator

タイムコードを表示するGUIと、発火したOSC信号、受信しているLTCがDF/NDFであるかを表示するGUIです。

![indicator](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Assets/Thumbnails/indicator.png)

# Endpoint Setting List

OSC送信先を複数設定できます。

![endpoint](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Assets/Thumbnails/endpoint.png)

Saveボタンを押した際に、すべての宛先に接続テストのOSC信号がそれぞれ以下で送信されます。

| address                 | data                              | 
| ----------------------- | --------------------------------- | 
| /connection-test-string | "Connection test : Hello string!" | 
| /connection-test-int    | 10                                | 
| /connection-test-float  | 3.14                              | 


# Pulse Setting List
OSCの信号を送信するタイムコードを定義する機能です。セーブした時点で、設定が有効になります。

コンポーネント右側のボタンで、リストの順番の入れ替え、及び削除が可能です。

![pulse](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Assets/Thumbnails/pulse.png)

- Address
    - 送信するOSC信号のアドレスを定義します。
- Data
    - 上記アドレスに付随するデータを定義します。
    - 入力からfloat, int, stringを自動で判別し、OSCの信号が該当の値型で送信されます。
- Timecode
    - 信号を送信するタイミングのタイムコードを入力します。
    - 受信するLTC信号がドロップフレームの場合は、ドロップフレームのタイムコードに変換する必要があるためご注意ください。
        - 内部的にタイムコードの比較をイコール判定しているためです。修正予定です。
- Override IP
    - 何も入力していない場合、エンドポイントリストにあるすべての宛先にOSCが送信されます。
    - エンドポイントリストにある特定の宛先にのみ送りたい場合は、該当するIPアドレスを入力すると、その宛先だけに飛ぶようになります。
- Sendボタン
    - LTC信号に関わらず、ボタンを押したタイミングで信号を送ることができます。

編集を行うと、背景が赤に変化するので、入力終了次第Saveを押してください。

![edit](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Assets/Thumbnails/edit.png)

# 保存データ

設定は StreamingAssetsフォルダ以下にEndpointSetting、PulseSetting、それぞれjsonで保存されています。

Editorの場合は、```Assets/StreamingAssets``` 以下に保存され、

ビルド済アプリの場合は、```アプリのフォルダパス/RepulserEngine_Data/StreamingAssets``` 以下に配置されます。

これらのファイルを直接上書きすることで、アプリを介さずに設定を行うことが可能です。

# For developer

This project is designed with [clean architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).

![arch](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Assets/Thumbnails/arch.png)



# LICENSE

All libraries are licensed under MIT license.

- libsoundio - https://github.com/keijiro/jp.keijiro.libsoundio
- Timecode4Net - https://github.com/ailen0ada/Timecode4net
- Extenject (Zenject) - https://github.com/svermeulen/Extenject
- UniRx - https://github.com/neuecc/UniRx
- unity-osc - https://github.com/nobnak/unity-osc
- UIGradient - https://github.com/azixMcAze/Unity-UIGradient