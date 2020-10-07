/** Funções para controle das informaçoes da entidade de Autor(a).
  * Fonte: js/biblioteca/categoria.js
  */
'use strict';

function categoria()
{
    if ($('#selectCategory').length)
    {
        categoria.vueSelect = new Vue({
            el: '#selectCategory',
            data: {
                selectCategory: [],
                selectedValues: [],
                loadedValue: [],
            },
            methods: {
                obterOpcoes: function ()
                {
                    var endereco = site.url + 'biblioteca/category/getcategories';
                    let parametros = {
                    };

                    $.get(endereco, parametros)
                        .done(function (response)
                        {
                            if (response && response.sucesso)
                            {
                                let data = response.dados;
                                categoria.vueSelect.atualizarLista(data);
                            }
                            else {
                                categoria.vueSelect.atualizarLista([]);
                            }

                            $('#selectCategory .carregando').addClass('hidden');
                        });
                },
                atualizarLista: function (data)
                {
                    this.selectCategory = data;
                    this.selectedValues = this.loadedValue;
                    this.$forceUpdate();
                },
                isEmpty: function ()
                {
                    return this.selectCategory.length == 0 || false;
                },
                setSelectedValues: function (value)
                {
                    if (value)
                    {
                        if (typeof value === 'string' || value instanceof String)
                        {
                            if (value.search(',') !== -1)
                            {
                                var values = value.split(',');
                                this.loadedValue = values;
                            }
                            else
                            {
                                var values = [value];
                                this.loadedValue = values;
                            }
                        }
                    }
                },
                getCategoryId: function (item)
                {
                    return item.categoryId;
                },
                getCategoryName: function (item)
                {
                    return item.description;
                },
                newCategory: function (url)
                {
                    window.open(url, '_blank');
                },
            },
            created: function ()
            {
                this.obterOpcoes();
            }
        });
    }
}
categoria();