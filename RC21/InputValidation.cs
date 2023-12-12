using System.Linq;

namespace RC21;

public class InputValidation
{
    private string? _fio;
    private string? _date;
    private string? _seriesPassport;
    private string? _numberPassport;
    private string? _email;
    private string? _phone;
    private string? _numberPolis;

    public string? FioTest
    {
        get { return _fio; }
        set
        {
            _fio = "";
            if (string.IsNullOrEmpty(value))
            {
                _fio = "ФИО не указанно!";
            }
        }
    }

    public string? DataTest
    {
        get { return _date; }
        set
        {
            _date = "";
            if (value?.Contains('_') == true)
            {
                _date = "Дата не введена или введена неправильно!";
            }
        }
    }

    public string? SeriesPassportTest
    {
        get { return _seriesPassport; }
        set
        {
            _seriesPassport = "";
            if (value?.Contains('_') == true)
            {
                _seriesPassport = "Серия паспорта не введена или введена неверно!";
            }
        }
    }

    public string? NumberPassportTest
    {
        get { return _numberPassport; }
        set
        {
            _numberPassport = "";
            if (value?.Contains('_') == true)
            {
                _numberPassport = "Номер паспорта не введен или введен неверно!";
            }
        }
    }
    
    public string? PhoneTest
    {
        get { return _phone; }
        set
        {
            _phone = "";
            if (value?.Contains('_') == true)
            {
                _phone = "Телефон не введен или введен неверно!";
            }
        }
    }

    public string? EmailTest
    {
        get { return _email; }
        set
        {
            _email = "";
            if (string.IsNullOrEmpty(value)
                || value.Contains("@") == false || value.Contains(".") == false || value.IndexOf('@') > value.IndexOf('.'))
            {
                _email = "E-mail не введен или введен неверно!";
            }
        }
    }

    public string? NumberPolisTest
    {
        get { return _numberPolis; }
        set
        {
            _numberPolis = "";
            if (value?.Contains('_') == true)
            {
                _numberPolis = "Номер полиса не введен или введен неверно!";
            }
        }
    }
}