using UnityEngine;

/// <summary>
/// スクリーンショットをキャプチャするサンプル
/// </summary>
public class ScreenShotCapturer : MonoBehaviour
{
    private void Update()
    {
        // スペースキーが押されたら
        if (Input.GetKeyDown(KeyCode.M))
        {
            // スクリーンショットを保存
            CaptureScreenShot("ScreenShot.png");
        }
    }

    // 画面全体のスクリーンショットを保存する
    private void CaptureScreenShot(string filePath)
    {
        ScreenCapture.CaptureScreenshot(filePath);
    }
}