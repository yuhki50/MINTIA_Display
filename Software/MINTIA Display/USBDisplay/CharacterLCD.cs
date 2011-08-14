using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FTD2XX_NET;

namespace USBDisplay
{
    public class CharacterLCD
    {
        /* 定数宣言 */
        // FTDIデバイス //
        private const int FTDI_BAUDRATE = 230400;  // bps

        // キャラクター液晶 //
        // コマンド //
        private const byte LCD_CLEARDISPLAY = 0x01;
        private const byte LCD_RETURNHOME = 0x02;
        private const byte LCD_ENTRYMODESET = 0x04;
        private const byte LCD_DISPLAYCONTROL = 0x08;
        private const byte LCD_CURSORSHIFT = 0x10;
        private const byte LCD_FUNCTIONSET = 0x20;
        private const byte LCD_SETCGRAMADDR = 0x40;
        private const byte LCD_SETDDRAMADDR = 0x80;

        // エントリーモード フラグ //
        private const byte LCD_ENTRYRIGHT = 0x00;
        private const byte LCD_ENTRYLEFT = 0x02;
        private const byte LCD_ENTRYSHIFTINCREMENT = 0x01;
        private const byte LCD_ENTRYSHIFTDECREMENT = 0x00;

        // ディスプレイ ON/OFF コントロール フラグ //
        private const byte LCD_DISPLAYON = 0x04;
        private const byte LCD_DISPLAYOFF = 0x00;
        private const byte LCD_CURSORON = 0x02;
        private const byte LCD_CURSOROFF = 0x00;
        private const byte LCD_BLINKON = 0x01;
        private const byte LCD_BLINKOFF = 0x00;

        // ディスプレイ/カーソルシフト フラグ //
        private const byte LCD_DISPLAYMOVE = 0x08;
        private const byte LCD_CURSORMOVE = 0x00;
        private const byte LCD_MOVERIGHT = 0x04;
        private const byte LCD_MOVELEFT = 0x00;

        // ファンクションセット フラグ //
        private const byte LCD_8BITMODE = 0x10;
        private const byte LCD_4BITMODE = 0x00;
        private const byte LCD_2LINE = 0x08;
        private const byte LCD_1LINE = 0x00;
        private const byte LCD_5x10DOTS = 0x04;
        private const byte LCD_5x8DOTS = 0x00;


        /* 変数宣言 */
        // FTDIデバイス //
        private FTDI _FTDI_Device;
        private List<byte> _commandList = new List<byte>();

        // ピン配列 //
        private byte _rs_pin;  // LOW: command.  HIGH: character.
        private byte _enable_pin;  // activated by a HIGH pulse.
        private byte _backlight_pin;
        private byte[] _data_pins;

        // キャラクター液晶 //
        private byte _displaymode;
        private byte _displaycontrol;
        private byte _displayfunction;
        private bool _backlight_state;
        private int _numcols;
        private int _numlines;


        /*** ユーザーメソッド ***/
        /// <summary>
        /// キャラクター液晶の幅（文字数）を取得します。
        /// </summary>
        public int Cols
        {
            get { return _numcols; }
        }

        /// <summary>
        /// キャラクター液晶の高さ（行数）を取得します。
        /// </summary>
        public int Lines
        {
            get { return _numlines; }
        }

