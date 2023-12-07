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
        saveFileDialog.Filters?.Add( new FileDialogFilter(){Name = "csv", Extensions = {"csv"}});
        saveFileDialog.Filters?.Add( new FileDialogFilter(){Name = "pdf", Extensions = {"pdf"}});
        var pathDialog = await saveFileDialog.ShowAsync(this);
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
            using (var doc = SKDocument.CreatePdf(path))
            {
                using (var canvas = doc.BeginPage(595,842))
                {
                    int yindex = 100;
                    
                    
                    
                    
                    
                    foreach (var savefile in _buf)
                    {
                        using (var paint = new SKPaint())
                        {
                            canvas.DrawText(savefile, 100, yindex, paint);
                        }
                        yindex += 20;
                    }
                    doc.EndPage();
                }
                doc.Close();
            }
        }

        

        
        
        
    }

    private void Orderinformation(object? sender, SelectionChangedEventArgs e)
    {
        if (ComboBoxCompani.SelectedIndex != -1)
        {
            List<Insurancecompanyname> namesCompaniList = Helper.Database.Insurancecompanynames.ToList();
            ListCost.Items = Helper.Database.Insurancompanychecks
                .Where(x => x.Companiname == namesCompaniList[ComboBoxCompani.SelectedIndex].Companiname)
                .Select(x => new
                {
                    Name= "Имя клиента: " + x.Fullname,
                    Servise= "Название услуги: " + x.Nameservice,
                    Cost= "Цена: " + x.Cost
                }).ToList();
        }
    }
}