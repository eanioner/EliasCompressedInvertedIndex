using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Coding
{
    class GammaEliasCoding
    {
        public class BufferEncoder
        {
            MemoryStream MemoryStream;
            byte CurrentByte = 0;
            byte CurrentPower = 0;
            Stack<bool> bits;

            public BufferEncoder()
            {
                MemoryStream = new MemoryStream();
                bits = new Stack<bool>();
            }

            void WriteBit(bool bit)
            {
                if (bit)
                {
                    CurrentByte |= (byte)(1 << CurrentPower);
                }
                CurrentPower++;
                if (CurrentPower > 7) Relax();
            }
            void Relax()
            {
                MemoryStream.WriteByte(CurrentByte);
                CurrentByte = CurrentPower = 0;
            }


            public void Append(int value)
            {
                if (value < 0) throw new ArgumentException("Число не может быть отрицательным.");

                bits.Clear();
                while (value > 0)
                {
                    bits.Push((value & 1) == 1);
                    value >>= 1;
                }

                bits.Pop();

                bits.ToList().ForEach(b => this.WriteBit(false));
                this.WriteBit(true);
                bits.ToList().ForEach(b => this.WriteBit(b));
            }

            public byte[] GetByteArray()
            {
                if (CurrentPower > 0) Relax();

                byte[] bytes = MemoryStream.ToArray();

                MemoryStream.Close();
                MemoryStream = new MemoryStream();

                return bytes;
            }
        }

        public class BufferDecoder
        {
            byte[] Buffer;

            public int NumberOfBits
            {
                get { return Buffer.Length * 8; }
            }

            public BufferDecoder(byte[] Buffer)
            {
                this.Buffer = Buffer;
            }

            public IEnumerable<bool> GetBit()
            {

                for (int CurPos = 0; CurPos < NumberOfBits; ++CurPos)
                {

                    int ByteIndex = CurPos / 8;
                    int BitIndex = CurPos % 8;

                    yield return (Buffer[ByteIndex] & (1 << BitIndex)) != 0;
                }
                yield break;
            }

            public IEnumerable<int> GetValue()
            {

                Stack<bool> bits = new Stack<bool>();
                bool getFromStackFlag = false;

                int value = 0;
                foreach (bool bit in this.GetBit())
                {
                    if (getFromStackFlag)
                    {
                        value <<= 1;
                        value |= bit ? 1 : 0;

                        bits.Pop();
                        if (bits.Count == 0)
                        {
                            getFromStackFlag = false;
                            yield return value;
                        }
                    }
                    else if (bit && !getFromStackFlag)
                    {
                        getFromStackFlag = true;
                        value = 1;

                        if (bits.Count == 0)
                        {
                            getFromStackFlag = false;
                            yield return value;
                        }
                    }
                    else
                    {
                        bits.Push(bit);
                    }
                }

                yield break;
            }


        }
    }
}
