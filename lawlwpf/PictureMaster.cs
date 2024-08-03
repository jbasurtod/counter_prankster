using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Counter_Pranker
{
    public class PictureMaster
    {
        private RectangleF rectf;

        public Image DrawPicture(Bitmap bitmap, string outputpath)
        {
            string picpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +"\\"+ outputpath;
            Image imageBackground = bitmap;
            Image imageOverlay = ResizeImage(Properties.Resources.laughing_kid,170,170);
            Image img = new Bitmap(imageBackground.Width, imageBackground.Height);
            using (Graphics gr = Graphics.FromImage(img))
            {
                gr.DrawImage(imageBackground, new Point(0, 0));
                gr.DrawImage(imageOverlay, new Point((int)(imageBackground.Width*0.82), (int)(imageBackground.Height * 0.05)));
                InsertText(gr, "GOTCHA", ((int)(img.Width * 0.08)), (int)(img.Width * 0.1), (int)(img.Height * 0.1), (int)(img.Width), (int)(img.Height * 0.2));
            }
            img.Save(picpath, ImageFormat.Jpeg);
            return img;
        }

        public Graphics InsertText(Graphics g, string text,int fontsize,int x,int y,int Width, int Height)
        {
                //this will center align our text at the bottom of the image
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Far;

                //define a font to use.
                Font f = new Font("Impact", fontsize, FontStyle.Bold, GraphicsUnit.Pixel);

                //pen for outline - set width parameter
                Pen p = new Pen(ColorTranslator.FromHtml("#77090C"), 8);
                p.LineJoin = LineJoin.Round; //prevent "spikes" at the path

                //this makes the gradient repeat for each text line
                Rectangle fr = new Rectangle(x,y, Width, f.Height);
                LinearGradientBrush b = new LinearGradientBrush(fr,
                                                                ColorTranslator.FromHtml("#FF6493"),
                                                                ColorTranslator.FromHtml("#D00F14"),
                                                                90);

                //this will be the rectangle used to draw and auto-wrap the text.
                //basically = image size
                Rectangle r = new Rectangle(0, 0, Width, Height);

                GraphicsPath gp = new GraphicsPath();

                //look mom! no pre-wrapping!
                gp.AddString(text, f.FontFamily, (int)FontStyle.Bold, fontsize, r, sf);

                //these affect lines such as those in paths. Textrenderhint doesn't affect
                //text in a path as it is converted to ..well, a path.    
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                g.DrawPath(p, gp);
                g.FillPath(b, gp);

                //cleanup
                gp.Dispose();
                b.Dispose();
                b.Dispose();
                f.Dispose();
                sf.Dispose();
                g.Dispose();
                string fileName = "ab.jpg";
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            return g;
            
        }

        public void DrawText(Image img)
        {
            rectf = new RectangleF((int)(img.Width*0.1), (int)(img.Width * 0.05), (int)(img.Width * 0.1), (int)(img.Width * 0.1)); //rectf for My Text
            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString("My\nText", new System.Drawing.Font("Tahoma", (int)(img.Width * 0.1), FontStyle.Bold), Brushes.White, rectf, sf);
            }
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

    }

}
