using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeChromium();
        }
        public ChromiumWebBrowser chromeBrowser;
        public CefSharp.OffScreen.ChromiumWebBrowser aaa;

        public void InitializeChromium()
        {
            //aaa = new CefSharp.OffScreen.ChromiumWebBrowser("https:/fastpokemap.se");
            //aaa.ExecuteScriptAsync("alert('test')");
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser("https:/fastpokemap.se");
            // Add it to the form and fill it to the form window.
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;

            chromeBrowser.FrameLoadEnd += (sender, args) =>
            {
                //Wait for the MainFrame to finish loading
                if (args.Frame.IsMain)
                {
                    args.Frame.ExecuteJavaScriptAsync("alert('MainFrame finished loading');");
                    args.Browser.SendMouseWheelEvent(300, 200, 0, 0, CefEventFlags.LeftMouseButton);
                }
            };
            chromeBrowser.RequestHandler = new RequestHandler();
            //chromeBrowser.Load("https:/fastpokemap.se");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           string script= @"
                var evt = new MouseEvent('click', {
                    view: window,
                    bubbles: true,
                    cancelable: true,
                    clientX:600,
                    clientY:300
                });

                var ele = document.getElementsByTagName('body')[0]
                ele.dispatchEvent(evt);
            ";
            
            chromeBrowser.GetMainFrame().ExecuteJavaScriptAsync(@"window.alert('my test');");
            chromeBrowser.GetBrowser().SendMouseWheelEvent(600, 600, 600, 600, CefEventFlags.LeftMouseButton);
            chromeBrowser.GetBrowser().SendMouseWheelEvent(600, 600, 600, 600, CefEventFlags.LeftMouseButton);

        }
    }
}
