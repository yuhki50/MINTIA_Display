using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FTD2XX_NET;
using System.Drawing;

namespace USBDisplay
{
    public class LedMatrix
    {
        /* 定数宣言 */
        // FTDIデバイス //
        private const int FTDI_BAUDRATE = 230400;  // bps


        /* 変数宣言 */
        // FTDIデバイス //
        private FTDI _FTDI_Device;
        private List<byte> _commandList = new List<byte>();

        // ピン配列 //
        private byte _clock_pin;
        private byte _enable_pin;
        private byte[] _data_pins;

        // 表示サイズ //
        private int _LedMatrix_Width;
        private int _LedMatrix_Height;


        /*** ユーザーメソッド ***/
        /// <summary>
        /// LEDマトリックスの幅（ピクセル単位）を取得します。
        /// </summary>
        public int Width
        {
            get { return _LedMatrix_Width; }
        }

        /// <summary>
        /// LEDマトリックスの高さ（ピクセル単位）を取得します。
        /// </summary>
        public int Height
        {
            get { return _LedMatrix_Height; }
        }

        /// <summary>
        /// ピンアサインを設定します。
        /// </summary>
        /// <param name="clock">クロックピン</param>
        /// <param name="enable">イネーブルピン</param>
        /// <param name="d0">データピン0</param>
        /// <param name="d1">データピン1</param>
        /// <param name="d2">データピン2</param>
        /// <param name="d3">データピン3</param>
        /// <param name="d4">データピン4</param>
        /// <param name="d5">データピン5</param>
        public LedMatrix(int clock, int enable, int d0, int d1, int d2, int d3, int d4, int d5)
        {
            /* 変数に記憶 */
            _clock_pin = (byte)(1 << clock);
            _enable_pin = (byte)(1 << enable);

            _data_pins = new byte[6];
            _data_pins[0] = (byte)(1 << d0);
            _data_pins[1] = (byte)(1 << d1);
            _data_pins[2] = (byte)(1 << d2);
            _data_pins[3] = (byte)(1 << d3);
            _data_pins[4] = (byte)(1 << d4);
            _data_pins[5] = (byte)(1 << d5);


            /* FTDIデバイス */
            // インスタンス作成 //
            _FTDI_Device = new FTDI();
        }

        /// <summary>
        /// デバイスを指定された解像度で初期化します。
        /// </summary>
        /// <param name="width">表示サイズの幅を設定</param>
        /// <param name="height">表示サイズの高さを設定</param>
        public void Begin(int width, int height)
        {
            /* 変数に記憶 */
            _LedMatrix_Width = width;
            _LedMatrix_Height = height;


            /* FTDIデバイスを初期化 */
            // 接続 //
            checkStatus(_FTDI_Device.OpenByIndex(1));

            // モード設定 //
            byte port = (byte)(_clock_pin |
                _enable_pin |
                _data_pins[0] |
                _data_pins[1] |
                _data_pins[2] |
                _data_pins[3] |
                _data_pins[4] |
                _data_pins[5]);
            checkStatus(_FTDI_Device.SetBitMode(port, FTDI.FT_BIT_MODES.FT_BIT_MODE_ASYNC_BITBANG));

            // ボーレート設定 //
            checkStatus(_FTDI_Device.SetBaudRate(FTDI_BAUDRATE));


            /* コマンドリスト */
            // クリア //
            _commandList.Clear();


            /* LEDマトリックスの初期化 */
            // リセット //
            addressReset();

            // 処理を実行 //
            Sync();
        }

