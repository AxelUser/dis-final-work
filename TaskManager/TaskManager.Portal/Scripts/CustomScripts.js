; (function () {
    function init() {
        if (window.location.hash === '#!/') {
            window.addEventListener('scroll', function (e) {
                var distanceY = window.pageYOffset || document.documentElement.scrollTop,
                    shrinkOn = 300,
                    header = document.querySelector("header");
                if (distanceY > shrinkOn) {
                    $('header').addClass('smaller');
                } else {
                    if ($('header').hasClass('smaller')) {
                        $('header').removeClass('smaller');
                    }
                }

            });
        } else {
            $('div.body').removeClass('body');
        }
    }
    //window.onload = init();
})();