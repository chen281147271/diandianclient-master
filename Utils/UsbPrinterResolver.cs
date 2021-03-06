﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DianDianClient.Utils
{
    public static class UsbPrinterResolver
    {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SP_DEVINFO_DATA
        {
            public uint cbSize;
            public Guid ClassGuid;
            public uint DevInst;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SP_DEVICE_INTERFACE_DATA
        {
            public uint cbSize;
            public Guid InterfaceClassGuid;
            public uint Flags;
            public IntPtr Reserved;
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        private struct SP_DEVICE_INTERFACE_DETAIL_DATA  // Only used for Marshal.SizeOf. NOT!  
        {
            public uint cbSize;
            public char DevicePath;
        }


        [DllImport("cfgmgr32.dll", CharSet = CharSet.Auto, SetLastError = false, ExactSpelling = true)]
        private static extern uint CM_Get_Parent(out uint pdnDevInst, uint dnDevInst, uint ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern uint CM_Get_Device_ID(uint dnDevInst, string Buffer, uint BufferLen, uint ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Auto, SetLastError = false, ExactSpelling = true)]
        private static extern uint CM_Get_Device_ID_Size(out uint pulLen, uint dnDevInst, uint ulFlags);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetupDiGetClassDevs([In(), MarshalAs(UnmanagedType.LPStruct)] System.Guid ClassGuid, string Enumerator, IntPtr hwndParent, uint Flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, uint MemberIndex, ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, [In()] ref SP_DEVINFO_DATA DeviceInfoData, [In(), MarshalAs(UnmanagedType.LPStruct)] System.Guid InterfaceClassGuid, uint MemberIndex, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, [In()] ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, uint DeviceInterfaceDetailDataSize, out uint RequiredSize, IntPtr DeviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);
        //读取设备文件
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern bool ReadFile
            (
                IntPtr hFile,
                byte[] lpBuffer,
                uint nNumberOfBytesToRead,
                ref uint lpNumberOfBytesRead,
                IntPtr lpOverlapped
            );

        private const uint DIGCF_PRESENT = 0x00000002U;
        private const uint DIGCF_DEVICEINTERFACE = 0x00000010U;
        private const int ERROR_INSUFFICIENT_BUFFER = 122;
        private const uint CR_SUCCESS = 0;

        private const int FILE_SHARE_READ = 1;
        private const int FILE_SHARE_WRITE = 2;
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const int OPEN_EXISTING = 3;

        private static readonly Guid GUID_PRINTER_INSTALL_CLASS = new Guid(0x4d36e979, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18);
        private static readonly Guid GUID_DEVINTERFACE_USBPRINT = new Guid(0x28d78fad, 0x5a12, 0x11D1, 0xae, 0x5b, 0x00, 0x00, 0xf8, 0x03, 0xa8, 0xc2);
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        
        private static string GetPrinterRegistryInstanceID(string PrinterName)
        {
            if (string.IsNullOrEmpty(PrinterName)) throw new ArgumentNullException("PrinterName");

            const string key_template = @"SYSTEM\CurrentControlSet\Control\Print\Printers\{0}\PNPData";

            using (var hk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                                string.Format(key_template, PrinterName),
                                Microsoft.Win32.RegistryKeyPermissionCheck.Default,
                                System.Security.AccessControl.RegistryRights.QueryValues
                            )
                   )
            {

                if (hk == null) throw new ArgumentOutOfRangeException("PrinterName", "This printer does not have PnP data.");

                return (string)hk.GetValue("DeviceInstanceId");
            }
        }

        private static string GetPrinterParentDeviceId(string RegistryInstanceID)
        {
            if (string.IsNullOrEmpty(RegistryInstanceID)) throw new ArgumentNullException("RegistryInstanceID");

            IntPtr hdi = SetupDiGetClassDevs(GUID_PRINTER_INSTALL_CLASS, RegistryInstanceID, IntPtr.Zero, DIGCF_PRESENT);
            if (hdi.Equals(INVALID_HANDLE_VALUE)) throw new System.ComponentModel.Win32Exception();

            try
            {
                SP_DEVINFO_DATA printer_data = new SP_DEVINFO_DATA();
                printer_data.cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA));

                if (SetupDiEnumDeviceInfo(hdi, 0, ref printer_data) == 0) throw new System.ComponentModel.Win32Exception();   // Only one device in the set  

                uint cmret = 0;

                uint parent_devinst = 0;
                cmret = CM_Get_Parent(out parent_devinst, printer_data.DevInst, 0);
                if (cmret != CR_SUCCESS) throw new Exception(string.Format("Failed to get parent of the device '{0}'. Error code: 0x{1:X8}", RegistryInstanceID, cmret));


                uint parent_device_id_size = 0;
                cmret = CM_Get_Device_ID_Size(out parent_device_id_size, parent_devinst, 0);
                if (cmret != CR_SUCCESS) throw new Exception(string.Format("Failed to get size of the device ID of the parent of the device '{0}'. Error code: 0x{1:X8}", RegistryInstanceID, cmret));

                parent_device_id_size++;  // To include the null character  

                string parent_device_id = new string('\0', (int)parent_device_id_size);
                cmret = CM_Get_Device_ID(parent_devinst, parent_device_id, parent_device_id_size, 0);
                if (cmret != CR_SUCCESS) throw new Exception(string.Format("Failed to get device ID of the parent of the device '{0}'. Error code: 0x{1:X8}", RegistryInstanceID, cmret));

                return parent_device_id;
            }
            finally
            {
                SetupDiDestroyDeviceInfoList(hdi);
            }
        }

        private static string GetUSBInterfacePath(string SystemDeviceInstanceID)
        {
            if (string.IsNullOrEmpty(SystemDeviceInstanceID)) throw new ArgumentNullException("SystemDeviceInstanceID");

            IntPtr hdi = SetupDiGetClassDevs(GUID_DEVINTERFACE_USBPRINT, SystemDeviceInstanceID, IntPtr.Zero, DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);
            if (hdi.Equals(INVALID_HANDLE_VALUE)) throw new System.ComponentModel.Win32Exception();

            try
            {
                SP_DEVINFO_DATA device_data = new SP_DEVINFO_DATA();
                device_data.cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA));

                if (SetupDiEnumDeviceInfo(hdi, 0, ref device_data) == 0) throw new System.ComponentModel.Win32Exception();  // Only one device in the set  

                SP_DEVICE_INTERFACE_DATA interface_data = new SP_DEVICE_INTERFACE_DATA();
                interface_data.cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA));

                if (SetupDiEnumDeviceInterfaces(hdi, ref device_data, GUID_DEVINTERFACE_USBPRINT, 0, ref interface_data) == 0) throw new System.ComponentModel.Win32Exception();   // Only one interface in the set  


                // Get required buffer size  
                uint required_size = 0;
                SetupDiGetDeviceInterfaceDetail(hdi, ref interface_data, IntPtr.Zero, 0, out required_size, IntPtr.Zero);

                int last_error_code = Marshal.GetLastWin32Error();
                if (last_error_code != ERROR_INSUFFICIENT_BUFFER) throw new System.ComponentModel.Win32Exception(last_error_code);

                IntPtr interface_detail_data = Marshal.AllocCoTaskMem((int)required_size);

                try
                {

                    // FIXME, don't know how to calculate the size.  
                    // See http://stackoverflow.com/questions/10728644/properly-declare-sp-device-interface-detail-data-for-pinvoke  

                    switch (IntPtr.Size)
                    {
                        case 4:
                            Marshal.WriteInt32(interface_detail_data, 4 + Marshal.SystemDefaultCharSize);
                            break;
                        case 8:
                            Marshal.WriteInt32(interface_detail_data, 8);
                            break;

                        default:
                            throw new NotSupportedException("Architecture not supported.");
                    }

                    if (SetupDiGetDeviceInterfaceDetail(hdi, ref interface_data, interface_detail_data, required_size, out required_size, IntPtr.Zero) == 0) throw new System.ComponentModel.Win32Exception();

                    // TODO: When upgrading to .NET 4, replace that with IntPtr.Add  
                    return Marshal.PtrToStringAuto(new IntPtr(interface_detail_data.ToInt64() + Marshal.OffsetOf(typeof(SP_DEVICE_INTERFACE_DETAIL_DATA), "DevicePath").ToInt64()));

                }
                finally
                {
                    Marshal.FreeCoTaskMem(interface_detail_data);
                }
            }
            finally
            {
                SetupDiDestroyDeviceInfoList(hdi);
            }
        }


        public static string GetUSBPath(string PrinterName)
        {
            return GetUSBInterfacePath(GetPrinterParentDeviceId(GetPrinterRegistryInstanceID(PrinterName)));
        }
        public static string GetUSBPathByInstanceID(string InstanceID)
        {
            return GetUSBInterfacePath(InstanceID);
        }
        //通过设备名称查找打印，不好使
        public static Microsoft.Win32.SafeHandles.SafeFileHandle OpenUSBPrinter(string PrinterName)
        {
            IntPtr Handle = CreateFile(GetUSBPath(PrinterName), GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
            return new Microsoft.Win32.SafeHandles.SafeFileHandle(Handle, true);
        }
        public static Microsoft.Win32.SafeHandles.SafeFileHandle OpenUSBPrinterBydeviceId(string InstanceID)
        {
            IntPtr Handle = CreateFile(GetUSBPathByInstanceID(InstanceID), GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
            //USBDataRead(Handle);
            return new Microsoft.Win32.SafeHandles.SafeFileHandle(Handle, true);
        }
        public static IntPtr OpenUSBdeviceId(string InstanceID)
        {
            IntPtr Handle = CreateFile(GetUSBPathByInstanceID(InstanceID), GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
            return Handle;
        }
        //根据CreateFile拿到的设备handle访问文件，并返回数据
        public static bool USBDataRead(IntPtr handle)
        {
            while (true)
            {
                uint read = 0;
                //注意字节的长度，我这里写的是8位，其实可以通过API获取具体的长度，这样安全点，
                //具体方法我知道，但是没有写，过几天整理完代码，一起给出来
                Byte[] m_rd_data = new Byte[8];
                bool isread = ReadFile(handle, m_rd_data, (uint)8, ref read, IntPtr.Zero);
                //这里已经是拿到的数据了
                Byte[] m_rd_dataout = new Byte[read];
                Array.Copy(m_rd_data, m_rd_dataout, read);


            }
        }
      
        //using (var sh = UsbPrinterResolver.OpenUSBPrinter("Zebra Large"))  
        //{  
        //    using (var f = new System.IO.FileStream(sh, System.IO.FileAccess.ReadWrite))  
        //    {  
        //        // Read from and write to the stream f  
        //    }  
        //}  
    }
}