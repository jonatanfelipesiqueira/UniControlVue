$("select[data-source]").each(function () {
    var $select = $(this);
    var $valueSelected = parseInt($select.attr("data-value"));
    const $valueFilterByConfiguration = Boolean($select.attr("data-filter-by-configuration"));
    const $valueFilter = $select.attr("data-filter");

    $select.append(`<option value="">${$select.attr('data-message')}</option>`);
    $.ajax({
        url: $select.attr("data-source"),
        type: "POST",
        data: { filtrarPorConfiguracao: $valueFilterByConfiguration, busca: $valueFilter },
        success: function (options) {
            if (options.data.length > 0) {
                options.data.map(function (option) {
                    const $option = $("<option>");

                    if ($select.attr("data-displayKey").includes(",")) {
                        const manyValues = $select.attr("data-displayKey").split(",");

                        var optionValue = "";
                        $.each(manyValues, function (index, value) {
                            if (index === manyValues.length - 1) {
                                optionValue += option[value];
                            } else {
                                optionValue += `${option[value]} - `;
                            }
                        });
                        $option
                            .val(option[$select.attr("data-valueKey")])
                            .text(optionValue);
                    } else {
                        $option
                            .val(option[$select.attr("data-valueKey")])
                            .text(option[$select.attr("data-displayKey")]);
                    }

                    if (option[$select.attr("data-valueKey")] === $valueSelected) {
                        $option.attr("selected", "selected");
                    }

                    $select.append($option);
                });
            } else {
                $select.attr('disabled', true);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensagemErro(`${thrownError}: ${xhr.responseJSON}`);
        }
    });
});