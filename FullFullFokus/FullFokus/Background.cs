using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace FullFokus
{
  enum SHOWWINDOW
  {
    SW_HIDE = 0,
    SW_SHOWNORMAL = 1,
    SW_NORMAL = 1,
    SW_SHOWMINIMIZED = 2,
    SW_SHOWMAXIMIZED = 3,
    SW_MAXIMIZE = 3,
    SW_SHOWNOACTIVATE = 4,
    SW_SHOW = 5,
    SW_MINIMIZE = 6,
    SW_SHOWMINNOACTIVE = 7,
    SW_SHOWNA = 8,
    SW_RESTORE = 9,
    SW_SHOWDEFAULT = 10,
    SW_FORCEMINIMIZE = 11,
    SW_MAX = 11,
  }

  public partial class Background : Form
  {
    const string ConfigFileName = "FullFokusConfig.txt";
    const bool DefaultExitTimed = false;
    const ushort DefaultExitTime = 60;
    const string DefaultProgram = "winword.exe";
    const string DefaultProgramDir = null;
    Dictionary<string, string> dictProgram = new Dictionary<string, string>();

    FileStream fs;
    bool exitTimed;
    ushort exitTime;
    string program;
    string programDir;

    // Win32 API references
    //System level functions to handle the taskbar
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

    //System level functions to be used for hook and unhook keyboard input  
    delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern bool UnhookWindowsHookEx(IntPtr hook);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern IntPtr GetModuleHandle(string name);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern short GetAsyncKeyState(Keys key);

    /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Starts Here */
    // Structure contain information about low-level keyboard input event 
    [StructLayout(LayoutKind.Sequential)]
    struct KBDLLHOOKSTRUCT
    {
      public Keys key;
      public int scanCode;
      public int flags;
      public int time;
      public IntPtr extra;
    }

    // Declaring Global objects - Windows hook and low level keyboard hook
    IntPtr ptrHook;
    LowLevelKeyboardProc objKeyboardProcess;

    /// <summary>
    /// Disable the Windows keys and only allow pre-defined key strokes to exit or start new program
    /// </summary>
    /// <param name="nCode"></param>
    /// <param name="wp"></param>
    /// <param name="lp"></param>
    /// <returns></returns>
    IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
    {
      if (nCode >= 0)
      {
        KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

        // Disabling the following Windows keys:
        // Left & right Windows
        // Alt + Tab
        // Ctrl + Escape
        // Alt + Escape
        // Alt + F4
        if (objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin || objKeyInfo.key == Keys.Tab && HasAltModifier(objKeyInfo.flags) ||
          objKeyInfo.key == Keys.Escape && (ModifierKeys & Keys.Control) == Keys.Control || objKeyInfo.key == Keys.Escape && HasAltModifier(objKeyInfo.flags) ||
          objKeyInfo.key == Keys.F4 && HasAltModifier(objKeyInfo.flags))
          // If 0 is returned then All the above keys will be enabled
          return (IntPtr)1;
        else if (objKeyInfo.key == Keys.Q && (ModifierKeys & Keys.Control) == Keys.Control && !exitTimed)
          // In the un-timed mode you can press Ctrl+Q to exit
          CleanUpAndExit();
        else if (objKeyInfo.key == Keys.W && (ModifierKeys & Keys.Control) == Keys.Control)
          // http://stackoverflow.com/questions/3173775/how-to-run-external-program-via-a-c-sharp-program
          StartProgram();
      }
      return CallNextHookEx(ptrHook, nCode, wp, lp);
    }

    /// <summary>
    /// Tell if the Alt key has been pressed down
    /// </summary>
    /// <param name="flags"></param>
    /// <returns></returns>
    bool HasAltModifier(int flags)
    {
      return (flags & 0x20) == 0x20;
    }

    /// <summary>
    /// Start the pre-defined program in the config file
    /// </summary>
    void StartProgram()
    {
      try
      {
        if (!string.IsNullOrEmpty(programDir))
        {
          // Start the program as contained in programDir
          Process.Start(programDir);
          return;
        }
    
        string lowerProgramName = program.ToLower();
        if (lowerProgramName.Contains("adobe photoshop"))
          // Start Adobe Photoshop
          Process.Start("photoshop.exe");
        else if (lowerProgramName.Contains("word"))
          // Start Microsoft Word
          Process.Start("winword.exe");
        else
          // Start the default program
          Process.Start(DefaultProgram);
      }
      catch
      {
        MessageBox.Show(string.Format("无法启动程序：{0}", string.IsNullOrEmpty(programDir) ? programDir : program));
      }
    }

    /// <summary>
    /// Clean up and restore the status and then exit
    /// </summary>
    void CleanUpAndExit()
    {
      // Close the file taskmgr.exe to re-enable the task manager
      fs.Close();
      // Restore the taskbar
      ShowWindow(FindWindow("Shell_TrayWnd", null), (int)SHOWWINDOW.SW_SHOW);
      ShowWindow(GetDlgItem(FindWindow("Shell_TrayWnd", null), 0x130), (int)SHOWWINDOW.SW_SHOW);
      // Close this form
      this.Close();
    }
    /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Ends Here */

    // http://www.pchenry.com/Home/tabid/36/EntryId/168/How-do-you-disable-a-WinForms-close-button.aspx
    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams param = base.CreateParams;
        param.ClassStyle = param.ClassStyle | 0x200;
        return param;
      }
    }

    /// <summary>
    /// Disable the close button
    /// </summary>
    /// <param name="message"></param>
    protected override void WndProc(ref Message message)
    {
      const int WM_NCHITTEST = 0x0084;

      if (message.Msg == WM_NCHITTEST)
        return;

      base.WndProc(ref message);
    }

    public Background()
    {
      // Initialise low level keyboard hook
      ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
      objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
      ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);

      // Open the file taskmgr.exe to disable the task manager
      // http://stackoverflow.com/questions/1660106/block-controlaltdelete      
      fs = new FileStream(System.IO.Path.Combine(Environment.SystemDirectory, "taskmgr.exe"), FileMode.Open, FileAccess.ReadWrite, FileShare.None);
      // Hide the taskbar
      // http://www.codeproject.com/Articles/7392/Lock-Windows-Desktop
      ShowWindow(FindWindow("Shell_TrayWnd", null), (int)SHOWWINDOW.SW_HIDE);
      ShowWindow(GetDlgItem(FindWindow("Shell_TrayWnd", null), 0x130), (int)SHOWWINDOW.SW_HIDE);

      // Disable the maximise and minise buttons. Also set the form to full screen
      // http://http://stackoverflow.com/questions/3025923/disabling-minimize-maximize-on-winform
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.WindowState = FormWindowState.Maximized;

      // Remove the title bar
      // http://stackoverflow.com/questions/3594086/how-to-create-a-form-with-a-border-but-no-title-bar-like-volume-control-on-wi
      this.Text = string.Empty;
      this.ControlBox = false;
      this.FormBorderStyle = FormBorderStyle.SizableToolWindow;

      if (!File.Exists(ConfigFileName))
      {
        // The config file does not exist. Use the default values
        exitTimed = DefaultExitTimed;
        exitTime = DefaultExitTime;
        program = DefaultProgram;
        programDir = DefaultProgramDir;
        return;
      }

      // Read the config file
      string line;
      string[] arrayVar;
      using (StreamReader reader = new StreamReader(ConfigFileName, true))
      {
        for (; !string.IsNullOrEmpty(line = reader.ReadLine()); )
        {
          arrayVar = line.Split('=');
          switch (arrayVar[0].Trim())
          {
            case "Timed":
              if (string.IsNullOrEmpty(arrayVar[1].Trim()))
                exitTimed = DefaultExitTimed;
              else
                exitTimed = Convert.ToBoolean(arrayVar[1]);
              break;
            case "Time":
              if (string.IsNullOrEmpty(arrayVar[1].Trim()))
                exitTime = DefaultExitTime;
              else
                exitTime = Convert.ToUInt16(arrayVar[1]);
              break;
            case "Program":
              if (string.IsNullOrEmpty(arrayVar[1].Trim()))
                program = DefaultProgram;
              else
                program = arrayVar[1].Trim();
              break;
            case "ProgramDir":
              if (string.IsNullOrEmpty(arrayVar[1].Trim()))
                programDir = DefaultProgramDir;
              else
                programDir = arrayVar[1].Trim();
              break;
            default:
              throw new NotImplementedException();
          }
        }
      }

      InitializeComponent();

      // Set up for the timed mode - No exit before the time expires
      if (exitTimed)
      {
        timerExit.Enabled = true;
        timerExit.Interval = exitTime * 60000;
        timerExit.Start();
      }
    }

    void timerExit_Tick(object sender, EventArgs e)
    {
      // In the timed mode the program exits after certain time
      CleanUpAndExit();
    }
  }
}

