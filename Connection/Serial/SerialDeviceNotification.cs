using System;
using System.Runtime.InteropServices;

internal static class SerialDeviceNotification
{
    public const int DbtDevicearrival = 0x8000; // system detected a new device        
    public const int DbtDeviceremovecomplete = 0x8004; // device is gone      
    public const int WmDevicechange = 0x0219; // device change event      
    private const int DbtDevtypDeviceinterface = 5;
    private static readonly Guid GuidDevinterfaceDevice = new Guid("4d36e978-e325-11ce-bfc1-08002be10318"); // Serial devices
    private static IntPtr notificationHandle;

    /// <summary>
    /// Registers a window to receive notifications when USB devices are plugged or unplugged.
    /// </summary>
    /// <param name="windowHandle">Handle to the window receiving notifications.</param>
    public static void RegisterDeviceNotification(IntPtr windowHandle)
    {
        DevBroadcastDeviceinterface dbi = new DevBroadcastDeviceinterface
        {
            DeviceType = DbtDevtypDeviceinterface,
            Reserved = 0,
            ClassGuid = GuidDevinterfaceDevice,
            Name = 0
        };

        dbi.Size = Marshal.SizeOf(dbi);
        IntPtr buffer = Marshal.AllocHGlobal(dbi.Size);
        Marshal.StructureToPtr(dbi, buffer, true);

        notificationHandle = RegisterDeviceNotification(windowHandle, buffer, 0);
    }

    /// <summary>
    /// Unregisters the window for USB device notifications
    /// </summary>
    public static void UnregisterDeviceNotification()
    {
        UnregisterDeviceNotification(notificationHandle);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, int flags);

    [DllImport("user32.dll")]
    private static extern bool UnregisterDeviceNotification(IntPtr handle);

    [StructLayout(LayoutKind.Sequential)]
    private struct DevBroadcastDeviceinterface
    {
        internal int Size;
        internal int DeviceType;
        internal int Reserved;
        internal Guid ClassGuid;
        internal short Name;
    }
}