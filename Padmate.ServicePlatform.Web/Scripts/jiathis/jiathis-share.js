function setShare(articleId) {

    $.ajax({
        type: "POST",
        url: "/Article/GetArticleById",
        data: { articleid: articleId },
        dataType: "json",
        async: false,   //同步
        success: function (result) {

            jiathis_config.title = result.SubTitle;
        }
    });

}

function setShare(articleId, url) {

    $.ajax({
        type: "POST",
        url: "/Article/GetArticleById",
        data: { articleid: articleId },
        dataType: "json",
        async: false,   //同步
        success: function (result) {
            jiathis_config.title = result.SubTitle;
            jiathis_config.url = url;
        }
    });

   
}


var jiathis_config = {
    data_track_clickback: true,
    summary: "",
    pic: "http://xiamenip.cn/img/logos/favicon.jpg",
    shortUrl: false,
    hideMore: false
}