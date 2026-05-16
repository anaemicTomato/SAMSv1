using System;
using System.Runtime.InteropServices;

namespace SAMSv1.Services
{
    // ===================================================================
    //  HCNetSDK.cs
    //
    //  This file is a C# "bridge" to the Hikvision HCNetSDK.dll.
    //  It tells C# exactly which functions exist in the DLL and what
    //  parameters they take. This technique is called P/Invoke.
    //
    //  You never call DLL functions directly — you call the static
    //  methods defined here and C# handles the rest automatically.
    //
    //  Only the functions we actually use are declared here.
    //  The full SDK has hundreds more, but we don't need them.
    // ===================================================================
    public static class HCNetSDK
    {
        // Path to the DLL — relative to where the .exe runs from.
        // Since DLLs must be in the same folder as the .exe at runtime,
        // we reference them by name only (no path needed at runtime).
        private const string SDK_DLL = "HCNetSDK.dll";

        // ---------------------------------------------------------------
        //  CONSTANTS
        // ---------------------------------------------------------------

        // NET_DVR_SetSDKInitCfg config types
        public const uint NET_SDK_INIT_CFG_SDK_PATH       = 2;   // where SDK support files are
        public const uint NET_SDK_INIT_CFG_LIBEAY32_PATH  = 3;
        public const uint NET_SDK_INIT_CFG_SSLEAY32_PATH  = 4;

        // Login mode
        public const uint ISAPI_PROTOCOL = 0;   // modern protocol, works with our device

        // Alarm types we listen for
        public const uint COMM_ALARM_ACS = 0x4000;   // Access Control System alarm (face scan)

        // Error codes
        public const uint NET_DVR_NOERROR = 0;

        // ---------------------------------------------------------------
        //  STRUCTS — match exactly what the SDK DLL expects in memory
        //
        //  IMPORTANT: StructLayout(LayoutKind.Sequential) means C# will
        //  lay out the fields in memory in the exact order written here,
        //  which is what the native DLL requires.
        // ---------------------------------------------------------------

        // Used by NET_DVR_Login_V40 to pass login parameters
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_USER_LOGIN_INFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDeviceAddress;       // device IP

            public byte   byUseTransport;       // 0 = TCP
            public ushort wPort;                // device port (usually 8000)

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sUserName;            // "admin"

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sPassword;            // your password

