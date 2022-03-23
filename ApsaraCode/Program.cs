using System;
using System.Windows.Forms;

namespace Apsara
{
   public class Program
    {
		[STAThreadAttribute]
		public static void Main()
		{
			frmLogin frm = new frmLogin();
			if (frm.ShowDialog() == DialogResult.OK)
				Application.Run(new frmMain());
		}

	}
}
