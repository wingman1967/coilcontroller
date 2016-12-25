using System;
using System.Diagnostics;
using System.Timers;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	System.Timers.Timer timer = new System.Timers.Timer(5000); 

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Gdk.Color fontColor = new Gdk.Color();
		Gdk.Color.Parse ("white", ref fontColor);

		Gdk.Color colStop = new Gdk.Color();
		Gdk.Color.Parse("red", ref colStop);
		button3.ModifyBg(StateType.Normal, colStop);

		Gdk.Color colStart = new Gdk.Color();
		Gdk.Color.Parse("green", ref colStart);
		button2.ModifyBg(StateType.Normal, colStart);

		button2.ModifyFg(StateType.Normal, fontColor);
		button3.ModifyFg(StateType.Normal, fontColor);

		lblOperational.Text = "COIL IDLE";
		lblOperational.ModifyFg (StateType.Normal, colStart);

		UpdatePiInfo ();

		//System.Timers.Timer timer = new System.Timers.Timer(5000); 
		timer.Elapsed += new ElapsedEventHandler(delegate{timerTick();});	
		timer.Start();	

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButton2Clicked (object sender, EventArgs e)
	{
		timer.Stop ();
		string command = "/home/pi/./turnledon.py";
		string argss = "";
		string verb = "";
		ProcessStartInfo procInfo = new ProcessStartInfo();

		procInfo.WindowStyle = ProcessWindowStyle.Normal;
		procInfo.UseShellExecute = false;
		procInfo.FileName = command;  
		procInfo.Arguments = argss;    
		procInfo.Verb = verb;         
		Process.Start(procInfo); 
		GC.Collect ();

		Gdk.Color colStop = new Gdk.Color();
		Gdk.Color.Parse("red", ref colStop);
		lblOperational.Text = "COIL ACTIVE";
		lblOperational.ModifyFg (StateType.Normal, colStop);
		timer.Start ();
	}

	protected void OnButton3Clicked (object sender, EventArgs e)
	{
		timer.Stop ();
		string command = "/home/pi/./turnledoff.py";
		string argss = "";
		string verb = "";
		ProcessStartInfo procInfo = new ProcessStartInfo();
		procInfo.WindowStyle = ProcessWindowStyle.Normal;
		procInfo.UseShellExecute = false;
		procInfo.FileName = command;  
		procInfo.Arguments = argss;    
		procInfo.Verb = verb;         
		Process.Start(procInfo);
		GC.Collect ();

		Gdk.Color colStart= new Gdk.Color();
		Gdk.Color.Parse("green", ref colStart);
		lblOperational.Text = "COIL IDLE";
		lblOperational.ModifyFg (StateType.Normal, colStart);
		timer.Start ();
	}

	private void UpdatePiInfo()
	{
		//Retrieve the pi info requested and display

		//ARM frequency
		Process proc = new Process();
		proc.StartInfo.FileName = "/bin/bash";
		proc.StartInfo.Arguments = "/home/pi/./showspeed";
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.RedirectStandardError = true;
		proc.StartInfo.RedirectStandardInput = true;
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start();

		var output = proc.StandardOutput.ReadToEnd();
		string workfreq = output.Trim ();
		int equalLocation = workfreq.IndexOf ("=");
		lblFrequency.Text = workfreq.Substring (equalLocation + 1, workfreq.Length - (equalLocation + 1)) + " MHz";
		//lblFrequency.Text = output.Trim();

		//CPU temp
		Process proc2 = new Process();
		proc2.StartInfo.FileName = "/bin/bash";
		proc2.StartInfo.Arguments = "/home/pi/./showcputemp";
		proc2.StartInfo.UseShellExecute = false;
		proc2.StartInfo.RedirectStandardError = true;
		proc2.StartInfo.RedirectStandardInput = true;
		proc2.StartInfo.RedirectStandardOutput = true;
		proc2.Start();

		var output2 = proc2.StandardOutput.ReadToEnd();
		lblCPU.Text = output2;

		//GPU temp
		Process proc3 = new Process();
		proc3.StartInfo.FileName = "/bin/bash";
		proc3.StartInfo.Arguments = "/home/pi/./gpushowtemp";
		proc3.StartInfo.UseShellExecute = false;
		proc3.StartInfo.RedirectStandardError = true;
		proc3.StartInfo.RedirectStandardInput = true;
		proc3.StartInfo.RedirectStandardOutput = true;
		proc3.Start();

		var output3 = proc3.StandardOutput.ReadToEnd();
		string workGPU = output3.Trim ();
		equalLocation = workGPU.IndexOf ("=");
		lblGPU.Text = workGPU.Substring (equalLocation + 1, workGPU.Length - (equalLocation + 1));

		proc.Dispose ();
		proc2.Dispose ();
		proc3.Dispose ();
		GC.WaitForPendingFinalizers ();
		GC.Collect ();
	}

	private void timerTick()
	{
		timer.Stop ();
		UpdatePiInfo ();
		timer.Start ();
	}
}
