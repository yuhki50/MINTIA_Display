using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FTD2XX_NET;
using System.Drawing;

namespace USBDisplay
{
    public class GraphicLCD
    {
        /* 定数宣言 */
        // FTDIデバイス //
        private const int FTDI_BAUDRATE = 230400;  // bps


        // LQ020B8UB02 コマンド //
        private const byte CASET = 0x15;  // Column Address  [start column address(lower)] [start column address(upper)] [end column address(lower)] [end column address(upper)]
        private const byte PASET = 0x75;  // Page Address [start page address] [end page address ]
        private const byte PFSET = 0xC8;  // Pixcel Format [sub pixel format]
        private const byte RAMWR = 0x5C;  // Write RAM [color code for the 1st pixel] [Color code for the 2nd pixel] [Color code for the 3rd pixel]
        private const byte RAMRD = 0x5D;  // Read RAM [Read dummy data] [Color code for the 1st pixel] [] [Color code for the 2nd pixel] [] [Color code for the 3rd pixel] [] [similarly]
        private const byte DISNOR = 0xA6;  // Display Normal
        private const byte DISINV = 0xA7;  // Display inverted
        private const byte SLPOUT = 0x94;  // Sleep Disable
        private const byte SLPIN = 0x95;  // Sleep Enable
        private const byte LPIN = 0xA8;  // Low Power Enable [start page address to scan from] [end page address to scan up to] [the number of colors]
        private const byte LPOUT = 0xA9;  // Low Power Disable
        private const byte DISOFF = 0xAE;  // Display Off
        private const byte DISON = 0xAF;  // Display On
        private const byte VOLCTL = 0xC6;  // Volume Control [parameter]
        private const byte NOP = 0x25;  // Non Operation
        private const byte READID1 = 0xDA;  // Read ID1 [read dummy data] [read ID1 data]
        private const byte READID2 = 0xDB;  // Read ID2 [read dummy data] [read ID2 data]

        // 表示サイズ //
        private const int _GLCD_Width = 132;
        private const int _GLCD_Height = 162;

        
        /* 変数宣言 */
        // FTDIデバイス //
        private FTDI _FTDI_Device;
        private List<byte> _commandList = new List<byte>();

        // ピン配列 //
        private byte _latch_pin;
        private byte _a0_pin;
        private byte _enable_pin;
        private byte _reset_pin;
        private byte[] _data_pins;


        /*** ユーザーメソッド ***/
        /// <summary>
        /// グラフィック液晶の幅（ピクセル単位）を取得します。
        /// </summary>
        public int Width
        {
            get { return _GLCD_Width; }
        }

        /// <summary>
        /// グラフィック液晶の高さ（ピクセル単位）を取得します。
        /// </summary>
        public int Height
        {
            get { return _GLCD_Height; }
        }

        /// <summary>
        /// ピンアサインを設定します。
        /// </summary>
        /// <param name="latch">ラッチ</param>
        /// <param name="a0">データ・コマンド切り替え</param>
        /// <param name="enable">イネーブル</param>
        /// <param name="reset">リセット</param>
        /// <param name="d0">データピン0</param>
        /// <param name="d1">データピン1</param>
        /// <param name="d2">データピン2</param>
        /// <param name="d3">データピン3</param>
        public GraphicLCD(int latch, int a0, int enable, int reset, int d0, int d1, int d2, int d3)
        {
            /* 変数に記憶 */
            _latch_pin = (byte)(1 << latch);
            _a0_pin = (byte)(1 << a0);
            _enable_pin = (byte)(1 << enable);
            _reset_pin = (byte)(1 << reset);

            _data_pins = new byte[4];
            _data_pins[0] = (byte)(1 << d0);
            _data_pins[1] = (byte)(1 << d1);
            _data_pins[2] = (byte)(1 << d2);
            _data_pins[3] = (byte)(1 << d3);


            /* FTDIデバイス */
            // インスタンス作成 //
            _FTDI_Device = new FTDI();
        }

        /// <summary>
        /// デバイスを初期化します。
        /// </summary>
        public void Begin()
        {
            /* FTDIデバイスを初期化 */
            // 接続 //
            checkStatus(_FTDI_Device.OpenByIndex(0));

            // IOモード設定 //
            byte port = (byte)(_latch_pin |
                _a0_pin |
                _enable_pin |
                _reset_pin |
                _data_pins[0] |
                _data_pins[1] |
                _data_pins[2] |
                _data_pins[3]);
            checkStatus(_FTDI_Device.SetBitMode(port, FTDI.FT_BIT_MODES.FT_BIT_MODE_ASYNC_BITBANG));

            // ボーレート設定 //
            checkStatus(_FTDI_Device.SetBaudRate(FTDI_BAUDRATE));


            /* コマンドリスト */
            // クリア //
            _commandList.Clear();


            /* グラフィックLCDの初期化 */
            // ポート初期化 //
            _commandList.Add(0x00);
            _commandList.Add(_latch_pin);
            _commandList.Add(0x00);

            // 初期化コマンド //
            _commandList.Add(_enable_pin);
            commandDelay(100);

            _commandList.Add((byte)(_enable_pin | _reset_pin));
            commandDelay(100);

            NoSleep();
            commandDelay(100);

            Clear(0xFFFF);

            Display();

            // 処理を実行 //
            Sync();
        }

