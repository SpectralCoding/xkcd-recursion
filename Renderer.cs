using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Text;

namespace XKCDRecursion {
	class Renderer {
		private Bitmap m_CurrentBmp;
		private System.Drawing.Pen m_BlackPen;

		public BitmapSource CurrentBmpSrc {
			get {
				return Imaging.CreateBitmapSourceFromHBitmap(
					m_CurrentBmp.GetHbitmap(),
					IntPtr.Zero,
					Int32Rect.Empty,
					BitmapSizeOptions.FromWidthAndHeight(m_CurrentBmp.Width, m_CurrentBmp.Height)
				);
			}
		}

		public Renderer() {
			m_BlackPen = new System.Drawing.Pen(System.Drawing.Color.Black);
			m_BlackPen.Width = 2;
		}

		public void CreateInitial() {
			m_CurrentBmp = new Bitmap(740, 180);
			Graphics gfx = Graphics.FromImage(m_CurrentBmp);
			gfx.DrawRectangle(m_BlackPen, new Rectangle(1, 1, 236, 178));
			gfx.DrawRectangle(m_BlackPen, new Rectangle(252, 1, 236, 178));
			gfx.DrawRectangle(m_BlackPen, new Rectangle(503, 1, 236, 178));
			m_CurrentBmp.Save("lol.bmp");
			gfx.Dispose();
		}
	}
}
