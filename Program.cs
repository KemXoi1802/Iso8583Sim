using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Iso8583Simu
{
    class Program
    {
        static void Main(string[] args)
        {
            SslSetup.InstallCertificate();
            FormClientSimulator form = new FormClientSimulator();
            form.ShowDialog();

        }
    }
}
