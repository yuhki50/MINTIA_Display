using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace ImageEffect
{
    public class BasicEdit
    {
        Bitmap _bmp;

        
        /*** コンストラクタ ***/
        public BasicEdit() {}

        public BasicEdit(Bitmap bmp)
        {
            import(bmp);
        }

        public BasicEdit(string path)
        {
            import(new Bitmap(path));
        }


        /*** 入出力メソッド ***/
        public void import(Bitmap bmp)
        {
            _bmp = bmp;
        }

        public Bitmap export()
        {
            return _bmp;
        }

        public void save(string fileName, System.Drawing.Imaging.ImageFormat format)
        {
            _bmp.Save(fileName, format);
        }


        /*** 編集メソッド ***/
        public void resize(int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);

            g.DrawImage(_bmp, 0, 0, width, height);

            g.Dispose();
            _bmp = new Bitmap(b);
            b.Dispose();
        }

        public void resize(int percent)
        {
            resize(_bmp.Width * percent / 100, _bmp.Height * percent / 100);
        }

        public enum FitType
        {
            normal = 0,
            zoom = 1,
        }

        public void campusResize(int width, int height, FitType fit, Color color)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);


            // キャンパスを塗りつぶす //
            g.Clear(color);


            // スケール計算 //
            float scale = 1;
            switch (fit)
            {
                case FitType.normal:
                    scale = Math.Min((float)width / (float)_bmp.Width, (float)height / (float)_bmp.Height);
                    break;

                case FitType.zoom:
                    scale = Math.Max((float)width / (float)_bmp.Width, (float)height / (float)_bmp.Height);
                    break;

            }

            int w2 = (int)(_bmp.Width * scale);
            int h2 = (int)(_bmp.Height * scale);

            g.DrawImage(_bmp, (width - w2) / 2, (height - h2) / 2, w2, h2);

            g.Dispose();
            _bmp = new Bitmap(b);
            b.Dispose();
        }

        public enum RouteType
        {
            right = 0,
            left = 1,
        }

        public void autoRoute(int width, int height, RouteType route)
        {
            // キャンパスとの対応を確認 //
            int bmpAspect = _bmp.Width / _bmp.Height;
            int campusAspect = width / height;

            if (bmpAspect == campusAspect)
            {
                return;
            }


            // 指定方向に回転 //
            switch (route)
            {
                case RouteType.right:
                    _bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;

                case RouteType.left:
                    _bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;

            }
        }
    }
}
