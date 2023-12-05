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

namespace RC21;

public partial class AccountantWindow : Window
{
    private byte i = 1;
    private List<string?> buf = new List<string?>();
    public AccountantWindow()
    {
        InitializeComponent();
        listing();
    }

    private void listing()
    {
        ComboBoxElement.Items = Helper.Database.Servicetipes.Select(x => new
        {
            x.Nameservice
        }).ToList();
    }

    private void Plus(object? sender, RoutedEventArgs e)
    {
        if (ComboBoxElement.SelectedIndex != -1)
        {
            List<Servicetipe> servicetipes = Helper.Database.Servicetipes.ToList();
            var information = servicetipes
                .Where(x => x.Id == ComboBoxElement.SelectedIndex + 1)
                .Select(x => new
                {
                    ServiseT = x.Nameservice + " " + x.Cost + " руб"
                })
                .ToList();
            buf.Add(information[0].ServiseT);
            ListCost.Items = buf.Select(x => new
            {
                NameAndCost = x
            }).ToList();
        }
    }

    private void Mines(object? sender, RoutedEventArgs e)
    {
        if (ListCost.SelectedIndex != -1)
        {
            buf.Remove(buf[ListCost.SelectedIndex]);
            ListCost.Items = buf.Select(x => new
            {
                NameAndCost = x
            }).ToList();
        }
    }

    private async void Save(object? sender, RoutedEventArgs e)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filters.Add( new FileDialogFilter(){Name = "csv", Extensions = {"csv"}});
        saveFileDialog.Filters.Add( new FileDialogFilter(){Name = "pdf", Extensions = {"pdf"}});
        var pathDialog = await saveFileDialog.ShowAsync(this);
        string path = string.Join("", pathDialog);
        
        
        
        
        using (FileStream saveFileStream = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter save = new StreamWriter(saveFileStream))
            {
                foreach (var VARIABLE in buf)
                {
                    save.WriteLine(VARIABLE);
                }
            }
        }
        
        
        
        
        
    }
}