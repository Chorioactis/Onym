// THEME SWITCHER
//TODO В localstorage записывать только кастомные темы, проверку на дефолт заменить проверкой на нулл, написать скрипт подгрузки темы без мерцания
function switchTheme(window)
{
    if (getThemeSetting(window, 'theme') === Dark_theme)
    {
        changeThemeSettings(window, Light_theme, getThemeSetting(window, 'accent'));
    }
    else
    {
        changeThemeSettings(window, Dark_theme, getThemeSetting(window, 'accent'));
    }
}
function switchAccent(window, accent)
{
    switch (accent)
    {
        case (Green_accent): changeThemeSettings(window, getThemeSetting(window, 'theme'), Green_accent);
            break;
        case (Yellow_accent): changeThemeSettings(window, getThemeSetting(window, 'theme'), Yellow_accent);
            break;
    }
}
function saveThemeSettings(window, theme, accent)
{
    window.localStorage.setItem(Onym_theme, JSON.stringify({'theme': theme, 'accent': accent}));
}
function getThemeSetting(window, arg)
{
    let string = JSON.parse(window.localStorage.getItem(Onym_theme));
    if (string === null)
    {
        defaultThemeSettings(window)
    }
    if(JSON.parse(window.localStorage.getItem(Onym_theme))[arg] === null)
    {
        switch (arg)
        {
            case 'theme': window.localStorage.setItem(Onym_theme, JSON.stringify({'theme': Light_theme, 'accent': getThemeSetting(window, 'accent')}));
                break;
            case 'accent': window.localStorage.setItem(Onym_theme, JSON.stringify({'theme': getThemeSetting(window, 'theme'), 'accent': Green_accent}));
                break;
        }
    }
    else
    {
        return JSON.parse(window.localStorage.getItem(Onym_theme))[arg]
    }
}
function setThemeSettings(window, theme, accent)
{
    const Html = window.document.getElementsByTagName("html")[0];
    Html.setAttribute('accent-color', accent);
    Html.setAttribute('theme-color', theme);
}
function defaultThemeSettings(window)
{
    changeThemeSettings(window, Light_theme, Green_accent)
}
function changeThemeSettings(window, theme, accent) 
{
    setThemeSettings(window, theme, accent)
    saveThemeSettings(window, theme, accent);
}

