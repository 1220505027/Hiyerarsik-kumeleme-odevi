using System;
using System.Collections.Generic;

namespace AglomeratifKumeler
{
    class VeriNoktasi
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Etiket { get; set; }
    }

    class Program
    {
        static float Uzaklik(VeriNoktasi nokta1, VeriNoktasi nokta2)
        {
            return (float)Math.Sqrt(Math.Pow(nokta2.X - nokta1.X, 2) + Math.Pow(nokta2.Y - nokta1.Y, 2));
        }

        static List<VeriNoktasi> KumeleriBirlestir(List<VeriNoktasi> kume1, List<VeriNoktasi> kume2, int birlesikEtiket)
        {
            List<VeriNoktasi> birlesikKume = new List<VeriNoktasi>();

            birlesikKume.AddRange(kume1);

            birlesikKume.AddRange(kume2);

            foreach (var nokta in birlesikKume)
            {
                nokta.Etiket = birlesikEtiket;
            }

            return birlesikKume;
        }

        static void AglomeratifKumeleriBirlestirme(List<VeriNoktasi> veri)
        {
            int kumeSayisi = veri.Count;
            List<List<VeriNoktasi>> kumeler = new List<List<VeriNoktasi>>();
            List<int> kumeEtiketleri = new List<int>();

            for (int i = 0; i < kumeSayisi; i++)
            {
                kumeler.Add(new List<VeriNoktasi>() { veri[i] });
                kumeEtiketleri.Add(i);
            }

            while (kumeSayisi > 1)
            {
                float minUzaklik = float.PositiveInfinity;
                int kume1 = 0;
                int kume2 = 0;

                for (int i = 0; i < kumeSayisi - 1; i++)
                {
                    for (int j = i + 1; j < kumeSayisi; j++)
                    {
                        float uzaklikDegeri = Uzaklik(kumeler[i][0], kumeler[j][0]);
                        if (uzaklikDegeri < minUzaklik)
                        {
                            minUzaklik = uzaklikDegeri;
                            kume1 = i;
                            kume2 = j;
                        }
                    }
                }

                int birlesikEtiket = kumeSayisi;
                List<VeriNoktasi> birlesikKume = KumeleriBirlestir(kumeler[kume1], kumeler[kume2], birlesikEtiket);

                kumeler.RemoveAt(kume2);
                kumeler.RemoveAt(kume1);

                kumeler.Add(birlesikKume);
                kumeEtiketleri[kume1] = birlesikEtiket;

                kumeSayisi--;
            }

            Console.WriteLine("Kümeleme Sonuçları:");
            for (int i = 0; i < veri.Count; i++)
            {
                Console.WriteLine("Nokta: ({0}, {1}) - Etiket: {2}", veri[i].X, veri[i].Y, kumeEtiketleri[i]);
            }
        }

        static void Main()
        {
            List<VeriNoktasi> veri = new List<VeriNoktasi>
            {
                new VeriNoktasi { X = 1.0f, Y = 2.0f, Etiket = 0 },
                new VeriNoktasi { X = 2.0f, Y = 3.0f, Etiket = 0 },
                new VeriNoktasi { X = 3.0f, Y = 4.0f, Etiket = 0 },
                new VeriNoktasi { X = 10.0f, Y = 5.0f, Etiket = 0 },
                new VeriNoktasi { X = 11.0f, Y = 6.0f, Etiket = 0 }
            };

            AglomeratifKumeleriBirlestirme(veri);

            Console.ReadLine();
        }
    }
}