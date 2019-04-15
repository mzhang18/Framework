$(function () {
    $('[data-admin-menu]').hover(function () {
        $('[data-admin-menu]').toggleClass('open');
    });
});

function DeleteData()
{
    var $form = $("#DeleteForm");
    $form.submit();

    return true;
}

