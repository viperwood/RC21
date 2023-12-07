using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RC21.Models;
using Avalonia.Skia;
using SkiaSharp;

namespace RC21;

public partial class AccountantWindow : Window
{
    private List<string?> _buf = new List<string?>();
    private bool _testDate = false;
    private int _sumCost = 0;
    public AccountantWindow()
    {
        InitializeComponent();
        BoxCompani();
    }
    
    private void BoxCompani()
    {
        ComboBoxCompani.Items = Helper.Database.Insurancecompanynames.Select(x => new
        {
            Namecompany = x.Companiname
        }).ToList();
    }

    private async void Save(object? sender, RoutedEventArgs e)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filters?.Add(new FileDialogFilter() { Name = "csv", Extensions = { "csv" } });
        saveFileDialog.Filters?.Add(new FileDialogFilter() { Name = "pdf", Extensions = { "pdf" } });
        var pathDialog = await saveFileDialog.ShowAsync(this);
        if (pathDialog != null)
        {
            string path = string.Join("", pathDialog);
            if (pathDialog!.LastIndexOf(".csv", StringComparison.Ordinal) != -1)
            {
                using (FileStream saveFileStream = new FileStream(path, FileMode.Create))
                {
                    using (StreamWriter save = new StreamWriter(saveFileStream))
                    {
                        foreach (var savefile in _buf)
                        {
                            save.WriteLine(savefile);
                        }
                    }
                }
            }
            else
            {
                bool cheack = false;
                using (var doc = SKDocument.CreatePdf(path))
                {
                    int position = 0;
                    do
                    {
                        int yindex = 100;
                        using (var canvas = doc.BeginPage(595, 842))
                        {
                            for (; position < _buf.Count; position++)
                            {
                                if (yindex < 742)
                                {
                                    using (var paint = new SKPaint())
                                    {
                                        canvas.DrawText(_buf[position], 100, yindex, paint);
                                    }

                                    yindex += 20;
                                    cheack = false;
                                }
                                else if (yindex >= 742)
                                {
                                    cheack = true;
                                    break;
                                }
                            }
                            doc.EndPage();
                        }
                    } while (cheack == true) ;
                    doc.Close();
                }
            }
        }
    }

    private void Orderinformation(object? sender, SelectionChangedEventArgs e)
    {
        if (ComboBoxCompani.SelectedIndex != -1)
        {
            _sumCost = 0;
            _buf.Clear();


            
            try
            {
                if (Convert.ToDateTime(DataFrov.Text) <= Convert.ToDateTime(DataOf.Text))
                {
                    _testDate = true;
                }
                else
                {
                    Test.Text = "Вы перепутали даты местами!";
                }
            }
            catch (Exception exception)
            {
                Test.Text = "Это не дата!";
            }
            
            
            
            List<Insurancecompanyname> namesCompaniList = Helper.Database.Insurancecompanynames.ToList();
            var information = Helper.Database.Insurancompanychecks
                .Where(x => x.Companiname == namesCompaniList[ComboBoxCompani.SelectedIndex].Companiname)
                .Select(x => new
                {
                    x.Fullname,
                    x.Nameservice,
                    x.Cost,
                    x.Userid,
                    x.Datecreate
                }).ToList();

            if (_testDate == true)
            {
                ListCost.Items = information
                    .Where(x => Convert.ToDateTime(DataFrov.Text) <= Convert.ToDateTime(x.Datecreate) 
                                && Convert.ToDateTime(DataOf.Text) >= Convert.ToDateTime(x.Datecreate))
                    .Select(x => new
                {
                    Name = "Имя пациента: " + x.Fullname,
                    Servise = "Название услуги: " + x.Nameservice,
                    Cost = "Цена: " + x.Cost,
                    Date = "Дата создания: " + x.Datecreate
                }).ToList();
            }
            else
            {
                ListCost.Items = information.Select(x => new
                {
                    Name = "Имя пациента: " + x.Fullname,
                    Servise = "Название услуги: " + x.Nameservice,
                    Cost = "Цена: " + x.Cost,
                    Date = "Дата создания: " + x.Datecreate
                }).ToList(); 
            }
            
            
            
            
            
            
            
            
            _buf.Add("Все заказы");
            for (int i = 0; i < information.Count; i++)
            {
                _sumCost += Convert.ToInt32(information[i].Cost);
                _buf.Add("Имя пациента: " + information[i].Fullname);
                _buf.Add("Название услуги: " + information[i].Nameservice);
                _buf.Add("Цена: " + information[i].Cost);
                _buf.Add("");
            }
            _buf.Add("Итоговая стоимость по каждому пациентам");
            
            
            List<Patient> informationClients = Helper.Database.Patients.ToList();
            for (int i = 0; i < informationClients.Count; i++)
            {
                int sumcost = 0;
                bool cheack = false;
                string nameclient = "";
                
                
                
                for (int j = 0; j < information.Count; j++)
                {
                    if (informationClients[i].Id == information[j].Userid)
                    {
                        sumcost += Convert.ToInt32(information[j].Cost);
                        nameclient = information[j].Fullname;
                        cheack = true;
                    }
                }

                if (cheack == true)
                {
                    _buf.Add("Имя пациента: " + nameclient);
                    _buf.Add("Цена: " + sumcost);
                    _buf.Add("");
                }
            }
            _buf.Add("Итоговая стоимость по всем пациентам: " + _sumCost);
        }
    }
}