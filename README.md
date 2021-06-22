![](https://github.com/kodai100/RepulserEngine/workflows/StandaloneWindows64/badge.svg) ![total](https://img.shields.io/github/downloads/kodai100/RepulserEngine/total)

# Repulser Engine

Timecode(LTC)信号を受信し、特定のタイムコードのタイミングでOSCの信号を発火するアプリケーションです。

![thumbnail](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/thumbnail.png)

# Audio Input Setting

オーディオ入力の設定を行います。

オーディオ入力設定は、アプリを再起動すると再度設定する必要がありますのでご注意ください。(修正予定)

![audio](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/audio.png)

- Device
    - PCに認識されているオーディオデバイスが列挙されます。
    - 使用したいデバイスを選択してください。

![device](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/device.png)

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

タイムコードを表示するGUIと、受信しているタイムコード信号がドロップフレーム(DF)/ノンドロップフレーム(NDF)であるかを表示するGUIです。

![indicator](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/indicator.png)

また、インジケーターの左側のプルダウンで、想定されている送信元のタイムコードのフレームレートを設定し、

その下のテキストフィールドに、任意のフレーム数で 後ろ倒し(-)・前倒し(+) を設定することが可能です。
例えば、30FPS、10を入力していた場合、01:00:00:00で受信されているタイムコードが、01:00:00:10として受信されます。
ドロップフレームにも対応しています。

対応しているフレームレートは以下の通りです。(SMPTE準拠)

| Frame Rate | Drop Frame | 
| ---------- | ---------- | 
| 30         | False      | 
| 29.97      | True       | 
| 60         | False      | 
| 59.97      | True       | 

# Endpoint Setting

シグナル送信先エンドポイントを複数設定可能です。
それぞれのエンドポイントで、Pingによる接続テスト、送信の可否の選択が可能です。

![endpoint](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/endpoint.png)

また、Mainパネルにて、簡易的に送信の可否、Pingによる接続テストが行えます。

![check](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/check.png)

さらに、STANDBYモード / ONAIRモードの切り替えによって、信号の送信をしない/するを切り替えることができます。
このモードは、アプリ終了時に自動的に保存されます。

![onair](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/onair.png)
![standby](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/standby.png)

# Command Setting
コマンドを定義します。
コマンドの名前、コマンドに付随する値、コマンドの送信形式(OSC/Raw UDP)、メモを保持可能です。

![command](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/command.png)

後述のTimecodeSettingでコマンド名を識別IDとして使用するため、CommandNameは一意の名称をつけてください。

送信形式にOSCを選択した場合、 `/[コマンド名] [値]` の形式で送信されます。

また、コマンドを追加すると、Triggersパネルにコマンド名のついたボタンが追加されます。
このボタンを押すことで、コマンドのポン出しが可能です。

![trigger](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/trigger.png)

# Timecode Setting
コマンドを発火するタイミングのタイムコードと、該当のコマンドを保存します。
ドロップダウンには、CommandSettingで追加したコマンド名が表示されます。

![timecode](https://github.com/ProjectBLUE-000/RepulserEngine/blob/master/Thumbnails/timecode.png)

コンポーネント右側のボタンで、リストの順番の入れ替え、及び削除が可能ですが、タイムコード順に並べ替えておくことが望ましいです。

編集を行うと、背景が赤に変化するので、入力終了次第Saveを押してください。

# 保存データ

設定は StreamingAssetsフォルダ以下にEndpointSetting、PulseSetting、それぞれjsonで保存されています。

Editorの場合は、```Assets/StreamingAssets``` 以下に保存され、

ビルド済アプリの場合は、```アプリのフォルダパス/RepulserEngine_Data/StreamingAssets``` 以下に配置されます。

これらのファイルを直接上書きすることで、アプリを介さずに設定を行うことが可能です。

# For developer

This project is designed with [clean architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).



# LICENSE

All libraries are licensed under MIT license.

- libsoundio - https://github.com/keijiro/jp.keijiro.libsoundio
- Timecode4Net - https://github.com/ailen0ada/Timecode4net
- Extenject (Zenject) - https://github.com/svermeulen/Extenject
- UniRx - https://github.com/neuecc/UniRx
- unity-osc - https://github.com/nobnak/unity-osc
- UIGradient - https://github.com/azixMcAze/Unity-UIGradient