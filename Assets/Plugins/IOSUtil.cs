using System.Diagnostics;
using System.Runtime.InteropServices;

public class TargetController 
{
    /* ほかのコード略 */
    [DllImport("__Internal")]
    private static extern void playSystemSound(int n);
    /* ほかのコード略 */
    public static void Play(int sound = 1519)
    {
#if !UNITY_EDITOR && UNITY_IOS
        playSystemSound(sound);
#endif
    }
}