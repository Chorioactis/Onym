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
themeCheckbox.addEventListener('onchange', e => {
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