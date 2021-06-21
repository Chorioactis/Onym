function Rate(elem, arg) {
    let animationDuration = 200;
    if (arg === 'RateUp') 
    {
        $(elem).addClass('active').attr('onclick', "Rate(this, 'RemoveUp')").parent().find('button[name="PublicationRateDown"]').attr('onclick', "Rate(this, 'DownFromUp')").parent().find('p').ratingTallyAnimation('hide', 'RateUp', animationDuration);
        $(elem).addClass('active').attr('onclick', "Rate(this, 'RemoveUp')").parent().find('button[name="CommentRateDown"]').attr('onclick', "Rate(this, 'DownFromUp')").parent().find('p').ratingTallyAnimation('hide', 'RateUp', animationDuration);
    }
    if (arg === 'RemoveUp')
    {
       $(elem).removeClass('active').attr('onclick',  "Rate(this, 'RateUp')").parent().find('button[name="PublicationRateDown"]').attr('onclick',  "Rate(this, 'RateDown')").parent().find('p').ratingTallyAnimation('hide', 'RemoveUp', animationDuration);
       $(elem).removeClass('active').attr('onclick',  "Rate(this, 'RateUp')").parent().find('button[name="CommentRateDown"]').attr('onclick',  "Rate(this, 'RateDown')").parent().find('p').ratingTallyAnimation('hide', 'RemoveUp', animationDuration);
    }
    if (arg === 'UpFromDown')
    {
       $(elem).addClass('active').attr('onclick',  "Rate(this, 'RemoveUp')").parent().find('button[name="PublicationRateDown"]').removeClass('active').attr('onclick',  "Rate(this, 'DownFromUp')").parent().find('p').ratingTallyAnimation('hide', 'UpFromDown', animationDuration);
       $(elem).addClass('active').attr('onclick',  "Rate(this, 'RemoveUp')").parent().find('button[name="CommentRateDown"]').removeClass('active').attr('onclick',  "Rate(this, 'DownFromUp')").parent().find('p').ratingTallyAnimation('hide', 'UpFromDown', animationDuration);
    }

    if (arg === 'RateDown')
    {
       $(elem).addClass('active').attr('onclick',  "Rate(this, 'RemoveDown')").parent().find('button[name="PublicationRateUp"]').attr('onclick',  "Rate(this, 'UpFromDown')").parent().find('p').ratingTallyAnimation('hide', 'RateDown', animationDuration);
       $(elem).addClass('active').attr('onclick',  "Rate(this, 'RemoveDown')").parent().find('button[name="CommentRateUp"]').attr('onclick',  "Rate(this, 'UpFromDown')").parent().find('p').ratingTallyAnimation('hide', 'RateDown', animationDuration);
    }
    if (arg === 'RemoveDown')
    {
       $(elem).removeClass('active').attr('onclick',  "Rate(this, 'RateDown')").parent().find('button[name="PublicationRateUp"]').attr('onclick',  "Rate(this, 'RateUp')").parent().find('p').ratingTallyAnimation('hide', 'RemoveDown', animationDuration);
       $(elem).removeClass('active').attr('onclick',  "Rate(this, 'RateDown')").parent().find('button[name="CommentRateUp"]').attr('onclick',  "Rate(this, 'RateUp')").parent().find('p').ratingTallyAnimation('hide', 'RemoveDown', animationDuration);
    }
    if (arg === 'DownFromUp')
    {
       $(elem).addClass('active').attr('onclick',  "Rate(this, 'RemoveDown')").parent().find('button[name="PublicationRateUp"]').removeClass('active').attr('onclick',  "Rate(this, 'UpFromDown')").parent().find('p').ratingTallyAnimation('hide', 'DownFromUp', animationDuration);
       $(elem).addClass('active').attr('onclick',  "Rate(this, 'RemoveDown')").parent().find('button[name="CommentRateUp"]').removeClass('active').attr('onclick',  "Rate(this, 'UpFromDown')").parent().find('p').ratingTallyAnimation('hide', 'DownFromUp', animationDuration);
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
function Hide(elem, arg) {
    if (arg === 'Show')
    {
       $(elem).attr('onclick',  "Hide(this, 'Hide')");
       $(elem).attr('title', "Скрыть пост (Для всех)");
       $(elem).removeClass('hide');
       $(elem).html('<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" stroke="currentColor" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"/><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" stroke="currentColor" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"/></svg>')
    }
    if (arg === 'Hide')
    {
       $(elem).attr('onclick',  "Hide(this, 'Show')");
       $(elem).attr('title', "Раскрыть пост (Для всех)");
       $(elem).addClass('hide');
       $(elem).html('<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" stroke="currentColor" d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.88 9.88l-3.29-3.29m7.532 7.532l3.29 3.29M3 3l3.59 3.59m0 0A9.953 9.953 0 0112 5c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m0 0L21 21"/></svg>')}
}
function LinkCopy(elem, link) {
    navigator.clipboard.writeText(Publication_link + link).then(function() {
        $(elem).attr('title', 'Ссылка скопирована!');
    });
    setTimeout(function() {$(elem).attr('title', 'Скопировать ссылку на пост')}, 2000);
}
