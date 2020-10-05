class DataTableCustom {
    static campoPesquisa() {
        return [
            '<div class="input-group mb-3">',
            '<input type="text" class="form-control" aria-label="Text input with dropdown button">',
            '<div class="input-group-prepend">',
            '<button class="btn btn-outline-secondary dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Dropdown</button>',
            '<div class="dropdown-menu">',
            '<a class="dropdown-item" href="#">Action</a>',
            '<a class="dropdown-item" href="#">Another action</a>',
            '<a class="dropdown-item" href="#">Something else here</a>',
            '<div role="separator" class="dropdown-divider"></div>',
            '<a class="dropdown-item" href="#">Separated link</a>',
            '</div>',
            '</div>',
            '</div>'
        ];
    }

    static traducao() {
        return {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "_START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ itens por página",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            },
            "select": {
                "rows": {
                    "_": "Selecionado %d linhas",
                    "0": "0 linhas selecionadas",
                    "1": "Selecionado 1 linha"
                }
            }
        };
    }

    static gerar(opcoes) {
        const colunaIdPadrao = { "data": "Id", "name": "Id", "title": "Código" };
        const tabela = $(`#${opcoes.id}`).DataTable({
            "language": this.traducao(),
            "scrollY": opcoes.scrollY === undefined ? "60vh" : opcoes.scrollY,
            "scrollCollapse": true,
            "paging": opcoes.paging === undefined,
            //"ordering": opcoes.paging === undefined ? true : false,
            "info": opcoes.paging === undefined,
            "lengthMenu": [[10, 25, 50, 100/*, -1*/], [10, 25, 50, 100/*, "Todos"*/]],
            "lengthChange": opcoes.paging === undefined,
            "dom":
                "<tr><'#datatable-row-bottom.row'<'#local-resultado.col-auto''l><'#local-info.col'i><'#local-pag.col''p>>",
            "searching": opcoes.searching === undefined,
            "responsive": true,
            "serverSide": true,
            "select": {
                "style": opcoes.selectStyle ? opcoes.selectStyle : "multi",
                "selector": "td:not(:last-child)"
            },
            "order": opcoes.order,
            "ajax": {
                "url": opcoes.url,
                "type": "POST",
                "datatype": "json",
                "data": opcoes.data,
                "error": function (xhr, ajaxOptions, thrownError) {
                    mensagemErro(`${xhr.responseText}`);
                    $(`#${opcoes.id}_processing`).hide();
                    $(".mensagem-erro").html(" ");
                    $("#conteudo-listagem").append(`<p class="mensagem-erro">${thrownError}</p>`);
                }
            },
            "columns": [opcoes.colunaPadrao ? opcoes.colunaPadrao : colunaIdPadrao].concat(opcoes.columns ? opcoes.columns : []),
            "columnDefs": [
                {
                    "targets": [0],
                    "checkboxes": {
                        "selectRow": true,
                        "selectAll": opcoes.selectStyle !== "single"
                    },
                    "createdCell": opcoes.createdCell ? opcoes.createdCell : ""
                }
            ].concat(opcoes.columnDefs ? opcoes.columnDefs : []),
            "drawCallback": opcoes.drawCallback === undefined ? function () {
                $(`#${opcoes.id}_paginate`).addClass("data-table-paginate");
            } : opcoes.drawCallback
        });

        return tabela;
    }

    static gerarLocal(opcoes) {
        const tabela = $(`#${opcoes.id}`).DataTable({
            "language": this.traducao(),
            "paging": false,
            "info": false,
            "lengthChange": false,
            "responsive": true,
            "filter": false,
            "scrollY": opcoes.scrollY === undefined ? "45vh" : opcoes.scrollY,
            "scrollCollapse": true,
            "dom": "<'row'<'col-md-3 col-12'f><'.col-md-3 col-12'><'col-md-6 col-12'l>><t><i><p>",
            "columnDefs": [
                {
                    "targets": [0],
                    "searchable": false,
                    "orderable": false,
                    "checkboxes": {
                        "selectRow": true
                    }
                }
            ].concat(opcoes.columnDefs),
            "drawCallback": opcoes.drawCallback === undefined ? function () {
                $(`#${opcoes.id}_wrapper`).removeClass("container-fluid");
                $(`#${opcoes.id}_paginate`).addClass("data-table-paginate");
            } : opcoes.drawCallback,
            "initComplete": function () {
                $(`#${opcoes.id}`).DataTable().columns.adjust();
            }
        });

        return tabela;
    }
}