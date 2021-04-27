// CONSTANTS
const Onym_theme = 'onym_theme'
const Light_theme = 'light'
const Dark_theme = 'dark'
const Green_accent = 'green'
const Yellow_accent = 'yellow'
const Sky_accent = 'sky'
const Pastel_accent = 'pastel'
const Document = this.document;
const Html = this.document.getElementsByTagName("html")[0];
const Search_form = this.document.getElementsByClassName('search')[0];
const Theme_switcher = this.document.getElementsByClassName('theme-switcher')[0];
const Accent_switchers = this.document.getElementsByClassName('popover-menu')[0];
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


// SEARCH FORM
function showSearchField() 
{
    Search_form.classList.remove('hidden');
}
function hideSearchField() 
{
    Search_form.classList.add('hidden');
    Search_form.getElementsByClassName('search-button')[0].removeAttribute('style', 'transition-delay: 0ms !important');
    Search_form.getElementsByClassName('search-input-field')[0].removeAttribute('style', 'transition-delay: 0ms !important');
}
function setSearchFormDelay()
{
    if(Search_form.getElementsByTagName('input')[0].value !== ''){return 5000;}else{return 1000;} 
}

// SEARCH FORM LISTENERS
Search_form.addEventListener('mouseenter', e => {
    showSearchField();
});
Search_form.addEventListener('mouseleave', e => {
    let timerId = setTimeout(hideSearchField, setSearchFormDelay())
    Search_form.addEventListener('mouseenter', e => {
        clearTimeout(timerId);
    });
    Search_form.addEventListener('input', e => {
        clearTimeout(timerId);
        timerId = setTimeout(hideSearchField, setSearchFormDelay())
    });
});
// VARIABLES
let timerId;
let popoverButton = Theme_switcher.getElementsByClassName('popover-button')[0];
let popoverMenu = Theme_switcher.getElementsByClassName('popover-menu')[0];
let themeCheckbox = Theme_switcher.getElementsByClassName('theme-switcher-check')[0].getElementsByTagName('input')[0];
let accentCheckboxes = Accent_switchers;
let activeAccent;
// THEME SWITCHER FORM
function switchTheme()
{
    if (getThemeSetting('theme') === Dark_theme)
    {
        changeThemeSettings(Light_theme, getThemeSetting('accent'));
    }
    else
    {
        changeThemeSettings(Dark_theme, getThemeSetting('accent'));
    }
}
function switchAccent(accent)
{
        switch (accent)
    {
        case (Green_accent): changeThemeSettings(getThemeSetting('theme'), Green_accent);
            break;
        case (Yellow_accent): changeThemeSettings(getThemeSetting('theme'), Yellow_accent);
            break;
        case (Sky_accent): changeThemeSettings(getThemeSetting('theme'), Sky_accent);
            break;
        case (Pastel_accent): changeThemeSettings(getThemeSetting('theme'), Pastel_accent);
            break;
    }
}
function saveThemeSettings(theme, accent)
{
    this.localStorage.setItem(Onym_theme, JSON.stringify({'theme': theme, 'accent': accent}));
}
function getThemeSetting(arg)
{
    let string = JSON.parse(this.localStorage.getItem(Onym_theme));
    if (string === null)
    {
        defaultThemeSettings(this)
    }
    if(JSON.parse(this.localStorage.getItem(Onym_theme))[arg] === null)
    {
        switch (arg)
        {
            case 'theme': this.localStorage.setItem(Onym_theme, JSON.stringify({'theme': Light_theme, 'accent': getThemeSetting('accent')}));
                break;
            case 'accent': this.localStorage.setItem(Onym_theme, JSON.stringify({'theme': getThemeSetting('theme'), 'accent': Green_accent}));
                break;
        }
    }
    else
    {
        return JSON.parse(this.localStorage.getItem(Onym_theme))[arg]
    }
}

