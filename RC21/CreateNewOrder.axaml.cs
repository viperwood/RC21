using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using RC21.Models;

namespace RC21;

public partial class CreateNewOrder : Window
{
    public CreateNewOrder()
    {
        InitializeComponent();
        List<Analyzer> analyzers = Helper.Database.Analyzers.ToList();
        Filter.Items = analyzers.Select(x=>x.Darcode.ToString()).OrderBy(x=> x);
    }
}