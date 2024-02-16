using Android.Hardware.Camera2;

namespace Playground.HåvardSamples.Scanning.Android;

public class CaptureSessionCallBack : CameraCaptureSession.CaptureCallback
{
    private readonly Action<CaptureResult> m_captureStillPicture;
    private readonly Action<CaptureResult> m_runPreCaptureSequence;
    private readonly Action<CaptureResult> m_previewingCapture;

    public CaptureSessionCallBack(Action<CaptureResult> captureStillPicture, Action<CaptureResult> runPreCaptureSequence, Action<CaptureResult> previewingCapture)
    {
        m_captureStillPicture = captureStillPicture;
        m_runPreCaptureSequence = runPreCaptureSequence;
        m_previewingCapture = previewingCapture;
    }

    private CaptureState m_state = CaptureState.Preview;

    private void Process(CaptureResult result)
    {
        switch (m_state)
        {
            case CaptureState.Preview:
                {
                    // We have nothing to do when the camera preview is working normally.
                    m_previewingCapture.Invoke(result);
                    break;
                }
            case CaptureState.WaitingLock:
                {
                    if (!int.TryParse(result.Get(CaptureResult.ControlAfState).ToString(), out var afState))
                    {
                        m_captureStillPicture.Invoke(result);
                    }
                    else if ((int)ControlAFState.FocusedLocked == afState ||
                             (int)ControlAFState.NotFocusedLocked == afState)
                    {
                        // CONTROL_AE_STATE can be null on some devices
                        if (!int.TryParse(result.Get(CaptureResult.ControlAfState).ToString(), out var aeState))
                        {
                            aeState = (int)ControlAEState.Converged;
                            m_state = CaptureState.PictureTaken;
                            m_captureStillPicture.Invoke(result);
                        }
                        else
                        {
                            m_runPreCaptureSequence.Invoke(result);
                        }
                    }

                    break;
                }
            case CaptureState.WaitingPreCapture:
                {
                    // CONTROL_AE_STATE can be null on some devices
                    if (!int.TryParse(result.Get(CaptureResult.ControlAfState).ToString(), out var aeState) ||
                        aeState == (int)ControlAEState.Precapture || aeState == (int)ControlAEState.FlashRequired)
                    {
                        m_state = CaptureState.WaitingNonPreCapture;
                    }

                    break;
                }
            case CaptureState.WaitingNonPreCapture:
                {
                    // CONTROL_AE_STATE can be null on some devices
                    if (!int.TryParse(result.Get(CaptureResult.ControlAfState).ToString(), out var aeState) ||
                        aeState == (int)ControlAEState.Precapture)
                    {
                        m_state = CaptureState.PictureTaken;
                        m_captureStillPicture.Invoke(result);
                    }

                    break;
                }
        }
    }

    public override void OnCaptureProgressed(CameraCaptureSession session, CaptureRequest request,
        CaptureResult partialResult)
    {
        Process(partialResult);
    }

    public override void OnCaptureCompleted(CameraCaptureSession session, CaptureRequest request,
        TotalCaptureResult result)
    {
        Process(result);
    }
}

public enum CaptureState
{
    Preview = 0,
    WaitingLock = 1,
    WaitingPreCapture = 2,
    WaitingNonPreCapture = 3,
    PictureTaken = 4
}