function setThemeSettings(theme, accent)
{
    Html.setAttribute('accent-color', accent);
    Html.setAttribute('theme-color', theme);
}
function defaultThemeSettings()
{
    changeThemeSettings(Light_theme, Green_accent)
}
function changeThemeSettings(theme, accent) 
{
    setThemeSettings(theme, accent)
    saveThemeSettings(theme, accent);
}
function showThemeSwitcher()
{
    Theme_switcher.classList.remove('hidden');
}
function hideThemeSwitcher()
{
    Theme_switcher.classList.add('hidden');
}
function themeSwitcherLoad()
{
    if(getThemeSetting('theme') === Dark_theme){themeCheckbox.checked = true;}
}
function accentSwitcherLoad()
{
    switch (getThemeSetting('accent'))
    {
        case (Green_accent): Document.getElementById('change-accent-green').checked = true;Document.getElementById('change-accent-yellow').checked = false;Document.getElementById('change-accent-sky').checked = false;Document.getElementById('change-accent-pastel').checked = false;
            break;
        case (Yellow_accent): Document.getElementById('change-accent-green').checked = false;Document.getElementById('change-accent-yellow').checked = true;Document.getElementById('change-accent-sky').checked = false;Document.getElementById('change-accent-pastel').checked = false;
            break;
        case (Sky_accent): Document.getElementById('change-accent-green').checked = false;Document.getElementById('change-accent-yellow').checked = false;Document.getElementById('change-accent-sky').checked = true;Document.getElementById('change-accent-pastel').checked = false;
            break;
        case (Pastel_accent): Document.getElementById('change-accent-green').checked = false;Document.getElementById('change-accent-yellow').checked = false;Document.getElementById('change-accent-sky').checked = false;Document.getElementById('change-accent-pastel').checked = true;
            break;
    }
}
Document.onvisibilitychange = function() {
    setThemeSettings(getThemeSetting('theme'),getThemeSetting('accent'));
    themeSwitcherLoad();
    accentSwitcherLoad();
};
// THEME SWITCHER LISTENERS
themeCheckbox.addEventListener('change', e => {
    switchTheme();
});
popoverButton.addEventListener('mouseenter', e => {
    clearTimeout(timerId);
    showThemeSwitcher();
});
popoverButton.addEventListener('mouseleave', e => {
    timerId = setTimeout(hideThemeSwitcher, 2000)
});
popoverMenu.addEventListener('mouseenter', e => {
    clearTimeout(timerId);
});
popoverMenu.addEventListener('mouseleave', e => {
    clearTimeout(timerId);
    timerId = setTimeout(hideThemeSwitcher, 2000)
});

accentCheckboxes.addEventListener('mouseenter', e => {
    Search_form.getElementsByClassName('search-button')[0].setAttribute('style', 'transition-delay: 0ms !important');
    Search_form.getElementsByClassName('search-input-field')[0].setAttribute('style', 'transition-delay: 0ms !important');

});
accentCheckboxes.addEventListener('mouseleave', e => {
    Search_form.getElementsByClassName('search-button')[0].removeAttribute('style', 'transition-delay: 0ms !important');
    Search_form.getElementsByClassName('search-input-field')[0].removeAttribute('style', 'transition-delay: 0ms !important');

});
// GREEN
accentCheckboxes.getElementsByClassName(Green_accent)[0].addEventListener('mouseenter', e => {
    this.activeAccent = getThemeSetting('accent');
    switchAccent(Green_accent);
    Document.getElementById('change-accent-green').addEventListener('change', e => {
        switchAccent(Green_accent)
        this.activeAccent = getThemeSetting('accent');
        accentSwitcherLoad();
    });
    accentCheckboxes.getElementsByClassName(Green_accent)[0].addEventListener('mouseleave', e => {
        switchAccent(this.activeAccent);
    });
});


// YELLOW
accentCheckboxes.getElementsByClassName(Yellow_accent)[0].addEventListener('mouseenter', e => {
    this.activeAccent = getThemeSetting('accent');
    switchAccent(Yellow_accent);
    Document.getElementById('change-accent-yellow').addEventListener('change', e => {
        switchAccent(Yellow_accent)
        this.activeAccent = getThemeSetting('accent');
        accentSwitcherLoad();
    });
    accentCheckboxes.getElementsByClassName(Yellow_accent)[0].addEventListener('mouseleave', e => {
        switchAccent(this.activeAccent);
    });
});
// SKY
accentCheckboxes.getElementsByClassName(Sky_accent)[0].addEventListener('mouseenter', e => {
    this.activeAccent = getThemeSetting('accent');
    switchAccent(Sky_accent);
    Document.getElementById('change-accent-sky').addEventListener('change', e => {
        switchAccent(Sky_accent)
        this.activeAccent = getThemeSetting('accent');
        accentSwitcherLoad();
    });
    accentCheckboxes.getElementsByClassName(Sky_accent)[0].addEventListener('mouseleave', e => {
        switchAccent(this.activeAccent);
    });
});
// PASTEL
accentCheckboxes.getElementsByClassName(Pastel_accent)[0].addEventListener('mouseenter', e => {
    this.activeAccent = getThemeSetting('accent');
    switchAccent(Pastel_accent);
    Document.getElementById('change-accent-pastel').addEventListener('change', e => {
        switchAccent(Pastel_accent)
        this.activeAccent = getThemeSetting('accent');
        accentSwitcherLoad();
    });
    accentCheckboxes.getElementsByClassName(Pastel_accent)[0].addEventListener('mouseleave', e => {
        switchAccent(this.activeAccent);
    });
});