﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Hardware;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Core.Utilities.Editor.Usb
{
    [InitializeOnLoad]
    public class USBDeviceListener
    {
        [SerializeField]
        public static USBDeviceInfo[] USBDevices;

        public delegate void OnUsbDevicesChanged(UsbDevice[] usbDevices);

        public static event OnUsbDevicesChanged UsbDevicesChanged;

        private static List<USBDeviceInfo> usbDevicesList = new List<USBDeviceInfo>(0);

        static USBDeviceListener()
        {
            UnityEditor.Hardware.Usb.DevicesChanged += NotifyUsbDevicesChanged;
        }

        private static void NotifyUsbDevicesChanged(UsbDevice[] devices)
        {
            UsbDevicesChanged?.Invoke(devices);

            usbDevicesList.Clear();

            foreach (UsbDevice device in devices)
            {
                usbDevicesList.Add(new USBDeviceInfo(device.vendorId, device.udid, device.productId, device.name, device.revision));
            }

            USBDevices = usbDevicesList.ToArray();
        }
    }
}