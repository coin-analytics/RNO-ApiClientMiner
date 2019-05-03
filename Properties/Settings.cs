// Decompiled with JetBrains decompiler
// Type: coinminner.Properties.Settings
// Assembly: rnocoinminer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C225A215-3AEB-4217-A7B2-DA79F9B782FD
// Assembly location: C:\Program Files (x86)\RNO\RnoMiner(Beta)\rnocoinminer.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace coinminner.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }
  }
}
