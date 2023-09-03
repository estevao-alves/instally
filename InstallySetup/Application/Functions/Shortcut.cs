using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace InstallySetup.Application.Functions
{
    class Shortcut
    {
        [DllImport("Shell32.dll")]
        private static extern void SHChangeNotify(int eventId, uint flags, IntPtr item1, IntPtr item2);

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SHCreateItemFromParsingName(string path, IntPtr pbc, ref Guid riid);

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        private interface IShellLinkW
        {
            void GetPath([Out] char[] pszFile, int cchMaxPath, ref IntPtr pfd, uint fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out] char[] pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out] char[] pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out] char[] pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out ushort pwHotkey);
            void SetHotkey(ushort wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out] char[] pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, uint dwReserved);
            void Resolve(IntPtr hwnd, uint fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        private class ShellLink { }

        [ComImport]
        [Guid("0000010B-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IPersistFile
        {
            void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string pszFile);
            void IsDirty();
            void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, uint dwMode);
            void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);
            void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
        }

        public static void Criar(string shortcutName, string shortcutTargetPath, string appPath, string description)
        {
            IShellLinkW shellLink = (IShellLinkW)new ShellLink();
            shellLink.SetPath(appPath);
            shellLink.SetDescription(description);
            shellLink.SetIconLocation(appPath, 0);

            IPersistFile persistFile = (IPersistFile)shellLink;
            string targetPath = shortcutTargetPath;
            string shortcutPath = Path.Combine(targetPath, $"{shortcutName}.lnk");

            persistFile.Save(shortcutPath, true);
            Marshal.ReleaseComObject(persistFile);
            SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
        }

        public static void Remover(string shortcutName, string shortcutPath)
        {
            shortcutPath = Path.Combine(shortcutPath, $"{shortcutName}.lnk");
            if(File.Exists(shortcutPath)) File.Delete(shortcutPath);
        }
    }
}
