using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using Microsoft.Win32;
using System.Management;
using System.Reflection;

namespace FullFokusConfig
{
  public partial class Config : Form
  {
    const string ConfigFileName = "FullFokusConfig.txt";
    const bool DefaultExitTimed = false;
    const string DefaultExitTime = "_____";
    Dictionary<string, string> dictProgram = new Dictionary<string, string>();

    public Config()
    {
      InitializeComponent();

      if (!File.Exists(ConfigFileName))
      {
        // The config file does not exist. Use the default values
        UseTimedMode = DefaultExitTimed;
        mskTime.Text = DefaultExitTime;
        return;
      }

      // Get all the supported program and load into the ComboBox
      GetAllSupportedProgram();
      cmbInstalled.DataSource = dictProgram.Values.ToList();

      // Read the config file
      string line;
      string[] arrayVar;
      using (StreamReader reader = new StreamReader(ConfigFileName))
      {
        for (; !string.IsNullOrEmpty(line = reader.ReadLine()); )
        {
          arrayVar = line.Split('=');
          switch (arrayVar[0].Trim())
          {
            case "Timed":
              // Set the status of chkTimed, i.e. whether the timed mode should be used
              if (string.IsNullOrEmpty(arrayVar[1].Trim()))
                UseTimedMode =  DefaultExitTimed;
              else
                UseTimedMode = Convert.ToBoolean(arrayVar[1]);
              break;
            case "Time":
              // Set the text of mskTime, i.e. the time to exit in the timed mode
              if (string.IsNullOrEmpty(arrayVar[1].Trim()))
                mskTime.Text = DefaultExitTime;
              else                
                mskTime.Text = Convert.ToUInt16(arrayVar[1]).ToString();
              break;
            case "Program":
              // Set the selected value of cmbProgram, i.e. the installed program we use
              if (dictProgram.ContainsKey(arrayVar[1].Trim()))
              {
                cmbInstalled.SelectedItem = dictProgram[arrayVar[1].Trim()];
                ChooseFromInstalled = true;
              }
              break;
            case "ProgramDir":
              // Set the text of txtDir, i.e. the manual input for the directory of uninstalled program
              if (!string.IsNullOrEmpty(arrayVar[1].Trim()))
              {
                txtDir.Text = arrayVar[1].Trim().ToString();
                ChooseFromInstalled = false;
              }
              break;
            default:
              throw new NotImplementedException();
          }
        }
      }      
    }

    /// <summary>
    /// Get all the supported program 
    /// http://stackoverflow.com/questions/908850/get-installed-applications-in-a-system
    /// </summary>
    private void GetAllSupportedProgram()
    {
      string programName;

      using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
      {
        foreach (string subkey_name in key.GetSubKeyNames())
        {
          using (RegistryKey subkey = key.OpenSubKey(subkey_name))
          {
            // Do not add a blank value
            if (subkey.GetValue("DisplayName") == null)
              continue;

            programName = subkey.GetValue("DisplayName").ToString();
            if (!IsSupportedProgram(programName))
              // Only add if this is a support program
              continue;

            dictProgram.Add(subkey_name, programName);
          }
        }
      }
    }

    /// <summary>
    /// Judge if the program is supported
    /// </summary>
    /// <param name="programName"></param>
    /// <returns></returns>
    private bool IsSupportedProgram(string programName)
    {
      if (string.IsNullOrEmpty(programName))
        return false;

      string lowerProgramName = programName.ToLower();
      if (!lowerProgramName.Contains("microsoft office word") && !lowerProgramName.Contains("adobe photoshop"))
        return false;

      return true;
    }

    /// <summary>
    /// Save the config file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cmdOK_Click(object sender, EventArgs e)
    {
      using (StreamWriter writer = new StreamWriter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" +  ConfigFileName, false))
      {
        writer.WriteLine(string.Format("Timed = {0}", UseTimedMode));
        if (UseTimedMode)
          writer.WriteLine(string.Format("Time = {0}", mskTime.Text));
        else
          writer.WriteLine("Time = ");
        if (ChooseFromInstalled)
        {
          // Find the program by the selected item as the key
          var linqResult = from entry in dictProgram
                           where entry.Value == cmbInstalled.SelectedItem.ToString()
                           select entry.Key;
          if (linqResult.Any())
            writer.WriteLine(string.Format( "Program = {0}", linqResult.First()));
          else
            writer.WriteLine("Program = ");
          writer.WriteLine(string.Format( "ProgramDir = "));
        }
        else
        {
          writer.WriteLine(string.Format( "Program = "));
          writer.WriteLine(string.Format( "ProgramDir = {0}", txtDir.Text));
        }
        writer.Close();
      }
      this.Close();
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    bool chooseFromInstalled;
    bool ChooseFromInstalled
    {
      get { return chooseFromInstalled; }
      set
      {
        // Flag to avoid iterative invocation
        invokedByProperty = true;

        chkInstalled.Checked = value;
        cmbInstalled.Enabled = value;

        chkManual.Checked = !value;
        txtDir.Enabled = !value;
        btnDir.Enabled = !value;

        chooseFromInstalled = value;

        invokedByProperty = false;
      }
    }

    bool invokedByProperty;
    private void chkInstalled_CheckedChanged(object sender, EventArgs e)
    {
      if (invokedByProperty)
        return;
      ChooseFromInstalled = true;
    }

    private void chkManual_CheckedChanged(object sender, EventArgs e)
    {
      if (invokedByProperty)
        return;
      ChooseFromInstalled = false;
    }

    private void btnDir_Click(object sender, EventArgs e)
    {
      ofdDir.ShowDialog();
    }

    private void ofdDir_FileOk(object sender, CancelEventArgs e)
    {
      txtDir.Text = ofdDir.FileName;
    }

    bool CheckInput()
    {
      if (ChooseFromInstalled && cmbInstalled.SelectedIndex == -1 ||
        !ChooseFromInstalled && string.IsNullOrEmpty(txtDir.Text))
        return false;

      return true;
    }

    bool useTimedMode;
    bool UseTimedMode
    {
      get { return useTimedMode; }
      set
      {
        // Flag to avoid iterative invocation
        invokedByProperty = true;

        chkTimed.Checked = value;
        mskTime.Enabled = value;
        if (!value)
          mskTime.Text = DefaultExitTime;

        useTimedMode = value;

        invokedByProperty = false;
      }
    }
    private void chkTimed_CheckedChanged(object sender, EventArgs e)
    {
      if (invokedByProperty)
        return;
      UseTimedMode = !UseTimedMode;
    }
  }
}
