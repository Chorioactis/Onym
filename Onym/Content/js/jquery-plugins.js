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
