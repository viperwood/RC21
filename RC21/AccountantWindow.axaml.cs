using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RC21.Models;
using Avalonia.Skia;
using SkiaSharp;

namespace RC21;

public partial class AccountantWindow : Window
{
    private int? _role;
    private List<string?> _buf = new List<string?>();
    private bool _testSave = false;
    private int _sumCost = 0;
    public AccountantWindow()
    {
        InitializeComponent();
        BoxCompani();
    }
    public AccountantWindow(int? role)
    {
        InitializeComponent();
        BoxCompani();
        _role = role;
    }
    //вывод всех компаний
    private void BoxCompani()
    {
        ComboBoxCompani.Items = Helper.Database.Insurancecompanynames.Select(x => new
        {
            Namecompany = x.Companiname
        }).ToList();
    }
    //Сохранение данных в csv/pdf
    private async void Save(object? sender, RoutedEventArgs e)
    {
        _testSave = false;
        FindInformation();
        if (_testSave == true)
        {
            //Получение аути к файлу
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filters?.Add(new FileDialogFilter() { Name = "pdf", Extensions = { "pdf" } });
            saveFileDialog.Filters?.Add(new FileDialogFilter() { Name = "csv", Extensions = { "csv" } });
            var pathDialog = await saveFileDialog.ShowAsync(this);
            if (pathDialog != null)
            {
                string path = string.Join("", pathDialog);
                //Сохранение csv файла
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
                //Сохранение pdf файла
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
    }
    
    
    
    
    
   
    /// <summary>
    /// Добавление данных для сохранения и вывода на экран
    /// </summary>
    private void FindInformation()
    {
        //Проверка на выбор компании
        if (ComboBoxCompani.SelectedIndex != -1)
        {
            //Проверка на наличие текста в полях дат
            if ((DataOf.Text != null || DataFrom.Text != null) && (DataOf.Text != "" || DataFrom.Text != ""))
            {
                bool notFindInformation = false;
                //Проверка на дату
                try
                {
                    //Проверка на правильность дат
                    if (Convert.ToDateTime(DataOf.Text) >= Convert.ToDateTime(DataFrom.Text))
                    {
                        notFindInformation = true;
                        TestErrore.Text = "";
                        _buf.Clear();
                        _sumCost = 0;
                        //Запись общей информации в буфер
                        List<Insurancecompanyname> nameCompani = Helper.Database.Insurancecompanynames.ToList();
                        //!
                        var information = Helper.Database.Insurancompanychecks
                            .Where(x => x.Companiname == nameCompani[ComboBoxCompani.SelectedIndex].Companiname 
                                        && x.Datecreate >= Convert.ToDateTime(DataFrom.Text)
                                        && x.Datecreate <= Convert.ToDateTime(DataOf.Text))
                            .Select(x => new
                            {
                                x.Fullname,
                                x.Cost,
                                x.Userid,
                                x.Datecreate,
                                x.Companiname,
                                x.Nameservice
                            })
                            .ToList();
                        //Выписка данных на экран
                        ListCost.Items = information
                            .Select(x => new
                            {
                                Name = "Имя пациента: " + x.Fullname,
                                Servise = "Название услуги: " + x.Nameservice,
                                Cost = "Цена: " + x.Cost,
                                Date = "Дата создания: " + x.Datecreate
                            }).ToList();
                        //Добавление данных
                        _buf.Add("Страховая компания: " + information[0].Companiname);
                        _buf.Add("");
                        _buf.Add("Оплата");
                        _buf.Add("С " + DataFrom.Text);
                        _buf.Add("По " + DataOf.Text);
                        _buf.Add("");
                        _buf.Add("Все заказы:");
                        //Добавление всех данных о всех заказах
                        for (int i = 0; i < information.Count(); i++)
                        {
                            _buf.Add("Имя пациента: " + information[i].Fullname);
                            _buf.Add("Название услуги: " + information[i].Nameservice);
                            _buf.Add("Цена: " + information[i].Cost);
                            _buf.Add("");
                        }
                        //Добавление общих данных о заказах каждого пациента
                        _buf.Add("Итоговая стоимость по каждому пациенту:");
                        List<Patient> patients = Helper.Database.Patients.ToList();
                        for (int i = 0; i < patients.Count(); i++)
                        {
                            int sumCostFromOnePatient = 0;
                            string? namePatient = "";
                            bool cheackPatient = false;
                            for (int j = 0; j < information.Count(); j++)
                            {
                                if (patients[i].Id == information[j].Userid)
                                {
                                    _sumCost += Convert.ToInt32(information[j].Cost);
                                    sumCostFromOnePatient += Convert.ToInt32(information[j].Cost);
                                    namePatient = information[j].Fullname;
                                    cheackPatient = true;
                                }
                            }
                            if (cheackPatient == true)
                            {
                                _buf.Add("Имя пациента: " + namePatient);
                                _buf.Add("Стоимость услуг: " + sumCostFromOnePatient);
                                _buf.Add("");
                            }
                        }
                        //Итоговая стоимость всех заказов
                        _buf.Add("Итоговая стоимость по всем пациентам: " + _sumCost);
                        _testSave = true;
                    }
                    else
                    {
                        TestErrore.Text = "Даты перепутаны!";
                    }
                }
                catch (Exception e)
                {
                    //Проверка на выподение инфорации
                    if (notFindInformation == true)
                    {
                        TestErrore.Text = "Ничего не найдено";
                    }
                    else
                    {
                        TestErrore.Text = "Дата введена неверно!";
                    }
                }
            }
            else
            {
                TestErrore.Text = "Дата не указана!";
            }
        }
        else
        {
            TestErrore.Text = "Данные не указаны!";
        }
    }
    
    //Кнопка поиска информации
    private void Find(object? sender, RoutedEventArgs e)
    {
        _testSave = false;
        FindInformation();
    }

    private void Beack(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow(_role);
        mainWindow.Show();
        Close();
    }
}