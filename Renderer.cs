using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Pen = System.Drawing.Pen;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;
using SolidBrush = System.Drawing.SolidBrush;
using System.Linq;
using System.Text;

namespace XKCDRecursion {
	class Renderer {
		private Bitmap[] m_PanelBmp = new Bitmap[3];
		private Graphics[] m_PanelGfx = new Graphics[3];
		private Pen m_BlackPen;
		private Pen m_WhitePen;

		public BitmapSource CurrentBmpSrc {
			get {
				Bitmap tempBmp = new Bitmap(740, 180);
				Graphics tempGfx = Graphics.FromImage(tempBmp);
				tempGfx.DrawImage(m_PanelBmp[0], 0, 0, m_PanelBmp[0].Width, m_PanelBmp[0].Height);
				tempGfx.DrawImage(m_PanelBmp[1], 252, 0, m_PanelBmp[1].Width, m_PanelBmp[1].Height);
				tempGfx.DrawImage(m_PanelBmp[2], 503, 0, m_PanelBmp[2].Width, m_PanelBmp[2].Height);
				tempBmp.Save("lol.bmp");
				return Imaging.CreateBitmapSourceFromHBitmap(
					tempBmp.GetHbitmap(),
					IntPtr.Zero,
					Int32Rect.Empty,
					BitmapSizeOptions.FromWidthAndHeight(tempBmp.Width, tempBmp.Height)
				);
			}
		}

		public Renderer() {
			m_BlackPen = new Pen(Color.Black);
			m_BlackPen.Width = 2;
			m_WhitePen = new Pen(Color.White);
			m_WhitePen.Width = 2;
		}

		public void CreateInitial() {
			m_PanelBmp[0] = new Bitmap(237, 180); m_PanelGfx[0] = Graphics.FromImage(m_PanelBmp[0]);
			m_PanelBmp[1] = new Bitmap(237, 180); m_PanelGfx[1] = Graphics.FromImage(m_PanelBmp[1]);
			m_PanelBmp[2] = new Bitmap(237, 180); m_PanelGfx[2] = Graphics.FromImage(m_PanelBmp[2]);
			DrawPanels();
			DrawPanel1();
			DrawPanel2();
			DrawPanel3();
			m_PanelGfx[0].Dispose();
			m_PanelGfx[1].Dispose();
			m_PanelGfx[2].Dispose();
		}

		private void DrawPanels() {
			m_PanelGfx[0].DrawRectangle(m_BlackPen, new Rectangle(1, 1, 235, 178));
			m_PanelGfx[1].DrawRectangle(m_BlackPen, new Rectangle(1, 1, 235, 178));
			m_PanelGfx[2].DrawRectangle(m_BlackPen, new Rectangle(1, 1, 235, 178));
		}

		private void DrawPanel1() {
			Point EllipseCenter = new Point(148, 90);
			Point SliceCenterInner = new Point();
			Point SliceCenterTan = new Point();
			Point SliceCenterOuter = new Point();
			Point WhiteInner = new Point();
			Point WhiteOuter = new Point();
			Double Percent = 0.15;
			int SliceAngle = 230;
			SliceCenterInner.X = (int)Math.Round(EllipseCenter.X + (57.5 * Math.Cos((360 - SliceAngle) * Math.PI / 180)));
			SliceCenterInner.Y = (int)Math.Round(EllipseCenter.Y + (57.5 * Math.Sin((360 - SliceAngle) * Math.PI / 180)));
			SliceCenterTan.X = (int)Math.Round(EllipseCenter.X + (72.5 * Math.Cos((360 - SliceAngle) * Math.PI / 180)));
			SliceCenterTan.Y = (int)Math.Round(EllipseCenter.Y + (72.5 * Math.Sin((360 - SliceAngle) * Math.PI / 180)));
			SliceCenterOuter.X = (int)Math.Round(EllipseCenter.X + (86 * Math.Cos((360 - SliceAngle) * Math.PI / 180)));
			SliceCenterOuter.Y = (int)Math.Round(EllipseCenter.Y + (86 * Math.Sin((360 - SliceAngle) * Math.PI / 180)));
			WhiteInner.X = (int)Math.Round(EllipseCenter.X + (57.5 * Math.Cos((360 - 125) * Math.PI / 180)));
			WhiteInner.Y = (int)Math.Round(EllipseCenter.Y + (57.5 * Math.Sin((360 - 125) * Math.PI / 180)));
			WhiteOuter.X = (int)Math.Round(EllipseCenter.X + (83 * Math.Cos((360 - 125) * Math.PI / 180)));
			WhiteOuter.Y = (int)Math.Round(EllipseCenter.Y + (83 * Math.Sin((360 - 125) * Math.PI / 180)));

			m_PanelGfx[0].DrawEllipse(m_BlackPen, new Rectangle(new Point(76, 18), new Size(145, 145)));
			m_PanelGfx[0].FillPie(new SolidBrush(Color.Black), 76, 18, 145, 145, Convert.ToInt32(360 - SliceAngle - (Percent * 180.0)), Convert.ToInt32(Percent * 360.0));
			m_PanelGfx[0].DrawLine(m_WhitePen, SliceCenterInner, SliceCenterTan);
			m_PanelGfx[0].DrawLine(m_BlackPen, SliceCenterTan, SliceCenterOuter);
			m_PanelGfx[0].DrawLine(m_BlackPen, new Point (SliceCenterOuter.X + 2, SliceCenterOuter.Y), new Point(SliceCenterOuter.X - 15, SliceCenterOuter.Y));
			m_PanelGfx[0].DrawLine(m_BlackPen, WhiteInner, WhiteOuter);
			m_PanelGfx[0].DrawLine(m_BlackPen, new Point(WhiteOuter.X + 1, WhiteOuter.Y), new Point(WhiteOuter.X - 20, WhiteOuter.Y));
			m_PanelGfx[0].DrawString("FRACTION OF", new Font("BrowalliaUPC", 12), new SolidBrush(Color.Black), new Point(4, 2));
			m_PanelGfx[0].DrawString("THIS IMAGE", new Font("BrowalliaUPC", 12), new SolidBrush(Color.Black), new Point(4, 13));
			m_PanelGfx[0].DrawString("WHICH IS WHITE", new Font("BrowalliaUPC", 12), new SolidBrush(Color.Black), new Point(4, 24));
			m_PanelGfx[0].DrawString("FRACTION OF", new Font("BrowalliaUPC", 12), new SolidBrush(Color.Black), new Point(4, 136));
			m_PanelGfx[0].DrawString("THIS IMAGE", new Font("BrowalliaUPC", 12), new SolidBrush(Color.Black), new Point(4, 147));
			m_PanelGfx[0].DrawString("WHICH IS BLACK", new Font("BrowalliaUPC", 12), new SolidBrush(Color.Black), new Point(4, 158));
		}

