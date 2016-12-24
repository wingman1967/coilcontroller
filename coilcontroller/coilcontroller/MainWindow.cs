using System;
using System.Diagnostics;

using Gtk;

public partial class MainWindow: Gtk.Window
{	
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


	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButton2Clicked (object sender, EventArgs e)
	{
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
	}

	protected void OnButton3Clicked (object sender, EventArgs e)
	{
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
	}
}
