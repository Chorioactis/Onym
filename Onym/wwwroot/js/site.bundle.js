// CONSTANTS
const Publication_link = 'https://localhost:5001/story/'
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
/* RatingTally Animation */
(function($){

    $.fn.extend({

        ratingTallyAnimation: function(className, rate, duration) {
            let elements = this;
            if (rate === 'RateUp')
            {
                setTimeout(function() {
                    elements.removeClass(className).text(parseInt(elements.text(),10) + 1);
                }, duration);
            }
            if (rate === 'RemoveUp')
            {
                setTimeout(function() {
                    elements.removeClass(className).text(parseInt(elements.text(),10) - 1);
                }, duration);
            }
            if (rate === 'UpFromDown')
            {
                setTimeout(function() {
                    elements.removeClass(className).text(parseInt(elements.text(),10) + 2);
                }, duration);
            }

            if (rate === 'RateDown')
            {
                setTimeout(function() {
                    elements.removeClass(className).text(parseInt(elements.text(),10) - 1);
                }, duration);
            }
            if (rate === 'RemoveDown')
            {
                setTimeout(function() {
                    elements.removeClass(className).text(parseInt(elements.text(),10) + 1);
                }, duration);
            }
            if (rate === 'DownFromUp')
            {
                setTimeout(function() {
                    elements.removeClass(className).text(parseInt(elements.text(),10) - 2);
                }, duration);
            }
            
            return this.each(function() {
                $(this).addClass(className);
            });
        }
    });
})(jQuery);
/* LazyLoading */
$(function() {
    $('.lazy').Lazy({scrollDirection: 'vertical',
        effect: 'fadeIn',
        visibleOnly: true,
        threshold: 500,
        onError: function(element) {
            console.log('error loading ' + element.data('src'));
        }
    });
});

function publicationRate(elem, arg) {
    let animationDuration = 200;
    if (arg === 'RateUp') 
    {
        $(elem).addClass('active').attr('onclick', "publicationRate(this, 'RemoveUp')").parent().find('button[name="PublicationRateDown"]').attr('onclick', "publicationRate(this, 'DownFromUp')").parent().find('p').ratingTallyAnimation('hide', 'RateUp', animationDuration);
    }
    if (arg === 'RemoveUp')
    {
       $(elem).removeClass('active').attr('onclick',  "publicationRate(this, 'RateUp')").parent().find('button[name="PublicationRateDown"]').attr('onclick',  "publicationRate(this, 'RateDown')").parent().find('p').ratingTallyAnimation('hide', 'RemoveUp', animationDuration);
    }
    if (arg === 'UpFromDown')
    {
       $(elem).addClass('active').attr('onclick',  "publicationRate(this, 'RemoveUp')").parent().find('button[name="PublicationRateDown"]').removeClass('active').attr('onclick',  "publicationRate(this, 'DownFromUp')").parent().find('p').ratingTallyAnimation('hide', 'UpFromDown', animationDuration);
    }

    if (arg === 'RateDown')
    {
       $(elem).addClass('active').attr('onclick',  "publicationRate(this, 'RemoveDown')").parent().find('button[name="PublicationRateUp"]').attr('onclick',  "publicationRate(this, 'UpFromDown')").parent().find('p').ratingTallyAnimation('hide', 'RateDown', animationDuration);
    }
    if (arg === 'RemoveDown')
    {
       $(elem).removeClass('active').attr('onclick',  "publicationRate(this, 'RateDown')").parent().find('button[name="PublicationRateUp"]').attr('onclick',  "publicationRate(this, 'RateUp')").parent().find('p').ratingTallyAnimation('hide', 'RemoveDown', animationDuration);
    }
    if (arg === 'DownFromUp')
    {
       $(elem).addClass('active').attr('onclick',  "publicationRate(this, 'RemoveDown')").parent().find('button[name="PublicationRateUp"]').removeClass('active').attr('onclick',  "publicationRate(this, 'UpFromDown')").parent().find('p').ratingTallyAnimation('hide', 'DownFromUp', animationDuration);
    }
}
function publicationFavorite(elem, arg) {   
    if (arg === 'Set')
    {
       $(elem).attr('onclick',  "publicationFavorite(this, 'Remove')");
       $(elem).addClass('favorite');    
    }
    if (arg === 'Remove')
    {
       $(elem).attr('onclick',  "publicationFavorite(this, 'Set')");
       $(elem).removeClass('favorite');
    }
}
function publicationHide(elem, arg) {
    if (arg === 'Show')
    {
       $(elem).attr('onclick',  "publicationHide(this, 'Hide')");
       $(elem).attr('title', "Скрыть пост (Для всех)");
       $(elem).removeClass('hide');
       $(elem).html('<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" stroke="currentColor" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"/><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" stroke="currentColor" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"/></svg>')
    }
    if (arg === 'Hide')
    {
       $(elem).attr('onclick',  "publicationHide(this, 'Show')");
       $(elem).attr('title', "Раскрыть пост (Для всех)");
       $(elem).addClass('hide');
       $(elem).html('<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" stroke="currentColor" d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.88 9.88l-3.29-3.29m7.532 7.532l3.29 3.29M3 3l3.59 3.59m0 0A9.953 9.953 0 0112 5c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m0 0L21 21"/></svg>')}
}
function publicationLinkCopy(elem, link) {
    navigator.clipboard.writeText(Publication_link + link).then(function() {
        $(elem).attr('title', 'Ссылка на пост скопирована!');
    });
    setTimeout(function() {$(elem).attr('title', 'Скопировать ссылку на пост')}, 2000);
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
    themeCheckbox.checked = getThemeSetting('theme') === Dark_theme;
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
themeSwitcherLoad();
accentSwitcherLoad();
Document.onvisibilitychange = function() {
    themeSwitcherLoad();
    accentSwitcherLoad();
    setThemeSettings(getThemeSetting('theme'),getThemeSetting('accent'));
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
    timerId = setTimeout(hideThemeSwitcher, 1000)
});
popoverMenu.addEventListener('mouseenter', e => {
    clearTimeout(timerId);
});
popoverMenu.addEventListener('mouseleave', e => {
    clearTimeout(timerId);
    timerId = setTimeout(hideThemeSwitcher, 1000)
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