﻿using System;
using System.Windows.Forms;
using Gecko;

namespace TestProject1
{
	public class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Xpcom.Initialize("Firefox");
			var geckoWebBrowser = new GeckoWebBrowser {Dock = DockStyle.Fill};
			Form f = new Form();
			f.Controls.Add(geckoWebBrowser);
			geckoWebBrowser.Navigate("www.google.com");
			Application.Run(f);
		}
	}
}

