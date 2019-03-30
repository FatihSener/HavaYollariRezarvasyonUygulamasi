using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using havaYollarıKutuphanesi;

namespace sprin2_havaYolları
{
    class Program
    {
        static string[] koltuklar = new string[20];
        const int businessClassKoltukSayisi = 8;
        const int economyClassKoltukSayisi = 12;

        static void Main(string[] args)
        {
            //karşılama ekranı yap
            //tuşa basıldıgında işlem yap.klavyeden okuma işlemi yap.1 mi basıldı, 2mi basıldı
            //1 veya 2 dışında girince yok say ve ilk gelen ekran açık kalsın

            //business seçilirse boş koltuklar 1 den 8 e kadar listelendsin 1 ve 8 dahil
            //boş koltuk yoksa boş koltuk yok mesajı döner ve economy sınıfından devam etmek ister misin diye sorulur.
            //evet derse economy sınıfındaki koltuklar göüntülenir.
            //hayır derse bir sonraki uçuş 4 saat sonra mesajı alır ve karşılama ekranı geri gelir.

            //seçilen koltuk daha önceden seçildiys o koltuk kime aitse onun adı yazar.
            //tekrar boş koltuklar görüntülenir ve koltuk numarası seçilir
            //boş koltuk varsa kullanıcı boş koltk numarasını yazar ve enter'a basar.boş bir koltuk seçilirse kullanıcıdan yolcu ismi alınır.
            //yolcu ismi alındıktan sonra enter a basınca hangi class tan,hangi koltugun hangi yolcuya verildiği bilgisi ekrana yazılır.
            //kullanıcı bir tuşa bastıgında program geri döner ve sınıf seçenekleri tekrar görüntülenir.

            Helper.EkranaYaz("***********************************\nRohan Hava Yollarına Hoşgeldiniz\n***********************************");
            Helper.SatirBasiYap();

            do
            {
                RezervasyonUygulaması();

            } while (true);

        }

        static void RezervasyonUygulaması()
        {
            string sinif = SinifSec();
            bool rezerveEdildiMi;
            int koltukNo;
            do
            {
                string secim = "";
                do
                {
                    secim = "";
                    koltukNo = BosKoltuklariListele(sinif);
                    if (koltukNo == -1)
                    {
                        if (sinif == "business")
                        {

                            Helper.EkranaYaz("Economy Class bölümünde boş koltukları görmek ister misiniz? (E/H)");
                            secim = Console.ReadKey().KeyChar.ToString();
                            if (secim.ToLower() == "e")
                            {
                                sinif = "economy";
                            }
                        }
                        else
                        {

                            Console.WriteLine("Business Class bölümünde boş koltukları görmek ister misiniz? (E/H)");
                            secim = Console.ReadKey().KeyChar.ToString();
                            if (secim.ToLower() == "e")
                            {
                                sinif = "business";
                            }
                        }
                    }
                    else if (!String.IsNullOrEmpty(koltuklar[koltukNo - 1]))
                    {
                        if (sinif == "business")
                        {
                            Console.WriteLine("Business Class bölümündeki {0} numaralı koltuğu daha önce {1} isimli yolcuya rezerve ettiniz\nLütfen boş koltuklardan birini seçiniz.", koltukNo, koltuklar[koltukNo - 1]);
                        }
                        else
                        {
                            Console.WriteLine("Economy Class bölümündeki {0} numaralı koltuğu daha önce {1} isimli yolcuya rezerve ettiniz\nLütfen boş koltuklardan birini seçiniz.", koltukNo, koltuklar[koltukNo - 1]);
                        }
                    }
                } while ((koltukNo == -1 && secim.ToLower() == "e") || (koltukNo >= 0 && !String.IsNullOrEmpty(koltuklar[koltukNo - 1])));
                if (secim.ToLower() == "h")
                {
                    Console.WriteLine("Bir sonraki uçuşlar 4 saat sonradır.");
                    return;
                }
                string yolcuAdi = YolcununAdiniAl(koltukNo);
                rezerveEdildiMi = RezerveEt(sinif, koltukNo, yolcuAdi);
                if (rezerveEdildiMi)
                {
                    Console.WriteLine("Devam etmek için bir tuşa basınız.");
                    Console.ReadKey(true);
                }
            } while (rezerveEdildiMi == false);
        }

