
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using Counter_Pranker;

namespace Counter_Prankster_Namespace
{
    partial class MainForm
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();

        private System.ComponentModel.IContainer components = null;

        private GlobalKeyboardHook _globalKeyboardHook;
        bool isCameraRunning = false;
        bool tomandoFoto = false;
        public static bool x = false;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // https://stackoverflow.com/questions/604410/global-keyboard-capture-in-c-sharp-application
        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            Cursor.Hide();
            Console.Out.WriteLine("Test");
            if (tomandoFoto == false)
            {
                tomandoFoto = true;
                TakePicture();
            }
            // EDT: No need to filter for VkSnapshot anymore. This now gets handled
            // through the constructor of GlobalKeyboardHook(...).
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                // Now you can access both, the key and virtual code
                Keys loggedKey = e.KeyboardData.Key;
                int loggedVkCode = e.KeyboardData.VirtualCode;
            }
        }

        private void TakePicture()
        {
            FilterInfoCollection fic;
            VideoCaptureDevice vcd;

            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            vcd = new VideoCaptureDevice(fic[0].MonikerString);

            vcd.NewFrame += new NewFrameEventHandler(video_NewFrame);
            vcd.Start();
            while (true)
            {
                if (x == true)
                {
                    vcd.SignalToStop();
                    break;
                }
            }
            
            pictureBox1.Image = new PictureMaster().DrawPicture(bitmap, "output.jpg");
            this.TopMost = true;
            tomandoFoto = false;
            _globalKeyboardHook.KeyboardPressed -= OnKeyPressed;
            label1.Text = "Sonrie!";
            this.WindowState = FormWindowState.Maximized;
            this.Show();
            
            new Thread(ByeBye).Start();
        }

        public static void ByeBye()
        {
            Thread.Sleep(5000);
            Cursor.Show();
            LockWorkStation();
        }
        static Bitmap bitmap;
        static void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            bitmap = (Bitmap)eventArgs.Frame.Clone();
            bitmap.Save("output_prank.jpg");

            x = true;
            
        }

        private void SetItUp()
        {
            
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
            label1.Text = "listening...";
            

        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            pictureBox1 = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            beginBtn = new Button();
            ((ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(3, 53);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(776, 197);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.Size = new Size(782, 253);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(beginBtn, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(776, 44);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(614, 44);
            label1.TabIndex = 2;
            label1.Text = "Ready";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // beginBtn
            // 
            beginBtn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            beginBtn.Location = new Point(623, 3);
            beginBtn.Name = "beginBtn";
            beginBtn.Size = new Size(150, 38);
            beginBtn.TabIndex = 3;
            beginBtn.Text = "Start";
            beginBtn.UseVisualStyleBackColor = true;
            beginBtn.Click += beginBtn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            ClientSize = new Size(782, 253);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Counter Prankster";
            FormClosing += Form1_FormClosing;
            ((ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Button beginBtn;
    }
}
