using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace havaYollarıKutuphanesi
{
    public class Helper
    {
        #region Framework
        public static string[] captchaMetinleri = { "AlAForTanFoni", "AlaFor12", "MusTaFa", "CrazyBoYY", "BeLaLIM93" };
        public static int KontrolluRakamRakamSayiAl(int rakamAdeti, string istekMesaji)
        {
            EkranaYaz(istekMesaji);
            string sayi = "";
            for (int i = 0; i < rakamAdeti; i++)
            {
                int rakam = KontrolluRakamAl();
                if (rakamAdeti > 1 && i == 0 && rakam == 0)
                {
                    i--;
                    continue;
                }
                sayi += rakam;
            }
            return int.Parse(sayi);
        }
        public static int KontrolluRakamAl()
        {
            char karakter = KlavyedenKarakterOku();
            if (karakter == 13)
            {
                return -1;
            }
            int sayi;
            bool donustuMu = int.TryParse(karakter.ToString(), out sayi);
            if (!donustuMu)
            {
                return KontrolluRakamAl();
            }
            EkranaYaz("*");
            return sayi;
        }
        public static char KlavyedenKarakterOku()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            return keyInfo.KeyChar;
        }
        public static void EkraniTemizle()
        {
            Console.Clear();
        }

        public static bool BoolSorusu(string soruMetni, string trueSecici, string falseSecici)
        {
            EkranaYaz(soruMetni + "(" + trueSecici + "/" + falseSecici + ")....");
            string input = EkrandanOku();
            if (input.ToLower() == trueSecici.ToLower())
            {
                return true;
            }
            else if (input.ToLower() == falseSecici.ToLower())
            {
                return false;
            }
            else
            {
                EkranaYaz("Hatalı bir giriş yaptınız.");
                SatirBasiYap();
                return BoolSorusu(soruMetni, trueSecici, falseSecici);
            }
        }

        public static int RastgeleSayiSec(int[] sayilar)
        {
            Random random = new Random();
            int indis = random.Next(0, sayilar.Length);
            return sayilar[indis];
        }

        public static void Bekle()
        {
            Console.ReadKey();
        }

        public static void EkranaYaz(int sayi)
        {
            EkranaYaz(sayi.ToString());
        }

        public static void EkranaYaz(string metin)
        {
            Console.WriteLine(metin);
        }

        public static int OrtalamaHesapla(int[] sayilar)
        {
            int toplam = Topla(sayilar);
            return toplam / sayilar.Length;
        }

        public static int Topla(int[] sayilar)
        {
            int toplam = 0;
            for (int i = 0; i < sayilar.Length; i++)
            {
                toplam += sayilar[i];
            }
            return toplam;
        }

        public static void EkranaYaz(int[] sayilar)
        {
            for (int i = 0; i < sayilar.Length; i++)
            {
                EkranaYaz(sayilar[i] + " ");
            }
        }

        public static int[] RastgeleSayiUret(int sayiAdeti, int sayiAltSiniri, int sayiUstSiniri)
        {
            Random random = new Random();
            int[] sayilar = new int[sayiAdeti];
            for (int i = 0; i < sayiAdeti; i++)
            {
                sayilar[i] = random.Next(sayiAltSiniri, sayiUstSiniri);
            }
            return sayilar;
        }

        public static int KontrolluSayiAl(string istekMesaji, string hataMesaji)
        {
            EkranaYaz(istekMesaji);
            string input = EkrandanOku();
            int sayi;
            bool donustuMu = int.TryParse(input, out sayi);
            if (donustuMu)
            {
                return sayi;
            }
            EkranaYaz(hataMesaji);
            SatirBasiYap();
            return KontrolluSayiAl(istekMesaji, hataMesaji);
        }

        public static void SatirBasiYap()
        {
            Console.WriteLine();
        }

        public static string EkrandanOku()
        {
            return Console.ReadLine();
        }

        public static void KarsilamaYap()
        {
            EkranaYaz("Rastgele Sayi Uygulamasına Hoş Geldiniz.");
            SatirBasiYap();
        }

        public static void DiziyeElemanEkle(ref string[,] dizi, string birinciBoyutDegeri, string ikinciBoyutDegeri)
        {
            string[,] yeniDizi = new string[dizi.GetLength(0) + 1, dizi.GetLength(1)];
            DiziKopyala(dizi, yeniDizi);

            yeniDizi[dizi.GetLength(0), 0] = birinciBoyutDegeri;
            yeniDizi[dizi.GetLength(0), 1] = ikinciBoyutDegeri;

            dizi = yeniDizi;
        }

        public static void DiziKopyala(string[,] kaynakDizi, string[,] hedefDizi)
        {
            for (int i = 0; i < kaynakDizi.GetLength(0); i++)
            {
                for (int j = 0; j < kaynakDizi.GetLength(1); j++)
                {
                    hedefDizi[i, j] = kaynakDizi[i, j];
                }
            }
        }

        public static string KontrolluRakamRakamSayiAl(int minRakamAdeti, string istekMesaji, string minSayiHataMesaji)
        {
            EkranaYaz(istekMesaji);
            string sayi = "";
            do
            {
                int rakam = KontrolluRakamAl();
                if (rakam == -1)
                {
                    break;
                }
                sayi += rakam;

            } while (true);

            if (sayi.Length < minRakamAdeti)
            {
                SatirBasiYap();
                EkranaYaz(minSayiHataMesaji);
                SatirBasiYap();
                return KontrolluRakamRakamSayiAl(minRakamAdeti, istekMesaji, minSayiHataMesaji);
            }
            return sayi;
        }

        public static string KontrolluMetinAl(string istekMesaji, string hataMesaji)
        {
            EkranaYaz(istekMesaji);
            string input = EkrandanOku();
            if (string.IsNullOrWhiteSpace(input))
            {
                SatirBasiYap();
                EkranaYaz(hataMesaji);
                SatirBasiYap();
                return KontrolluMetinAl(istekMesaji, hataMesaji);
            }
            return input;
        }


        public static void UygulamayiKapat()
        {
            Environment.Exit(0);
        }
        #endregion
    }
}