        static string SinifSec()
        {
            string tus = "";
            Helper.EkranaYaz("1. Business Class bölümü için 1 tuşuna basın.");
            Helper.EkranaYaz("2. Economy Class bölümü için 2 tuşuna basın.");

            do
            {
                tus = Console.ReadKey(true).KeyChar.ToString();
            } while (tus != "1" && tus != "2");

            if (tus == "1")
            {
                return "business";
            }
            else
            {
                return "economy";
            }

        }

        static string YolcununAdiniAl(int koltukNo)
        {

            Console.WriteLine("Seçilen koltuk numarası: {0}", koltukNo);
            Helper.EkranaYaz("Lütfen yolcunun Adını ve Soyadını giriniz: ");
            return Helper.EkrandanOku();
        }

        static int BosKoltuklariListele(string sinif)
        {
            if (sinif == "business")
            {
                bool bosKoltukVarMi = false;
                Helper.EkranaYaz("Business Class bölümünde kalan boş koltuklar:");
                for (int i = 0; i < businessClassKoltukSayisi; i++)
                {
                    if (String.IsNullOrEmpty(koltuklar[i]))
                    {
                        Console.WriteLine("- {0}", i + 1);
                        bosKoltukVarMi = true;
                    }
                }

                Helper.SatirBasiYap();

                if (bosKoltukVarMi == false)
                {

                    Helper.EkranaYaz("Seçtiğiniz Business Class bölümünde boş koltuk kalmamıştır.");
                    return -1;
                }
            }
            else if (sinif == "economy")
            {
                bool bosKoltukVarMi = false;
                Helper.EkranaYaz("Economy Class bölümünde kalan boş koltuklar:");
                for (int i = businessClassKoltukSayisi; i < koltuklar.Length; i++)
                {
                    if (String.IsNullOrEmpty(koltuklar[i]))
                    {
                        Console.WriteLine("- {0}", i + 1);
                        bosKoltukVarMi = true;
                    }
                }
                if (bosKoltukVarMi == false)
                {

                    Helper.EkranaYaz("Seçtiğiniz Economy Class bölümünde boş koltuk kalmamıştır.");
                    return -1;
                }
            }
            string koltukNoDışardan = "";
            bool sayiMi = false;
            int koltukNo;
            do
            {
                koltukNoDışardan = Console.ReadLine();
                sayiMi = Int32.TryParse(koltukNoDışardan, out koltukNo);
            } while (sayiMi == false);
            return koltukNo;
        }

        static bool RezerveEt(string sinif, int koltukNo, string yolcuAdi)
        {
            if (sinif == "business" && koltukNo > 0 && koltukNo <= businessClassKoltukSayisi)
            {
                if (String.IsNullOrEmpty(koltuklar[koltukNo - 1]))
                {
                    koltuklar[koltukNo - 1] = yolcuAdi;
                    Console.WriteLine("Business Class bölümündeki {0} numaralı koltuğu {1} isimli yolcuya rezerve ettiniz.", koltukNo, yolcuAdi);
                    return true;
                }
                else
                {
                    Console.WriteLine("{0} numaralı koltuğu daha önce \"{1}\" adlı kişiye rezerve ettiniz.", koltukNo, koltuklar[koltukNo - 1]);
                    Helper.EkranaYaz("Lütfen boş koltuklardan birisini seçiniz.");
                    return false;
                }
            }
            else if (sinif == "economy" && koltukNo > businessClassKoltukSayisi && koltukNo <= koltuklar.Length)
            {
                if (String.IsNullOrEmpty(koltuklar[koltukNo - 1]))
                {
                    koltuklar[koltukNo - 1] = yolcuAdi;
                    Console.WriteLine("Economy Class bölümündeki {0} numaralı koltuğu {1} isimli yolcuya rezerve ettiniz.", koltukNo, yolcuAdi);
                    return true;
                }
                else
                {
                    Console.WriteLine("{0} numaralı koltuğu daha önce \"{1}\" adlı kişiye rezerve ettiniz.", koltukNo, koltuklar[koltukNo - 1]);
                    Helper.EkranaYaz("Lütfen boş koltuklardan birisini seçiniz.");
                    return false;
                }
            }
            else
            {
                Helper.EkranaYaz("Geçersiz sınıf veya koltuk numarası girildi. Lütfen tekrar deneyiniz.");
                return false;
            }
        }



    }
}
