using Gecko;
using PaPortable;
using PaPortable.Views;
using SIL.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
			Application.ApplicationExit += (object sender, EventArgs e) => 
			{
				Xpcom.Shutdown();
			}; 
            Xpcom.Initialize("Firefox");
            var geckoWebBrowser = new GeckoWebBrowser { Dock = DockStyle.Fill };
            Form f = new Form();
            f.Controls.Add(geckoWebBrowser);
			f.Size = new System.Drawing.Size (750, 725);

            var model = new Lip3Data().MyRecs;
            var template = new DataCorpus() { Model = model };
            var page = template.GenerateString();
            using (var tempfile = TempFile.WithExtension("html"))
            {
                File.WriteAllText(tempfile.Path, page);
                var uri = new Uri(tempfile.Path);
                var folder = Path.GetDirectoryName(tempfile.Path);
                WriteResource(folder, "Content", "bootstrap.css");
                WriteResource(folder, "Scripts", "bootstrap.js");
                WriteResource(folder, "Scripts", "jquery-1.9.1.js");
                geckoWebBrowser.Navigate(uri.AbsoluteUri);
                Application.Run(f);
            }
        }

		private static void WriteResource(string folder, string projectLoc, string name)
        {
            using (var str = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("TestProject1." + projectLoc + "." + name)))
            {
                File.WriteAllText(Path.Combine(folder, name), str.ReadToEnd());
            }
        }
    }
}
