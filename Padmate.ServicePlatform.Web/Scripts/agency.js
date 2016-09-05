/*!
 * Start Bootstrap - Agency Bootstrap Theme (http://startbootstrap.com)
 * Code licensed under the Apache License v2.0.
 * For details, see http://www.apache.org/licenses/LICENSE-2.0.
 */

// jQuery for page scrolling feature - requires jQuery Easing plugin
$(function() {
    $('a.page-scroll').bind('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top
        }, 1500, 'easeInOutExpo');
        event.preventDefault();
    });
});

// Highlight the top nav as scrolling occurs
$('body').scrollspy({
    target: '.navbar-fixed-top'
})

// Closes the Responsive Menu on Menu Item Click
$('.navbar-collapse ul li a').click(function() {
    $('.navbar-toggle:visible').click();
});

$(function () {
    
    //��������˵�ʱѡ������İ�ť
    var clickedUrl = window.location;
    var clickHref = clickedUrl.href.toLowerCase();

    //����һ�������˵��Ƿ�����������ƥ��
    var element = $('.common-nav>li.checkmenu>a').filter(function () {
        debugger;
        var thisPathName = this.pathname.toLowerCase();
        var clickPathName = clickedUrl.pathname.toLowerCase();
        //URL��׺�ָ��ȡ����URL �磺/about.html
        var arrHeadPath = thisPathName.split('/');
        var firstHeadPath = arrHeadPath[1].split('.');
        var firstCompareHeadPath = firstHeadPath[0];

        var arrClickPath = clickPathName.split('/');
        var firstClickPath = arrClickPath[1].split('.');
        var firstCompareClickPath = firstClickPath[0];

        //�����ǰ�����URL���ܹ�ƥ�䵼��URL
        if (firstCompareHeadPath == firstCompareClickPath) {
            return true;
        }

        var currentHref = this.href.toLowerCase();
        return currentHref == clickHref || clickHref.indexOf(currentHref) == 0;
    }); 
    //���һ��û��ƥ�䣬��˵����ǰ������Ƕ����˵�
    if (element.length == 0) {
        //ƥ������˵�
        var secondNavelement = $('.common-nav>li>ul a').filter(function () {

            var currentHref = this.href.toLowerCase();
            return currentHref == clickHref || clickHref.indexOf(currentHref) == 0;
        });
        //ѡ�ж����˵�������Ӧ��һ���˵�
        secondNavelement.parent().addClass('active').parent().parent().addClass("active");

    } else {
        element.parent().addClass('active');

    }

    //�����˵�����
    $(".fa-dropdown").hover(function () {
        $(this).find("ul").show();
    }, function () {
        $(this).find("ul").hide();

    })

    /*���ض���*/
    $(window).scroll(function () {
        if ($(document).scrollTop() >= 600) {
            $("#linavtop").removeClass("navtop");
        }
        else {
            $("#linavtop").addClass("navtop");
        }
    });

    //IE8 placeholder
    // Invoke the plugin
    $('input, textarea').placeholder();

})