        /// <summary>
        /// ピンアサインを設定します。
        /// </summary>
        /// <param name="rs">データ・コマンド切り替え</param>
        /// <param name="enable">イネーブル</param>
        /// <param name="d4">データピン4</param>
        /// <param name="d5">データピン5</param>
        /// <param name="d6">データピン6</param>
        /// <param name="d7">データピン7</param>
        /// <param name="backlight">バックライト</param>
        public CharacterLCD(int rs, int enable, int d4, int d5, int d6, int d7, int backlight)
        {
            /* 変数に記憶 */
            _rs_pin = (byte)(1 << rs);
            _enable_pin = (byte)(1 << enable);
            _backlight_pin = (byte)(1 << backlight);

            _data_pins = new byte[4];
            _data_pins[0] = (byte)(1 << d4);
            _data_pins[1] = (byte)(1 << d5);
            _data_pins[2] = (byte)(1 << d6);
            _data_pins[3] = (byte)(1 << d7);

            _displaymode = LCD_ENTRYMODESET | LCD_ENTRYLEFT | LCD_ENTRYSHIFTDECREMENT;
            _displaycontrol = LCD_DISPLAYCONTROL | LCD_DISPLAYON | LCD_CURSOROFF | LCD_BLINKOFF;
            _displayfunction = LCD_FUNCTIONSET | LCD_4BITMODE | LCD_2LINE | LCD_5x8DOTS;


            /* FTDIデバイス */
            // インスタンス作成 //
            _FTDI_Device = new FTDI();
        }