        /// <summary>
        /// デバイスとの接続を切断します。
        /// </summary>
        public void End()
        {
            // 表示をクリア //
            Clear();

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
        /// <param name="sync">即時反映</param>
        public void Clear(bool sync)
        {
            // アドレスリセット //
            addressReset();

            // データを送信 //
            for (int i = 0; i < _LedMatrix_Width * _LedMatrix_Height; i++)
            {
                _commandList.Add(_enable_pin);
                _commandList.Add((byte)(_enable_pin | _clock_pin));
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
        public void Clear()
        {
            Clear(false);
        }

        /// <summary>
        /// ビットマップを描画
        /// </summary>
        /// <param name="array">色の数値配列</param>
        /// <param name="sync">即時反映</param>
        public void Draw(byte[] array, bool sync)
        {
            // アドレスリセット //
            addressReset();

            // データを送信 //
            for (int i = 0; i < _LedMatrix_Width * _LedMatrix_Height; i++)
            {
                _commandList.Add((byte)(data2pin(array[i] & 0x3F) | _enable_pin));
                _commandList.Add((byte)(data2pin(array[i] & 0x3F) | _enable_pin | _clock_pin));
            }

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// ビットマップを描画
        /// </summary>
        /// <param name="array">色の数値配列</param>
        public void Draw(byte[] array)
        {
            Draw(array, false);
        }

        /// <summary>
        /// ビットマップを描画
        /// </summary>
        /// <param name="bmp">ビットマップ</param>
        /// <param name="sync">即時反映</param>
        public void Draw(Bitmap bmp, bool sync)
        {
            List<byte> colorDataList = new List<byte>();
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
                colorDataList.Add((byte)(rgbValue[i + 2] | rgbValue[i + 0]));  // need check
            }

            bmp.UnlockBits(bmpData);
            colorDataList.Reverse();

            Draw(colorDataList.ToArray(), sync);
        }

        /// <summary>
        /// ビットマップを描画
        /// </summary>
        /// <param name="bmp">ビットマップ</param>
        public void Draw(Bitmap bmp)
        {
            Draw(bmp, false);
        }


        /*** クラス内メソッド ***/
        //private void cmd8bits(byte value)
        //{
        //    commandAdd(value, false);
        //}

        //private void cmd8bits(int value)
        //{
        //    commandAdd((byte)value, false);
        //}

        //private void data8bits(byte value)
        //{
        //    commandAdd(value, true);
        //}

        //private void data8bits(int value)
        //{
        //    commandAdd((byte)value, true);
        //}

        //private void commandAdd(byte value, bool rs, bool dataShift)
        //{
        //    // データ セット //
        //    byte dataUpper = data2pin((value >> 4) & 0x0F);
        //    byte dataLower = data2pin(value & 0x0F);

        //    // コマンド追加 //
        //    switch (rs)
        //    {
        //        case true:  // データモード
        //            _commandList.Add((byte)(dataUpper | _a0_pin | _reset_pin | _enable_pin));
        //            _commandList.Add((byte)(dataUpper | _a0_pin | _reset_pin | _enable_pin | _latch_pin));

        //            //_commandList.Add((byte)(dataLower | _a0_pin | _reset_pin | _enable_pin));
        //            _commandList.Add((byte)(dataLower | _a0_pin | _reset_pin));
        //            //_commandList.Add((byte)(dataLower | _a0_pin | _reset_pin | _enable_pin));
        //            break;

        //        case false:  // コマンドモード
        //            _commandList.Add((byte)(dataUpper | _reset_pin | _enable_pin));
        //            _commandList.Add((byte)(dataUpper | _reset_pin | _enable_pin | _latch_pin));

        //            //_commandList.Add((byte)(dataLower | _reset_pin | _enable_pin));
        //            _commandList.Add((byte)(dataLower | _reset_pin));
        //            //_commandList.Add((byte)(dataLower | _reset_pin | _enable_pin));
        //            break;

        //    }
        //}

        //private void commandDelay(int microseconds)
        //{
        //    if (_commandList.Count == 0)
        //    {
        //        return;
        //    }

        //    byte lastCommand = _commandList.Last();

        //    int nopCount = (int)(microseconds * FTDI_BAUDRATE / Math.Pow(10, 5));  // 補正値
        //    //int nopCount = (int)(microseconds * FTDI_BAUDRATE / Math.Pow(10, 6));  // 計算値

        //    for (int i = 0; i <= nopCount; i++)
        //    {
        //        _commandList.Add(lastCommand);
        //    }
        //}

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

        private void addressReset()
        {
            // アドレスリセット //
            _commandList.Add(0x00);
            _commandList.Add(_clock_pin);
            _commandList.Add(0x00);
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
