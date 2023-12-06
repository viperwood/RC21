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
        /*Listing();*/
        BoxCompani();
    }

    /*private void Listing()
    {
        ComboBoxElement.Items = Helper.Database.Servicetipes.Select(x => new
        {
            x.Nameservice
        }).ToList();
    }*/
    
    private void BoxCompani()
    {
        ComboBoxCompani.Items = Helper.Database.Insurancecompanies.Select(x => new
        {
            x.Namecompany
        }).ToList();
    }

    /*private void Plus(object? sender, RoutedEventArgs e)
    {
        if (ComboBoxElement.SelectedIndex != -1)
        {
            List<Servicetipe> servicetipes = Helper.Database.Servicetipes.ToList();
            var information = servicetipes
                .Where(x => x.Id == ComboBoxElement.SelectedIndex + 1)
                .Select(x => new
                {
                    ServiseT = x.Nameservice + " " + " руб"
                })
                .ToList();
            _buf.Add(information[0].ServiseT);
            ListCost.Items = _buf.Select(x => new
            {
                NameAndCost = x
            }).ToList();
        }
    }

    private void Mines(object? sender, RoutedEventArgs e)
    {
        if (ListCost.SelectedIndex != -1)
        {
            _buf.Remove(_buf[ListCost.SelectedIndex]);
            ListCost.Items = _buf.Select(x => new
            {
                NameAndCost = x
            }).ToList();
        }
    }*/

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
                    /*using (var paint = new SKPaint())
                    {
                        canvas.DrawText(compani, 100, yindex, paint);
                        yindex += 20;
                        canvas.DrawText(compani, 100, yindex, paint);
                        yindex += 20;
                    }*/
                    
                    
                    
                    
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
}