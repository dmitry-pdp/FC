var main;

function onCreateNew()
{
    main.html("");
}

function updateCharacterList(characters)
{
    if (characters == null || !Array.isArray(characters))
    {
        return false;
    }

    var cnt = "<div class=\"tb\">Select the character</div><div>";
    for (index = 0; index < characters.length; index++)
    {
        var c = characters[index];
        cnt += 
            "<div class=\"cb\"><div class=\"cb-t\">" + c.name + "</div>" +
            "<div class=\"cb-d\">Level " + c.level + " · " + c.race + " · " + c.class + "</div>" +
            "<div class=\"cb-l\">Location: " + c.location + "</div></div>";
    }

    cnt += "<div class=\"btn\" onclick=\"onCreateNew()\">Create new ...</div></div>";
    main.html(cnt);
}

$(function () {
    main = $("#main");

    $.ajax({ url: "/?v=c", cache: false, method: "POST" })
    .done(function (data) {
        updateCharacterList(data);
    })
    .fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });
});