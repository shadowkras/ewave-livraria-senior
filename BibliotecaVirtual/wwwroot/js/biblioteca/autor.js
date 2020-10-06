/** Funções para controle das informaçoes da entidade de Autor(a).
  * Fonte: js/biblioteca/autor.js
  */
'use strict';

function autor()
{
    if ($('#selectAuthor').length)
    {
        autor.vueSelect = new Vue({
            el: '#selectAuthor',
            data: {
                selectAuthor: [],
                isEmpty: this.selectPublisher.length == 0,
                selectedValue: -1,
                loadedValue: -1,
            },
            methods: {
                obterOpcoes: function ()
                {
                    var endereco = site.url + 'biblioteca/author/getauthors';
                    let parametros = {
                    };

                    $.get(endereco, parametros)
                        .done(function (response)
                        {
                            if (response && response.Sucesso)
                            {
                                let data = response.Dados;
                                autor.vueSelect.atualizarLista(data);
                            }
                            else {
                                autor.vueSelect.atualizarLista([]);
                            }

                            $('#selectAuthor .carregando').addClass('hidden');
                        });
                },
                atualizarLista: function (data)
                {
                    this.selectAuthor = data;
                    this.selectedValue = this.loadedValue;
                    this.$forceUpdate();
                },
                setSelectedValue: function (value)
                {
                    if (value && parseInt(value))
                    {
                        
                        this.loadedValue = value;
                    }
                },
                getAuthorId: function (item)
                {
                    return item.authorId;
                },
                getAuthorName: function (item)
                {
                    return item.name;
                },
                newAuthor: function (url)
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
autor();