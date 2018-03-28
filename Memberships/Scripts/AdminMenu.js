
//permet de surcharger l'evt over the menu pour faire open automatique
$(function () {
    $('[data-admin-menu]').hover(function () {
        $('[data-admin-menu]').toggleClass('open')
    });
});