		private void DrawPanel2() {
			//		0 = TL		1 = TR		2 = BL		3 = BR
			double[] Percent = new double[3];
			Point[][] Panel = new Point[3][]; Panel[0] = new Point[4]; Panel[1] = new Point[4]; Panel[2] = new Point[4];
			Percent[0] = 0.10;
			Percent[1] = 0.30;
			Percent[2] = 0.20;
			int Multiplier = Convert.ToInt32(1.0 / Percent.Max()) * 100;
			#region Specify Corners of Rectangles
			Panel[0][2] = new Point(66, 152); Panel[0][3] = new Point(87, 152);		// 1
			Panel[0][0] = new Point(Panel[0][2].X, Panel[0][2].Y - Convert.ToInt32(Percent[0] * Multiplier)); Panel[0][1] = new Point(Panel[0][3].X, Panel[0][3].Y - Convert.ToInt32(Percent[0] * Multiplier));
			Panel[1][2] = new Point(113, 152); Panel[1][3] = new Point(133, 152);	// 2
			Panel[1][0] = new Point(Panel[1][2].X, Panel[1][2].Y - Convert.ToInt32(Percent[1] * Multiplier)); Panel[1][1] = new Point(Panel[1][3].X, Panel[1][3].Y - Convert.ToInt32(Percent[1] * Multiplier));
			Panel[2][2] = new Point(159, 152); Panel[2][3] = new Point(179, 152);	// 3
			Panel[2][0] = new Point(Panel[2][2].X, Panel[2][2].Y - Convert.ToInt32(Percent[2] * Multiplier)); Panel[2][1] = new Point(Panel[2][3].X, Panel[2][3].Y - Convert.ToInt32(Percent[2] * Multiplier));
			#endregion
			#region Draw The Graph Lines/Bars
			m_PanelGfx[1].DrawLine(m_BlackPen, new Point(31, 54), new Point(31, 153));
			m_PanelGfx[1].DrawLine(m_BlackPen, new Point(30, 153), new Point(218, 153));
			m_PanelGfx[1].FillRectangle(new SolidBrush(Color.Black), new Rectangle(Panel[0][0], new Size((Panel[0][1].X - Panel[0][0].X),(Panel[0][2].Y - Panel[0][0].Y))));
			m_PanelGfx[1].FillRectangle(new SolidBrush(Color.Black), new Rectangle(Panel[1][0], new Size((Panel[1][1].X - Panel[1][0].X), (Panel[1][2].Y - Panel[1][0].Y))));
			m_PanelGfx[1].FillRectangle(new SolidBrush(Color.Black), new Rectangle(Panel[2][0], new Size((Panel[2][1].X - Panel[2][0].X), (Panel[2][2].Y - Panel[2][0].Y))));
			#endregion
			#region Draw the Text
			m_PanelGfx[1].DrawString("1", new Font("BrowalliaUPC", 14), new SolidBrush(Color.Black), new Point(68, 155));
			m_PanelGfx[1].DrawString("2", new Font("BrowalliaUPC", 14), new SolidBrush(Color.Black), new Point(115, 155));
			m_PanelGfx[1].DrawString("3", new Font("BrowalliaUPC", 14), new SolidBrush(Color.Black), new Point(161, 155));
			m_PanelGfx[1].DrawString("AMOUNT OF", new Font("BrowalliaUPC", 12), new SolidBrush(Color.Black), new Point(14, 7));
			m_PanelGfx[1].DrawString("BLACK INK", new Font("BrowalliaUPC", 12), new SolidBrush(Color.Black), new Point(14, 18));
			m_PanelGfx[1].DrawString("BY PANEL:", new Font("BrowalliaUPC", 12), new SolidBrush(Color.Black), new Point(14, 29));
			#endregion
		}

		private void DrawPanel3() {

		}
	}
}
