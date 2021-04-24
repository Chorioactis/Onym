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

