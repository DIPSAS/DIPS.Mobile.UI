The `AmplitudeView` is a versatile component designed for visualizing audio input, such as capturing audio from a microphone. It provides a dynamic representation of audio amplitude in real-time, making it ideal for applications requiring audio visualization.

## Features

- **Audio Visualization**: Displays real-time amplitude data.
- **Timer Integration**: Includes a built-in timer to track the duration of audio input or visualization sessions.
- **Customizable**: Easily adaptable to fit various design and functional requirements.

## Use Cases

- Visualizing live audio input from a microphone.
- Monitoring audio levels during recording sessions.
- Enhancing user interfaces with dynamic audio feedback.

## Initialization of `AmplitudeView`

To initialize the `AmplitudeView`, you need to create a custom controller that inherits from `AmplitudeViewController`. This controller will manage the logic and data binding for the view.

```csharp
public class Controller : AmplitudeViewController
{
    public override float GetNextAmplitude()
    {
        return (float)new Random().NextDouble();
    }
}
```

```xml
<dui:AmplitudeView Controller="{Binding Controller}" />
```

The `GetNextAmplitude` function expects a normalized float value between `0` and `1`, where `0` corresponds to the minimum amplitude (minimum height) and `1` corresponds to the maximum amplitude (maximum height). It is your responsibility to ensure that the amplitude values are appropriately scaled to fit within this range.

## Running state

To control the running state of the `AmplitudeView`, toggle the `IsRunning` property in the `AmplitudeViewController`. Setting `IsRunning` to `true` starts the visualization, while setting it to `false` pauses it.

## Customization

### SampleRate

The `SampleRate` property determines how frequently the `AmplitudeView` fetches and updates amplitude data. A `SampleRate` of 15 retrieves 15 amplitudes a second.

Adjust the `SampleRate` to balance performance and visualization quality based on your application's requirements.

> The `SampleRate` is default set to 15.

### Frames per second

The `FramesPerSecond` property controls the rendering speed of the `AmplitudeView`. It defines how many frames are drawn per second, directly impacting the smoothness of the visualization.

A higher `FramesPerSecond` value results in smoother animations but may increase CPU usage. Conversely, a lower value reduces resource consumption but may make the visualization appear less fluid.

> The `FramesPerSecond` is default set to the device's screen refresh rate.

### HasTimer
The `HasTimer` property determines whether the `AmplitudeView` includes a timer to track the duration of the visualization session. When set to `true`, the timer is displayed and actively tracks the elapsed time. If set to `false`, the timer is hidden.

This property is useful for applications where tracking the duration of audio input or visualization is necessary, such as in recording or monitoring scenarios.

> The `HasTimer` property is default set to `true`.

### Colors
#### AmplitudeColor
The `AmplitudeColor` property defines the color of the amplitude bars displayed in the visualization. You can set this property to match your application's theme or to provide a visually appealing representation of the audio data.

#### PlaceholderAmplitudeColor
The `PlaceholderAmplitudeColor` property specifies the color used for placeholder amplitudes when no real amplitude data is available. This can be useful for providing a visual cue or fallback state in the absence of live audio input.

#### FadeColor
The `FadeColor` property determines the color used for fading effects in the amplitude visualization. This is utilized to create a smooth transition effect, enhancing the overall visual appeal of the `AmplitudeView`.

>Ideally, this should be set to match the background color of the container in which the `AmplitudeView` resides. This ensures that the fading effects blend seamlessly with the background, making the fading boxes invisible.



