/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    // 设置宽高
    config.height = 400;
    config.filebrowserImageUploadUrl = "/Manage/CKEditorUpload";
    config.font_names = '宋体/SimSun;新宋体/NSimSun;仿宋/FangSong;楷体/KaiTi;仿宋_GB2312/FangSong_GB2312;' +
        '楷体_GB2312/KaiTi_GB2312;黑体/SimHei;华文细黑/STXihei;华文楷体/STKaiti;华文宋体/STSong;华文中宋/STZhongsong;' +
        '华文仿宋/STFangsong;华文彩云/STCaiyun;华文琥珀/STHupo;华文隶书/STLiti;华文行楷/STXingkai;华文新魏/STXinwei;' +
        '方正舒体/FZShuTi;方正姚体/FZYaoti;细明体/MingLiU;新细明体/PMingLiU;微软雅黑/Microsoft YaHei;微软正黑/Microsoft JhengHei;' +
        'Arial Black/Arial Black;' + config.font_names;
    config.extraPlugins += (config.extraPlugins ? ',lineheight' : 'lineheight');
    CKEDITOR.config.toolbar_Full = [
        {
            name: 'document', items:
              ['Source', '-', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', '-', 'Templates']
        },
        {
            name: 'clipboard', items:
              ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo']
        },
        {
            name: 'editing', items:
              ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt']
        },
        {
            name: 'forms', items:
              ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField']
        }, '/',
        {
            name: 'basicstyles', items:
              ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat']
        },
        {
            name: 'paragraph', items:
              ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
        },
        {
            name: 'links', items:
              ['Link', 'Unlink', 'Anchor']
        }, {
            name: 'insert', items:
                ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe']
        }, '/',
        {
            name: 'styles', items:
              ['Styles', 'Format', 'Font', 'FontSize', 'lineheight']
        },
        {
            name: 'colors', items:
              ['TextColor', 'BGColor']
        },
        {
            name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About']
        }
    ];
};

//CKEDITOR.editorConfig = function (config) {
//    config.filebrowserBrowseUrl = '/../ckfinder/ckfinder.html';
//    config.filebrowserImageBrowseUrl = '/../ckfinder/ckfinder.html?Type=Images';
//    config.filebrowserFlashBrowseUrl = '/../ckfinder/ckfinder.html?Type=Flash';
//    config.filebrowserUploadUrl = '/../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
//    config.filebrowserImageUploadUrl = '/../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
//    config.filebrowserFlashUploadUrl = '/../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
//};



