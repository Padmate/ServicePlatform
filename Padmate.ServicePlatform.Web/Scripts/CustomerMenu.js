$(function () {

    //当选中某一项时，选中该项
    var url = window.location;
    var element = $('.menu-sidebar-nav a').filter(function () {
        return this.href == url || url.href.indexOf(this.href) == 0;
    }).addClass('active').parent().parent().addClass('in').parent();
    if (element.is('li')) {
     
        element.addClass('active');
    }

    //
    $(".menu-sidebar-nav a").filter(function () {
        return $(this).attr('data-toggle') != undefined;
    }).each(function () {
        InitMenuCollapseStyle(this);

    });


    //点击选项时改变右侧箭头样式
    $(".menu-sidebar-nav a").click(function () {

        ChangeMenuCollapseStyle(this);
    });
});

function InitMenuCollapseStyle(menuElement) {
    //判断选中的按钮是否含有子菜单
    if ($(menuElement).attr('data-toggle') != undefined) {
        //判断对应的子菜单是否展开
        var target = $(menuElement).attr('href');
        var hasalreadyexpanded = $(target).hasClass('in');

        //查找对应的收缩样式
        $(menuElement).find("span").each(function () {
            if ($(this).hasClass("fa-chevron-left")) $(this).removeClass("fa-chevron-left");
            if ($(this).hasClass("fa-chevron-down")) $(this).removeClass("fa-chevron-down");

            if (!hasalreadyexpanded) {

                $(this).addClass("fa-chevron-left");

            } else {
                $(this).addClass("fa-chevron-down");
            }
        });
    }
}
function ChangeMenuCollapseStyle(menuElement) {
    //判断选中的按钮是否含有子菜单
    if ($(menuElement).attr('data-toggle') != undefined) {
        //判断对应的子菜单是否展开
        var target = $(menuElement).attr('href');
        var hasalreadyexpanded = $(target).hasClass('in');

        //查找对应的收缩样式
        $(menuElement).find("span").each(function () {
            if ($(this).hasClass("fa-chevron-left")) $(this).removeClass("fa-chevron-left");
            if ($(this).hasClass("fa-chevron-down")) $(this).removeClass("fa-chevron-down");

            if (!hasalreadyexpanded) {

                $(this).addClass("fa-chevron-down");

            } else {
                $(this).addClass("fa-chevron-left");
            }
        });
    }
}