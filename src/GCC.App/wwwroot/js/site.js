
$(document).ready(function () {
    $("#CPF").mask('000.000.000-00', { reverse: false });
    $("#Telefone").mask("(00) 00000-0000");
    $("#CRM_Numero").mask("#");
});

$('#Telefone').on('change', function () {
    if ($("#Telefone").val().length == 14) {
        $("#Telefone").mask("(00) 0000-00009");
        return;
    }
    $("#Telefone").mask("(00) 00000-0000");
});

$(document).ready(function () {
    $('#ExamesDropdown').multiselect({ nonSelectedText: 'NENHUM SELECIONADO', allSelectedText: 'TODOS SELECIONADOS' });
});
