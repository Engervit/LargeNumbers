using System.Numerics;
using System.Diagnostics;

namespace LargeNumbers
{
    internal class Program
    {
        static void Main()
        {
            KeyGenerator [] keys ={new(8), new(16),new(32),new(64),new(128),new(256),new(512),new(1024),new(2048),new(4096), /*new(8192), new(16384), new(32768),*/ };
            Bruteforcer heck = new();

            //KeyGenerator key16Bit = new(16);
            //KeyGenerator key32Bit = new(32);
            //KeyGenerator key64Bit = new(64);
            //KeyGenerator key128Bit = new(128);
            //KeyGenerator key256Bit = new(256);
            //KeyGenerator key512Bit = new(512); //limit of double value
            //KeyGenerator key1024Bit = new(1024);
            //KeyGenerator key2048Bit = new(2048);
            //KeyGenerator key4096Bit = new(4096);

            foreach (KeyGenerator key in keys)
            {
                key.Generate();
                Console.WriteLine($" {key._keyLenght}-bit key space is: {key.KeySpace}");
                Console.WriteLine($" Random {key._keyLenght}-bit key is: {key.Key} \n");
            }

            //key8Bit.Generate();
            //key16Bit.Generate();
            //key32Bit.Generate();
            //key64Bit.Generate();
            //key128Bit.Generate();
            //key256Bit.Generate();
            //key512Bit.Generate();
            //key1024Bit.Generate();
            //key2048Bit.Generate();
            //key4096Bit.Generate();

            //Console.WriteLine($" Key is: {key8Bit.Key}");
            //Console.WriteLine($" Key is: {key16Bit.Key}");
            //Console.WriteLine($" Key is: {key32Bit.Key}");
            //Console.WriteLine($" Key is: {key64Bit.Key}");
            //Console.WriteLine($" Key is: {key128Bit.Key}");
            //Console.WriteLine($" Key is: {key256Bit.Key}");
            //Console.WriteLine($" Key is: {key512Bit.Key}");
            //Console.WriteLine($" Key is: {key1024Bit.Key}");
            //Console.WriteLine($" Key is: {key2048Bit.Key}");
            //Console.WriteLine($" Key is: {key4096Bit.Key}");

            foreach (KeyGenerator key in keys)
            {
                heck.Brute(key.Key);
                Console.WriteLine($"The key was found. Brute time: {heck.BruteTime}");
            }
        }
    }

    internal class KeyGenerator
    {
        internal readonly int _keyLenght;
        private readonly Random rand = new();

        internal BigInteger Key { get; set; }
        internal BigInteger KeySpace
        {
            get
            {
                return BigInteger.Pow(2, _keyLenght);
            }
        }
        public KeyGenerator(int keyLenght)
        {
            _keyLenght = keyLenght;
        }

        internal void Generate()
        {
            double a;
            int i = 0;
            BigInteger rez = 0;

            while (true)
            {
                if (((double)(KeySpace / BigInteger.Pow(10, i))) == double.PositiveInfinity)
                {
                    i++;
                    continue;
                }
                a = (double)(KeySpace / BigInteger.Pow(10, i));
                break;
            }

            for (int k = i; k >= -(int)Math.Log10(double.MaxValue); k -= (int)Math.Log10(double.MaxValue))
            {
                if (k >= 0)
                {
                    rez += (((BigInteger)(a * (rand.NextDouble()))) * BigInteger.Pow(10, k));
                }
                else
                {
                    rez +=  (((BigInteger)(a * (rand.NextDouble()))) / BigInteger.Pow(10, Math.Abs(k)));
                }
            }
            Key = rez;
        }
    }

    internal class Bruteforcer
    {
        private Stopwatch time = new();
        internal BigInteger FoundKey { get; set; }
        internal TimeSpan BruteTime { get; set; }

        internal void Brute(BigInteger key)
        {
            time.Start();
            while (true)
            {
                if (FoundKey == key)
                {
                    break;
                }
                FoundKey++;
            }
            time.Stop();
            BruteTime = time.Elapsed;
        }
    }
}