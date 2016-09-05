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
    
    //点击导航菜单时选中所点的按钮
    var clickedUrl = window.location;
    var clickHref = clickedUrl.href.toLowerCase();

    //过滤一级导航菜单是否与点击的链接匹配
    var element = $('.common-nav>li.checkmenu>a').filter(function () {
        debugger;
        var thisPathName = this.pathname.toLowerCase();
        var clickPathName = clickedUrl.pathname.toLowerCase();
        //URL后缀分割，获取导航URL 如：/about.html
        var arrHeadPath = thisPathName.split('/');
        var firstHeadPath = arrHeadPath[1].split('.');
        var firstCompareHeadPath = firstHeadPath[0];

        var arrClickPath = clickPathName.split('/');
        var firstClickPath = arrClickPath[1].split('.');
        var firstCompareClickPath = firstClickPath[0];

        //如果当前点击的URL中能够匹配导航URL
        if (firstCompareHeadPath == firstCompareClickPath) {
            return true;
        }

        var currentHref = this.href.toLowerCase();
        return currentHref == clickHref || clickHref.indexOf(currentHref) == 0;
    }); 
    //如果一级没有匹配，则说明当前点击的是二级菜单
    if (element.length == 0) {
        //匹配二级菜单
        var secondNavelement = $('.common-nav>li>ul a').filter(function () {

            var currentHref = this.href.toLowerCase();
            return currentHref == clickHref || clickHref.indexOf(currentHref) == 0;
        });
        //选中二级菜单，及对应的一级菜单
        secondNavelement.parent().addClass('active').parent().parent().addClass("active");

    } else {
        element.parent().addClass('active');

    }

    //导航菜单收缩
    $(".fa-dropdown").hover(function () {
        $(this).find("ul").show();
    }, function () {
        $(this).find("ul").hide();

    })

    /*返回顶部*/
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