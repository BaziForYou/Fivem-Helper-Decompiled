using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace FivemHelper
{
  public class Form1 : Form
  {
    private string url = "https://raw.githubusercontent.com/AhmadZare/FivemHelper/main/hosts.json";
    private string hostsPath = "C:\\Windows\\System32\\drivers\\etc\\hosts";
    private string etcPath = "C:\\Windows\\System32\\drivers\\etc";
    private string version = "1.3";
    private Form1.Root data;
    private string[] OldDnsSettings;
    private TimeSpan timeSpan = TimeSpan.FromMinutes(5.0);
    private IContainer components;
    private Button button1;
    private System.Windows.Forms.Timer timer1;
    private Button button2;
    private Label label1;
    private Label label2;

    public Form1() => this.InitializeComponent();

    private async void Form1_Load(object sender, EventArgs e)
    {
      Form1 ctl = this;
      Rectangle workingArea = Screen.GetWorkingArea((Control) ctl);
      ctl.Location = new Point(workingArea.Right - ctl.Size.Width, workingArea.Bottom - ctl.Size.Height);
      ctl.TopMost = true;
      if (ctl.FillData())
      {
        if (System.IO.File.Exists(ctl.hostsPath))
        {
          string contents = System.IO.File.ReadAllText(ctl.hostsPath);
          System.IO.File.Delete(ctl.hostsPath);
          System.IO.File.WriteAllText(ctl.hostsPath, contents);
        }
        else if (Directory.Exists(ctl.etcPath))
        {
          System.IO.File.Create(ctl.hostsPath).Close();
        }
        else
        {
          Directory.CreateDirectory(ctl.etcPath);
          System.IO.File.Create(ctl.hostsPath).Close();
        }
        ctl.RemoveExitHost();
        ctl.label2.Text = "نسخه " + ctl.data.version;
        if (ctl.version != ctl.data.version)
        {
          int num = (int) MessageBox.Show("برنامه نیاز به بروز رسانی دارد لطفا مجدد دانلود بفرمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          Process.Start(ctl.data.site);
          ctl.Close();
        }
      }
      else
      {
        int num = (int) MessageBox.Show("خطایی رخ داده برنامه غیرقابل استفاده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        ctl.Close();
      }
      await Task.Delay(1000);
      ctl.button1.PerformClick();
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      Exception exceptionObject = (Exception) e.ExceptionObject;
      this.SendDiscordLog("UnhandledException: " + exceptionObject.Message + "\n\n\n" + exceptionObject.StackTrace);
    }

    private void SendDiscordLog(string message)
    {
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(this.data.hook);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
          string str = "{\"content\":\"" + message + "\"}";
          streamWriter.Write(str);
        }
        using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
          streamReader.ReadToEnd();
      }
      catch (Exception ex)
      {
      }
    }

    private string[] GetDnsSettings()
    {
      foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
      {
        if (networkInterface.OperationalStatus == OperationalStatus.Up)
        {
          IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
          List<string> stringList = new List<string>();
          foreach (IPAddress dnsAddress in ipProperties.DnsAddresses)
          {
            if (dnsAddress.AddressFamily == AddressFamily.InterNetwork)
              stringList.Add(dnsAddress.ToString());
          }
          if (stringList.Count >= 2)
            return new string[2]
            {
              stringList[0],
              stringList[1]
            };
        }
      }
      return new string[2]{ "0.0.0.0", "0.0.0.0" };
    }

    private void ChangeDnsSettings(string primaryDns, string secondaryDns)
    {
      foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
      {
        if (networkInterface.OperationalStatus == OperationalStatus.Up)
          new Process()
          {
            StartInfo = new ProcessStartInfo()
            {
              WindowStyle = ProcessWindowStyle.Hidden,
              FileName = "cmd.exe",
              Arguments = ("/C netsh interface ip set dns name=\"" + networkInterface.Name + "\" static " + primaryDns + " primary && netsh interface ip add dns name=\"" + networkInterface.Name + "\" addr=" + secondaryDns + " index=2"),
              Verb = "runas"
            }
          }.Start();
      }
    }

    private void FlushDns()
    {
      new Process()
      {
        StartInfo = new ProcessStartInfo()
        {
          WindowStyle = ProcessWindowStyle.Hidden,
          FileName = "cmd.exe",
          Arguments = "/C ipconfig /flushdns",
          Verb = "runas"
        }
      }.Start();
    }

    public bool RemoveExitHost()
    {
      try
      {
        List<string> linesToRemove = new List<string>();
        linesToRemove.Add("\n");
        foreach (KeyValuePair<object, object> host in this.data.hosts)
        {
          object obj1 = (object) host;
          
          if (Form1.\u003C\u003Eo__15.\u003C\u003Ep__1 == null)
          {
            
            Form1.\u003C\u003Eo__15.\u003C\u003Ep__1 = CallSite<Action<CallSite, List<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          
          Action<CallSite, List<string>, object> target = Form1.\u003C\u003Eo__15.\u003C\u003Ep__1.Target;
          
          CallSite<Action<CallSite, List<string>, object>> p1 = Form1.\u003C\u003Eo__15.\u003C\u003Ep__1;
          List<string> stringList = linesToRemove;
          
          if (Form1.\u003C\u003Eo__15.\u003C\u003Ep__0 == null)
          {
            
            Form1.\u003C\u003Eo__15.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Key", typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          
          
          object obj2 = Form1.\u003C\u003Eo__15.\u003C\u003Ep__0.Target((CallSite) Form1.\u003C\u003Eo__15.\u003C\u003Ep__0, obj1);
          target((CallSite) p1, stringList, obj2);
        }
        List<string> list = ((IEnumerable<string>) System.IO.File.ReadAllLines(this.hostsPath)).ToList<string>();
        list.RemoveAll((Predicate<string>) (line => linesToRemove.Any<string>((Func<string, bool>) (lineToRemove => line.Contains(lineToRemove)))));
        System.IO.File.WriteAllLines(this.hostsPath, (IEnumerable<string>) list);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool PullData()
    {
      try
      {
        using (StreamWriter streamWriter1 = System.IO.File.AppendText(this.hostsPath))
        {
          foreach (KeyValuePair<object, object> host in this.data.hosts)
          {
            object obj1 = (object) host;
            
            if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__0 == null)
            {
              
              Form1.\u003C\u003Eo__16.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            
            
            if (!(Form1.\u003C\u003Eo__16.\u003C\u003Ep__0.Target((CallSite) Form1.\u003C\u003Eo__16.\u003C\u003Ep__0, obj1) is string))
            {
              
              if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__3 == null)
              {
                
                Form1.\u003C\u003Eo__16.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string[]>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string[]), typeof (Form1)));
              }
              
              Func<CallSite, object, string[]> target1 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__3.Target;
              
              CallSite<Func<CallSite, object, string[]>> p3 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__3;
              
              if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__2 == null)
              {
                
                Form1.\u003C\u003Eo__16.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToObject", (IEnumerable<Type>) new Type[1]
                {
                  typeof (string[])
                }, typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              
              Func<CallSite, object, object> target2 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__2.Target;
              
              CallSite<Func<CallSite, object, object>> p2 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__2;
              
              if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__1 == null)
              {
                
                Form1.\u003C\u003Eo__16.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              
              
              object obj2 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__1.Target((CallSite) Form1.\u003C\u003Eo__16.\u003C\u003Ep__1, obj1);
              object obj3 = target2((CallSite) p2, obj2);
              int num1 = new Random().Next(target1((CallSite) p3, obj3).Length);
              
              if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__6 == null)
              {
                
                Form1.\u003C\u003Eo__16.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (Form1)));
              }
              
              Func<CallSite, object, string> target3 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__6.Target;
              
              CallSite<Func<CallSite, object, string>> p6 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__6;
              
              if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__5 == null)
              {
                
                Form1.\u003C\u003Eo__16.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                }));
              }
              
              Func<CallSite, object, int, object> target4 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__5.Target;
              
              CallSite<Func<CallSite, object, int, object>> p5 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__5;
              
              if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__4 == null)
              {
                
                Form1.\u003C\u003Eo__16.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Value", typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              
              
              object obj4 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__4.Target((CallSite) Form1.\u003C\u003Eo__16.\u003C\u003Ep__4, obj1);
              int num2 = num1;
              object obj5 = target4((CallSite) p5, obj4, num2);
              string str1 = target3((CallSite) p6, obj5);
              StreamWriter streamWriter2 = streamWriter1;
              string str2 = str1;
              
              if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__7 == null)
              {
                
                Form1.\u003C\u003Eo__16.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Key", typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              
              
              object obj6 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__7.Target((CallSite) Form1.\u003C\u003Eo__16.\u003C\u003Ep__7, obj1);
              string str3 = string.Format("{0} {1}", (object) str2, obj6);
              streamWriter2.WriteLine(str3);
            }
            else
            {
              
              if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__10 == null)
              {
                
                Form1.\u003C\u003Eo__16.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              
              Func<CallSite, object, bool> target5 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__10.Target;
              
              CallSite<Func<CallSite, object, bool>> p10 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__10;
              
              if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__9 == null)
              {
                
                Form1.\u003C\u003Eo__16.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
                }));
              }
              
              Func<CallSite, object, string, object> target6 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__9.Target;
              
              CallSite<Func<CallSite, object, string, object>> p9 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__9;
              
              if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__8 == null)
              {
                
                Form1.\u003C\u003Eo__16.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              
              
              object obj7 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__8.Target((CallSite) Form1.\u003C\u003Eo__16.\u003C\u003Ep__8, obj1);
              object obj8 = target6((CallSite) p9, obj7, "0");
              if (target5((CallSite) p10, obj8))
              {
                StreamWriter streamWriter3 = streamWriter1;
                
                if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__11 == null)
                {
                  
                  Form1.\u003C\u003Eo__16.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                  }));
                }
                
                
                object obj9 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__11.Target((CallSite) Form1.\u003C\u003Eo__16.\u003C\u003Ep__11, obj1);
                
                if (Form1.\u003C\u003Eo__16.\u003C\u003Ep__12 == null)
                {
                  
                  Form1.\u003C\u003Eo__16.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Key", typeof (Form1), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                  }));
                }
                
                
                object obj10 = Form1.\u003C\u003Eo__16.\u003C\u003Ep__12.Target((CallSite) Form1.\u003C\u003Eo__16.\u003C\u003Ep__12, obj1);
                string str = string.Format("{0} {1}", obj9, obj10);
                streamWriter3.WriteLine(str);
              }
            }
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool FillData()
    {
      try
      {
        using (WebClient webClient = new WebClient())
        {
          webClient.Encoding = Encoding.UTF8;
          this.data = JsonConvert.DeserializeObject<Form1.Root>(webClient.DownloadString(this.url));
        }
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      try
      {
        X509Certificate2 certificate = new X509Certificate2("server.crt");
        using (X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
        {
          x509Store.Open(OpenFlags.ReadWrite);
          if (!x509Store.Certificates.Contains(certificate))
            x509Store.Add(certificate);
        }
      }
      catch (Exception ex)
      {
      }
      if (!this.RemoveExitHost() || !this.PullData())
        return;
      this.button1.Enabled = false;
      this.timeSpan = TimeSpan.FromMinutes((double) this.data.closeval);
      this.timer1.Enabled = true;
      this.timer1.Start();
      int num = (int) MessageBox.Show(this.data.successm, "موفقیت آمیز", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e) => this.RemoveExitHost();

    private void Form1_FormClosing(object sender, FormClosingEventArgs e) => this.RemoveExitHost();

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (this.timeSpan == TimeSpan.FromSeconds(1.0))
        this.Close();
      this.timeSpan = this.timeSpan.Add(TimeSpan.FromSeconds(-1.0));
      this.button1.Text = this.timeSpan.ToString("mm\\:ss");
    }

    private void button2_Click(object sender, EventArgs e) => this.Close();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.button1 = new Button();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.button2 = new Button();
      this.label1 = new Label();
      this.label2 = new Label();
      this.SuspendLayout();
      this.button1.Location = new Point(36, 29);
      this.button1.Name = "button1";
      this.button1.Size = new Size(118, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "شروع";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.timer1.Interval = 1000;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.button2.DialogResult = DialogResult.Cancel;
      this.button2.Location = new Point(36, 58);
      this.button2.Name = "button2";
      this.button2.Size = new Size(118, 23);
      this.button2.TabIndex = 1;
      this.button2.Text = "خروج";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(36, 84);
      this.label1.Name = "label1";
      this.label1.Size = new Size(121, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "ساده ، کاربردی ، مطمئن";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(66, 109);
      this.label2.Name = "label2";
      this.label2.Size = new Size(50, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "نسخه 1.1";
      this.AcceptButton = (IButtonControl) this.button1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackgroundImageLayout = ImageLayout.None;
      this.CancelButton = (IButtonControl) this.button2;
      this.ClientSize = new Size(184, 129);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (Form1);
      this.RightToLeft = RightToLeft.Yes;
      this.ShowIcon = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "FiveM Helper . ir";
      this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
      this.FormClosed += new FormClosedEventHandler(this.Form1_FormClosed);
      this.Load += new EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public class Root
    {
      public Dictionary<object, object> hosts { get; set; }

      public string successm { get; set; }

      public string version { get; set; }

      public string hook { get; set; }

      public string site { get; set; }

      public int closeval { get; set; }
    }
  }
}
