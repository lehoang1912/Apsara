using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Apsara
{
    public partial class frmMain : Form
    {
        public static string IP = "";
        public static string[] ipAddresses;
        public static string[] subnets;
        public static string[] gateways;
        public static string[] dnses;
        public static Account LoginAccount;
        ApsaraDataContext context = new ApsaraDataContext();
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnChonCardMang_Click(object sender, EventArgs e)
        {
          
        }
        List<DataLink> dsAll = null;
        void loadAllData()
        {
            dsAll = context.DataLinks.Where(x => x.IdAccount == LoginAccount.Id && x.IsDeleted==0).ToList();
            lvFile.Items.Clear();
            int i = 1;
            foreach (DataLink x in dsAll)
            {
                ListViewItem lvi = new ListViewItem(i + "");
                lvi.Tag = x;
                lvi.SubItems.Add(x.Title);
                lvi.SubItems.Add("Xem");
                lvi.SubItems.Add("Xóa");
                i++;
                lvFile.Items.Add(lvi);
            }

            ListViewExtender extender = new ListViewExtender(lvFile);
            // extend 2nd column
            ListViewButtonColumn buttonAction = new ListViewButtonColumn(2);
            buttonAction.Click += OnButtonActionClick;
            buttonAction.FixedWidth = true;

            extender.AddColumn(buttonAction);

            buttonAction = new ListViewButtonColumn(3);
            buttonAction.Click += OnButtonActionClick;
            buttonAction.FixedWidth = true;

            extender.AddColumn(buttonAction);

            buttonAction = new ListViewButtonColumn(4);
            buttonAction.Click += OnButtonActionClick;
            buttonAction.FixedWidth = true;

            extender.AddColumn(buttonAction);

            buttonAction = new ListViewButtonColumn(5);
            buttonAction.Click += OnButtonActionClick;
            buttonAction.FixedWidth = true;

            extender.AddColumn(buttonAction);
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            loadAllData();
        }

        private void mnuCauHinhCardMang_Click(object sender, EventArgs e)
        {
            
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            DataLink dl = new DataLink();
            dl.IdAccount = LoginAccount.Id;
            dl.Title = txtTieuDe.Text;
            dl.Link = txtDuongDan.Text;
            dl.IsDeleted = 0;
            dl.DateUpdated = DateTime.Now;
            context.DataLinks.InsertOnSubmit(dl);
            context.SubmitChanges();
            loadAllData();
            txtDuongDan.Text = "";
            txtTieuDe.Text = "";

        }
       
        private void OnButtonActionClick(object sender, ListViewColumnMouseEventArgs e)
        {
            if (e.Item.Tag != null)
            {
                ListViewButtonColumn lbc = (ListViewButtonColumn)sender;
                DataLink item = (DataLink)e.Item.Tag;
                if (lbc.ColumnIndex == 2)
                {
                    _ytUrl = dsAll[0].Link;
                    webBrowser1.Navigate($"http://youtube.com/v/{VideoId}?version=3");
                    lblLink.Text = item.Link;
                    lblTitle.Text = item.Title;
                }
                else if (lbc.ColumnIndex == 3)
                {
                    item.IsDeleted = 1;
                    item.DateUpdated = DateTime.Now;
                    context.SubmitChanges();
                    loadAllData();
                }
               
            }
        }
        string _ytUrl;
        string VideoId
        {
            get
            {
                var ytMatch = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)").Match(_ytUrl);
                return ytMatch.Success ? ytMatch.Groups[1].Value : string.Empty;
            }
        }

        private static void EthernetInf(out string ip, out string dns, out string nic)  // To get current ethernet config
        {
            ip = "";
            dns = "";
            nic = "";
            string[] NwDesc = { "TAP", "VMware", "Windows", "Virtual" };  // Adapter types (Description) to be ommited
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet && !NwDesc.Any(ni.Description.Contains))  // check for adapter type and its description
                {

                    foreach (IPAddress dnsAdress in ni.GetIPProperties().DnsAddresses)
                    {
                        if (dnsAdress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            dns = dnsAdress.ToString();
                        }
                    }



                    foreach (UnicastIPAddressInformation ips in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ips.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !ips.Address.ToString().StartsWith("169")) //to exclude automatic ips
                        {
                            ip = ips.Address.ToString();
                            nic = ni.Name;
                        }
                    }
                }
            }

        }





        private static void WifiInf(out string ip, out string dns, out string nic)  // To get current wifi config
        {
            ip = "";
            dns = "";
            nic = "";
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {

                    foreach (IPAddress dnsAdress in ni.GetIPProperties().DnsAddresses)
                    {
                        if (dnsAdress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            dns = dnsAdress.ToString();                            
                        }
                    }


                    foreach (UnicastIPAddressInformation ips in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ips.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !ips.Address.ToString().StartsWith("169")) //to exclude automatic ips
                        {
                            ip = ips.Address.ToString();
                            nic = ni.Name;                            
                        }
                    }
                }
            }

        }
        
        string EthDns;
        string EthIP;
        string EthName;

        string WifiIP;
        string WifiDns;
        string WifiName;
        void setIP(string ip)
        {

        }
        List<string> newIps = null;
        int stepNewIp = 0;
        List<string>GetNewIP(string ip,string dns)
        {
            List<string> dsIP = new List<string>();

            string ip2 = ip.Substring(0, ip.LastIndexOf(".")+1);
            int x= int.Parse (dns.Substring(dns.LastIndexOf(".") + 1));
            for (int i=x+1;i<=254;i++)
            {
                string newIp = ip2 + i;
                if (newIp != dns && newIp!=ip)
                    dsIP.Add(newIp);
            }
            return dsIP;
        }
        void randomIP()
        {
            EthernetInf(out EthIP, out EthDns, out EthName);
            WifiInf(out WifiIP, out WifiDns, out WifiName);
            string arg="";
            string subnet = "255.255.255.0";
            string ipnew = "192.168.0.2";
            if (EthIP!="")
            {
                newIps= GetNewIP(EthIP, EthDns);
                stepNewIp = 0;
                ipnew = newIps[stepNewIp];
                arg = "/c netsh interface ip set address \"" + EthName + "\" static " + ipnew + " " + subnet + " " + EthDns + " & netsh interface ip set dns \"" + EthName + "\" static " + EthDns;
            }    
            else
            {
                newIps = GetNewIP(WifiIP, WifiDns);
                stepNewIp = 0;
                ipnew = newIps[stepNewIp];
                arg = "/c netsh interface ip set address \"" + WifiName + "\" static " + ipnew + " " + subnet + " " + WifiDns + " & netsh interface ip set dns \"" + WifiName + "\" static " + WifiDns;
            }    
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
                psi.UseShellExecute = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.Verb = "runas";
                psi.Arguments = arg;
                Process.Start(psi);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        void SetNewIP()
        {
            stepNewIp++;
            if (stepNewIp >= newIps.Count)
                stepNewIp = 0;
            string arg = "";
            string subnet = "255.255.255.0";
            string ipnew = "192.168.0.2";
            if (EthIP != "")
            {
                ipnew = newIps[stepNewIp];
                arg = "/c netsh interface ip set address \"" + EthName + "\" static " + ipnew + " " + subnet + " " + EthDns + " & netsh interface ip set dns \"" + EthName + "\" static " + EthDns;
            }
            else
            {
                ipnew = newIps[stepNewIp];
                arg = "/c netsh interface ip set address \"" + WifiName + "\" static " + ipnew + " " + subnet + " " + WifiDns + " & netsh interface ip set dns \"" + WifiName + "\" static " + WifiDns;
            }
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
                
                psi.UseShellExecute = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.Verb = "runas";
                psi.Arguments = arg;
                Process.Start(psi);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        int step = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            step = 0;
            
            if (button1.Tag.Equals("0"))
            {
                randomIP();
                System.Threading.Thread.Sleep(5000);
                t1 = DateTime.Now;
                _ytUrl = dsAll[0].Link;
                webBrowser1.Navigate($"http://youtube.com/v/{VideoId}?version=3");
                //webBrowser1.Navigate(_ytUrl);
                webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
                lblTitle.Text = dsAll[0].Title;
                lblLink.Text = dsAll[0].Link;

                timer1.Interval = int.Parse(textBox1.Text) * 1000;
                timer1.Enabled = true;
                timer1.Start();
                button1.Text = "Dừng";
                button1.Tag = "1";
            }
            else
            {
                button1.Tag = "0";
                button1.Text = "Bắt đầu";
                timer1.Enabled = false;
                timer1.Stop();
            }
        }
        bool loaded = false;
        DateTime t1;
        void setAutoView()
        {
            try
            {
                DateTime d = (DateTime)context.GetCurrentTimeOnServer().FirstOrDefault().CurrentTime;
                TimeSpan t = (d - LoginAccount.DateRegistered.Value);
                double n = t.TotalDays;
                if (n > 3 && LoginAccount.Type == 1)
                {
                    timer1.Stop();
                    MessageBox.Show("Đã quá 3 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (n > 30 && LoginAccount.Type == 2)
                {
                    timer1.Stop();
                    MessageBox.Show("Đã hết 30 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (n > 90 && LoginAccount.Type == 3)
                {
                    timer1.Stop();
                    MessageBox.Show("Đã hết 90 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (n > 180 && LoginAccount.Type == 4)
                {
                    timer1.Stop();
                    MessageBox.Show("Đã hết 180 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (n > 270 && LoginAccount.Type == 5)
                {
                    timer1.Stop();
                    MessageBox.Show("Đã hết 270 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (n > 365 && LoginAccount.Type == 6)
                {
                    timer1.Stop();
                    MessageBox.Show("Đã hết 365 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch { }

            SetNewIP();
            System.Threading.Thread.Sleep(5000);
            if (loaded == true)
            {
                step++;
                loaded = false;
                t1 = DateTime.Now;
                if (step >= dsAll.Count)
                    step = 0;
                _ytUrl = dsAll[step].Link;
                //webBrowser1.Navigate(_ytUrl);
                webBrowser1.Navigate($"http://youtube.com/v/{VideoId}?version=3");
                //webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
                lblTitle.Text = dsAll[step].Title;
                lblLink.Text = _ytUrl;
            }
            else
            {
                DateTime t2 = DateTime.Now;
                TimeSpan ts = t2.Subtract(t1);
                if (ts.TotalSeconds >= int.Parse(txtTimeout.Text))
                {
                    //webBrowser1.Stop();
                    loaded = true;
                }
            }
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            loaded = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            setAutoView();
        }
    }
    public class ListViewExtender : IDisposable
    {
        private readonly Dictionary<int, ListViewColumn> _columns = new Dictionary<int, ListViewColumn>();

        public ListViewExtender(ListView listView)
        {
            if (listView == null)
                throw new ArgumentNullException("listView");

            if (listView.View != System.Windows.Forms.View.Details)
                throw new ArgumentException(null, "listView");

            ListView = listView;
            ListView.OwnerDraw = true;
            ListView.DrawItem += OnDrawItem;
            ListView.DrawSubItem += OnDrawSubItem;
            ListView.DrawColumnHeader += OnDrawColumnHeader;
            ListView.MouseMove += OnMouseMove;
            ListView.MouseClick += OnMouseClick;

            Font = new System.Drawing.Font(ListView.Font.FontFamily, ListView.Font.Size - 2);
        }

        public virtual System.Drawing.Font Font { get; private set; }
        public ListView ListView { get; private set; }

        protected virtual void OnMouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem item;
            ListViewItem.ListViewSubItem sub;
            ListViewColumn column = GetColumnAt(e.X, e.Y, out item, out sub);
            if (column != null)
            {
                column.MouseClick(e, item, sub);
            }
        }

        public ListViewColumn GetColumnAt(int x, int y, out ListViewItem item, out ListViewItem.ListViewSubItem subItem)
        {
            subItem = null;
            item = ListView.GetItemAt(x, y);
            if (item == null)
                return null;

            subItem = item.GetSubItemAt(x, y);
            if (subItem == null)
                return null;

            for (int i = 0; i < item.SubItems.Count; i++)
            {
                if (item.SubItems[i] == subItem)
                    return GetColumn(i);
            }
            return null;
        }

        protected virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem item;
            ListViewItem.ListViewSubItem sub;
            ListViewColumn column = GetColumnAt(e.X, e.Y, out item, out sub);
            if (column != null)
            {
                column.Invalidate(item, sub);
                return;
            }
            if (item != null)
            {
                ListView.Invalidate(item.Bounds);
            }
        }

        protected virtual void OnDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        protected virtual void OnDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewColumn column = GetColumn(e.ColumnIndex);
            if (column == null)
            {
                e.DrawDefault = true;
                return;
            }

            column.Draw(e);
        }

        protected virtual void OnDrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // do nothing
        }

        public void AddColumn(ListViewColumn column)
        {
            if (column == null)
                throw new ArgumentNullException("column");

            column.Extender = this;
            _columns[column.ColumnIndex] = column;
        }

        public ListViewColumn GetColumn(int index)
        {
            ListViewColumn column;
            return _columns.TryGetValue(index, out column) ? column : null;
        }

        public IEnumerable<ListViewColumn> Columns
        {
            get
            {
                return _columns.Values;
            }
        }

        public virtual void Dispose()
        {
            if (Font != null)
            {
                Font.Dispose();
                Font = null;
            }
        }
    }

    public abstract class ListViewColumn
    {
        public event EventHandler<ListViewColumnMouseEventArgs> Click;

        protected ListViewColumn(int columnIndex)
        {
            if (columnIndex < 0)
                throw new ArgumentException(null, "columnIndex");

            ColumnIndex = columnIndex;
        }

        public virtual ListViewExtender Extender { get; protected internal set; }
        public int ColumnIndex { get; private set; }

        public virtual System.Drawing.Font Font
        {
            get
            {
                return Extender == null ? null : Extender.Font;
            }
        }

        public ListView ListView
        {
            get
            {
                return Extender == null ? null : Extender.ListView;
            }
        }

        public abstract void Draw(DrawListViewSubItemEventArgs e);

        public virtual void MouseClick(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            if (Click != null)
            {
                Click(this, new ListViewColumnMouseEventArgs(e, item, subItem));
            }
        }

        public virtual void Invalidate(ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            if (Extender != null)
            {
                Extender.ListView.Invalidate(subItem.Bounds);
            }
        }
    }

    public class ListViewColumnMouseEventArgs : MouseEventArgs
    {
        public ListViewColumnMouseEventArgs(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            Item = item;
            SubItem = subItem;
        }

        public ListViewItem Item { get; private set; }
        public ListViewItem.ListViewSubItem SubItem { get; private set; }
    }

    public class ListViewButtonColumn : ListViewColumn
    {
        private System.Drawing.Rectangle _hot = System.Drawing.Rectangle.Empty;

        public ListViewButtonColumn(int columnIndex)
            : base(columnIndex)
        {
        }

        public bool FixedWidth { get; set; }
        public bool DrawIfEmpty { get; set; }

        public override ListViewExtender Extender
        {
            get
            {
                return base.Extender;
            }
            protected internal set
            {
                base.Extender = value;
                if (FixedWidth)
                {
                    base.Extender.ListView.ColumnWidthChanging += OnColumnWidthChanging;
                }
            }
        }

        protected virtual void OnColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == ColumnIndex)
            {
                e.Cancel = true;
                e.NewWidth = ListView.Columns[e.ColumnIndex].Width;
            }
        }

        public override void Draw(DrawListViewSubItemEventArgs e)
        {
            if (_hot != System.Drawing.Rectangle.Empty)
            {
                if (_hot != e.Bounds)
                {
                    ListView.Invalidate(_hot);
                    _hot = System.Drawing.Rectangle.Empty;
                }
            }

            if ((!DrawIfEmpty) && (string.IsNullOrEmpty(e.SubItem.Text)))
                return;

            System.Drawing.Point mouse = e.Item.ListView.PointToClient(Control.MousePosition);
            if ((ListView.GetItemAt(mouse.X, mouse.Y) == e.Item) && (e.Item.GetSubItemAt(mouse.X, mouse.Y) == e.SubItem))
            {
                ButtonRenderer.DrawButton(e.Graphics, e.Bounds, e.SubItem.Text, Font, true, PushButtonState.Hot);
                _hot = e.Bounds;
            }
            else
            {
                ButtonRenderer.DrawButton(e.Graphics, e.Bounds, e.SubItem.Text, Font, false, PushButtonState.Default);
            }
        }
    }
    public class ListViewComparer : System.Collections.IComparer
    {
        private int ColumnNumber;
        private SortOrder SortOrder;

        public ListViewComparer(int column_number,
            SortOrder sort_order)
        {
            ColumnNumber = column_number;
            SortOrder = sort_order;
        }

        // Compare two ListViewItems.
        public int Compare(object object_x, object object_y)
        {
            // Get the objects as ListViewItems.
            ListViewItem item_x = object_x as ListViewItem;
            ListViewItem item_y = object_y as ListViewItem;

            // Get the corresponding sub-item values.
            string string_x;
            if (item_x.SubItems.Count <= ColumnNumber)
            {
                string_x = "";
            }
            else
            {
                string_x = item_x.SubItems[ColumnNumber].Text;
            }

            string string_y;
            if (item_y.SubItems.Count <= ColumnNumber)
            {
                string_y = "";
            }
            else
            {
                string_y = item_y.SubItems[ColumnNumber].Text;
            }

            // Compare them.
            int result;
            double double_x, double_y;
            if (double.TryParse(string_x, out double_x) &&
                double.TryParse(string_y, out double_y))
            {
                // Treat as a number.
                result = double_x.CompareTo(double_y);
            }
            else
            {
                DateTime date_x, date_y;
                if (DateTime.TryParse(string_x, out date_x) &&
                    DateTime.TryParse(string_y, out date_y))
                {
                    // Treat as a date.
                    result = date_x.CompareTo(date_y);
                }
                else
                {
                    // Treat as a string.
                    result = string_x.CompareTo(string_y);
                }
            }

            // Return the correct result depending on whether
            // we're sorting ascending or descending.
            if (SortOrder == SortOrder.Ascending)
            {
                return result;
            }
            else
            {
                return -result;
            }
        }
    }
}
