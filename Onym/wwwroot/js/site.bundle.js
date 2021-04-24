const Onym_theme = 'onym_theme'
const Light_theme = 'light'
const Dark_theme = 'dark'
const Green_accent = 'green'
const Yellow_accent = 'yellow'
// PRELOAD
$("header").ready(function() {
    $("header").removeClass('preload');
});


function publicationRate(elem, arg) {
    let animationDuration = 100;
    if (arg === 'RateUp') 
    {
        $(elem).addClass('accent-color').attr('onclick', "publicationRate(this, 'RemoveUp')").parent().find('button[name="PublicationRateDown"]').attr('onclick', "publicationRate(this, 'DownFromUp')").parent().find('p').ratingTallyAnimation('hide', 'RateUp', animationDuration);
    }
    if (arg === 'RemoveUp')
    {
        $(elem).removeClass('accent-color').attr('onclick',  "publicationRate(this, 'RateUp')").parent().find('button[name="PublicationRateDown"]').attr('onclick',  "publicationRate(this, 'RateDown')").parent().find('p').ratingTallyAnimation('hide', 'RemoveUp', animationDuration);
    }
    if (arg === 'UpFromDown')
    {
        $(elem).addClass('accent-color').attr('onclick',  "publicationRate(this, 'RemoveUp')").parent().find('button[name="PublicationRateDown"]').removeClass('danger-color').attr('onclick',  "publicationRate(this, 'DownFromUp')").parent().find('p').ratingTallyAnimation('hide', 'UpFromDown', animationDuration);
    }

    if (arg === 'RateDown')
    {
        $(elem).addClass('danger-color').attr('onclick',  "publicationRate(this, 'RemoveDown')").parent().find('button[name="PublicationRateUp"]').attr('onclick',  "publicationRate(this, 'UpFromDown')").parent().find('p').ratingTallyAnimation('hide', 'RateDown', animationDuration);
    }
    if (arg === 'RemoveDown')
    {
        $(elem).removeClass('danger-color').attr('onclick',  "publicationRate(this, 'RateDown')").parent().find('button[name="PublicationRateUp"]').attr('onclick',  "publicationRate(this, 'RateUp')").parent().find('p').ratingTallyAnimation('hide', 'RemoveDown', animationDuration);
    }
    if (arg === 'DownFromUp')
    {
        $(elem).addClass('danger-color').attr('onclick',  "publicationRate(this, 'RemoveDown')").parent().find('button[name="PublicationRateUp"]').removeClass('accent-color').attr('onclick',  "publicationRate(this, 'UpFromDown')").parent().find('p').ratingTallyAnimation('hide', 'DownFromUp', animationDuration);
    }
}

function publicationFavorite(elem, arg) {   
    if (arg === 'Set')
    {
        $(elem).attr('onclick',  "publicationFavorite(this, 'Remove')").text('Добавить в избранное');    
    }
    if (arg === 'Remove')
    {
        $(elem).attr('onclick',  "publicationFavorite(this, 'Set')").text('Удалить из избранного');
    }
}

function publicationHide(elem, arg) {
    if (arg === 'Hide')
    {
        $(elem).attr('onclick',  "publicationHide(this, 'Show')").text('Показать пост');
    }
    if (arg === 'Show')
    {
        $(elem).attr('onclick',  "publicationHide(this, 'Hide')").text('Скрыть пост');
    }
}


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