        /// <summary>
        /// デバイスとの接続を切断します。
        /// </summary>
        public void End()
        {
            // スリープモードに移行 //
            NoDisplay();
            Sleep();

            // 処理を実行 //
            Sync();

            // FTDIデバイスとの接続を切断 //
            _FTDI_Device.Close();
        }

        /// <summary>
        /// バッファリングされたデータを転送します。
        /// </summary>
        public void Sync()
        {
            // データを転送 //
            if (_commandList.Count == 0)
            {
                return;
            }

            uint numBytesWritten = new uint();

            _FTDI_Device.Write(_commandList.ToArray(), _commandList.Count, ref numBytesWritten);
            _commandList.Clear();
        }

        /// <summary>
        /// バッファをクリアします。
        /// </summary>
        public void Reset()
        {
            _commandList.Clear();
        }

        /// <summary>
        /// 表示をクリアします。
        /// </summary>
        /// <param name="color">塗りつぶし色</param>
        /// <param name="x">四角形の左上隅のx座標</param>
        /// <param name="y">四角形の左上隅のy座標</param>
        /// <param name="width">四角形の幅</param>
        /// <param name="width">四角形の高さ</param>
        /// <param name="sync">即時反映</param>
        public void Clear(int color, int x, int y, int width, int height, bool sync)
        {
            // 表示エリアを設定 //
            drawArea(x, y, width, height);

            // RAMに書き込み //
            cmd8bits(RAMWR);

            int length = width * height;
            for (int i = 0; i < length; i++)
            {
                data8bits(color >> 8);
                data8bits(color);
            }

            // コマンドを送信 //
            if (sync)
	        {
                Sync();
	        }
        }

        /// <summary>
        /// 表示をクリアします。
        /// </summary>
        /// <param name="color">塗りつぶし色</param>
        /// <param name="x">四角形の左上隅のx座標</param>
        /// <param name="y">四角形の左上隅のy座標</param>
        /// <param name="width">四角形の幅</param>
        /// <param name="width">四角形の高さ</param>
        public void Clear(int color, int x, int y, int width, int height)
        {
            Clear(color, x, y, width, height, false);
        }

        /// <summary>
        /// 表示をクリアします。
        /// </summary>
        /// <param name="color">塗りつぶし色</param>
        /// <param name="sync">即時反映</param>
        public void Clear(int color, bool sync)
        {
            Clear(color, 0, 0, _GLCD_Width, _GLCD_Height, sync);
        }

        /// <summary>
        /// 表示をクリアします。
        /// </summary>
        /// <param name="color">塗りつぶし色</param>
        public void Clear(int color)
        {
            Clear(color, false);
        }

        /// <summary>
        /// ビットマップを描画します。
        /// </summary>
        /// <param name="array">色の数値配列</param>
        /// <param name="x">ビットマップの左上隅のx座標</param>
        /// <param name="y">ビットマップの左上隅のy座標</param>
        /// <param name="width">ビットマップの幅</param>
        /// <param name="width">ビットマップの高さ</param>
        /// <param name="sync">即時反映</param>
        public void Draw(int[] array, int x, int y, int width, int height, bool sync)
        {
            // 表示エリアを設定 //
            drawArea(x, y, width, height);

            // 配列の要素数をチェック //
            if (array.Length == 0)
            {
                return;
            }

            // RAMに書き込み //
            cmd8bits(RAMWR);

            int length = width * height;
            for (int i = 0; i < length; i++)
            {
                data8bits(array[i] >> 8);
                data8bits(array[i]);
            }

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// ビットマップを描画します。
        /// </summary>
        /// <param name="array">色の数値配列</param>
        /// <param name="x">ビットマップの左上隅のx座標</param>
        /// <param name="y">ビットマップの左上隅のy座標</param>
        /// <param name="width">ビットマップの幅</param>
        /// <param name="width">ビットマップの高さ</param>
        public void Draw(int[] array, int x, int y, int width, int height)
        {
            Draw(array, x, y, width, height, false);
        }

        /// <summary>
        /// ビットマップを描画します。
        /// </summary>
        /// <param name="bmp">ビットマップ</param>
        /// <param name="x">ビットマップの左上隅のx座標</param>
        /// <param name="y">ビットマップの左上隅のy座標</param>
        /// <param name="sync">即時反映</param>
        public void Draw(Bitmap bmp, bool sync)
        {
            List<int> colorDataList = new List<int>();
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            IntPtr ptr = bmpData.Scan0;
            int size = bmpData.Stride * bmpData.Height;
            byte[] rgbValue = new byte[size];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValue, 0, size);

            for (int i = 0; i < size; i += 3)
            {
                colorDataList.Add(((rgbValue[i + 2] >> 3) << 11) | ((rgbValue[i + 1] >> 3) << 6) | (rgbValue[i + 0] >> 3));
            }

            bmp.UnlockBits(bmpData);
            colorDataList.Reverse();

            Draw(colorDataList.ToArray(), 0, 0, bmp.Width, bmp.Height, sync);
        }