            public IntPtr cbLoginResult;        // callback (we don't use it)
            public IntPtr pUser;                // user data pointer (we don't use it)
            public bool   bUseAsynLogin;        // false = synchronous login
            public byte   byProxyType;
            public byte   byUseUTCTime;
            public int    iLoginMode;           // 0 = ISAPI_PROTOCOL
            public int    iHttps;               // 0 = no HTTPS
            public int    iProxyID;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 120)]
            public byte[] byRes3;               // reserved — must be zeroed
        }

        // Returned by NET_DVR_Login_V40 with device info
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DEVICEINFO_V40
        {
            public NET_DVR_DEVICEINFO_V30 struDeviceV30;
            public byte   bySupportLock;
            public byte   byRetryLoginTime;
            public byte   byPasswordLevel;
            public byte   byProxyType;
            public uint   dwSurplusLockTime;
            public byte   byCharEncodeType;
            public byte   bySupportDev5;
            public byte   bySupport;
            public byte   byLoginMode;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 120)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DEVICEINFO_V30
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            public byte[] sSerialNumber;

            public byte   byAlarmInPortNum;
            public byte   byAlarmOutPortNum;
            public byte   byDiskNum;
            public byte   byDVRType;
            public byte   byChanNum;
            public byte   byStartChan;
            public byte   byAudioChanNum;
            public byte   byIPChanNum;
            public byte   byZeroChanNum;
            public byte   byMainProto;
            public byte   bySubProto;
            public byte   bySupport;
            public byte   bySupport1;
            public byte   bySupport2;
            public ushort wDevType;
            public byte   bySupport3;
            public byte   byMultiStreamProto;
            public byte   byStartDChan;
            public byte   byStartDTalkChan;
            public byte   byHighDChanNum;
            public byte   bySupport4;
            public byte   byLanguageType;
            public byte   byVoiceInChanNum;
            public byte   byStartVoiceInChanNo;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes3;

            public byte   byMirrorChanNum;
            public ushort wStartMirrorChanNo;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes2;
        }

        // The alarm callback receives this struct for ACS (access control) events.
        // This is where the face scan data lives.
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ALARM_ISAPI_INFO
        {
            public uint   dwSize;
            public IntPtr pAlarmData;       // pointer to the raw alarm JSON/XML bytes
            public uint   dwAlarmDataLen;   // length of the data
            public byte   byDataType;       // 1 = JSON, 2 = XML
            public byte   byRes1;
            public ushort wRes2;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;
        }

        // Wrapper that the alarm callback receives — contains alarm type and data
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMER
        {
            public byte   byUserIDValid;
            public byte   bySerialValid;
            public byte   byVersionValid;
            public byte   byDeviceNameValid;
            public byte   byMacAddrValid;
            public byte   byLinkPortValid;
            public byte   byDeviceIPValid;
            public byte   byDomainNameValid;

            public int    lUserID;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            public byte[] sSerialNumber;

            public uint   dwAlarmVersion;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sDeviceName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byMacAddr;

            public ushort wLinkPort;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sDeviceIP;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string sDomainName;

            public byte   byIPProtocol;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public byte[] byRes2;
        }

        // SDK path config struct (used during initialization)
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_LOCAL_SDK_PATH
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string sPath;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public byte[] byRes;
        }

        // ---------------------------------------------------------------
        //  DELEGATES — define the shape of callback functions
        //
        //  The SDK calls our C# function when an alarm fires.
        //  The delegate tells C# what parameters that function takes.
        // ---------------------------------------------------------------

        // This is the callback the SDK fires when a face is scanned.
        // lCommand tells us what type of event it is (we check for COMM_ALARM_ACS).
        // pAlarmInfo points to a NET_DVR_ALARM_ISAPI_INFO struct.
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void MSGCallBack(
            int    lCommand,
            ref NET_DVR_ALARMER pAlarmer,
            IntPtr pAlarmInfo,
            uint   dwBufLen,
            IntPtr pUser);

        // ---------------------------------------------------------------
        //  SDK FUNCTIONS — imported from HCNetSDK.dll
        // ---------------------------------------------------------------

        // Initialize the SDK — must be called first before anything else.
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern bool NET_DVR_Init();

        // Set SDK config (e.g. tell SDK where its support files are).
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern bool NET_DVR_SetSDKInitCfg(uint enumType, IntPtr lpInBuff);

        // Clean up SDK resources — call when app closes.
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern bool NET_DVR_Cleanup();

        // Log into the device. Returns a userID (≥0) on success, -1 on failure.
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int NET_DVR_Login_V40(
            ref NET_DVR_USER_LOGIN_INFO pLoginInfo,
            ref NET_DVR_DEVICEINFO_V40  lpDeviceInfo);

        // Log out of the device.
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern bool NET_DVR_Logout(int lUserID);

        // Register the callback function that fires when alarms/events occur.
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V50(
            int          iIndex,
            MSGCallBack  fMessageCallBack,
            IntPtr       pUser);

        // Tell the device to start sending alarm events to our callback.
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int NET_DVR_StartListen_V30(
            string   sLocalIP,
            ushort   wLocalPort,
            MSGCallBack DataCallBack,
            IntPtr   pUserData);

        // Stop listening for alarms.
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern bool NET_DVR_StopListen_V30(int lListenHandle);

        // Set up alarm upload from a logged-in device.
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int NET_DVR_SetupAlarmChan_V41(int lUserID);

        // Stop alarm upload.
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern bool NET_DVR_CloseAlarmChan_V30(int lAlarmHandle);

        // Get the last error code if something fails.
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern uint NET_DVR_GetLastError();

        // Send an ISAPI request through the SDK (used for registering faces).
        [DllImport(SDK_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern bool NET_DVR_STDXMLConfig(
            int         lUserID,
            ref NET_DVR_XML_CONFIG_INPUT  lpInputParam,
            ref NET_DVR_XML_CONFIG_OUTPUT lpOutputParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_XML_CONFIG_INPUT
        {
            public uint   dwSize;
            public IntPtr lpRequestUrl;       // pointer to URL string bytes
            public uint   dwRequestUrlLen;
            public IntPtr lpInBuffer;         // pointer to request body bytes
            public uint   dwInBufferSize;
            public uint   dwRecvTimeOut;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_XML_CONFIG_OUTPUT
        {
            public uint   dwSize;
            public IntPtr lpOutBuffer;        // pointer to response buffer
            public uint   dwOutBufferSize;
            public uint   dwReturnedXMLSize;
            public IntPtr lpStatusBuffer;     // pointer to status buffer
            public uint   dwStatusSize;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }
    }
}
