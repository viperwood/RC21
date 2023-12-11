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
            if (string.IsNullOrEmpty(value))
            {
                _fio = "ФИО не указанно!";
            }
            else
            {
                _fio = "";
            }
        }
    }

    public string? DataTest
    {
        get { return _date; }
        set
        {
            if (value?.Split('_') != null)
            {
                _date = "Дата не введена или введена неправильно!";
            }
            else
            {
                _date = "";
            }
        }
    }

    public string? SeriesPassportTest
    {
        get { return _seriesPassport; }
        set
        {
            if (value?.Split('_') != null)
            {
                _seriesPassport = "Серия паспорта не введена или введена неверно!";
            }
            else
            {
                _seriesPassport = "";
            }
        }
    }

    public string? NumberPassportTest
    {
        get { return _numberPassport; }
        set
        {
            if (value?.Split('_') != null)
            {
                _numberPassport = "Номер паспорта не введен или введен неверно!";
            }
            else
            {
                _numberPassport = "";
            }
        }
    }

    public string? EmailTest
    {
        get { return _email; }
        set
        {
            if (string.IsNullOrEmpty(value)
                || value.Contains("@") || value.Contains(".") || value.IndexOf('@') > value.IndexOf('.'))
            {
                _email = "E-mail не введен или введен неверно!";
            }
            else
            {
                _email = "";
            }
        }
    }

    public string? PhoneTest
    {
        get { return _phone; }
        set
        {
            if (value?.Split('_') != null)
            {
                _phone = "Телефон не введен или введен неверно!";
            }
            else
            {
                _phone = "";
            }
        }
    }

    public string? NumberPolisTest
    {
        get { return _numberPolis; }
        set
        {
            if (value?.Split('_') != null)
            {
                _numberPolis = "Номер полиса не введен или введен неверно!";
            }
            else
            {
                _numberPolis = "";
            }
        }
    }
}