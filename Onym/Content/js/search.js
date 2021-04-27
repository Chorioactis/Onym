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