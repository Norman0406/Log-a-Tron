#region Assembly Interop.OmniRig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// D:\Projekte\Programmieren\DotNet\WpfApp1\HamRadioLib\obj\Debug\net6.0\Interop.OmniRig.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace OmniRig
{
    internal sealed class IOmniRigXEvents_EventProvider : IOmniRigXEvents_Event, IDisposable
    {
        private IConnectionPointContainer m_ConnectionPointContainer;

        private ArrayList m_aEventSinkHelpers;

        private IConnectionPoint m_ConnectionPoint;

        private void Init()
        {
            IConnectionPoint ppCP = null;
            Guid riid = new Guid(new byte[16]
            {
                95, 23, 25, 34, 97, 229, 231, 71, 173, 23,
                115, 196, 216, 137, 26, 161
            });
            m_ConnectionPointContainer.FindConnectionPoint(ref riid, out ppCP);
            m_ConnectionPoint = ppCP;
            m_aEventSinkHelpers = new ArrayList();
        }

        public override void add_VisibleChange(IOmniRigXEvents_VisibleChangeEventHandler P_0)
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_ConnectionPoint == null)
                {
                    Init();
                }

                IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = new IOmniRigXEvents_SinkHelper();
                int pdwCookie = 0;
                m_ConnectionPoint.Advise(omniRigXEvents_SinkHelper, out pdwCookie);
                omniRigXEvents_SinkHelper.m_dwCookie = pdwCookie;
                omniRigXEvents_SinkHelper.m_VisibleChangeDelegate = P_0;
                m_aEventSinkHelpers.Add(omniRigXEvents_SinkHelper);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public override void remove_VisibleChange(IOmniRigXEvents_VisibleChangeEventHandler P_0)
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_aEventSinkHelpers == null)
                {
                    return;
                }

                int count = m_aEventSinkHelpers.Count;
                int num = 0;
                if (0 >= count)
                {
                    return;
                }

                do
                {
                    IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = (IOmniRigXEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (omniRigXEvents_SinkHelper.m_VisibleChangeDelegate != null && ((omniRigXEvents_SinkHelper.m_VisibleChangeDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(omniRigXEvents_SinkHelper.m_dwCookie);
                        if (count <= 1)
                        {
                            Marshal.ReleaseComObject(m_ConnectionPoint);
                            m_ConnectionPoint = null;
                            m_aEventSinkHelpers = null;
                        }

                        break;
                    }

                    num++;
                }
                while (num < count);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public override void add_RigTypeChange(IOmniRigXEvents_RigTypeChangeEventHandler P_0)
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_ConnectionPoint == null)
                {
                    Init();
                }

                IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = new IOmniRigXEvents_SinkHelper();
                int pdwCookie = 0;
                m_ConnectionPoint.Advise(omniRigXEvents_SinkHelper, out pdwCookie);
                omniRigXEvents_SinkHelper.m_dwCookie = pdwCookie;
                omniRigXEvents_SinkHelper.m_RigTypeChangeDelegate = P_0;
                m_aEventSinkHelpers.Add(omniRigXEvents_SinkHelper);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public override void remove_RigTypeChange(IOmniRigXEvents_RigTypeChangeEventHandler P_0)
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_aEventSinkHelpers == null)
                {
                    return;
                }

                int count = m_aEventSinkHelpers.Count;
                int num = 0;
                if (0 >= count)
                {
                    return;
                }

                do
                {
                    IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = (IOmniRigXEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (omniRigXEvents_SinkHelper.m_RigTypeChangeDelegate != null && ((omniRigXEvents_SinkHelper.m_RigTypeChangeDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(omniRigXEvents_SinkHelper.m_dwCookie);
                        if (count <= 1)
                        {
                            Marshal.ReleaseComObject(m_ConnectionPoint);
                            m_ConnectionPoint = null;
                            m_aEventSinkHelpers = null;
                        }

                        break;
                    }

                    num++;
                }
                while (num < count);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public override void add_StatusChange(IOmniRigXEvents_StatusChangeEventHandler P_0)
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_ConnectionPoint == null)
                {
                    Init();
                }

                IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = new IOmniRigXEvents_SinkHelper();
                int pdwCookie = 0;
                m_ConnectionPoint.Advise(omniRigXEvents_SinkHelper, out pdwCookie);
                omniRigXEvents_SinkHelper.m_dwCookie = pdwCookie;
                omniRigXEvents_SinkHelper.m_StatusChangeDelegate = P_0;
                m_aEventSinkHelpers.Add(omniRigXEvents_SinkHelper);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public override void remove_StatusChange(IOmniRigXEvents_StatusChangeEventHandler P_0)
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_aEventSinkHelpers == null)
                {
                    return;
                }

                int count = m_aEventSinkHelpers.Count;
                int num = 0;
                if (0 >= count)
                {
                    return;
                }

                do
                {
                    IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = (IOmniRigXEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (omniRigXEvents_SinkHelper.m_StatusChangeDelegate != null && ((omniRigXEvents_SinkHelper.m_StatusChangeDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(omniRigXEvents_SinkHelper.m_dwCookie);
                        if (count <= 1)
                        {
                            Marshal.ReleaseComObject(m_ConnectionPoint);
                            m_ConnectionPoint = null;
                            m_aEventSinkHelpers = null;
                        }

                        break;
                    }

                    num++;
                }
                while (num < count);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public override void add_ParamsChange(IOmniRigXEvents_ParamsChangeEventHandler P_0)
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_ConnectionPoint == null)
                {
                    Init();
                }

                IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = new IOmniRigXEvents_SinkHelper();
                int pdwCookie = 0;
                m_ConnectionPoint.Advise(omniRigXEvents_SinkHelper, out pdwCookie);
                omniRigXEvents_SinkHelper.m_dwCookie = pdwCookie;
                omniRigXEvents_SinkHelper.m_ParamsChangeDelegate = P_0;
                m_aEventSinkHelpers.Add(omniRigXEvents_SinkHelper);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public override void remove_ParamsChange(IOmniRigXEvents_ParamsChangeEventHandler P_0)
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_aEventSinkHelpers == null)
                {
                    return;
                }

                int count = m_aEventSinkHelpers.Count;
                int num = 0;
                if (0 >= count)
                {
                    return;
                }

                do
                {
                    IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = (IOmniRigXEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (omniRigXEvents_SinkHelper.m_ParamsChangeDelegate != null && ((omniRigXEvents_SinkHelper.m_ParamsChangeDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(omniRigXEvents_SinkHelper.m_dwCookie);
                        if (count <= 1)
                        {
                            Marshal.ReleaseComObject(m_ConnectionPoint);
                            m_ConnectionPoint = null;
                            m_aEventSinkHelpers = null;
                        }

                        break;
                    }

                    num++;
                }
                while (num < count);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public override void add_CustomReply(IOmniRigXEvents_CustomReplyEventHandler P_0)
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_ConnectionPoint == null)
                {
                    Init();
                }

                IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = new IOmniRigXEvents_SinkHelper();
                int pdwCookie = 0;
                m_ConnectionPoint.Advise(omniRigXEvents_SinkHelper, out pdwCookie);
                omniRigXEvents_SinkHelper.m_dwCookie = pdwCookie;
                omniRigXEvents_SinkHelper.m_CustomReplyDelegate = P_0;
                m_aEventSinkHelpers.Add(omniRigXEvents_SinkHelper);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public override void remove_CustomReply(IOmniRigXEvents_CustomReplyEventHandler P_0)
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_aEventSinkHelpers == null)
                {
                    return;
                }

                int count = m_aEventSinkHelpers.Count;
                int num = 0;
                if (0 >= count)
                {
                    return;
                }

                do
                {
                    IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = (IOmniRigXEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (omniRigXEvents_SinkHelper.m_CustomReplyDelegate != null && ((omniRigXEvents_SinkHelper.m_CustomReplyDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(omniRigXEvents_SinkHelper.m_dwCookie);
                        if (count <= 1)
                        {
                            Marshal.ReleaseComObject(m_ConnectionPoint);
                            m_ConnectionPoint = null;
                            m_aEventSinkHelpers = null;
                        }

                        break;
                    }

                    num++;
                }
                while (num < count);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public IOmniRigXEvents_EventProvider(object P_0)
        {
            //Error decoding local variables: Signature type sequence must have at least one element.
            m_ConnectionPointContainer = (IConnectionPointContainer)P_0;
        }

        public override void Finalize()
        {
            bool lockTaken = default(bool);
            try
            {
                Monitor.Enter(this, ref lockTaken);
                if (m_ConnectionPoint == null)
                {
                    return;
                }

                int count = m_aEventSinkHelpers.Count;
                int num = 0;
                if (0 < count)
                {
                    do
                    {
                        IOmniRigXEvents_SinkHelper omniRigXEvents_SinkHelper = (IOmniRigXEvents_SinkHelper)m_aEventSinkHelpers[num];
                        m_ConnectionPoint.Unadvise(omniRigXEvents_SinkHelper.m_dwCookie);
                        num++;
                    }
                    while (num < count);
                }

                Marshal.ReleaseComObject(m_ConnectionPoint);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this);
                }
            }
        }

        public override void Dispose()
        {
            //Error decoding local variables: Signature type sequence must have at least one element.
            Finalize();
            GC.SuppressFinalize(this);
        }
    }
}
#if false // Decompilation log
'161' items in cache
------------------
Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\mscorlib.dll'
------------------
Resolve: 'Microsoft.Win32.Registry, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'Microsoft.Win32.Registry, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\Microsoft.Win32.Registry.dll'
------------------
Resolve: 'System.Runtime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Runtime.dll'
------------------
Resolve: 'System.Security.Principal.Windows, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Principal.Windows, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Security.Principal.Windows.dll'
------------------
Resolve: 'System.Security.Permissions, Version=0.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Could not find by name: 'System.Security.Permissions, Version=0.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
------------------
Resolve: 'System.Collections, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Collections.dll'
------------------
Resolve: 'System.Collections.NonGeneric, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.NonGeneric, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Collections.NonGeneric.dll'
------------------
Resolve: 'System.Collections.Concurrent, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.Concurrent, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Collections.Concurrent.dll'
------------------
Resolve: 'System.ObjectModel, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ObjectModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.ObjectModel.dll'
------------------
Resolve: 'System.Console, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Console, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Console.dll'
------------------
Resolve: 'System.Runtime.InteropServices, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.InteropServices, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Runtime.InteropServices.dll'
------------------
Resolve: 'System.Diagnostics.Contracts, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Contracts, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Diagnostics.Contracts.dll'
------------------
Resolve: 'System.Diagnostics.StackTrace, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.StackTrace, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Diagnostics.StackTrace.dll'
------------------
Resolve: 'System.Diagnostics.Tracing, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Tracing, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Diagnostics.Tracing.dll'
------------------
Resolve: 'System.IO.FileSystem.DriveInfo, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.FileSystem.DriveInfo, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.IO.FileSystem.DriveInfo.dll'
------------------
Resolve: 'System.IO.IsolatedStorage, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.IsolatedStorage, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.IO.IsolatedStorage.dll'
------------------
Resolve: 'System.ComponentModel, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.ComponentModel.dll'
------------------
Resolve: 'System.Threading.Thread, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Thread, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Threading.Thread.dll'
------------------
Resolve: 'System.Reflection.Emit, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Reflection.Emit.dll'
------------------
Resolve: 'System.Reflection.Emit.ILGeneration, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit.ILGeneration, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Reflection.Emit.ILGeneration.dll'
------------------
Resolve: 'System.Reflection.Emit.Lightweight, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit.Lightweight, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Reflection.Emit.Lightweight.dll'
------------------
Resolve: 'System.Reflection.Primitives, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Primitives, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Reflection.Primitives.dll'
------------------
Resolve: 'System.Resources.Writer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Resources.Writer, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Resources.Writer.dll'
------------------
Resolve: 'System.Runtime.CompilerServices.VisualC, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.CompilerServices.VisualC, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Runtime.CompilerServices.VisualC.dll'
------------------
Resolve: 'System.Runtime.InteropServices.RuntimeInformation, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.InteropServices.RuntimeInformation, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Runtime.InteropServices.RuntimeInformation.dll'
------------------
Resolve: 'System.Runtime.Serialization.Formatters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Formatters, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Runtime.Serialization.Formatters.dll'
------------------
Resolve: 'System.Security.AccessControl, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.AccessControl, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Security.AccessControl.dll'
------------------
Resolve: 'System.IO.FileSystem.AccessControl, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.FileSystem.AccessControl, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.IO.FileSystem.AccessControl.dll'
------------------
Resolve: 'System.Threading.AccessControl, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Could not find by name: 'System.Threading.AccessControl, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
------------------
Resolve: 'System.Security.Claims, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Claims, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Security.Claims.dll'
------------------
Resolve: 'System.Security.Cryptography.Algorithms, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography.Algorithms, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Security.Cryptography.Algorithms.dll'
------------------
Resolve: 'System.Security.Cryptography.Primitives, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography.Primitives, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Security.Cryptography.Primitives.dll'
------------------
Resolve: 'System.Security.Cryptography.Csp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography.Csp, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Security.Cryptography.Csp.dll'
------------------
Resolve: 'System.Security.Cryptography.Encoding, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography.Encoding, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Security.Cryptography.Encoding.dll'
------------------
Resolve: 'System.Security.Cryptography.X509Certificates, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography.X509Certificates, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Security.Cryptography.X509Certificates.dll'
------------------
Resolve: 'System.Text.Encoding.Extensions, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Text.Encoding.Extensions, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Text.Encoding.Extensions.dll'
------------------
Resolve: 'System.Threading, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Threading.dll'
------------------
Resolve: 'System.Threading.Overlapped, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Overlapped, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Threading.Overlapped.dll'
------------------
Resolve: 'System.Threading.ThreadPool, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.ThreadPool, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Threading.ThreadPool.dll'
------------------
Resolve: 'System.Threading.Tasks.Parallel, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Tasks.Parallel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '0.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Threading.Tasks.Parallel.dll'
------------------
Resolve: 'System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.14\ref\net6.0\System.Runtime.dll'
#endif
