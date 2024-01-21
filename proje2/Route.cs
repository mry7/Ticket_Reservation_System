using proje2;
using System;
using System.Collections.Generic;

public class Route
{
    private Dictionary<string, Dictionary<string, int>> busDistances = new Dictionary<string, Dictionary<string, int>>();
    private Dictionary<string, Dictionary<string, int>> trainDistances = new Dictionary<string, Dictionary<string, int>>();
    private Dictionary<string, Dictionary<string, int>> airplaneDistances = new Dictionary<string, Dictionary<string, int>>();

    public string KalkisYeri { get; private set; }
    public string VarisYeri { get; private set; }
    public double Mesafe { get; private set; }
    public int SeferId { get; set; }
    public List<string> Sehirler { get; set; }

    public static List<Route> GuzergahBilgileri = new List<Route>
    {
        new Route(1, new List<string> { "İstanbul", "Kocaeli", "Bilecik", "Eskişehir", "Ankara", "Eskişehir", "Bilecik", "Kocaeli", "İstanbul" }),
        new Route(2, new List<string> { "İstanbul", "Kocaeli", "Bilecik", "Eskişehir", "Konya", "Eskişehir", "Bilecik", "Kocaeli", "İstanbul" }),
        new Route(3, new List<string> { "İstanbul", "Kocaeli", "Ankara", "Kocaeli", "İstanbul" }),
        new Route(4, new List<string> { "İstanbul", "Kocaeli", "Eskişehir", "Konya", "Eskişehir", "Kocaeli", "İstanbul" }),
        new Route(5, new List<string> { "İstanbul", "Konya", "İstanbul" }),
        new Route(6, new List<string> { "İstanbul", "Ankara", "İstanbul" })
    };

    public Route(int seferId, List<string> sehirler)
    {
        SeferId = seferId;
        Sehirler = sehirler;
    }

    public Route()
    {
        string[] sehirler = { "Istanbul", "Kocaeli", "Ankara", "Eskisehir", "Konya" ,"Bilecik"};

        foreach (var kalkisYeri in sehirler)
        {
            busDistances[kalkisYeri] = new Dictionary<string, int>();
            trainDistances[kalkisYeri] = new Dictionary<string, int>();
            airplaneDistances[kalkisYeri] = new Dictionary<string, int>();

            foreach (var varisYeri in sehirler)
            {
                busDistances[kalkisYeri][varisYeri] = -1; // Mesafe bilgisi bulunamadı
                trainDistances[kalkisYeri][varisYeri] = -1; // Mesafe bilgisi bulunamadı
                airplaneDistances[kalkisYeri][varisYeri] = -1; // Mesafe bilgisi bulunamadı
            }
        }

        // Bus mesafeleri
        SetBusDistance("Istanbul", "Kocaeli", 100);
        SetBusDistance("Istanbul", "Ankara", 500);
        SetBusDistance("Istanbul", "Eskisehir", 300);
        SetBusDistance("Istanbul", "Konya", 600);

        SetBusDistance("Kocaeli", "Ankara", 400);
        SetBusDistance("Kocaeli", "Eskisehir", 200);
        SetBusDistance("Kocaeli", "Konya", 500);

        SetBusDistance("Ankara", "Eskisehir", 300);

        SetBusDistance("Eskisehir", "Konya", 300);


        // Train mesafeleri
        SetTrainDistance("Istanbul", "Kocaeli", 75);
        SetTrainDistance("Istanbul", "Bilecik", 225);
        SetTrainDistance("Istanbul", "Ankara", 375);
        SetTrainDistance("Istanbul", "Eskisehir", 300);
        SetTrainDistance("Istanbul", "Konya", 450);

        SetTrainDistance("Kocaeli", "Bilecik", 75);
        SetTrainDistance("Kocaeli", "Ankara", 300);
        SetTrainDistance("Kocaeli", "Eskisehir", 150);
        SetTrainDistance("Kocaeli", "Konya", 350);

        SetTrainDistance("Bilecik", "Ankara", 225);
        SetTrainDistance("Bilecik", "Eskisehir", 75);
        SetTrainDistance("Bilecik", "Konya", 300);

        SetTrainDistance("Ankara", "Eskisehir", 150);

        SetTrainDistance("Eskisehir", "Konya", 225);

        // Airplane mesafeleri
        SetAirplaneDistance("Istanbul", "Ankara", 250);
        SetAirplaneDistance("Istanbul", "Konya", 300);
    }

    // Bus mesafesi belirleme
    private void SetBusDistance(string kalkisYeri, string varisYeri, int mesafe)
    {
        busDistances[kalkisYeri][varisYeri] = mesafe;
        // İki yönlü mesafe kaydı
        busDistances[varisYeri][kalkisYeri] = mesafe;
    }

    // Train mesafesi belirleme
    private void SetTrainDistance(string kalkisYeri, string varisYeri, int mesafe)
    {
        trainDistances[kalkisYeri][varisYeri] = mesafe;
        // İki yönlü mesafe kaydı
        trainDistances[varisYeri][kalkisYeri] = mesafe;
    }

    // Airplane mesafesi belirleme
    private void SetAirplaneDistance(string kalkisYeri, string varisYeri, int mesafe)
    {
        airplaneDistances[kalkisYeri][varisYeri] = mesafe;
        // İki yönlü mesafe kaydı
        airplaneDistances[varisYeri][kalkisYeri] = mesafe;
    }

    
 
}