        /// <summary>
        /// ビットマップを描画します。
        /// </summary>
        /// <param name="bmp">ビットマップ</param>
        /// <param name="x">ビットマップの左上隅のx座標</param>
        /// <param name="y">ビットマップの左上隅のy座標</param>
        public void Draw(Bitmap bmp)
        {
            Draw(bmp, false);
        }

        /// <summary>
        /// ディスプレイを有効化します。
        /// </summary>
        /// <param name="sync">即時反映</param>
        public void Display(bool sync)
        {
            // コマンドを追加 //
            cmd8bits(DISON);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// ディスプレイを有効化します。
        /// </summary>
        public void Display()
        {
            Display(false);
        }

        /// <summary>
        /// ディスプレイを無効化します。
        /// </summary>
        /// <param name="sync">即時反映</param>
        public void NoDisplay(bool sync)
        {
            // コマンドを追加 //
            cmd8bits(DISOFF);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// ディスプレイを無効化します。
        /// </summary>
        public void NoDisplay()
        {
            NoDisplay(false);
        }

        /// <summary>
        /// スリープモードに移行します。
        /// </summary>
        /// <param name="sync">即時反映</param>
        public void Sleep(bool sync)
        {
            // コマンドを追加 //
            cmd8bits(SLPIN);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// スリープモードに移行します。
        /// </summary>
        public void Sleep()
        {
            Sleep(false);
        }

        /// <summary>
        /// スリープモードから回復します。
        /// </summary>
        /// <param name="sync">即時反映</param>
        public void NoSleep(bool sync)
        {
            // コマンドを追加 //
            cmd8bits(SLPOUT);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// スリープモードから回復します。
        /// </summary>
        public void NoSleep()
        {
            NoSleep(false);
        }

        /// <summary>
        /// 通常モードで表示します。
        /// </summary>
        /// <param name="sync">即時反映</param>
        public void DisplayNormal(bool sync)
        {
            // コマンドを追加 //
            cmd8bits(DISNOR);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 通常モードで表示します。
        /// </summary>
        public void DisplayNormal()
        {
            DisplayNormal(false);
        }

        /// <summary>
        /// 反転モードで表示します。
        /// </summary>
        /// <param name="sync">即時反映</param>
        public void DisplayInverted(bool sync)
        {
            // コマンドを追加 //
            cmd8bits(DISINV);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 反転モードで表示します。
        /// </summary>
        public void DisplayInverted()
        {
            DisplayInverted(false);
        }


        /*** クラス内メソッド ***/
        private void cmd8bits(byte value)
        {
            commandAdd(value, false);
        }

        private void cmd8bits(int value)
        {
            commandAdd((byte)value, false);
        }

        private void data8bits(byte value)
        {
            commandAdd(value, true);
        }

        private void data8bits(int value)
        {
            commandAdd((byte)value, true);
        }

        private void commandAdd(byte value, bool a0)
        {
            // データ セット //
            byte dataUpper = data2pin((value >> 4) & 0x0F);
            byte dataLower = data2pin(value & 0x0F);

            // コマンド追加 //
            switch (a0)
            {
                case true:  // データモード
                    _commandList.Add((byte)(dataUpper | _a0_pin | _reset_pin | _enable_pin));
                    _commandList.Add((byte)(dataUpper | _a0_pin | _reset_pin | _enable_pin | _latch_pin));

                    //_commandList.Add((byte)(dataLower | _a0_pin | _reset_pin | _enable_pin));
                    _commandList.Add((byte)(dataLower | _a0_pin | _reset_pin));
                    //_commandList.Add((byte)(dataLower | _a0_pin | _reset_pin | _enable_pin));
                    break;

                case false:  // コマンドモード
                    _commandList.Add((byte)(dataUpper | _reset_pin | _enable_pin));
                    _commandList.Add((byte)(dataUpper | _reset_pin | _enable_pin | _latch_pin));

                    //_commandList.Add((byte)(dataLower | _reset_pin | _enable_pin));
                    _commandList.Add((byte)(dataLower | _reset_pin));
                    //_commandList.Add((byte)(dataLower | _reset_pin | _enable_pin));
                    break;

            }
        }

        private void commandDelay(int microseconds)
        {
        	if (_commandList.Count == 0)
            {
                return;
            }

            byte lastCommand = _commandList.Last();

            int nopCount = (int)(microseconds * FTDI_BAUDRATE / Math.Pow(10, 5));  // 補正値
            //int nopCount = (int)(microseconds * FTDI_BAUDRATE / Math.Pow(10, 6));  // 計算値

            for (int i = 0; i <= nopCount; i++)
            {
                _commandList.Add(lastCommand);
            }
        }

        private byte data2pin(byte data)
        {
            // データをピンに変換 //
            byte pinData = 0;

            for (int i = 0; i < _data_pins.Length; i++)
            {
                if (((data >> i) & 0x01) == 1)
                {
                    pinData |= _data_pins[i];
                }
            }

            return pinData;
        }

        private byte data2pin(int data)
        {
            return data2pin((byte)data);
        }

        private void drawArea(int x, int y, int width, int height)
        {
            // コラム アドレス //
            cmd8bits(CASET);
            data8bits(x);  // 開始アドレス 下位
            data8bits(x >> 8);  // 開始アドレス 上位

            int stopAddress = ((width * 2) - 1);
            data8bits(stopAddress);  // 終了アドレス 下位
            data8bits(stopAddress >> 8);  // 終了アドレス 上位


            // ページ アドレス //
            cmd8bits(PASET);
            data8bits(y);  // 開始アドレス
            data8bits(height - 1);  // 終了アドレス
        }

        private void checkStatus(FTDI.FT_STATUS status)
        {
            switch (status)
            {
                case FTDI.FT_STATUS.FT_OK:
                    return;

                case FTDI.FT_STATUS.FT_DEVICE_NOT_FOUND:
                    throw new USB_IOException("デバイスが見つかりません。");

                case FTDI.FT_STATUS.FT_DEVICE_NOT_OPENED:
                    throw new USB_IOException("デバイスと接続されていません。");

                case FTDI.FT_STATUS.FT_DEVICE_NOT_OPENED_FOR_ERASE:
                    throw new USB_IOException("削除のためにデバイスと接続されていません。");

                case FTDI.FT_STATUS.FT_DEVICE_NOT_OPENED_FOR_WRITE:
                    throw new USB_IOException("書き込むためにデバイスと接続されていません。");

                case FTDI.FT_STATUS.FT_EEPROM_ERASE_FAILED:
                    throw new USB_IOException("EEPROMの削除に失敗しました。");

                case FTDI.FT_STATUS.FT_EEPROM_NOT_PRESENT:
                    throw new USB_IOException("EEPROMが見つかりません。");

                case FTDI.FT_STATUS.FT_EEPROM_NOT_PROGRAMMED:
                    throw new USB_IOException("EEPROMがプログラムされていません。");

                case FTDI.FT_STATUS.FT_EEPROM_READ_FAILED:
                    throw new USB_IOException("EEPROMの読み込みに失敗しました。");

                case FTDI.FT_STATUS.FT_EEPROM_WRITE_FAILED:
                    throw new USB_IOException("EEPROMの書き込みに失敗しました。");

                case FTDI.FT_STATUS.FT_FAILED_TO_WRITE_DEVICE:
                    throw new USB_IOException("データ転送に失敗しました。");

                case FTDI.FT_STATUS.FT_INSUFFICIENT_RESOURCES:
                    throw new USB_IOException("リソースが不足しています。");

                case FTDI.FT_STATUS.FT_INVALID_ARGS:
                    throw new USB_IOException("引数が無効です。");

                case FTDI.FT_STATUS.FT_INVALID_BAUD_RATE:
                    throw new USB_IOException("ボーレートが無効です。");

                case FTDI.FT_STATUS.FT_INVALID_HANDLE:
                    throw new USB_IOException("ハンドルが無効です。");

                case FTDI.FT_STATUS.FT_INVALID_PARAMETER:
                    throw new USB_IOException("パラメーターが無効です。");

                case FTDI.FT_STATUS.FT_IO_ERROR:
                    throw new USB_IOException("I/Oエラーが発生しました。");

                default:
                    throw new USB_IOException("不明なエラーが発生しました。");
            }
        }
    }
}
