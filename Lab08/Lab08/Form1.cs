
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DrawItem
{
    public partial class Form1 : Form
    {
        private ComboBox cboMode;
        private NumericUpDown nudWidth;
        private Button btnColor;
        private Button btnUndo;
        private Button btnClear;
        private Panel canvas;
        private Label lblStatus;

        private Color currentColor = Color.Black;
        private readonly List<DrawItem> items = new List<DrawItem>();
        private DrawItem currentItem;
        private bool isDrawing;

        public Form1()
        {
            InitializeComponent();
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            Text = "Paint GDI+";
            Width = 1000;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;

            FlowLayoutPanel toolbar = new FlowLayoutPanel();
            toolbar.Dock = DockStyle.Top;
            toolbar.Height = 48;
            toolbar.Padding = new Padding(5);
            toolbar.WrapContents = false;
            toolbar.AutoScroll = true;
            Controls.Add(toolbar);

            toolbar.Controls.Add(new Label
            {
                Text = "Che do:",
                AutoSize = true,
                Margin = new Padding(3, 8, 3, 3)
            });

            cboMode = new ComboBox();
            cboMode.Name = "cboMode";
            cboMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMode.Items.AddRange(new object[]
            {
                "Pen", "Line", "Rectangle", "Ellipse"
            });
            cboMode.SelectedIndex = 0;
            toolbar.Controls.Add(cboMode);

            toolbar.Controls.Add(new Label
            {
                Text = "Do day:",
                AutoSize = true,
                Margin = new Padding(8, 8, 3, 3)
            });

            nudWidth = new NumericUpDown();
            nudWidth.Name = "nudWidth";
            nudWidth.Minimum = 1;
            nudWidth.Maximum = 20;
            nudWidth.Value = 2;
            nudWidth.Width = 55;
            toolbar.Controls.Add(nudWidth);

            btnColor = new Button();
            btnColor.Name = "btnColor";
            btnColor.Text = "Chon mau";
            toolbar.Controls.Add(btnColor);

            btnUndo = new Button();
            btnUndo.Name = "btnUndo";
            btnUndo.Text = "Undo";
            toolbar.Controls.Add(btnUndo);

            btnClear = new Button();
            btnClear.Name = "btnClear";
            btnClear.Text = "Clear";
            toolbar.Controls.Add(btnClear);

            lblStatus = new Label();
            lblStatus.Name = "lblStatus";
            lblStatus.Text = "Toa do: (0, 0)";
            lblStatus.AutoSize = true;
            lblStatus.Margin = new Padding(8, 8, 3, 3);
            toolbar.Controls.Add(lblStatus);

            canvas = new Panel();
            canvas.Name = "canvas";
            canvas.Dock = DockStyle.Fill;
            canvas.BackColor = Color.White;
            canvas.Cursor = Cursors.Cross;
            canvas.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(canvas);

            canvas.BringToFront();
            toolbar.BringToFront();

            canvas.Paint += Canvas_Paint;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;

            btnColor.Click += BtnColor_Click;
            btnUndo.Click += BtnUndo_Click;
            btnClear.Click += BtnClear_Click;
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            isDrawing = true;

            currentItem = new DrawItem
            {
                Mode = cboMode.SelectedItem.ToString(),
                Color = currentColor,
                Width = (float)nudWidth.Value,
                Start = e.Location,
                End = e.Location
            };

            if (currentItem.Mode == "Pen")
                currentItem.Points.Add(e.Location);

            canvas.Invalidate();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            lblStatus.Text = "Toa do: (" + e.X + ", " + e.Y + ")";

            if (!isDrawing || currentItem == null)
                return;

            currentItem.End = e.Location;

            if (currentItem.Mode == "Pen")
                currentItem.Points.Add(e.Location);

            canvas.Invalidate();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing || currentItem == null)
                return;

            currentItem.End = e.Location;

            if (currentItem.Mode == "Pen")
                currentItem.Points.Add(e.Location);

            items.Add(currentItem);
            currentItem = null;
            isDrawing = false;

            canvas.Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (DrawItem item in items)
                VeMotHinh(e.Graphics, item);

            if (currentItem != null)
                VeMotHinh(e.Graphics, currentItem);
        }

        private void VeMotHinh(Graphics g, DrawItem item)
        {
            using (Pen pen = new Pen(item.Color, item.Width))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                if (item.Mode == "Pen")
                {
                    if (item.Points.Count >= 2)
                    {
                        g.DrawLines(pen, item.Points.ToArray());
                    }
                    else if (item.Points.Count == 1)
                    {
                        Point p = item.Points[0];
                        g.DrawLine(pen, p, p);
                    }
                }
                else if (item.Mode == "Line")
                {
                    g.DrawLine(pen, item.Start, item.End);
                }
                else
                {
                    Rectangle rect = LayHinhChuNhat(
                        item.Start, item.End);

                    if (rect.Width > 0 && rect.Height > 0)
                    {
                        if (item.Mode == "Rectangle")
                            g.DrawRectangle(pen, rect);
                        else if (item.Mode == "Ellipse")
                            g.DrawEllipse(pen, rect);
                    }
                }
            }
        }

        private Rectangle LayHinhChuNhat(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int width = Math.Abs(p2.X - p1.X);
            int height = Math.Abs(p2.Y - p1.Y);

            return new Rectangle(x, y, width, height);
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = currentColor;

                if (dialog.ShowDialog() == DialogResult.OK)
                    currentColor = dialog.Color;
            }
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (items.Count > 0)
            {
                items.RemoveAt(items.Count - 1);
                canvas.Invalidate();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            items.Clear();
            currentItem = null;
            isDrawing = false;
            canvas.Invalidate();
        }
    }

    public class DrawItem
    {
        public string Mode;
        public Color Color;
        public float Width;
        public Point Start;
        public Point End;
        public List<Point> Points = new List<Point>();
    }
}