        /// <summary>
        /// デバイスを指定された解像度で初期化します。
        /// </summary>
        /// <param name="cols">キャラクター液晶の幅（文字数）を設定</param>
        /// <param name="lines">キャラクター液晶の高さ（行数）を設定</param>
        public void Begin(int cols, int lines)
        {
            /* 変数に記憶 */
            _numcols = cols;
            _numlines = lines;


            /* FTDIデバイスを初期化 */
            // 接続 //
            checkStatus(_FTDI_Device.OpenByIndex(0));

            // IOモード設定 //
            byte port = (byte)(_rs_pin |
                _enable_pin |
                _backlight_pin |
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


            /* キャラクタLCDの初期化 */
            // 初期化 //
            // 8ビットモード //
            cmd4bits(0x03);
            commandDelay(4500);
            cmd4bits(0x03);
            commandDelay(4500);
            cmd4bits(0x03);
            commandDelay(150);

            // 4ビットモード //
            cmd4bits(0x02);

            // ファンクション セット //
            cmd8bits(_displayfunction);

            // ディスプレイ コントロール //
            cmd8bits(_displaycontrol);

            // クリア ディスプレイ //
            Clear();

            // エントリ モード //
            cmd8bits(_displaymode);

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
        /// バックライトをON/OFFします。
        /// </summary>
        /// <param name="state">ON/OFF</param>
        public void Backlight(bool state)
        {
            _backlight_state = state;
            Display();
            Sync();
        }

        /// <summary>
        /// 表示をクリアします。
        /// </summary>
        public void Clear(bool sync)
        {
            cmd8bits(LCD_CLEARDISPLAY);
            commandDelay(2000);

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
        /// カーソル位置をディスプレイ先頭に移動します。
        /// </summary>
        public void Home(bool sync)
        {
            cmd8bits(LCD_RETURNHOME);
            commandDelay(2000);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// カーソル位置をディスプレイ先頭に移動します。
        /// </summary>
        public void Home()
        {
            Home(false);
        }

        /// <summary>
        /// カーソルを指定位置に移動します。
        /// </summary>
        /// <param name="col">列（0 - *）</param>
        /// <param name="row">行（0 - *）</param>
        public void SetCursor(int col, int row, bool sync)
        {
            uint[] row_offsets = { 0x00, 0x40, 0x14, 0x54 };
            if (row > _numlines)
            {
                row = _numlines - 1;
            }

            cmd8bits((byte)(LCD_SETDDRAMADDR | (col + row_offsets[row])));

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// カーソルを指定位置に移動します。
        /// </summary>
        /// <param name="col">列（0 - *）</param>
        /// <param name="row">行（0 - *）</param>
        public void SetCursor(int col, int row)
        {
            SetCursor(col, row, false);
        }

        /// <summary>
        /// 文字コードを指定して表示します。
        /// </summary>
        /// <param name="value">文字コード</param>
        public void Write(byte value, bool sync)
        {
            char8bits(value);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 文字コードを指定して表示します。
        /// </summary>
        /// <param name="value">文字コード</param>
        public void Write(byte value)
        {
            Write(value, false);
        }

        /// <summary>
        /// 文字を表示します。
        /// </summary>
        /// <param name="value">文字列</param>
        public void Print(string value, bool sync)
        {
            byte[] bytedata = System.Text.Encoding.GetEncoding(932).GetBytes(value);

            foreach (byte charData in bytedata)
            {
                char8bits(charData);
            }

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 文字を表示します。
        /// </summary>
        /// <param name="value">文字列</param>
        public void Print(string value)
        {
            Print(value, false);
        }

        /// <summary>
        /// カーソルを表示します。
        /// </summary>
        public void Cursor(bool sync)
        {
            _displaycontrol |= LCD_CURSORON;
            cmd8bits(LCD_DISPLAYCONTROL | _displaycontrol);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// カーソルを表示します。
        /// </summary>
        public void Cursor()
        {
            Cursor(false);
        }

        /// <summary>
        /// カーソルを非表示します。
        /// </summary>
        public void NoCursor(bool sync)
        {
            _displaycontrol &= unchecked((byte)~LCD_CURSORON);
            cmd8bits(_displaycontrol);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// カーソルを非表示します。
        /// </summary>
        public void NoCursor()
        {
            NoCursor(false);
        }

        /// <summary>
        /// カーソルを点滅させます。
        /// </summary>
        public void Blink(bool sync)
        {
            _displaycontrol |= LCD_BLINKON;
            cmd8bits(_displaycontrol);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// カーソルを点滅させます。
        /// </summary>
        public void Blink()
        {
            Blink();
        }

        /// <summary>
        /// カーソルを非点滅させます。
        /// </summary>
        public void NoBlink(bool sync)
        {
            _displaycontrol &= unchecked((byte)~LCD_BLINKON);
            cmd8bits(LCD_DISPLAYCONTROL | _displaycontrol);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// カーソルを非点滅させます。
        /// </summary>
        public void NoBlink()
        {
            NoBlink(false);
        }

        /// <summary>
        /// 表示を有効化します。
        /// </summary>
        public void Display(bool sync)
        {
            _displaycontrol |= LCD_DISPLAYON;
            cmd8bits(_displaycontrol);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 表示を有効化します。
        /// </summary>
        public void Display()
        {
            Display(false);
        }

        /// <summary>
        /// 表示を無効化します。
        /// </summary>
        public void NoDisplay(bool sync)
        {
            _displaycontrol &= unchecked((byte)~LCD_DISPLAYON);
            cmd8bits(LCD_DISPLAYCONTROL | _displaycontrol);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 表示を無効化します。
        /// </summary>
        public void NoDisplay()
        {
            NoDisplay(false);
        }

        /// <summary>
        /// 左にスクロール表示します。
        /// </summary>
        public void ScrollDisplayLeft(bool sync)
        {
            cmd8bits(LCD_CURSORSHIFT | LCD_DISPLAYMOVE | LCD_MOVELEFT);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 左にスクロール表示します。
        /// </summary>
        public void ScrollDisplayLeft()
        {
            ScrollDisplayLeft(false);
        }

        /// <summary>
        /// 右にスクロール表示します。
        /// </summary>
        public void ScrollDisplayRight(bool sync)
        {
            cmd8bits(LCD_CURSORSHIFT | LCD_DISPLAYMOVE | LCD_MOVERIGHT);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 右にスクロール表示します。
        /// </summary>
        public void ScrollDisplayRight()
        {
            ScrollDisplayRight(false);
        }

        /// <summary>
        /// 自動スクロールを有効化します。
        /// </summary>
        public void Autoscroll(bool sync)
        {
            _displaymode |= LCD_ENTRYSHIFTINCREMENT;
            cmd8bits(_displaymode);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 自動スクロールを有効化します。
        /// </summary>
        public void Autoscroll()
        {
            Autoscroll(false);
        }

        /// <summary>
        /// 自動スクロールを無効化します。
        /// </summary>
        public void NoAutoscroll(bool sync)
        {
            _displaymode &= unchecked((byte)~LCD_ENTRYSHIFTINCREMENT);
            cmd8bits(LCD_ENTRYMODESET | _displaymode);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 自動スクロールを無効化します。
        /// </summary>
        public void NoAutoscroll()
        {
            NoAutoscroll(false);
        }

        /// <summary>
        /// 左から右に表示します。
        /// </summary>
        public void LeftToRight(bool sync)
        {
            _displaymode |= LCD_ENTRYLEFT;
            cmd8bits(_displaymode);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 左から右に表示します。
        /// </summary>
        public void LeftToRight()
        {
            LeftToRight(false);
        }

        /// <summary>
        /// 右から左に表示します。
        /// </summary>
        public void RightToLeft(bool sync)
        {
            _displaymode &= unchecked((byte)~LCD_ENTRYLEFT);
            cmd8bits(LCD_ENTRYMODESET | _displaymode);

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 右から左に表示します。
        /// </summary>
        public void RightToLeft()
        {
            RightToLeft(false);
        }

        /// <summary>
        /// 外字を登録します。
        /// </summary>
        /// <param name="location">記憶位置（0-7）</param>
        /// <param name="charmap">文字マップ</param>
        public void CreateChar(byte location, byte[] charmap, bool sync)
        {
            location &= 0x07;
            cmd8bits(LCD_SETCGRAMADDR | (location << 3));
            for (int i = 0; i < 8; i++)
            {
                char8bits(charmap[i]);
            }
            Clear();

            // コマンドを送信 //
            if (sync)
            {
                Sync();
            }
        }

        /// <summary>
        /// 外字を登録します。
        /// </summary>
        /// <param name="location">記憶位置（0-7）</param>
        /// <param name="charmap">文字マップ</param>
        public void CreateChar(byte location, byte[] charmap)
        {
            CreateChar(location, charmap, false);
        }


        /*** クラス内メソッド ***/
        private void cmd4bits(byte value)
        {
            commandAdd(value, false, false);
        }

        private void cmd4bits(int value)
        {
            commandAdd((byte)value, false, false);
        }

        private void cmd8bits(byte value)
        {
            commandAdd(value, false, true);
            commandAdd(value, false, false);
        }

        private void cmd8bits(int value)
        {
            commandAdd((byte)value, false, true);
            commandAdd((byte)value, false, false);
        }

        private void char8bits(byte value)
        {
            commandAdd(value, true, true);
            commandAdd(value, true, false);
        }

        private void char8bits(int value)
        {
            commandAdd((byte)value, true, true);
            commandAdd((byte)value, true, false);
        }

        private void commandAdd(byte value, bool rs, bool dataShift)
        {
            // データをシフト //
            if (dataShift)
            {
                value = data2pin((byte)(value >> 4));
            }
            else
            {
                value = data2pin((byte)(value & 0x0F));
            }

            // バックライトビット //
            switch (_backlight_state)
            {
                case true:
                    value |= _backlight_pin;
                    break;

                case false:
                    value &= (byte)~_backlight_pin;
                    break;

            }

            // コマンド追加 //
            switch (rs)
            {
                case true:  // データモード
                    _commandList.Add((byte)(value | _rs_pin));
                    commandDelay(100);
                    _commandList.Add((byte)(value | _rs_pin | _enable_pin));
                    commandDelay(100);
                    _commandList.Add((byte)(value | _rs_pin));
                    commandDelay(100);
                    break;

                case false:  // コマンドモード
                    _commandList.Add((byte)(value));
                    commandDelay(100);
                    _commandList.Add((byte)(value | _enable_pin));
                    commandDelay(100);
                    _commandList.Add((byte)(value));
                    commandDelay(100);
                    break;

            }
        }

        private void commandDelay(int microseconds)
        {
        	if (_commandList.Count == 0)
            {
                return;
            }

            byte lastCommand = (byte)_commandList[_commandList.Count - 1];

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
